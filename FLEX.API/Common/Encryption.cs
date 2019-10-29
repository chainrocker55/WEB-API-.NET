using System;
using System.Security.Cryptography;
using System.Text;

namespace FLEX.API.Common
{
    public class Encryption
    {
        public static string MD5EncryptString(string UserCD, string Password)
        {
            byte[] byteUpper = Encryption.MD5EncryptString(UserCD.ToUpper());
            byte[] byteLower = Encryption.MD5EncryptString(UserCD.ToLower());

            byte[] bytePassword = Encryption.MD5EncryptString(Password);

            string strEncrypted = string.Empty;
            for (int i = 0; i < byteUpper.Length; i++)
            {
                strEncrypted += String.Format("{0:X2}", (byte)((byteUpper[i] ^ byteLower[i]) ^ bytePassword[i]));
            }

            return strEncrypted;
        }

        private static byte[] MD5EncryptString(string plainText)
        {
            MD5 md5 = MD5.Create();
            byte[] byteText = Encoding.ASCII.GetBytes(plainText);

            byte[] byteHashText = md5.ComputeHash(byteText);
            return byteHashText;
        }
    }
}
