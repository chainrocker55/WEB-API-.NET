using System.ComponentModel.DataAnnotations;

namespace FLEX.API.Modules.PMS.Models
{
    public class TB_CLASS_LIST_MS_PMS
    {
        [Key]
        public string VALUE { get; set; }
        public string DISPLAY { get; set; }
        public string CODE { get; set; }
    }

}
