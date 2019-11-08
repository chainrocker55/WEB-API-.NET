using System.Collections.Generic;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS061_DTO
    {
        public PMS061_GetCheckJobH_Result Header { get; set; }
        public PMS061_GetCheckJobH_OH_Result HeaderOverHaul { get; set; }
        public List<PMS061_GetCheckJobPersonInCharge_Result> PersonInCharge { get; set; }
        public List<PMS062_GetJobPmChecklist_Result> PmChecklist { get; set; }
        public List<PMS062_GetJobPmPart_Result> PmParts { get; set; }
        public string DefaultComponent { get; set; }
        public string CurrentUser { get; set; }
    }
}
