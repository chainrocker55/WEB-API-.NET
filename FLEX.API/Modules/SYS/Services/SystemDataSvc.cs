using FLEX.API.Common;
using FLEX.API.Context;
using FLEX.API.Models;
using FLEX.API.Modules.SYS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace FLEX.API.Modules.SYS.Services
{
    public interface ISystemDataSvc
    {
        List<TB_CLASS_LIST_MS> GetClassList();
        void SaveClassList(TB_CLASS_LIST_MS data);

        List<TZ_USER_MS> GetUserList();
        sp_SFM031_LoadUser_Result GetUser(string UserCd);
        void SaveUser(sp_SFM031_LoadUser_Result data, string userCd);

        List<TZ_USER_GROUP_MS> GetUserGroupList();
        List<SFM0061_GetStandardPermission_Result> sp_SFM0061_GetStandardPermission(string userGroup);
        List<SFM0061_GetSpecialPermission_Result> sp_SFM0061_GetSpecialPermission(string userGroup);
    }
    public class SystemDataSvc : ISystemDataSvc
    {
        private readonly AppSettings _appSettings;
        private readonly FLEXContext ct;

        public SystemDataSvc(IOptions<AppSettings> appSettings, FLEXContext context)
        {
            _appSettings = appSettings.Value;
            ct = context;
        }
        public List<TB_CLASS_LIST_MS> GetClassList()
        {
            return ct.TB_CLASS_LIST_MS.ToList();
        }
        public void SaveClassList(TB_CLASS_LIST_MS data)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                var tb = ct.TB_CLASS_LIST_MS.SingleOrDefault(x => x.CLS_INFO_CD == data.CLS_INFO_CD && x.CLS_CD == data.CLS_CD);
                tb.CLS_DESC = data.CLS_DESC;
                tb.SEQ = data.SEQ;
                ct.SaveChanges();
                trans.Complete();
            }
        }

        public List<TZ_USER_MS> GetUserList()
        {
            return ct.TZ_USER_MS.Select(x => new TZ_USER_MS()
            {
                USER_ACCOUNT = x.USER_ACCOUNT,
                FULL_NAME = x.FULL_NAME,
                EMAILADDR = x.EMAILADDR,
                FLG_ABSENCE = x.FLG_ABSENCE,
                FLG_ACTIVE = x.FLG_ACTIVE,
                FLG_RESIGN = x.FLG_RESIGN,
            }).ToList();
        }
        public sp_SFM031_LoadUser_Result GetUser(string UserCd)
        {
            return ct.sp_SFM031_LoadUser.FromSqlInterpolated($"sp_SFM031_LoadUser @p_USER_ACCOUNT={UserCd}").ToList().FirstOrDefault();
        }
        public void SaveUser(sp_SFM031_LoadUser_Result data, string userCd)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                Byte[] imgtype = { 0 };
                if (data.SIGNATURE == null) { data.SIGNATURE = imgtype; }
                var objs = new object[] { 
                    data.USER_ACCOUNT,
                    data.MENU_SET_CD, 
                    data.GROUP_CD, 
                    data.DEPARTMENT_CD, 
                    data.LANG_CD, 
                    data.DATE_FORMAT,
                    data.PASS, 
                    data.FULL_NAME, 
                    data.APPLY_DATE, 
                    data.FLG_RESIGN, 
                    data.FLG_ACTIVE,
                    data.FLG_ABSENCE, 
                    data.EMAILADDR, 
                    data.SIGNATURE, 
                    data.DIVISIONID, 
                    null,
                    null, 
                    data.POSITIONID, 
                    "N", 
                    userCd, 
                    null 
                };

                ct.Database.ExecuteSqlRaw("sp_SFM031_InsertOrUpdateUser {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}", objs);
                trans.Complete();
            }
        }

        public List<TZ_USER_GROUP_MS> GetUserGroupList()
        {
            return ct.TZ_USER_GROUP_MS.OrderBy(x => x.GROUP_CD).ToList();
        }

        public List<SFM0061_GetStandardPermission_Result> sp_SFM0061_GetStandardPermission(string userGroup)
        {
            return ct.sp_SFM0061_GetStandardPermission.FromSqlRaw("sp_SFM0061_GetStandardPermission {0}", userGroup).ToList();
        }
        public List<SFM0061_GetSpecialPermission_Result> sp_SFM0061_GetSpecialPermission(string userGroup)
        {
            return ct.sp_SFM0061_GetSpecialPermission.FromSqlRaw("sp_SFM0061_GetSpecialPermission {0}", userGroup).ToList();
        }
    }
}
