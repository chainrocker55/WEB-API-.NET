using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.Flex.Models.Combo
{
    public class ComboStringValue
    {
        [Key]
        public string VALUE { get; set; }
        public string DISPLAY { get; set; }
        public string CODE { get; set; }
    }
}
