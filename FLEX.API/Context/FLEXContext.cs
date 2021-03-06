﻿using FLEX.API.Models;
using FLEX.API.Modules.Flex.Models;
using FLEX.API.Modules.Flex.Models.Combo;
using FLEX.API.Modules.PMS.Models;
using FLEX.API.Modules.SYS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Data.Common;

namespace FLEX.API.Context
{
    public class FLEXContext : DbContext
    {

        public FLEXContext(DbContextOptions options) : base(options)
        {
                Database.SetCommandTimeout(300);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TZ_SCREEN_DETAIL_LANG_MS>().HasKey(c => new { c.CONTROL_CD, c.LANG_CD, c.SCREEN_CD });
            modelBuilder.Entity<TZ_ACCESS_CONTROL_MS>().HasKey(c => new { c.GROUP_CD, c.SCREEN_CD, c.METHOD });
            modelBuilder.Entity<TZ_SYS_CONFIG>().HasKey(c => new { c.SYS_GROUP_ID, c.SYS_KEY });

            modelBuilder.Entity<TB_CLASS_LIST_MS>().HasKey(c => new { c.CLS_INFO_CD, c.CLS_CD });
            modelBuilder.Entity<SpecialPermissionResult>().HasNoKey();

            modelBuilder.Entity<PMS060_CheckListAndRepairOrder_Result>().HasNoKey();
            modelBuilder.Entity<PMS062_GetJobPmChecklist_Result>().HasNoKey();

            modelBuilder.Entity<SFM0061_GetStandardPermission_Result>().HasNoKey();
            modelBuilder.Entity<SFM0061_GetSpecialPermission_Result>().HasNoKey();
            
            modelBuilder.Entity<PMS062_GetJobPmPart_Result>().HasNoKey();
            modelBuilder.Entity<PMS062_GetInQty_Result>().HasNoKey();

            modelBuilder.Entity<DLG045_ItemFindDialogWithParam_Result>().HasNoKey();

            modelBuilder.Entity<PMS062_GetJobPmPart_Result>().HasNoKey();
            modelBuilder.Entity<PMS062_GetItemOnhandAtDate_Result>().HasNoKey();
            modelBuilder.Entity<PMS062_Transaction>().HasNoKey();
            modelBuilder.Entity<PMS062_ConvertToInventoryUnit_Result>().HasNoKey();
            modelBuilder.Entity<PMS062_GetApproveRoute_Result>().HasNoKey();

            modelBuilder.Entity<PMS063_GetCrHeader_Result>().HasNoKey();
            modelBuilder.Entity<PMS063_GetJobCrAfterService_Result>().HasNoKey();
            modelBuilder.Entity<PMS063_GetJobCrCheck_Result>().HasNoKey();
            modelBuilder.Entity<PMS063_GetJobCrPart_Result>().HasNoKey();
            modelBuilder.Entity<PMS063_GetPersonalChecklist_Result>().HasNoKey();
            modelBuilder.Entity<PMS031_LoadMachineData_Result>().HasNoKey();
            modelBuilder.Entity<PMS060_UserDefaultValue>().HasNoKey();
            modelBuilder.Entity<sp_PMS062_LoadApproveHistory_Result>().HasNoKey();
            modelBuilder.Entity<sp_PMS031_LoadAttachment_Result>().HasNoKey();
            modelBuilder.Entity<sp_RPMS001_PartsWithdrawalSlipPM_Result>().HasNoKey();
            modelBuilder.Entity<sp_RPMS001_PartsWithdrawalSlipCorrective_Result>().HasNoKey();
            modelBuilder.Entity<CommonUnitDecimalDigit_KIBUN_Result>().HasNoKey();
            modelBuilder.Entity<sp_PMS151_GetDailyChecklist_Detail>().HasNoKey();
            modelBuilder.Entity<sp_PMS151_GetDailyChecklist_Detail_Item>().HasNoKey();
            modelBuilder.Entity<sp_PMS151_PrepareDailyChecklist_Result>().HasNoKey();
        }

        #region Flex
        public DbSet<UserInfo> UserLogin { get; set; }
        public DbSet<sp_SYS_GetMenu_Result> sp_SYS_GetMenu { get; set; }
        public DbSet<TZ_MESSAGE_MS> TZ_MESSAGE_MS { get; set; }
        public DbSet<TZ_SCREEN_DETAIL_LANG_MS> TZ_SCREEN_DETAIL_LANG_MS { get; set; }
        public DbSet<TZ_USER_MS> TZ_USER_MS { get; set; }
        public DbSet<TZ_USER_GROUP_MS> TZ_USER_GROUP_MS { get; set; }
        public DbSet<TZ_ACCESS_CONTROL_MS> TZ_ACCESS_CONTROL_MS { get; set; }
        public DbSet<Notify> sp_Common_GetNotify { get; set; }
        #endregion

        #region Combo
        public DbSet<TZ_LANG_MS> TZ_LANG_MS { get; set; }
        public DbSet<TZ_MENU_SET_MS> TZ_MENU_SET_MS { get; set; }
        public DbSet<TBM_DIVISION> TBM_DIVISION { get; set; }
        public DbSet<TBM_POSITION> TBM_POSITION { get; set; }
        public DbSet<TBM_STATUS> TBM_STATUS { get; set; }

        public DbSet<ComboPersonInCharge_KIBUN_Result> sp_Combo_GetPersonInCharge_KIBUN { get; set; }

        public DbSet<ComboStringValue> sp_Combo_GetUserAndPosition { get; set; }
        public DbSet<ComboStringValue> sp_Combo_GetLocationMs { get; set; }
        public DbSet<ComboStringValue> sp_Combo_GetUserApproveLocation { get; set; }
        public DbSet<ComboIntValue> sp_Combo_GetSupplier { get; set; }
        public DbSet<ComboIntValue> sp_Combo_GetMachineScheduleType { get; set; }
        public DbSet<ComboStringValue> sp_Combo_GetMachineStatus { get; set; }
        public DbSet<ComboStringValue> sp_Combo_GetMachine_KIBUN { get; set; }
        public DbSet<ComboIntValue> sp_Combo_GetCheckListPoNumber { get; set; }
        public DbSet<ComboIntValue> sp_Combo_GetMachinePeriod { get; set; }
        public DbSet<ComboStringValue> sp_Combo_GetMachineComponent_KIBUN { get; set; }
        public DbSet<ComboStringValue> sp_Combo_GetItemUnit_KIBUN { get; set; }
        public DbSet<ComboStringValue> sp_Combo_GetUnit_KIBUN { get; set; }

        #endregion

        #region Combo DailyChecklist
        public DbSet<ComboIntValue> sp_Combo_GetShiftType_KIBUN { get; set; }
        public DbSet<ComboIntValue> sp_Combo_GetLineCode_KIBUN { get; set; }
        public DbSet<ComboStringValue> sp_Combo_GetDailyChecklistStatus_KIBUN { get; set; }
        public DbSet<ComboStringValue> GetComboByClsInfoCD { get; set; }
        #endregion

        #region System
        public DbSet<TB_CLASS_LIST_MS> TB_CLASS_LIST_MS { get; set; }
        public DbSet<TZ_SYS_CONFIG> TZ_SYS_CONFIG { get; set; }
        public DbSet<sp_SFM031_LoadUser_Result> sp_SFM031_LoadUser { get; set; }
        public DbSet<SFM0061_GetStandardPermission_Result> sp_SFM0061_GetStandardPermission { get; set; }
        public DbSet<SFM0061_GetSpecialPermission_Result> sp_SFM0061_GetSpecialPermission { get; set; }
        public DbSet<SpecialPermissionResult> sp_Common_GetSpecialPermission { get; set; }
        #endregion

        #region PMS
        public DbSet<PMS060_CheckListAndRepairOrder_Result> sp_PMS060_GetMachineRepairOrderList { get; set; }
        public DbSet<PMS061_GetCheckJobH_Result> sp_PMS061_GetCheckJobH { get; set; }
        public DbSet<PMS061_GetCheckJobH_OH_Result> sp_PMS061_GetCheckJobH_OH { get; set; }
        public DbSet<PMS062_GetJobPmChecklist_Result> sp_PMS062_GetJobPmChecklist { get; set; }
        public DbSet<PMS061_GetCheckJobPersonInCharge_Result> sp_PMS061_GetCheckJobPersonInCharge { get; set; }
        public DbSet<sp_PMS062_LoadApproveHistory_Result> sp_PMS062_LoadApproveHistory { get; set; }
        public DbSet<String_Result> PMS061_SaveData { get; set; }
        public DbSet<String_Result> sp_PMS062_GetMachineDefaultComponent { get; set; }
        public DbSet<PMS062_GetJobPmPart_Result> sp_PMS062_GetJobPmPart { get; set; }
        public DbSet<PMS062_GetInQty_Result> sp_PMS062_GetInQty { get; set; }
        public DbSet<DLG045_ItemFindDialogWithParam_Result> sp_DLG045_ItemFindDialogWithParam { get; set; }
        public DbSet<PMS062_GetItemOnhandAtDate_Result> sp_PMS062_GetItemOnhandAtDate { get; set; }
        public DbSet<PMS062_ConvertToInventoryUnit_Result> sp_PMS062_ConvertToInventoryUnit { get; set; }
        public DbSet<PMS062_GetApproveRoute_Result> PMS062_GetApproveRoute { get; set; }
        public DbSet<PMS063_GetCrHeader_Result> sp_PMS063_GetCrHeader { get; set; }
        public DbSet<PMS063_GetJobCrPart_Result> sp_PMS063_GetJobCrPart { get; set; }
        public DbSet<PMS063_GetPersonalChecklist_Result> sp_PMS063_GetPersonalChecklist { get; set; }
        public DbSet<PMS063_GetJobCrCheck_Result> sp_PMS063_GetJobCrCheck { get; set; }
        public DbSet<PMS063_GetJobCrAfterService_Result> sp_PMS063_GetJobCrAfterService { get; set; }
        public DbSet<PMS031_LoadMachineData_Result> sp_PMS031_LoadMachineData { get; set; }

        public DbSet<PMS060_UserDefaultValue> sp_PMS060_GetCheckDefaultValueByUser { get; set; }


        public DbSet<String_Result> sp_PMS062_GetApprover { get; set; }

        public DbSet<sp_PMS031_LoadAttachment_Result> sp_PMS031_LoadAttachment { get; set; }

        public DbSet<String_Result> sp_PMS062_ValidateDateInPeriod { get; set; }
        public DbSet<sp_RPMS001_PartsWithdrawalSlipPM_Result> sp_RPMS001_PartsWithdrawalSlipPM { get; set; }
        public DbSet<sp_RPMS001_PartsWithdrawalSlipCorrective_Result> sp_RPMS001_PartsWithdrawalSlipCorrective { get; set; }
        public DbSet<CommonUnitDecimalDigit_KIBUN_Result> sp_Common_GetUnitDecimalDigit_KIBUN { get; set; }

        #endregion

        #region DailyChecklist
        public DbSet<TB_CLASS_LIST_MS_PMS> TB_CLASS_LIST_MS_PMS { get; set; }
        public DbSet<sp_PMS150_GetDailyChecklist> sp_PMS150_GetDailyChecklist { get; set; }
        public DbSet<sp_PMS151_GetDailyChecklist_Detail> sp_PMS151_GetDailyChecklist_Detail { get; set; }
        public DbSet<sp_PMS151_GetDailyChecklist_Detail_Item> sp_PMS151_GetDailyChecklist_Detail_Item { get; set; }
        public DbSet<String_Result> sp_PMS151_ValidateBeforeSaveDailyChecklist { get; set; }
        public DbSet<sp_PMS151_PrepareDailyChecklist_Result> sp_PMS151_PrepareDailyChecklist { get; set; }
        public DbSet<String_Result> sp_PMS151_SaveDailyChecklist_Header { get; set; }
        public DbSet<String_Result> sp_PMS151_SaveDailyChecklist_Detail { get; set; }
        public DbSet<String_Result> sp_PMS151_SaveDailyChecklist_Detail_Item { get; set; }

        #endregion

    }
}
