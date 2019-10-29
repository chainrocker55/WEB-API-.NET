using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FLEX.API.Models
{
    public class sp_SYS_GetMenu_Result
    {
        public string MENU_SET_CD { get; set; }
        public string MENU_SUB_CD { get; set; }
        public string MENU_SUB_DESC { get; set; }
        public string MENU_SUB_URL { get; set; }
        public string MENU_SUB_ICON { get; set; }
        [Key]
        public string SCREEN_CD { get; set; }
        public string SCREEN_DESC { get; set; }
        public string SCREEN_URL { get; set; }
        public string SCREEN_ICON { get; set; }
    }
    public class NavData
    {
        public string ScreenCd { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string icon { get; set; }
        public NavBadge badge { get; set; }
        public bool title { get; set; }
        public bool divider { get; set; }
        public List<NavData> children { get; set; }
    }

    public class NavBadge
    {
        public string text { get; set; }
        public string variant { get; set; }
    }
}
