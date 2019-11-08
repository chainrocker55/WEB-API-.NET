using System;
using System.ComponentModel.DataAnnotations;

namespace FLEX.API.Models
{
    public class AuthModel
    {
        public string UserCd { get; set; }
        public string Password { get; set; }
    }

    public class UserInfo
    {
        [Key]
        public string USER_CD { get; set; }
        public string FULL_NAME { get; set; }
        public string EMAILADDR { get; set; }
        public string GROUP_CD { get; set; }
        public string LANG_CD { get; set; }
        public string TOKEN { get; set; }
    }

    public class Notify
    {
        [Key]
        public DateTime InfoDateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ScreenCD { get; set; }
        public string SubScreenCD { get; set; }
        public string Parameter { get; set; }
        public string Receiver { get; set; }
        public string ItemTemplateName { get; set; }
        public int Seq { get; set; }
        public bool HasRead { get; set; }
    }

    public class LanguageModel
    {
        public string LangCd { get; set; }
    }

    public class ServiceException : Exception
    {
        public string MSG_CD { get; set; }
        public object[] objParam { get; set; }

        public ServiceException() { }
        public ServiceException(string msg_cd, object[] param = null)
        {
            this.MSG_CD = msg_cd;
            this.objParam = param;
        }
    }
}
