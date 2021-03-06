﻿using FLEX.API.Common;
using FLEX.API.Context;
using FLEX.API.Models;
using FLEX.API.Modules.Flex.Models;
using FLEX.API.Modules.PMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace FLEX.API.Services
{
    public interface IFlexDataSvc
    {
        UserInfo UserLogin(string UserId, string Password);
        string GetToken(UserInfo user);
        List<NavData> GetMenu(string UserCd);
        List<TZ_MESSAGE_MS> GetMessage(string LangCd);
        List<TZ_SCREEN_DETAIL_LANG_MS> GetScreenDetail(string LangCd);
        void InsertScreenDetail(List<TZ_SCREEN_DETAIL_LANG_MS> dataList);
        TZ_USER_MS GetUserProfile(string UserCd);
        List<Notify> GetNotify(string UserCd);
        bool ResponseNotify(Notify noti);
        List<ActivePermissionValue> GetActivePermission(string userGroup);
        List<SpecialPermissionResult> GetSpecialPermission(string userGroup, string screenCd);
        List<TBM_STATUS> GetStatusList();
        TZ_SYS_CONFIG GetSysConfig(string SYS_GROUP_ID, string SYS_KEY);
        FileTemplate LoadMachineAttachment(string mACHINE_NO, string fILE_ID);
    }

    public class FlexDataSvc : IFlexDataSvc
    {
        private readonly AppSettings _appSettings;
        private readonly FLEXContext ct;

        public FlexDataSvc(IOptions<AppSettings> appSettings, FLEXContext context)
        {
            _appSettings = appSettings.Value;
            ct = context;
        }
        public UserInfo UserLogin(string UserCd, string Password)
        {
            var user = ct.UserLogin.FromSqlRaw(
                "SELECT USER_ACCOUNT as [USER_CD], FULL_NAME, EMAILADDR, GROUP_CD, LANG_CD, null as [TOKEN] " +
                "FROM TZ_USER_MS WHERE USER_ACCOUNT = {0} AND PASS = {1}"
                , UserCd, Password).SingleOrDefault();

            // return null if user not found
            if (user == null)
                return null;

            user.TOKEN = this.GetToken(user);
            return user;
        }
        public string GetToken(UserInfo user)
        {
            string token = "";

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("USER_CD", user.USER_CD),
                    new Claim("USER_FULLNAME", user.FULL_NAME),
                    new Claim("EMAILADDR", user.EMAILADDR??""),
                    new Claim("GROUP_CD", user.GROUP_CD),
                    new Claim("LANG_CD", user.LANG_CD),
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var t = tokenHandler.CreateToken(tokenDescriptor);
            token = tokenHandler.WriteToken(t);

            return token;
        }
        public List<NavData> GetMenu(string UserCd)
        {
            var menuList = ct.sp_SYS_GetMenu.FromSqlRaw("sp_SYS_GetMenu {0}", UserCd).ToList();

            List<NavData> menus = menuList?.GroupBy(g => new
            {
                MENU_SUB_CD = g.MENU_SUB_CD,
                MENU_SUB_DESC = g.MENU_SUB_DESC,
                MENU_SUB_URL = g.MENU_SUB_URL,
                MENU_SUB_ICON = g.MENU_SUB_ICON,
            }).Select(x => new NavData
            {
                ScreenCd = x.Key.MENU_SUB_CD,
                name = x.Key.MENU_SUB_DESC,
                url = $"/{x.Key.MENU_SUB_URL}",
                icon = x.Key.MENU_SUB_ICON,
                children = x.Select(y => new NavData()
                {
                    ScreenCd = y.SCREEN_CD,
                    name = y.SCREEN_DESC,
                    url = $"/{y.MENU_SUB_URL}/{y.SCREEN_URL}",
                    icon = y.SCREEN_ICON,
                }).ToList()
            }).ToList();

            menus?.Insert(0, new NavData()
            {
                name = "Dashboard",
                url = "/dashboard",
                icon = "icon-speedometer",
            });

            return menus;
        }
        public List<TZ_MESSAGE_MS> GetMessage(string LangCd)
        {
            if (string.IsNullOrEmpty(LangCd))
            {
                return ct.TZ_MESSAGE_MS.ToList();
            }
            else
            {
                return ct.TZ_MESSAGE_MS.Where(x => x.LANG_CD == LangCd).ToList();
            }
        }
        public List<TZ_SCREEN_DETAIL_LANG_MS> GetScreenDetail(string LangCd)
        {
            if (string.IsNullOrEmpty(LangCd))
            {
                return ct.TZ_SCREEN_DETAIL_LANG_MS.ToList();
            }
            else
            {
                return ct.TZ_SCREEN_DETAIL_LANG_MS.Where(x => x.LANG_CD == LangCd).ToList();
            }
        }
        public void InsertScreenDetail(List<TZ_SCREEN_DETAIL_LANG_MS> dataList)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                foreach (var data in dataList)
                {
                    if (!ct.TZ_SCREEN_DETAIL_LANG_MS.Any(x => x.SCREEN_CD == data.SCREEN_CD && x.CONTROL_CD == data.CONTROL_CD))
                    {
                        var ll = ct.TZ_LANG_MS.ToList();
                        foreach (var l in ll)
                        {
                            data.LANG_CD = l.LANG_CD;
                            ct.TZ_SCREEN_DETAIL_LANG_MS.Add(data);
                            ct.SaveChanges();
                        }
                    }
                }
                trans.Complete();
            }
        }
        public TZ_USER_MS GetUserProfile(string UserCd)
        {
            return ct.TZ_USER_MS.SingleOrDefault(x => x.USER_ACCOUNT == UserCd);
        }
        public List<Notify> GetNotify(string UserCd)
        {
            return ct.sp_Common_GetNotify.FromSqlRaw("sp_Common_GetNotify {0}", UserCd).ToList();
        }
        public bool ResponseNotify(Notify noti)
        {
            ct.Database.ExecuteSqlRaw("sp_Common_ResponseNotify {0}, {1}", noti.InfoDateTime, noti.Receiver);
            return true;
        }
        public List<ActivePermissionValue> GetActivePermission(string userGroup)
        {
            var P = ct.sp_SFM0061_GetStandardPermission.FromSqlRaw("sp_SFM0061_GetStandardPermission {0}", userGroup).ToList();
            var AP = P.GroupBy(g => new
            {
                SCREEN_CD = g.SCREEN_CD,
            }).Select(x => new ActivePermissionValue()
            {
                SCREEN_CD = x.Key.SCREEN_CD,
                VIEW = x.Where(w => w.METHOD == "VIEW").Max(m => m.CAN_EXECUTE).GetValueOrDefault(),
                ADD = x.Where(w => w.METHOD == "ADD").Max(m => m.CAN_EXECUTE).GetValueOrDefault(),
                EDIT = x.Where(w => w.METHOD == "EDIT").Max(m => m.CAN_EXECUTE).GetValueOrDefault(),
                DELETE = x.Where(w => w.METHOD == "DELETE").Max(m => m.CAN_EXECUTE).GetValueOrDefault(),
                PRINT = x.Where(w => w.METHOD == "PRINT").Max(m => m.CAN_EXECUTE).GetValueOrDefault(),
                CANCEL = x.Where(w => w.METHOD == "CANCEL").Max(m => m.CAN_EXECUTE).GetValueOrDefault(),
            }).ToList();
            return AP;
        }

        public List<SpecialPermissionResult> GetSpecialPermission(string userGroup, string screenCd )
        {
            var result = ct.sp_Common_GetSpecialPermission.FromSqlRaw("sp_Common_GetSpecialPermission {0}, {1}", userGroup, screenCd).ToList();
            return result;
        }

        public List<TBM_STATUS> GetStatusList()
        {
            return ct.TBM_STATUS.ToList();
        }

        public TZ_SYS_CONFIG GetSysConfig(string SYS_GROUP_ID, string SYS_KEY)
        {
            var result = ct.TZ_SYS_CONFIG.ToList().Where(c=>c.SYS_GROUP_ID==SYS_GROUP_ID && c.SYS_KEY==SYS_KEY).FirstOrDefault();
            return result;
        }
        public FileTemplate LoadMachineAttachment(string mACHINE_NO, string fILE_ID)
        {
            var result = ct.sp_PMS031_LoadAttachment.FromSqlRaw("sp_PMS031_LoadAttachment {0}", mACHINE_NO).ToList();
            var attachment = result.Where(a=>a.FILE_ID?.ToString() == fILE_ID).Select(f => new FileTemplate()
            {
                DisplayName = f.FILE_NAME_ORG,
                FILEHID = f.MACHINE_NO,
                FILEID = f.FILE_ID ?? 0,
                PhysicalName = Path.Combine(f.FILE_PATH, f.FILE_NAME),
                IsFromServer = true,
                FilePath = f.FILE_PATH
            }).FirstOrDefault();

            return attachment;
        }
    }
}
