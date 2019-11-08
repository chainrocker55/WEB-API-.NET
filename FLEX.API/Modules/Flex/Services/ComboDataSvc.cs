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
        List<ComboStringValue> GetComboUserWithPosition();
        List<ComboStringValue> GetComboLocation();
        List<ComboIntValue> GetComboSupplier();
        List<ComboIntValue> GetComboMachineScheduleType();
        List<ComboStringValue> GetComboMachineStatus();
        List<ComboStringValue> GetComboMachine();
        List<ComboIntValue> GetComboPoNumber();
        List<ComboIntValue> GetComboMachinePeriod();
        List<ComboStringValue> GetComboMachineComponent(string MACHINE_NO);
        List<ComboStringValue> GetComboItemUnit(string ITEM_CD);
        List<ComboStringValue> GetComboUnit(bool? SHOW_CODE);
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

        public List<ComboStringValue> GetComboUserWithPosition()
        {
            return this.ct.sp_Combo_GetUserAndPosition.FromSqlRaw("sp_Combo_GetUserAndPosition").ToList();
        }

        public List<ComboStringValue> GetComboLocation()
        {
            return this.ct.sp_Combo_GetLocationMs.FromSqlRaw("sp_Combo_GetLocationMs").ToList();
        }
        public List<ComboIntValue> GetComboSupplier()
        {
            return this.ct.sp_Combo_GetSupplier.FromSqlRaw("sp_Combo_GetSupplier").ToList();
        }

        public List<ComboIntValue> GetComboMachineScheduleType()
        {
            return this.ct.sp_Combo_GetMachineScheduleType.FromSqlRaw("sp_Combo_GetMachineScheduleType").ToList();

        }
        public List<ComboStringValue> GetComboMachineStatus()
        {
            return this.ct.sp_Combo_GetMachineStatus.FromSqlRaw("sp_Combo_GetMachineStatus").ToList();

        }
        public List<ComboStringValue> GetComboMachine()
        {
            return this.ct.sp_Combo_GetMachine_KIBUN.FromSqlRaw("sp_Combo_GetMachine_KIBUN").ToList();

        }
        public List<ComboIntValue> GetComboPoNumber()
        {
            return this.ct.sp_Combo_GetCheckListPoNumber.FromSqlRaw("sp_Combo_GetCheckListPoNumber").ToList();

        }
        public List<ComboIntValue> GetComboMachinePeriod()
        {
            return this.ct.sp_Combo_GetMachinePeriod.FromSqlRaw("sp_Combo_GetMachinePeriod").ToList();

        }

        public List<ComboStringValue> GetComboMachineComponent(string MACHINE_NO)
        {
            return this.ct.sp_Combo_GetMachineComponent_KIBUN.FromSqlRaw("sp_Combo_GetMachineComponent_KIBUN {0}", MACHINE_NO).ToList();

        }

        public List<ComboStringValue> GetComboItemUnit(string ITEM_CD)
        {
            return this.ct.sp_Combo_GetItemUnit_KIBUN.FromSqlRaw("sp_Combo_GetItemUnit_KIBUN {0}, {1}, {2}", ITEM_CD, null, true).ToList();

        }

        public List<ComboStringValue> GetComboUnit(bool? SHOW_CODE)
        {
            return this.ct.sp_Combo_GetUnit_KIBUN.FromSqlRaw("sp_Combo_GetUnit_KIBUN {0}", SHOW_CODE).ToList();
        }

    }
}
