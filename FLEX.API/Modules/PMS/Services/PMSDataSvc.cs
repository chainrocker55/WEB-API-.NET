using FLEX.API.Common;
using FLEX.API.Common.Extensions;
using FLEX.API.Common.Utils;
using FLEX.API.Context;
using FLEX.API.Models;
using FLEX.API.Modules.PMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace FLEX.API.Modules.Services.PMS
{
    public interface IPMSDataSvc
    {
        PMS060_UserDefaultValue PMS060_GetUserDefaultValue(string USER_CD);
        List<PMS060_CheckListAndRepairOrder_Result> sp_PMS060_GetMachineRepairOrderList(PMS060_Search_Criteria criteria);
        List<PMS061_GetCheckJobH_Result> sp_PMS061_GetCheckJobH(string CHECK_REPH_ID);
        List<PMS061_GetCheckJobH_OH_Result> sp_PMS061_GetCheckJobH_OH(string CHECK_REPH_ID);
        List<PMS061_GetCheckJobPersonInCharge_Result> sp_PMS061_GetCheckJobPersonInCharge(string CHECK_REPH_ID, string MACHINE_NO);
        string PMS061_SaveData(PMS061_DTO data, string user);
        string PMS062_SaveData(PMS061_DTO data);
        string sp_PMS062_GetMachineDefaultComponent(string MACHINE_NO);
        List<PMS062_GetJobPmChecklist_Result> sp_PMS062_GetJobPmChecklist(string CHECK_REPH_ID, string MACHINE_NO);
        List<PMS062_GetJobPmPart_Result> sp_PMS062_GetJobPmPart(string CHECK_REPH_ID, string MCBOM_CD, string PARTS_LOC_CD);
        List<PMS062_GetInQty_Result> sp_PMS062_GetInQty(string CHECK_REPH_ID, string xml);
        List<DLG045_ItemFindDialogWithParam_Result> sp_DLG045_ItemFindDialogWithParam(DLG045_Search_Criteria criteria);
        string PMS062_SendToApprove(PMS061_DTO data);
        string PMS062_Revise(PMS061_DTO data);
        void PMS062_Cancel(PMS061_DTO data);

        List<PMS063_GetCrHeader_Result> sp_PMS063_GetCrHeader(string CHECK_REPH_ID);
        List<PMS063_GetJobCrPart_Result> sp_PMS063_GetJobCrPart(string CHECK_REPH_ID, int? detailType);
        List<PMS063_GetPersonalChecklist_Result> sp_PMS063_GetPersonalChecklist(string CHECK_REPH_ID);
        List<PMS063_GetJobCrCheck_Result> sp_PMS063_GetJobCrCheck(string cHECK_REPH_ID);
        List<PMS063_GetJobCrAfterService_Result> sp_PMS063_GetJobCrAfterService(string cHECK_REPH_ID);

        string PMS063_SaveData(PMS063_DTO data);
        string PMS063_Confirm(PMS063_DTO data);
        string PMS063_SendToApprove(PMS063_DTO data);
        string PMS063_Approve(PMS063_DTO data);
        string PMS063_Revise(PMS063_DTO data);
        void PMS063_Cancel(PMS063_DTO data);

        PMS031_LoadMachineData_Result sp_PMS031_LoadMachineData(string MACHINE_NO);
        List<PMS061_GetCheckJobPersonInCharge_Result> sp_PMS031_LoadMachineData(string CHECK_REPH_ID, string MACHINE_NO);
        void PMS061_Cancel(PMS061_DTO data);
        List<sp_PMS062_LoadApproveHistory_Result> sp_PMS062_LoadApproveHistory(string stringValue);
        List<String_Result> PMS062_GetApprover(string cHECK_REPH_ID);
        List<FileTemplate> sp_PMS031_LoadAttachment(string stringValue);
    }

    public class PMSDataSvc : IPMSDataSvc
    {
        private readonly FLEXContext ct;

        public PMSDataSvc(FLEXContext context)
        {
            ct = context;
        }

        #region pms status
        const string STATUS_ACTIVE_PLAN = "F01"; // Active Plan
        const string STATUS_CANCEL_PLAN = "F02"; // Cancelled Plan
        const string STATUS_NEW = "F03"; // New Check/Repair Order
        const string STATUS_DURING_ASSIGN = "F04"; // During Assign
        const string STATUS_RECEIVED = "F05"; // Received Check/Repair Order
        const string STATUS_DURING_APPROVE = "F06"; // During Approve
        const string STATUS_REVISE = "F07"; // Revised
        const string STATUS_PARTIAL = "F08"; // Partial Check/Repair Order
        const string STATUS_COMPLETE = "F09"; // Completed Check/Repair Order
        const string STATUS_CANCEL = "F10"; // Cancelled Check/Repair Order
        #endregion

        public List<PMS060_CheckListAndRepairOrder_Result> sp_PMS060_GetMachineRepairOrderList(PMS060_Search_Criteria criteria)
        {
            return ct.sp_PMS060_GetMachineRepairOrderList.FromSqlRaw("sp_PMS060_GetMachineRepairOrderList {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}",
                criteria.REQUEST_DATE_FROM?.ToLocalTime(),
                criteria.REQUEST_DATE_TO?.ToLocalTime(),
                criteria.TEST_DATE_FROM?.ToLocalTime(),
                criteria.TEST_DATE_TO?.ToLocalTime(),
                criteria.COMPLETE_DATE_FROM?.ToLocalTime(),
                criteria.COMPLETE_DATE_TO?.ToLocalTime(),
                criteria.CHECK_REP_NO_FROM.NullIfEmpty(),
                criteria.CHECK_REP_NO_TO.NullIfEmpty(),
                criteria.PONUMBER_FROM.NullIfEmpty(),
                criteria.PONUMBER_TO.NullIfEmpty(),
                criteria.PERSONINCHARGE,
                criteria.REQUESTER.NullIfEmpty(),
                criteria.MACHINE_LOC_CD.NullIfEmpty(),
                criteria.CUR_PERSON.NullIfEmpty(),
                criteria.MACHINE_NO_FROM.NullIfEmpty(),
                criteria.MACHINE_NO_TO.NullIfEmpty(),
                criteria.MACHINE_NAME.NullIfEmpty(),
                criteria.VENDORID,
                criteria.SCHEDULE_TYPEID,
                criteria.STATUSID).ToList();
        }

        public List<PMS061_GetCheckJobH_Result> sp_PMS061_GetCheckJobH(string CHECK_REPH_ID)
        {
            return ct.sp_PMS061_GetCheckJobH.FromSqlRaw("sp_PMS061_GetCheckJobH {0}", CHECK_REPH_ID).ToList();
        }

        public List<PMS063_GetCrHeader_Result> sp_PMS063_GetCrHeader(string CHECK_REPH_ID)
        {
            return ct.sp_PMS063_GetCrHeader.FromSqlRaw("sp_PMS063_GetCrHeader {0}", CHECK_REPH_ID).ToList();
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


        public string sp_PMS062_GetMachineDefaultComponent(string MACHINE_NO)
        {
            try
            {
                return ct.sp_PMS062_GetMachineDefaultComponent.FromSqlRaw("sp_PMS062_GetMachineDefaultComponent {0}", MACHINE_NO).ToList()?.FirstOrDefault()?.VALUE;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public PMS060_UserDefaultValue PMS060_GetUserDefaultValue(string USER_CD)
        {
            var data = ct.sp_PMS060_GetCheckDefaultValueByUser.FromSqlRaw("sp_PMS060_GetCheckDefaultValueByUser {0}", USER_CD)?.ToList()?.FirstOrDefault();
            return data;
        }
        public string PMS061_SaveData(PMS061_DTO data, string user)
        {
            ValidateJobUpdateDate(data.Header.CHECK_REPH_ID, data.Header.LASTUPDATEDATETIME);
            using (var trans = new TransactionScope())
            {
                var hid = ct.PMS061_SaveData.FromSqlRaw("sp_PMS061_InsertOrUpdateCheckJobHeader {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}",
                    data.Header.CHECK_REPH_ID,
                    data.Header.CHECK_REP_NO,
                    data.Header.MACHINE_SCHEDULEID,
                    data.Header.MACHINE_NO,
                    data.Header.MACHINE_NAME,
                    data.Header.PLAN_DATE?.ToLocalTime(),
                    data.Header.REQUEST_DATE?.ToLocalTime(),
                    data.Header.COMPLETE_DATE?.ToLocalTime(),
                    data.Header.COMPLETE_TIME,
                    data.Header.TEST_DATE?.ToLocalTime(),
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
                    item.DELETEFLAG = "N";
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

        private void ValidateJobUpdateDate(string CHECK_REPH_ID, DateTime? LASTUPDATEDATETIME)
        {
            if (CHECK_REPH_ID != null)
            {
                var h = sp_PMS061_GetCheckJobH(CHECK_REPH_ID).FirstOrDefault();
                if (h == null)
                    return;

                if (h.LASTUPDATEDATETIME != LASTUPDATEDATETIME)
                {
                    throw new ServiceException("VLM0352");
                }
            }
        }

        public string PMS062_SaveData(PMS061_DTO data)
        {
            // validate data
            Validate_PMS062(data);
            PMS062_SetPartSeq(data.PmParts);


            // get transaction           
            var partTransaction = GetPartTransaction(data.PmParts, data.Header.CHECK_REPH_ID, data.Header.TEST_DATE?.ToLocalTime(), null);

            return PMS062_SaveAll(data.Header, data.PersonInCharge, data.PmChecklist, data.PmParts, partTransaction, null, null, data.CurrentUser, false);

        }

        private void PMS062_SetPartSeq(List<PMS062_GetJobPmPart_Result> partData)
        {
            int seq = 1;
            foreach (var item in partData)
            {
                item.SEQ = seq++;
            }
        }

        public string PMS062_SaveAll(PMS061_GetCheckJobH_Result h, List<PMS061_GetCheckJobPersonInCharge_Result> personInCharge, List<PMS062_GetJobPmChecklist_Result> checklist, List<PMS062_GetJobPmPart_Result> partsData, List<PMS062_Transaction> partTransaction, List<string> approver, ApproveHistory approveHistory, string userCd, bool sendNotification)
        {
            ValidateJobUpdateDate(h.CHECK_REPH_ID, h.LASTUPDATEDATETIME);
            using (var trans = new TransactionScope())
            {
                var hid = ct.PMS061_SaveData.FromSqlRaw("sp_PMS061_InsertOrUpdateCheckJobHeader {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}",
                    h.CHECK_REPH_ID,
                    h.CHECK_REP_NO,
                    h.MACHINE_SCHEDULEID,
                    h.MACHINE_NO,
                    h.MACHINE_NAME,
                    h.PLAN_DATE?.ToLocalTime(),
                    h.REQUEST_DATE?.ToLocalTime(),
                    h.COMPLETE_DATE?.ToLocalTime(),
                    h.COMPLETE_TIME,
                    h.TEST_DATE?.ToLocalTime(),
                    h.MACHINE_LOC_CD,
                    h.MACHINE_LOC,
                    h.APPROVE_ID,
                    h.CUR_LEVEL,
                    h.NEXT_LEVEL,
                    h.PERIOD,
                    h.PERIOD_ID,
                    h.COMPLETE_MTN,
                    h.COMPLETE_RQ,
                    h.SCHEDULE_TYPEID,
                    h.AUTO_CREATEFLAG,
                    h.CHECK_REP_NO_REF,
                    h.REVISE_REMARK,
                    h.CANCEL_REMARK,
                    h.PRINT_FLAG,
                    h.STATUSID,
                    h.DELETEFLAG,

                    userCd,
                    Constant.DEFAULT_MACHINE
                ).ToList().FirstOrDefault()?.VALUE;



                #region person in charge      
                if (personInCharge != null)
                {
                    for (int i = 0; i < personInCharge.Count; i++)
                    {
                        var item = personInCharge[i];
                        item.CHECK_REPH_ID = hid;
                        item.SEQ = i + 1;
                        item.DELETEFLAG = "N";
                    }

                    var xml = XmlUtil.ConvertToXml_Store(personInCharge);
                    ct.Database.ExecuteSqlRaw("sp_PMS061_InsertOrUpdateCheckJobPersonInCharge {0}, {1}, {2}, {3}",
                        hid,
                        xml,

                        userCd,
                        Constant.DEFAULT_MACHINE
                    );
                }
                #endregion

                #region checklist
                if (checklist != null)
                {
                    var xmlChecklist = XmlUtil.ConvertToXml_Store(checklist);
                    ct.Database.ExecuteSqlRaw("sp_PMS062_InsertOrUpdatePmChecklist {0}, {1}, {2}, {3}",
                        hid,
                        xmlChecklist,
                        userCd,
                        Constant.DEFAULT_MACHINE
                    );
                }
                #endregion

                #region part
                if (partsData != null)
                {
                    var xmlPart = XmlUtil.ConvertToXml_Store(partsData);
                    ct.Database.ExecuteSqlRaw("sp_PMS062_InsertOrUpdatePmPart {0}, {1}, {2}, {3}, {4}, {5}",
                        hid,
                        h.TEST_DATE?.ToLocalTime(),
                        h.MACHINE_NO,
                        xmlPart,
                        userCd,
                        Constant.DEFAULT_MACHINE
                    );
                    
                }

                if (partTransaction != null)
                {
                    var xmlTrans = XmlUtil.ConvertToXml_Store(partTransaction);
                    ct.Database.ExecuteSqlRaw("sp_PMS062_SaveCheckJobTransaction {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                        hid,
                        h.CHECK_REP_NO,
                        h.TEST_DATE?.ToLocalTime(),
                        h.MACHINE_NO,
                        xmlTrans,
                        userCd,
                        Constant.DEFAULT_MACHINE
                    );
                }

                #endregion

                #region approver
                if (approver != null)
                {
                    ct.Database.ExecuteSqlRaw("sp_PMS062_SaveApprover {0}, {1}",
                        hid,
                        String.Join(",", approver)
                    );
                }

                if (approveHistory != null)
                {
                    ct.Database.ExecuteSqlRaw("sp_Common_InsertApproveHistory {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
                        approveHistory.APPROVE_ID,
                        approveHistory.DOCUMENT_HID,
                        approveHistory.DOCUMENT_DID,
                        approveHistory.SourceType,
                        approveHistory.DocumentNo,
                        approveHistory.APPROVE_LEVEL,
                        approveHistory.APPROVE_STATUS,
                        approveHistory.REMARK,
                        userCd
                    );

                }
                #endregion


                if (sendNotification)
                {
                    SendJobNotification(hid, "PMS062");
                }

                trans.Complete();
                return hid;
            }
        }

        public List<PMS062_Transaction> GetPartTransaction(List<PMS062_GetJobPmPart_Result> parts, string hid, DateTime? testDate, List<PMS062_GetItemOnhandAtDate_Result> onhand)
        {
            var result = new List<PMS062_Transaction>();

            if (testDate == null)
                return result;

            if (onhand == null)
                onhand = PMS062_GetItemOnhandAtDate(parts, hid, testDate.Value);

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
            var result = ct.sp_PMS062_ConvertToInventoryUnit.FromSqlRaw("sp_PMS062_ConvertToInventoryUnit {0}", xml).ToList();
            return result;
        }

        public bool Validate_PMS062(PMS061_DTO data)
        {
            var partData = data.PmParts.Where(p => p.REQUEST_QTY > 0).ToList();
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
                ValidateOnHand(data.Header.CHECK_REPH_ID, DateTime.Today, parts);

                if (DateTime.Today != data.Header.TEST_DATE?.ToLocalTime())
                {
                    ValidateOnHand(data.Header.CHECK_REPH_ID, data.Header.TEST_DATE?.ToLocalTime(), parts);
                }
            }

            data.PmParts = partData;

            return true;
        }

        public bool Validate_PMS063(string CHECK_REPH_ID, DateTime? COMPLETE_DATE, List<PMS062_GetJobPmPart_Result> parts)
        {
            if (parts.Count > 0)
            {
                ValidateOnHand(CHECK_REPH_ID, DateTime.Today, parts);

                if (DateTime.Today != COMPLETE_DATE)
                {
                    ValidateOnHand(CHECK_REPH_ID, COMPLETE_DATE, parts);
                }
            }

            return true;
        }

        private bool ValidateOnHand(string CHECK_REPH_ID, DateTime? date, List<PMS062_GetJobPmPart_Result> parts)
        {
            if (date == null)
                return true;

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
                throw new ServiceException("VLM0463", new object[] { String.Join(", ", errorItem) });
            }

            return true;


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

        public string PMS062_SendToApprove(PMS061_DTO data)
        {
            // validate data
            Validate_PMS062(data);

            //validate approve route
            var approveList = ct.PMS062_GetApproveRoute.FromSqlRaw("sp_PMS062_GetApproveRoute {0}, {1}", data.Header.MACHINE_NO, data.Header.MACHINE_LOC_CD).ToList();
            if (approveList.Count == 0)
            {
                throw new ServiceException("VLM0439");
            }

            var approveHistory = GetApproveHistory(data.Header, data.CurrentUser);
            SetApproverData(data.Header, approveList);
            approveHistory.APPROVE_STATUS = "APPROVE"; //data.Header.STATUSID;
            if (data.Header.STATUSID == STATUS_COMPLETE)
            {
                var partTransaction = GetPartTransaction(data.PmParts, data.Header.CHECK_REPH_ID, data.Header.TEST_DATE?.ToLocalTime(), null);
                data.Header.CHECK_REPH_ID = PMS062_SaveAll(
                    data.Header,
                    null,
                    null,
                    null,
                    partTransaction,
                    GetApprover(data.Header, approveList),
                    approveHistory,
                    data.CurrentUser,
                    true
                );
            }
            else
            {
                data.Header.CHECK_REPH_ID = PMS062_SaveAll(
                    data.Header,
                    null,
                    null,
                    null,
                    null,
                    GetApprover(data.Header, approveList),
                    approveHistory,
                    data.CurrentUser,
                    true
                );
            }

            return data.Header.CHECK_REPH_ID;
        }

        private List<string> GetApprover(PMS061_GetCheckJobH_Result mData, List<PMS062_GetApproveRoute_Result> approveList)
        {
            if (mData.CUR_LEVEL == null)
                return new List<string>();

            return approveList.Where(a => a.APPROVE_LEVEL == mData.CUR_LEVEL).Select(a => a.APPROVE_USER).ToList();
        }
        private List<string> GetApprover(PMS063_GetCrHeader_Result mData, List<PMS062_GetApproveRoute_Result> approveList)
        {
            if (mData.CUR_LEVEL == null || approveList == null)
                return new List<string>();

            return approveList?.Where(a => a.APPROVE_LEVEL == mData.CUR_LEVEL).Select(a => a.APPROVE_USER).ToList();
        }

        private ApproveHistory GetApproveHistory(PMS061_GetCheckJobH_Result mData, string user)
        {
            return new ApproveHistory()
            {
                APPROVE_ID = mData.APPROVE_ID,
                APPROVE_LEVEL = mData.CUR_LEVEL,
                APPROVE_STATUS = null,
                APPROVE_USER = user,
                DocumentNo = mData.CHECK_REPH_ID,
                DOCUMENT_DID = null,
                DOCUMENT_HID = null,
                REMARK = null,
                SourceType = "PMS"
            };
        }

        private ApproveHistory GetApproveHistory(PMS063_GetCrHeader_Result mData, string user)
        {
            return new ApproveHistory()
            {
                APPROVE_ID = mData.APPROVE_ID,
                APPROVE_LEVEL = mData.CUR_LEVEL,
                APPROVE_STATUS = null,
                APPROVE_USER = user,
                DocumentNo = mData.CHECK_REPH_ID,
                DOCUMENT_DID = null,
                DOCUMENT_HID = null,
                REMARK = null,
                SourceType = "PMS"
            };
        }

        private void SetApproverData(PMS061_GetCheckJobH_Result mData, List<PMS062_GetApproveRoute_Result> approveList)
        {
            var approve = approveList.First();
            //var maxLevel = approveList.Select(a => a.APPROVE_LEVEL).Max()??0;

            mData.APPROVE_ID = approve.APPROVE_ID;
            mData.CUR_LEVEL = approveList
                                .Where(a => (a.APPROVE_LEVEL ?? 0) > (mData.CUR_LEVEL ?? 0))
                                .Select(a => a.APPROVE_LEVEL)
                                .Min() ?? 0;
            mData.NEXT_LEVEL = approveList
                                .Where(a => (a.APPROVE_LEVEL ?? 0) > (mData.CUR_LEVEL ?? 0))
                                .Select(a => a.APPROVE_LEVEL)
                                .Min() ?? 0;
            mData.STATUSID = mData.CUR_LEVEL == 0 ? STATUS_COMPLETE : STATUS_DURING_APPROVE;
        }

        private void SetApproverData(PMS063_GetCrHeader_Result mData, List<PMS062_GetApproveRoute_Result> approveList)
        {
            var approve = approveList.FirstOrDefault();
            //var maxLevel = approveList.Select(a => a.APPROVE_LEVEL).Max()??0;

            mData.APPROVE_ID = approve?.APPROVE_ID;
            mData.CUR_LEVEL = approveList?
                                .Where(a => (a.APPROVE_LEVEL ?? 0) > (mData.CUR_LEVEL ?? 0))
                                .Select(a => a.APPROVE_LEVEL)
                                .Min() ?? 0;
            mData.NEXT_LEVEL = approveList
                                .Where(a => (a.APPROVE_LEVEL ?? 0) > (mData.CUR_LEVEL ?? 0))
                                .Select(a => a.APPROVE_LEVEL)
                                .Min() ?? 0;
            mData.STATUSID = mData.CUR_LEVEL == 0 ? STATUS_COMPLETE : STATUS_DURING_APPROVE;
        }

        public string PMS062_Revise(PMS061_DTO data)
        {
            // validate data
            //Validate_PMS062(data);
            var approveHistory = GetApproveHistory(data.Header, data.CurrentUser);
            data.Header.APPROVE_ID = null;
            data.Header.CUR_LEVEL = null;
            data.Header.NEXT_LEVEL = null;
            data.Header.STATUSID = STATUS_REVISE;
            //data.Header.REVISE_REMARK = dlg.Remark;

            //approveHistory.APPROVE_STATUS = data.Header.STATUSID;
            approveHistory.APPROVE_STATUS = "REVISE";
            approveHistory.REMARK = data.Header.REVISE_REMARK;

            data.Header.CHECK_REPH_ID = PMS062_SaveAll(
                data.Header,
                null,
                null,
                null,
                null,
                new List<string>(),
                approveHistory,
                data.CurrentUser,
                true
            );

            return data.Header.CHECK_REPH_ID;
        }

        public void PMS062_Cancel(PMS061_DTO data)
        {
            ct.Database.ExecuteSqlRaw("sp_PMS062_CancelMachineCheckJob_PM {0}, {1}, {2}, {3}",
                   data.Header.CHECK_REPH_ID,
                   data.Header.CANCEL_REMARK,
                   data.CurrentUser,
                   Constant.DEFAULT_MACHINE
                );

            SendJobNotification(data.Header.CHECK_REPH_ID, "PMS062");
        }

        public List<PMS063_GetJobCrPart_Result> sp_PMS063_GetJobCrPart(string CHECK_REPH_ID, int? detailType)
        {
            return ct.sp_PMS063_GetJobCrPart.FromSqlRaw("sp_PMS063_GetJobCrPart {0}, {1}",
                CHECK_REPH_ID,
                detailType
            ).ToList();
        }

        public List<PMS063_GetPersonalChecklist_Result> sp_PMS063_GetPersonalChecklist(string CHECK_REPH_ID)
        {
            var result = ct.sp_PMS063_GetPersonalChecklist.FromSqlRaw("sp_PMS063_GetPersonalChecklist {0}",
                CHECK_REPH_ID
            ).ToList();

            //foreach(var item in result)
            //{
            //    item.PASS = item.PASS_CHECK == null ? null : (bool?)(item.PASS_CHECK == "Y");
            //    item.NOT_PASS = item.PASS_CHECK == null ? null : (bool?)(item.PASS_CHECK == "N");

            //}

            return result;
        }

        public List<PMS063_GetJobCrCheck_Result> sp_PMS063_GetJobCrCheck(string CHECK_REPH_ID)
        {
            return ct.sp_PMS063_GetJobCrCheck.FromSqlRaw("sp_PMS063_GetJobCrCheck {0}",
                CHECK_REPH_ID
            ).ToList();
        }

        public List<PMS063_GetJobCrAfterService_Result> sp_PMS063_GetJobCrAfterService(string CHECK_REPH_ID)
        {
            return ct.sp_PMS063_GetJobCrAfterService.FromSqlRaw("sp_PMS063_GetJobCrAfterService {0}",
                CHECK_REPH_ID
            ).ToList();
        }

        public string PMS063_SaveData(PMS063_DTO data)
        {
            // validate data
            var partData = data.Parts.Where(p => p.REQUEST_QTY > 0).ToList();
            var parts = partData.Select(p => new PMS062_GetJobPmPart_Result()
            {
                PARTS_ITEM_CD = p.ITEM_CD,
                PARTS_ITEM_DESC = p.ITEM_DESC,
                PARTS_LOC_CD = p.LOC_CD,
                UNITCODE = p.UNITCODE,
                USED_QTY = p.OUT_USEDQTY
            }).ToList();
            Validate_PMS063(data.Header.CHECK_REPH_ID, data.Header.COMPLETE_DATE?.ToLocalTime(), parts);

            // get transaction           
            var partTransaction = GetPartTransaction(parts, data.Header.CHECK_REPH_ID, data.Header.COMPLETE_DATE?.ToLocalTime(), null);
            return PMS063_SaveAll(
                data.Header,
                data.PersonInCharge,
                data.Tools,
                data.Parts,
                partTransaction,
                data.PersonalChecklist,
                data.Check,
                null,
                null,
                data.CurrentUser,
                false
            );
        }

        public string PMS063_Confirm(PMS063_DTO data)
        {
            if (data.Header.STATUSID == STATUS_NEW)
                data.Header.STATUSID = STATUS_DURING_ASSIGN;
            else if (data.Header.STATUSID == STATUS_DURING_ASSIGN)
                data.Header.STATUSID = STATUS_RECEIVED;


            //// validate data
            //var partData = data.Parts.Where(p => p.OUT_USEDQTY > 0 || p.REQUEST_QTY > 0).ToList();
            //var parts = partData.Select(p => new PMS062_GetJobPmPart_Result()
            //{
            //    PARTS_ITEM_CD = p.ITEM_CD,
            //    PARTS_ITEM_DESC = p.ITEM_DESC,
            //    PARTS_LOC_CD = p.LOC_CD,
            //    UNITCODE = p.UNITCODE,
            //    USED_QTY = p.OUT_USEDQTY
            //}).ToList();
            //Validate_PMS063(data.Header.CHECK_REPH_ID, data.Header.COMPLETE_DATE, parts);

            //// get transaction           
            //var partTransaction = GetPartTransaction(parts, data.Header.CHECK_REPH_ID, data.Header.TEST_DATE, null);

            return PMS063_SaveAll(
                data.Header,
                data.PersonInCharge,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                data.CurrentUser,
                true
            );
        }

        public string PMS063_SaveAll(
            PMS063_GetCrHeader_Result h,
            List<PMS061_GetCheckJobPersonInCharge_Result> personInCharge,
            List<PMS063_GetJobCrPart_Result> tools,
            List<PMS063_GetJobCrPart_Result> parts,
            List<PMS062_Transaction> partTransaction,
            List<PMS063_GetPersonalChecklist_Result> personalChecklist,
            PMS063_GetJobCrCheck_Result crCheck,
            List<string> approver,
            ApproveHistory approveHistory,
            string userCd,
            bool sendNotification)
        {

            ValidateJobUpdateDate(h.CHECK_REPH_ID, h.LASTUPDATEDATETIME);
            using (var trans = new TransactionScope())
            {
                var hid = ct.PMS061_SaveData.FromSqlRaw("sp_PMS061_InsertOrUpdateCheckJobHeader {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}",
                    h.CHECK_REPH_ID,
                    h.CHECK_REP_NO,
                    h.MACHINE_SCHEDULEID,
                    h.MACHINE_NO,
                    h.MACHINE_NAME,
                    h.PLAN_DATE?.ToLocalTime(),
                    h.REQUEST_DATE?.ToLocalTime(),
                    h.COMPLETE_DATE?.ToLocalTime(),
                    h.COMPLETE_TIME,
                    h.TEST_DATE?.ToLocalTime(),
                    h.MACHINE_LOC_CD,
                    h.MACHINE_LOC,
                    h.APPROVE_ID,
                    h.CUR_LEVEL,
                    h.NEXT_LEVEL,
                    h.PERIOD,
                    h.PERIOD_ID,
                    h.COMPLETE_MTN,
                    h.COMPLETE_RQ,
                    h.SCHEDULE_TYPEID,
                    h.AUTO_CREATEFLAG,
                    h.CHECK_REP_NO_REF,
                    h.REVISE_REMARK,
                    h.CANCEL_REMARK,
                    h.PRINT_FLAG,
                    h.STATUSID,
                    h.DELETEFLAG,

                    userCd,
                    Constant.DEFAULT_MACHINE
                ).ToList().FirstOrDefault()?.VALUE;



                #region person in charge      
                if (personInCharge != null)
                {
                    for (int i = 0; i < personInCharge.Count; i++)
                    {
                        var item = personInCharge[i];
                        item.CHECK_REPH_ID = hid;
                        item.SEQ = i + 1;
                        item.DELETEFLAG = "N";
                    }

                    var xml = XmlUtil.ConvertToXml_Store(personInCharge);
                    ct.Database.ExecuteSqlRaw("sp_PMS061_InsertOrUpdateCheckJobPersonInCharge {0}, {1}, {2}, {3}",
                        hid,
                        xml,

                        userCd,
                        Constant.DEFAULT_MACHINE
                    );
                }
                #endregion

                #region TB_CHECK_JOBH_CR_RQ
                ct.Database.ExecuteSqlRaw("sp_PMS063_InsertOrUpdateCheckJob_CR_RQ {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                    hid,
                    h.START_REQ_DATE?.ToLocalTime(),
                    h.START_REQ_TIME,
                    h.TROUBLE,
                    h.REQUESTER,
                    h.POSITIONID,
                    userCd,
                        Constant.DEFAULT_MACHINE
                );
                #endregion

                #region TB_CHECK_JOBH_CR_MTN
                ct.Database.ExecuteSqlRaw("sp_PMS063_InsertOrUpdateCheckJob_CR_MTN {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}",
                    hid,
                    h.REC_REQUEST_DATE?.ToLocalTime(),
                    h.REC_REQUEST_TIME,
                    h.SKIP_APPROVAL,
                    h.ASSIGNER,
                    h.ASSIGN_POSITIONID, h.APPROVE_RQ,
                    h.IN_OUT_PROD_LINE,
                    h.PROBLEM_DESC,
                    h.REPAIR_METHOD,
                    h.PREVENTIVE_METHOD,
                    h.CAUSE_DELAY,
                    h.CLEAN_FLAG,
                    h.CLEAN_PERSON,
                    h.CLEAN_POSITIONID,
                    h.CLEAN_DATE?.ToLocalTime(),
                    h.CHECK_MC_FLAG,
                    h.CHECK_MC_PERSON,
                    h.CHECK_MC_POSITIONID,
                    h.CHECK_MC_DATE?.ToLocalTime(),

                    userCd,
                        Constant.DEFAULT_MACHINE
                );
                #endregion               

                #region approver
                if (approver != null)
                {
                    ct.Database.ExecuteSqlRaw("sp_PMS062_SaveApprover {0}, {1}",
                        hid,
                        String.Join(",", approver)
                    );
                }

                if (approveHistory != null)
                {
                    ct.Database.ExecuteSqlRaw("sp_Common_InsertApproveHistory {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
                        approveHistory.APPROVE_ID,
                        approveHistory.DOCUMENT_HID,
                        approveHistory.DOCUMENT_DID,
                        approveHistory.SourceType,
                        approveHistory.DocumentNo,
                        approveHistory.APPROVE_LEVEL,
                        approveHistory.APPROVE_STATUS,
                        approveHistory.REMARK,
                        userCd
                    );

                }
                #endregion



                #region part

                var partData = new List<PMS063_GetJobCrPart_Result>();

                if (tools != null)
                {
                    foreach (var item in tools)
                    {
                        item.DETAILTYPE = 1;
                        partData.Add(item);
                    }
                }

                if (parts != null)
                {
                    foreach (var item in parts)
                    {
                        item.DETAILTYPE = 2;
                        partData.Add(item);
                    }
                }

                if (partData.Count > 0)
                {
                    var xmlPart = XmlUtil.ConvertToXml_Store(partData);
                    ct.Database.ExecuteSqlRaw("sp_PMS063_InsertOrUpdateCrPart {0}, {1}, {2}, {3}",
                       hid,
                        xmlPart,
                        userCd,
                        Constant.DEFAULT_MACHINE
                    );
                }

                #endregion

                #region transaction

                if (partTransaction != null)
                {
                    var xmlTrans = XmlUtil.ConvertToXml_Store(partTransaction);
                    ct.Database.ExecuteSqlRaw("sp_PMS062_SaveCheckJobTransaction  {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                        hid,
                        h.CHECK_REP_NO,
                        h.COMPLETE_DATE?.ToLocalTime(),
                        h.MACHINE_NO,
                        xmlTrans,
                        userCd,
                        Constant.DEFAULT_MACHINE
                    );
                }
                #endregion


                #region personal checklist
                if (personalChecklist != null)
                {
                    foreach (var item in personalChecklist)
                    {
                        if (item.PASS != true && item.NOT_PASS != true)
                        {
                            item.PASS = null;
                        }
                    }

                    var xmlPH = XmlUtil.ConvertToXml_Store(personalChecklist);
                    ct.Database.ExecuteSqlRaw("sp_PMS063_InsertOrUpdateCrPersonal {0}, {1}, {2}, {3}",
                        hid,
                        xmlPH,
                        userCd,
                        Constant.DEFAULT_MACHINE
                    );
                }
                #endregion

                #region TB_CHECK_JOBD_CR_CHECK 

                if (crCheck != null)
                {
                    ct.Database.ExecuteSqlRaw("sp_PMS063_InsertOrUpdateCrCheck {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}",
                        hid,
                        crCheck.CHECK_MC_ID,
                        crCheck.CHECK_MC_DATE_FR?.ToLocalTime(),
                        crCheck.CHECK_MC_DATE_TO?.ToLocalTime(),
                        crCheck.CHECK_MC_PERSON,
                        crCheck.CHECK_MC_POSITIONID,
                        crCheck.CHECK_MC_DATE?.ToLocalTime(),
                        crCheck.CLEAN_FLAG,
                        crCheck.CLEAN_PERSON,
                        crCheck.CLEAN_POSITIONID,
                        crCheck.CLEAN_DATE?.ToLocalTime(),
                        crCheck.QC_CHECK,
                        crCheck.QC_CHECK_DESC,
                        crCheck.QC_PERSON,
                        crCheck.QC_POSITIONID,
                        crCheck.QC_DATE?.ToLocalTime(),

                        userCd,
                        Constant.DEFAULT_MACHINE
                    );
                }

                #endregion



                #region complete
                if (h.STATUSID == "F09")
                {
                    //if (h.COMPLETE_DATE == null)
                    //    throw new Exception("Complete Date is empty.");

                    // generate job
                    var machineData = ct.sp_PMS031_LoadMachineData.FromSqlRaw("sp_PMS031_LoadMachineData {0}", h.MACHINE_NO).ToList().FirstOrDefault();

                    if (machineData?.CR_PERIOD1 != null && machineData?.CR_PERIOD1_ID != null && h.COMPLETE_DATE?.ToLocalTime() != null)
                    {

                        var sch1 = ScheduleUtil.GetNextDate(new Schedule()
                        {
                            PERIOD = machineData?.CR_PERIOD1,
                            PERIOD_ID = machineData?.CR_PERIOD1_ID,
                            //DAYS = null,
                            //END_DATE = null,
                            //START_DATE = h.COMPLETE_DATE
                        }, h.COMPLETE_DATE?.ToLocalTime());
                        var sch1_hid = GenerateAutoJob(ct, h, sch1, userCd);

                        DateTime? sch2 = null;
                        string sch2_id = null;
                        if (machineData?.CR_PERIOD2 != null && machineData?.CR_PERIOD2_ID != null)
                        {
                            sch2 = ScheduleUtil.GetNextDate(new Schedule()
                            {
                                PERIOD = machineData?.CR_PERIOD2,
                                PERIOD_ID = machineData?.CR_PERIOD2_ID,
                                //DAYS = null,
                                //END_DATE = null,
                                //START_DATE = sch1
                            }, sch1);

                            sch2_id = GenerateAutoJob(ct, h, sch2.Value, userCd);
                        }

                        ct.Database.ExecuteSqlRaw("sp_PMS063_InsertOrUpdateCrAfterService {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                           hid,
                           sch1,
                           sch1_hid,
                           sch2,
                           sch2_id,

                           userCd,
                           Constant.DEFAULT_MACHINE

                       );
                    }

                }

                #endregion

                if (sendNotification)
                {
                    SendJobNotification(hid, "PMS063");
                }





                trans.Complete();
                return hid;
            }



        }

        private string GenerateAutoJob(FLEXContext ct, PMS063_GetCrHeader_Result h, DateTime sch1, string userCd)
        {
            var sch1_hid = ct.PMS061_SaveData.FromSqlRaw("sp_PMS061_InsertOrUpdateCheckJobHeader {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}",
                null,
                null,
                null,
                h.MACHINE_NO,
                h.MACHINE_NAME,
                sch1,
                sch1,
                null,
                null,
                null,
                null,
                h.MACHINE_LOC,
                null,
                null,
                null,
                null,
                null,
                "N",
                "N",
                4, // for generate cr
                "Y",
                null,
                null,
                null,
                null,
                "F03", // new
                "N",

                userCd,
                Constant.DEFAULT_MACHINE
            ).ToList().FirstOrDefault()?.VALUE;

            ct.Database.ExecuteSqlRaw("sp_PMS061_InsertOrUpdateCheckJobH_OH {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
                sch1_hid,
                null,
                null,
                null,
                null,
                null,
                null,
                null,

                userCd,
                Constant.DEFAULT_MACHINE

            );

            return sch1_hid;
        }

        public string PMS063_SendToApprove(PMS063_DTO data)
        {
            List<PMS062_GetApproveRoute_Result> approveList = null;
            if ((data.Header.SKIP_APPROVAL ?? "N") == "N")
            {
                //validate approve route
                approveList = ct.PMS062_GetApproveRoute.FromSqlRaw("sp_PMS063_GetApproveRoute {0}, {1}", data.Header.MACHINE_NO, data.Header.MACHINE_LOC_CD).ToList();
                if (approveList.Count == 0)
                {
                    throw new ServiceException("VLM0439");
                }
            }

            var approveHistory = GetApproveHistory(data.Header, data.CurrentUser);
            SetApproverData(data.Header, approveList);
            //approveHistory.APPROVE_STATUS = data.Header.STATUSID;
            approveHistory.APPROVE_STATUS = "APPROVE";

            var partsData = data.Parts.Where(p => p.OUT_USEDQTY > 0 || p.REQUEST_QTY > 0).ToList();
            var parts = partsData.Select(p => new PMS062_GetJobPmPart_Result()
            {
                PARTS_ITEM_CD = p.ITEM_CD,
                PARTS_LOC_CD = p.LOC_CD,
                UNITCODE = p.UNITCODE,
                USED_QTY = p.OUT_USEDQTY
            }).ToList();

            Validate_PMS063(data.Header.CHECK_REPH_ID, data.Header.COMPLETE_DATE?.ToLocalTime(), parts);

            //var toolsData = data.Tools.Where(p => p.OUT_USEDQTY > 0 || p.REQUEST_QTY > 0).ToList();


            var partTransaction = GetPartTransaction(parts, data.Header.CHECK_REPH_ID, data.Header.COMPLETE_DATE?.ToLocalTime(), null);
            data.Header.CHECK_REPH_ID = PMS063_SaveAll(
                data.Header,
                data.PersonInCharge,

                //toolsData,
                data.Tools,

                partsData,
                partTransaction,
                null,
                null,
                GetApprover(data.Header, approveList),
                null,
                data.CurrentUser,
                true
            );

            return data.Header.CHECK_REPH_ID;
        }

        public string PMS063_Approve(PMS063_DTO data)
        {
            //validate approve route
            var approveList = ct.PMS062_GetApproveRoute.FromSqlRaw("sp_PMS063_GetApproveRoute {0}, {1}", data.Header.MACHINE_NO, data.Header.MACHINE_LOC_CD).ToList();
            if (approveList.Count == 0)
            {
                throw new ServiceException("VLM0439");
            }

            var approveHistory = GetApproveHistory(data.Header, data.CurrentUser);
            SetApproverData(data.Header, approveList);
            //approveHistory.APPROVE_STATUS = data.Header.STATUSID;
            approveHistory.APPROVE_STATUS = "APPROVE";

            if (data.Header.STATUSID == STATUS_COMPLETE)
            {
                var partsData = data.Parts.Where(p => p.OUT_USEDQTY > 0 || p.REQUEST_QTY > 0).ToList();
                var parts = partsData.Select(p => new PMS062_GetJobPmPart_Result()
                {
                    PARTS_ITEM_CD = p.ITEM_CD,
                    PARTS_LOC_CD = p.LOC_CD,
                    UNITCODE = p.UNITCODE,
                    USED_QTY = p.OUT_USEDQTY
                }).ToList();
                Validate_PMS063(data.Header.CHECK_REPH_ID, data.Header.COMPLETE_DATE?.ToLocalTime(), parts);
                var toolsData = data.Tools.Where(p => p.OUT_USEDQTY > 0 || p.REQUEST_QTY > 0).ToList();
                var partTransaction = GetPartTransaction(parts, data.Header.CHECK_REPH_ID, data.Header.COMPLETE_DATE?.ToLocalTime(), null);

                data.Header.CHECK_REPH_ID = PMS063_SaveAll(
                    data.Header,
                    null,
                    null,
                    null,
                    partTransaction,
                    data.PersonalChecklist,
                    data.Check,
                    GetApprover(data.Header, approveList),
                    approveHistory,
                    data.CurrentUser,
                    true
                );
            }
            else
            {
                data.Header.CHECK_REPH_ID = PMS063_SaveAll(
                    data.Header,
                    null,
                    null,
                    null,
                    null,
                    data.PersonalChecklist,
                    data.Check,
                    GetApprover(data.Header, approveList),
                    approveHistory,
                    data.CurrentUser,
                    true
                );
            }

            return data.Header.CHECK_REPH_ID;
        }

        public string PMS063_Revise(PMS063_DTO data)
        {
            // validate data
            //Validate_PMS062(data);
            var approveHistory = GetApproveHistory(data.Header, data.CurrentUser);
            data.Header.APPROVE_ID = null;
            data.Header.CUR_LEVEL = null;
            data.Header.NEXT_LEVEL = null;
            data.Header.STATUSID = STATUS_REVISE;
            //data.Header.REVISE_REMARK = dlg.Remark;

            //approveHistory.APPROVE_STATUS = data.Header.STATUSID;
            approveHistory.APPROVE_STATUS = "REVISE";
            approveHistory.REMARK = data.Header.REVISE_REMARK;

            data.Header.CHECK_REPH_ID = PMS063_SaveAll(
                data.Header,
                null,
                null,
                null,
                null,
                null,
                null,
                new List<string>(),
                approveHistory,
                data.CurrentUser,
                true
            );

            return data.Header.CHECK_REPH_ID;
        }

        public void PMS063_Cancel(PMS063_DTO data)
        {
            ct.Database.ExecuteSqlRaw("sp_PMS063_CancelMachineCheckJob_CR {0}, {1}, {2}, {3}",
                   data.Header.CHECK_REPH_ID,
                   data.Header.CANCEL_REMARK,
                   data.CurrentUser,
                   Constant.DEFAULT_MACHINE
                );

            SendJobNotification(data.Header.CHECK_REPH_ID, "PMS063");
        }

        public void SendJobNotification(string hid, string subScreenCd)
        {
            ct.Database.ExecuteSqlRaw("sp_PMS062_SendNotification {0}, {1}", hid, subScreenCd);
        }

        public PMS031_LoadMachineData_Result sp_PMS031_LoadMachineData(string MACHINE_NO)
        {
            var machineData = ct.sp_PMS031_LoadMachineData.FromSqlRaw("sp_PMS031_LoadMachineData {0}", MACHINE_NO).ToList().FirstOrDefault();
            return machineData;
        }

        public List<PMS061_GetCheckJobPersonInCharge_Result> sp_PMS031_LoadMachineData(string CHECK_REPH_ID, string MACHINE_NO)
        {
            var result = ct.sp_PMS061_GetCheckJobPersonInCharge.FromSqlRaw("sp_PMS061_GetCheckJobPersonInCharge {0} {1}", CHECK_REPH_ID, MACHINE_NO).ToList();
            return result;
        }

        public void PMS061_Cancel(PMS061_DTO data)
        {
            ct.Database.ExecuteSqlRaw("sp_PMS061_CancelMachineCheckList {0}, {1}, {2}, {3}",
                   data.Header.CHECK_REPH_ID,
                   data.Header.CANCEL_REMARK,
                   data.CurrentUser,
                   Constant.DEFAULT_MACHINE
                );

            //SendJobNotification(data.Header.CHECK_REPH_ID, "PMS062");
        }

        public List<sp_PMS062_LoadApproveHistory_Result> sp_PMS062_LoadApproveHistory(string CHECK_REPH_ID)
        {
            var result = ct.sp_PMS062_LoadApproveHistory.FromSqlRaw("sp_PMS062_LoadApproveHistory {0}", CHECK_REPH_ID).ToList();
            return result;
        }

        public List<String_Result> PMS062_GetApprover(string cHECK_REPH_ID)
        {
            var result = ct.sp_PMS062_GetApprover.FromSqlRaw("sp_PMS062_GetApprover {0}", cHECK_REPH_ID).ToList();
            return result;
        }

        public List<FileTemplate> sp_PMS031_LoadAttachment(string MACHINE_NO)
        {
            var result = ct.sp_PMS031_LoadAttachment.FromSqlRaw("sp_PMS031_LoadAttachment {0}", MACHINE_NO).ToList();
            var attachment = result.Select(f => new FileTemplate()
            {
                DisplayName = f.FILE_NAME_ORG,
                FILEHID = f.MACHINE_NO,
                FILEID = f.FILE_ID ?? 0,
                PhysicalName = Path.Combine(f.FILE_PATH, f.FILE_NAME),
                IsFromServer = true,
                FilePath = f.FILE_PATH
            }).ToList();

            return attachment;
        }
    }
}
