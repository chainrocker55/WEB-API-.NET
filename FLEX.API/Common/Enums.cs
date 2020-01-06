using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Common
{
    public enum eConnection
    {
        CSI_FLEX_KIBUN,
        KIBUN_UAT,
        KIBUN_PRODUCTION,
    }

    public enum eInstuctionClass
    {
        /// <summary>
        /// 01 : Input
        /// </summary>
        [Description("01")]
        INPUT,

        /// <summary>
        /// 02 : Use
        /// </summary>
        [Description("02")]
        USE,

        /// <summary>
        /// 03 : Output
        /// </summary>
        [Description("03")]
        OUTPUT,
    }


    /// <summary>
    /// Kibun Unit Selection for GetUnit_ByModule_KIBUN
    /// </summary>
    public enum eKibunUnitSelection
    {
        /// <summary>
        /// Get Unit Active for Sales Modules
        /// </summary>
        Sales = 0,

        /// <summary>
        /// Get Unit Active for Module that have effect to Stock
        /// </summary>
        Stock = 1,

        /// <summary>
        /// Get Unit Active for Purchase Module
        /// </summary>
        Purchase = 2,
    }

    public enum eKibunWithdrawalType
    {
        /// <summary>
        /// 01 : Ingredient
        /// </summary>
        [Description("01")]
        Ingredient,

        /// <summary>
        /// 02 : Package
        /// </summary>
        [Description("02")]
        Package,

        /// <summary>
        /// 03 : Raw Material
        /// </summary>
        [Description("03")]
        RawMaterial,

        /// <summary>
        /// 04 : Raw Material Surimi
        /// </summary>
        [Description("04")]
        RawMaterialSurimi,

        /// <summary>
        /// 05 : Semi Finish Goods
        /// </summary>
        [Description("05")]
        SemiFinishGoods,

        /// <summary>
        /// 06 : Parts Withdrawal
        /// </summary>
        [Description("06")]
        PartsWithdrawal,
    }
}
