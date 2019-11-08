using FLEX.API.Common;
using FLEX.API.Common.Utils;
using FLEX.API.Context;
using FLEX.API.Models;
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
        List<PMS061_GetCheckJobPersonInCharge_Result> sp_PMS061_GetCheckJobPersonInCharge(string CHECK_REPH_ID, string MACHINE_NO);
        string PMS061_SaveData(PMS061_DTO data, string user);
        string PMS062_SaveData(PMS061_DTO data, string user);
        string GetMachineDefaultComponent(string MACHINE_NO);
        List<PMS062_GetJobPmChecklist_Result> sp_PMS062_GetJobPmChecklist(string CHECK_REPH_ID, string MACHINE_NO);
        List<PMS062_GetJobPmPart_Result> sp_PMS062_GetJobPmPart(string CHECK_REPH_ID, string MCBOM_CD, string PARTS_LOC_CD);
        List<PMS062_GetInQty_Result> sp_PMS062_GetInQty(string CHECK_REPH_ID, string xml);
        List<DLG045_ItemFindDialogWithParam_Result> sp_DLG045_ItemFindDialogWithParam(DLG045_Search_Criteria criteria);
        ErrorMessage Validate_PMS062(PMS061_DTO data);
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
            return ct.sp_PMS062_GetJobPmChecklist.FromSqlRaw("sp_PMS062_GetJobPmChecklist {0}, {1}", CHECK_REPH_ID, MACHINE_NO).ToList();
        }

        public List<PMS061_GetCheckJobPersonInCharge_Result> sp_PMS061_GetCheckJobPersonInCharge(string CHECK_REPH_ID, string MACHINE_NO)
        {
            return ct.sp_PMS061_GetCheckJobPersonInCharge.FromSqlRaw("sp_PMS061_GetCheckJobPersonInCharge {0}, {1}", CHECK_REPH_ID, MACHINE_NO).ToList();

        }


        public string GetMachineDefaultComponent(string MACHINE_NO)
        {
            return ct.sp_PMS062_GetMachineDefaultComponent.FromSqlRaw("sp_PMS062_GetMachineDefaultComponent {0}", MACHINE_NO).ToList().FirstOrDefault()?.VALUE;

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

        public string PMS062_SaveData(PMS061_DTO data, string user)
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

                #region checklist
                if (data.PmChecklist != null)
                {
                    var xmlChecklist = XmlUtil.ConvertToXml_Store(data.PmChecklist);
                    ct.Database.ExecuteSqlRaw("sp_PMS062_InsertOrUpdatePmChecklist {0}, {1}, {2}, {3}",
                        hid,
                        xmlChecklist,
                        user,
                        Constant.DEFAULT_MACHINE
                    );
                }
                #endregion

                #region part
                if (data.PmParts != null)
                {
                    var xmlPart = XmlUtil.ConvertToXml_Store(data.PmParts);
                    ct.Database.ExecuteSqlRaw("sp_PMS062_InsertOrUpdatePmPart {0}, {1}, {2}, {3}, {4}, {5}",
                        hid,
                        data.Header.TEST_DATE,
                        data.Header.MACHINE_NO,
                        xmlPart,
                        user,
                        Constant.DEFAULT_MACHINE
                    );

                    var onhand = PMS062_GetItemOnhandAtDate(data.PmParts.ToList(), hid, data.Header.TEST_DATE.Value);
                    var partTransaction = GetPartTransaction(data.PmParts, hid, data.Header.TEST_DATE, onhand);
                    var xmlTrans = XmlUtil.ConvertToXml_Store(partTransaction);
                    ct.Database.ExecuteSqlRaw("ct.sp_PMS062_SaveCheckJobTransaction {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                        hid,
                        data.Header.CHECK_REP_NO,
                        data.Header.TEST_DATE,
                        data.Header.MACHINE_NO,
                        xmlTrans,
                        user,
                        Constant.DEFAULT_MACHINE
                    );
                }
                #endregion

                #region approver
                //if (approver != null)
                //{
                //    ct.sp_PMS062_SaveApprover(
                //        hid,
                //        String.Join(",", approver)
                //    );
                //}

                //if (approveHistory != null)
                //{
                //    ct.sp_Common_InsertApproveHistory(
                //        approveHistory.APPROVE_ID,
                //        approveHistory.DOCUMENT_HID,
                //        approveHistory.DOCUMENT_DID,
                //        approveHistory.SourceType,
                //        approveHistory.DocumentNo,
                //        approveHistory.APPROVE_LEVEL,
                //        approveHistory.APPROVE_STATUS,
                //        approveHistory.REMARK,
                //        user
                //    );

                //}
                #endregion

                trans.Complete();
                return hid;
            }
        }

        public List<PMS062_Transaction> GetPartTransaction(List<PMS062_GetJobPmPart_Result> parts, string hid, DateTime? testDate, List<PMS062_GetItemOnhandAtDate_Result> onhand)
        {
            var result = new List<PMS062_Transaction>();

            if (testDate == null)
                return result;

            var partInv = ConvertToInventoryUnit(parts);
            //var onhand = mSvc.PMS062_GetItemOnhandAtDate(parts, hid, date.Value);
            foreach (var item in partInv)
            {
                decimal remainQty = item.INVQTY;
                foreach (var oh in onhand)
                {
                    if (remainQty <= 0)
                        break;

                    decimal qty = Math.Min(remainQty, oh.AVAILABLE_AT_DATE ?? 0);
                    result.Add(new PMS062_Transaction()
                    {
                        ITEM_CD = item.PARTS_ITEM_CD,
                        LOC_CD = item.PARTS_LOC_CD,
                        LOT_NO = oh.LOT_NO,
                        QTY = qty
                    });
                    remainQty -= qty;
                }
            }

            return result;
        }

        private List<PMS062_ConvertToInventoryUnit_Result> ConvertToInventoryUnit(List<PMS062_GetJobPmPart_Result> parts)
        {
            var xml = XmlUtil.ConvertToXml_Store(parts);
            var result = ct.sp_PMS062_ConvertToInventoryUnit.FromSqlRaw("sp_PMS062_ConvertToInventoryUnit {0}",xml).ToList();
            return result;
        }

        public ErrorMessage Validate_PMS062(PMS061_DTO data)
        {
            var partData = data.PmParts.Where(p => p.USED_QTY > 0).ToList();
            var parts = partData.Select(p => new PMS062_GetJobPmPart_Result()
            {
                PARTS_ITEM_CD = p.PARTS_ITEM_CD,
                PARTS_ITEM_DESC = p.PARTS_ITEM_DESC,
                PARTS_LOC_CD = p.PARTS_LOC_CD,
                UNITCODE = p.UNITCODE,
                USED_QTY = p.USED_QTY
            }).ToList();

            if (partData.Count > 0)
            {
                var error = ValidateOnHand(data.Header.CHECK_REPH_ID, DateTime.Today, parts);
                if (error != null)
                {
                    return error;
                }

                if (DateTime.Today != data.Header.TEST_DATE)
                {
                    error = ValidateOnHand(data.Header.CHECK_REPH_ID, data.Header.TEST_DATE, parts);
                    if (error != null)
                    {
                        return error;
                    }
                }
            }

            return null;
        }

        private ErrorMessage ValidateOnHand(string CHECK_REPH_ID, DateTime? date, List<PMS062_GetJobPmPart_Result> parts)
        {
            if (date == null)
                return null;

            var mOnhand = PMS062_GetItemOnhandAtDate(parts.ToList(), CHECK_REPH_ID, date.Value);

            var errorItem = new List<string>();
            foreach (var item in parts)
            {
                if ((item.USED_QTY ?? 0) == 0)
                    continue;

                var used = item.USED_QTY;
                var oh = mOnhand.Where(i => i.ITEM_CD == item.PARTS_ITEM_CD).ToList();
                if (oh == null || oh.Count == 0 || used > oh.Sum(i => i.AVAILABLE_AT_DATE) || used > oh.Sum(i => i.ONHAND_AT_DATE))
                {
                    errorItem.Add(item.PARTS_ITEM_DESC);
                    continue;
                }
            }

            if (errorItem.Count > 0)
            {
                //MessageDialogUtil.ShowBusiness(this, MessageCode.eValidate.VLM0463, new object[] { String.Join(", ", errorItem) });
                //return false;
                return new ErrorMessage("VLM0463", new object[] { String.Join(", ", errorItem) });
            }

            return null;


        }

        public List<PMS062_GetItemOnhandAtDate_Result> PMS062_GetItemOnhandAtDate(List<PMS062_GetJobPmPart_Result> parts, string CHECK_REPH_ID, DateTime targetDate)
        {
            var xml = XmlUtil.ConvertToXml_Store(parts);
            return ct.sp_PMS062_GetItemOnhandAtDate.FromSqlRaw("sp_PMS062_GetItemOnhandAtDate {0}, {1}, {2}", CHECK_REPH_ID, xml, targetDate).ToList();
        }

        public List<PMS062_GetJobPmPart_Result> sp_PMS062_GetJobPmPart(string CHECK_REPH_ID, string MCBOM_CD, string PARTS_LOC_CD)
        {
            return ct.sp_PMS062_GetJobPmPart.FromSqlRaw("sp_PMS062_GetJobPmPart {0}, {1}, {2}", CHECK_REPH_ID, MCBOM_CD, PARTS_LOC_CD).ToList();
        }

        public List<PMS062_GetInQty_Result> sp_PMS062_GetInQty(string CHECK_REPH_ID, string xml)
        {
            return ct.sp_PMS062_GetInQty.FromSqlRaw("sp_PMS062_GetInQty {0}, {1}", CHECK_REPH_ID, xml).ToList();
        }

        public List<DLG045_ItemFindDialogWithParam_Result> sp_DLG045_ItemFindDialogWithParam(DLG045_Search_Criteria criteria)
        {
            String ItemCls = "", ItemCate = "";
            if (criteria.FilterItemCls != null) ItemCls = String.Join(",", criteria.FilterItemCls);
            if (criteria.FilterItemCategory != null) ItemCate = String.Join(",", criteria.FilterItemCategory);
            return ct.sp_DLG045_ItemFindDialogWithParam.FromSqlRaw("sp_DLG045_ItemFindDialogWithParam {0}, {1}, {2}, {3}", criteria.ShowDeleted, criteria.ShowStopItem, ItemCls, ItemCate).ToList();
        }
    }
}
