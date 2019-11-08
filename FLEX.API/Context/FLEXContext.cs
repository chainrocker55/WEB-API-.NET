using FLEX.API.Models;
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

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TZ_SCREEN_DETAIL_LANG_MS>()
                .HasKey(c => new { c.CONTROL_CD, c.LANG_CD, c.SCREEN_CD });

            modelBuilder.Entity<TB_CLASS_LIST_MS>()
                .HasKey(c => new { c.CLS_INFO_CD, c.CLS_CD });

            modelBuilder.Entity<PMS060_CheckListAndRepairOrder_Result>()
                .HasNoKey();

            modelBuilder.Entity<PMS062_GetJobPmChecklist_Result>()
                .HasNoKey();


            modelBuilder.Entity<SFM0061_GetStandardPermission_Result>().HasNoKey();
            modelBuilder.Entity<SFM0061_GetSpecialPermission_Result>().HasNoKey();
            
            modelBuilder.Entity<PMS062_GetJobPmPart_Result>()
               .HasNoKey();

            modelBuilder.Entity<PMS062_GetInQty_Result>()
               .HasNoKey();

            modelBuilder.Entity<DLG045_ItemFindDialogWithParam_Result>()
               .HasNoKey();

            modelBuilder.Entity<PMS062_GetJobPmPart_Result>()
               .HasNoKey();

            modelBuilder.Entity<PMS062_GetItemOnhandAtDate_Result>().HasNoKey();
            modelBuilder.Entity<PMS062_Transaction>().HasNoKey();
            modelBuilder.Entity<PMS062_ConvertToInventoryUnit_Result>().HasNoKey();
            modelBuilder.Entity<PMS062_GetApproveRoute_Result>().HasNoKey();
        }

        #region Flex
        public DbSet<UserInfo> UserLogin { get; set; }
        public DbSet<sp_SYS_GetMenu_Result> sp_SYS_GetMenu { get; set; }
        public DbSet<TZ_MESSAGE_MS> TZ_MESSAGE_MS { get; set; }
        public DbSet<TZ_SCREEN_DETAIL_LANG_MS> TZ_SCREEN_DETAIL_LANG_MS { get; set; }
        public DbSet<TZ_USER_MS> TZ_USER_MS { get; set; }
        public DbSet<TZ_USER_GROUP_MS> TZ_USER_GROUP_MS { get; set; }
        public DbSet<Notify> sp_Common_GetNotify { get; set; }
        #endregion

        #region Combo
        public DbSet<TZ_LANG_MS> TZ_LANG_MS { get; set; }
        public DbSet<TZ_MENU_SET_MS> TZ_MENU_SET_MS { get; set; }
        public DbSet<TBM_DIVISION> TBM_DIVISION { get; set; }
        public DbSet<TBM_POSITION> TBM_POSITION { get; set; }

        public DbSet<ComboPersonInCharge_KIBUN_Result> sp_Combo_GetPersonInCharge_KIBUN { get; set; }

        public DbSet<ComboStringValue> sp_Combo_GetUserAndPosition { get; set; }
        public DbSet<ComboStringValue> sp_Combo_GetLocationMs { get; set; }
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

        #region System
        public DbSet<TB_CLASS_LIST_MS> TB_CLASS_LIST_MS { get; set; }
        public DbSet<sp_SFM031_LoadUser_Result> sp_SFM031_LoadUser { get; set; }
        public DbSet<SFM0061_GetStandardPermission_Result> sp_SFM0061_GetStandardPermission { get; set; }
        public DbSet<SFM0061_GetSpecialPermission_Result> sp_SFM0061_GetSpecialPermission { get; set; }
        #endregion

        #region PMS
        public DbSet<PMS060_CheckListAndRepairOrder_Result> sp_PMS060_GetMachineRepairOrderList { get; set; }
        public DbSet<PMS061_GetCheckJobH_Result> sp_PMS061_GetCheckJobH { get; set; }
        public DbSet<PMS061_GetCheckJobH_OH_Result> sp_PMS061_GetCheckJobH_OH { get; set; }
        public DbSet<PMS062_GetJobPmChecklist_Result> sp_PMS062_GetJobPmChecklist { get; set; }
        public DbSet<PMS061_GetCheckJobPersonInCharge_Result> sp_PMS061_GetCheckJobPersonInCharge { get; set; }
        public DbSet<String_Result> PMS061_SaveData { get; set; }
        public DbSet<String_Result> sp_PMS062_GetMachineDefaultComponent { get; set; }
        public DbSet<PMS062_GetJobPmPart_Result> sp_PMS062_GetJobPmPart { get; set; }
        public DbSet<PMS062_GetInQty_Result> sp_PMS062_GetInQty { get; set; }
        public DbSet<DLG045_ItemFindDialogWithParam_Result> sp_DLG045_ItemFindDialogWithParam { get; set; }
        public DbSet<PMS062_GetItemOnhandAtDate_Result> sp_PMS062_GetItemOnhandAtDate { get; set; }
        public DbSet<PMS062_ConvertToInventoryUnit_Result> sp_PMS062_ConvertToInventoryUnit { get; set; }
        public DbSet<PMS062_GetApproveRoute_Result> PMS062_GetApproveRoute { get; set; }
        #endregion

    }
}
