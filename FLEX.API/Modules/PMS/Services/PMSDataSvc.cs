using FLEX.API.Common;
using FLEX.API.Common.Utils;
using FLEX.API.Context;
using FLEX.API.Modules.PMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace FLEX.API.Modules.Services.PMS
{
    public interface IPMSDataSvc
    {
        List<PMS060_CheckListAndRepairOrder_Result> sp_PMS060_GetMachineRepairOrderList(PMS060_Search_Criteria criteria);
        List<PMS061_GetCheckJobH_Result> sp_PMS061_GetCheckJobH(string CHECK_REPH_ID);
        List<PMS061_GetCheckJobH_OH_Result> sp_PMS061_GetCheckJobH_OH(string CHECK_REPH_ID);
        List<PMS062_GetJobPmChecklist_Result> sp_PMS062_GetJobPmChecklist(string CHECK_REPH_ID, string MACHINE_NO);
        List<PMS061_GetCheckJobPersonInCharge_Result> sp_PMS061_GetCheckJobPersonInCharge(string CHECK_REPH_ID, string MACHINE_NO);
        string PMS061_SaveData(PMS061_DTO data, string user);
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

        public List<PMS062_GetJobPmChecklist_Result> sp_PMS062_GetJobPmChecklist(string CHECK_REPH_ID, string MACHINE_NO)
        {
            return ct.sp_PMS062_GetJobPmChecklist.FromSqlRaw("sp_PMS062_GetJobPmChecklist {0}, {1}", CHECK_REPH_ID , MACHINE_NO).ToList();
        }

        public List<PMS061_GetCheckJobPersonInCharge_Result> sp_PMS061_GetCheckJobPersonInCharge(string CHECK_REPH_ID, string MACHINE_NO)
        {
            return ct.sp_PMS061_GetCheckJobPersonInCharge.FromSqlRaw("sp_PMS061_GetCheckJobPersonInCharge {0}, {1}", CHECK_REPH_ID, MACHINE_NO).ToList();

        }


        public string PMS061_SaveData(PMS061_DTO data, string user)
        {
            using (var trans = new TransactionScope())
            {
                var hid = ct.PMS061_SaveData.FromSqlRaw("sp_PMS061_InsertOrUpdateCheckJobHeader {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}",
                    data.Header.CHECK_REPH_ID,
                    data.Header.CHECK_REP_NO,
                    data.Header.MACHINE_SCHEDULEID,
                    data.Header.MACHINE_NO,
                    data.Header.MACHINE_NAME,
                    data.Header.PLAN_DATE,
                    data.Header.REQUEST_DATE,
                    data.Header.COMPLETE_DATE,
                    data.Header.COMPLETE_TIME,
                    data.Header.TEST_DATE,
                    data.Header.MACHINE_LOC_CD,
                    data.Header.MACHINE_LOC,
                    data.Header.APPROVE_ID,
                    data.Header.CUR_LEVEL,
                    data.Header.NEXT_LEVEL,
                    data.Header.PERIOD,
                    data.Header.PERIOD_ID,
                    data.Header.COMPLETE_MTN,
                    data.Header.COMPLETE_RQ,
                    data.Header.SCHEDULE_TYPEID,
                    data.Header.AUTO_CREATEFLAG,
                    data.Header.CHECK_REP_NO_REF,
                    data.Header.REVISE_REMARK,
                    data.Header.CANCEL_REMARK,
                    data.Header.PRINT_FLAG,
                    data.Header.STATUSID,
                    data.Header.DELETEFLAG,

                    user,
                    Constant.DEFAULT_MACHINE
                ).ToList().FirstOrDefault()?.VALUE;

                ct.Database.ExecuteSqlRaw("sp_PMS061_InsertOrUpdateCheckJobH_OH {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
                    hid,
                    data.HeaderOverHaul.VENDORID,
                    data.HeaderOverHaul.POHID,
                    data.HeaderOverHaul.PONUMBER,
                    data.HeaderOverHaul.TOTAL_TIME_OPERATION,
                    data.HeaderOverHaul.PROBLEM_DESC,
                    data.HeaderOverHaul.REPAIR_METHOD,
                    data.HeaderOverHaul.CAUSE_DELAY,

                    user,
                    Constant.DEFAULT_MACHINE
                );

                #region person in charge
                var personInCharge = data.PersonInCharge;
                for (int i = 0; i < personInCharge.Count; i++)
                {
                    var item = personInCharge[i];
                    item.CHECK_REPH_ID = hid;
                    item.SEQ = i + 1;
                }
                var xml = XmlUtil.ConvertToXml_Store(personInCharge);
                ct.Database.ExecuteSqlRaw("sp_PMS061_InsertOrUpdateCheckJobPersonInCharge {0}, {1}, {2}, {3}",
                    hid,
                    xml,

                    user,
                    Constant.DEFAULT_MACHINE
                );
                #endregion

                trans.Complete();
                return hid;
            }
        }
    }
}
