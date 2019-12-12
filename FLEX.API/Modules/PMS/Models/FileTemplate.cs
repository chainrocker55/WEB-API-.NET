using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class FileTemplate
    {
        /// <summary>
        /// Module : Use Screen Code String. ex: HPCPOS011.
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// ID : File ID on HPC_TB_FILE_ATTACHMENT.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// IsFromServer : true = file on server / false = file on local.
        /// </summary>
        public bool IsFromServer { get; set; }

        /// <summary>
        /// IsDelete : true = user do delete action.
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// DisplayName : File name to display.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// PhysicalName : Physical file name on server. 
        /// </summary>
        public string PhysicalName { get; set; }

        /// <summary>
        /// FilePath : File location on server.
        /// </summary>
        public string FilePath { get; set; }
        //public string DestinationPath { get; set; }

        /// <summary>
        /// LocalPath : File location on local.
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// FILEID : File ID on HPC_TB_FILE_ATTACHMENT.
        /// </summary>
        public int FILEID { get; set; }

        /// <summary>
        /// FILEHID : Module Header ID Or Unique Key 
        /// </summary>
        public string FILEHID { get; set; }

        /// <summary>
        /// FILEDID : Module Detail ID 
        /// </summary>
        public int? FILEDID { get; set; }


        public string TempPath { get; set; }
    }
}
