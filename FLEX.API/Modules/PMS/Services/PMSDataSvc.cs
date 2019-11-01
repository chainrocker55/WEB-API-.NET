using FLEX.API.Context;
using FLEX.API.Modules.Models.PMS;
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
        ActionResult<List<PMS060_CheckListAndRepairOrder_Result>> sp_PMS060_GetMachineRepairOrderList(PMS060_Search_Criteria criteria);
    }

    public class PMSDataSvc : IPMSDataSvc
    {
        private readonly FLEXContext ct;

        public PMSDataSvc(FLEXContext context)
        {
            ct = context;
        }

        public ActionResult<List<PMS060_CheckListAndRepairOrder_Result>> sp_PMS060_GetMachineRepairOrderList(PMS060_Search_Criteria criteria)
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
    }
}
