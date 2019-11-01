using FLEX.API.Context;
using FLEX.API.Modules.PMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.Services.PMS
{
    public interface IPMSDataSvc
    {
        List<PMS060_CheckListAndRepairOrder_Result> sp_PMS060_GetMachineRepairOrderList(PMS060_Search_Criteria criteria);
        List<PMS061_GetCheckJobH_Result> sp_PMS061_GetCheckJobH(string CHECK_REPH_ID);
        List<PMS061_GetCheckJobH_OH_Result> sp_PMS061_GetCheckJobH_OH(string CHECK_REPH_ID);
    }

    public class PMSDataSvc : IPMSDataSvc
    {
        private readonly FLEXContext ct;

        public PMSDataSvc(FLEXContext context)
        {
            ct = context;
        }

        public List<PMS060_CheckListAndRepairOrder_Result> sp_PMS060_GetMachineRepairOrderList(PMS060_Search_Criteria criteria)
        {
            return ct.sp_PMS060_GetMachineRepairOrderList.FromSqlRaw("sp_PMS060_GetMachineRepairOrderList {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}",
                criteria.REQUEST_DATE_FROM,
                criteria.REQUEST_DATE_TO,
                criteria.TEST_DATE_FROM,
                criteria.TEST_DATE_TO,
                criteria.COMPLETE_DATE_FROM,
                criteria.COMPLETE_DATE_TO,
                criteria.CHECK_REP_NO_FROM,
                criteria.CHECK_REP_NO_TO,
                criteria.PONUMBER_FROM,
                criteria.PONUMBER_TO,
                criteria.PERSONINCHARGE,
                criteria.REQUESTER,
                criteria.MACHINE_LOC_CD,
                criteria.CUR_PERSON,
                criteria.MACHINE_NO_FROM,
                criteria.MACHINE_NO_TO,
                criteria.MACHINE_NAME,
                criteria.VENDORID,
                criteria.SCHEDULE_TYPEID,
                criteria.STATUSID).ToList();
        }

        public List<PMS061_GetCheckJobH_Result> sp_PMS061_GetCheckJobH(string CHECK_REPH_ID)
        {
            return ct.sp_PMS061_GetCheckJobH.FromSqlRaw("sp_PMS061_GetCheckJobH {0}", CHECK_REPH_ID).ToList();
        }
        public List<PMS061_GetCheckJobH_OH_Result> sp_PMS061_GetCheckJobH_OH(string CHECK_REPH_ID)
        {
            return ct.sp_PMS061_GetCheckJobH_OH.FromSqlRaw("sp_PMS061_GetCheckJobH_OH {0}", CHECK_REPH_ID).ToList();
        }
    }
}
