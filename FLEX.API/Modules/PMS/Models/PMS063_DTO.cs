using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS063_DTO
    {

        public PMS063_GetCrHeader_Result Header { get; set; }
        public PMS063_GetJobCrAfterService_Result AfterService { get; set; }
        public PMS063_GetJobCrCheck_Result Check { get; set; }
        public List<PMS061_GetCheckJobPersonInCharge_Result> PersonInCharge { get; set; }

        public List<PMS063_GetJobCrPart_Result> Parts { get; set; }
        public List<PMS063_GetJobCrPart_Result> Tools { get; set; }
        public List<PMS063_GetPersonalChecklist_Result> PersonalChecklist { get; set; }
        public string DefaultComponent { get; set; }
        public string CurrentUser { get; set; }
    }
}
