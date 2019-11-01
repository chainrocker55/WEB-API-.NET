using FLEX.API.Common;
using FLEX.API.Context;
using FLEX.API.Models;
using FLEX.API.Modules.Flex.Models.Combo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FLEX.API.Services
{
    public interface IComboDataSvc
    {
        List<TZ_LANG_MS> GetLanguage();
        List<TZ_USER_GROUP_MS> GetUserGroup();
        List<TZ_MENU_SET_MS> GetMenuSet();
        List<TBM_DIVISION> GetDivision();
        List<TBM_POSITION> GetPosition();
        List<ComboPersonInCharge_KIBUN_Result> GetComboPersonInCharge_KIBUN();
    }
    public class ComboDataSvc : IComboDataSvc
    {
        private readonly AppSettings _appSettings;
        private readonly FLEXContext ct;

        public ComboDataSvc(IOptions<AppSettings> appSettings, FLEXContext context)
        {
            _appSettings = appSettings.Value;
            ct = context;
        }

        public List<TZ_LANG_MS> GetLanguage()
        {
            return ct.TZ_LANG_MS.ToList();
        }
        public List<TZ_USER_GROUP_MS> GetUserGroup()
        {
            return ct.TZ_USER_GROUP_MS.ToList();
        }
        public List<TZ_MENU_SET_MS> GetMenuSet()
        {
            return ct.TZ_MENU_SET_MS.ToList();
        }
        public List<TBM_DIVISION> GetDivision()
        {
            return ct.TBM_DIVISION.OrderBy(x => x.CODE).ToList();
        }
        public List<TBM_POSITION> GetPosition()
        {
            return ct.TBM_POSITION.OrderBy(x => x.POSITIONCODE).ToList();
        }

        public List<ComboPersonInCharge_KIBUN_Result> GetComboPersonInCharge_KIBUN()
        {
            return this.ct.sp_Combo_GetPersonInCharge_KIBUN.FromSqlRaw("sp_Combo_GetPersonInCharge_KIBUN").ToList();
        }
    }
}
