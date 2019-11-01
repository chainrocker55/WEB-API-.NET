﻿using FLEX.API.Models;
using FLEX.API.Modules.Flex.Models.Combo;
using FLEX.API.Modules.Models.PMS;
using FLEX.API.Modules.SYS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Data.Common;

namespace FLEX.API.Context
{
    public class FLEXContext: DbContext
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
        }

        #region Flex
        public DbSet<UserInfo> UserLogin { get; set; }
        public DbSet<sp_SYS_GetMenu_Result> sp_SYS_GetMenu { get; set; }
        public DbSet<TZ_MESSAGE_MS> TZ_MESSAGE_MS { get; set; }
        public DbSet<TZ_SCREEN_DETAIL_LANG_MS> TZ_SCREEN_DETAIL_LANG_MS { get; set; }
        public DbSet<TZ_USER_MS> TZ_USER_MS { get; set; }
        public DbSet<Notify> sp_Common_GetNotify { get; set; }
        #endregion

        #region Combo
        public DbSet<TZ_LANG_MS> TZ_LANG_MS { get; set; }
        public DbSet<TZ_USER_GROUP_MS> TZ_USER_GROUP_MS { get; set; }
        public DbSet<TZ_MENU_SET_MS> TZ_MENU_SET_MS { get; set; }
        public DbSet<TBM_DIVISION> TBM_DIVISION { get; set; }
        public DbSet<TBM_POSITION> TBM_POSITION { get; set; }

        public DbSet<ComboPersonInCharge_KIBUN_Result> sp_Combo_GetPersonInCharge_KIBUN { get; set; }
        #endregion

        #region System
        public DbSet<TB_CLASS_LIST_MS> TB_CLASS_LIST_MS { get; set; }
        public DbSet<sp_SFM031_LoadUser_Result> sp_SFM031_LoadUser { get; set; }
        #endregion

        #region PMS
        public DbSet<PMS060_CheckListAndRepairOrder_Result> sp_PMS060_GetMachineRepairOrderList { get; set; }
        #endregion
    }
}
