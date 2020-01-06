using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FLEX.API.Common
{

    public class FlexQRBase
    {
        private const string TAGS_BEGIN = "BEGIN:FLEXQR";
        private const string TAGS_END = "END:FLEXQR";
        private const string TAGS_PROPERTYFORMAT = "{0}:{1}";
        private const char TAGS_NEWLINE = '\n';
        private const string TAG_ENCRYPTEDDATA = "XD";
        private const string TAG_IV = "XV";

        private static byte[] KEY = new byte[] { 152, 47, 174, 31, 201, 141, 136, 120, 179, 141, 34, 211, 107, 137, 203, 58, 103, 201, 80, 193, 239, 190, 133, 39, 45, 149, 9, 199, 153, 220, 32, 74, };

        public bool IsEncryptData { get; set; } = false;

        private Dictionary<string, string> _properties = new Dictionary<string, string>();
        protected string this[string key]
        {
            get
            {
                if (_properties.ContainsKey(key))
                    return _properties[key];
                else
                    return null;
            }
            set
            {
                if (_properties.ContainsKey(key))
                    _properties[key] = value;
                else
                    _properties.Add(key, value);
            }
        }

        public void Clear()
        {
            _properties.Clear();
        }

        public string ToQRCode()
        {
            var sb = new StringBuilder();
            sb.Append(TAGS_BEGIN);
            sb.Append(TAGS_NEWLINE);

            if (this.IsEncryptData)
            {
                var sbData = new StringBuilder();
                foreach (var kvp in _properties)
                {
                    sbData.AppendFormat(TAGS_PROPERTYFORMAT, kvp.Key, kvp.Value);
                    sbData.Append(TAGS_NEWLINE);
                }
                var data = sbData.ToString();
                var encrypted = FlexQRBase.EncryptData(data);
                sb.Append(encrypted);
                sb.Append(TAGS_NEWLINE);
            }
            else
            {
                foreach (var kvp in _properties)
                {
                    sb.AppendFormat(TAGS_PROPERTYFORMAT, kvp.Key, kvp.Value);
                    sb.Append(TAGS_NEWLINE);
                }
            }

            sb.Append(TAGS_END);
            return sb.ToString();
        }

        protected static bool TryParse(string qrcode, FlexQRBase target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (string.IsNullOrEmpty(qrcode) || !qrcode.StartsWith(TAGS_BEGIN))
                return false;

            target.Clear();
            var lines = qrcode.Split(TAGS_NEWLINE);

            var encryptedline = lines.FirstOrDefault(l => l.StartsWith(TAG_ENCRYPTEDDATA));
            var ivline = lines.FirstOrDefault(l => l.StartsWith(TAG_IV));
            if (!string.IsNullOrEmpty(encryptedline) && !string.IsNullOrEmpty(ivline))
            {
                var args1 = encryptedline.Split(new char[] { ':' }, 2);
                var args2 = ivline.Split(new char[] { ':' }, 2);
                if (args1.Length == 2 && args2.Length == 2)
                {
                    var data = FlexQRBase.DecryptData(args1[1], args2[1]);
                    lines = data.Split(TAGS_NEWLINE);
                    target.IsEncryptData = true;
                }
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                    continue;
                if (lines[i].StartsWith(TAGS_BEGIN))
                    continue;
                if (lines[i].StartsWith(TAGS_END))
                    break;
                var args = lines[i].Split(new char[] { ':' }, 2);
                if (args.Length == 1)
                    target[args[0]] = null;
                if (args.Length == 2)
                    target[args[0]] = args[1];
            }

            return true;
        }

        protected string GetValue(string key)
        {
            return this[key];
        }

        protected int? GetIntValue(string key)
        {
            int result;
            if (int.TryParse(this[key], out result))
                return result;
            else
                return null;
        }

        protected decimal? GetDecimalValue(string key)
        {
            int result;
            if (int.TryParse(this[key], out result))
                return result;
            else
                return null;
        }

        protected void SetValue<T>(string key, T value)
        {
            if (value == null)
                this[key] = null;
            else
                this[key] = value.ToString();
        }


        private static string EncryptData(string data)
        {
            try
            {
                using (RijndaelManaged myRijndael = new RijndaelManaged())
                {
                    myRijndael.GenerateIV();

                    byte[] encrypted = EncryptStringToBytes(data, FlexQRBase.KEY, myRijndael.IV);
                    string encryptedtxt = System.Convert.ToBase64String(encrypted);
                    string encryptediv = System.Convert.ToBase64String(myRijndael.IV);

                    var sb = new StringBuilder();
                    sb.AppendFormat(TAGS_PROPERTYFORMAT, TAG_ENCRYPTEDDATA, encryptedtxt);
                    sb.Append(TAGS_NEWLINE);
                    sb.AppendFormat(TAGS_PROPERTYFORMAT, TAG_IV, encryptediv);
                    return sb.ToString();
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("Unable to encrypt data.", e);
            }
        }

        private static string DecryptData(string data, string iv)
        {
            try
            {
                byte[] encrypted = System.Convert.FromBase64String(data);
                byte[] ivbytes = System.Convert.FromBase64String(iv);
                string decrypted = DecryptStringFromBytes(encrypted, FlexQRBase.KEY, ivbytes);
                return decrypted;
            }
            catch (Exception e)
            {
                throw new ApplicationException("Unable to decrypt data.", e);
            }
        }

        static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using (RijndaelManaged rijAlg = new RijndaelManaged())
            {
                rijAlg.Key = Key;
                rijAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }
    }

    public class FlexQR : FlexQRBase
    {
        public int? ITEM_ID
        {
            get { return base.GetIntValue(nameof(ITEM_ID)); }
            set { base.SetValue(nameof(ITEM_ID), value); }
        }
        public string RETURN_NO
        {
            get { return base.GetValue(nameof(RETURN_NO)); }
            set { base.SetValue(nameof(RETURN_NO), value); }
        }
        public string LOT_NO
        {
            get { return base.GetValue(nameof(LOT_NO)); }
            set { base.SetValue(nameof(LOT_NO), value); }
        }
        public decimal? QTY
        {
            get { return base.GetDecimalValue(nameof(QTY)); }
            set { base.SetValue(nameof(QTY), value); }
        }
        public string UNITCODE
        {
            get { return base.GetValue(nameof(UNITCODE)); }
            set { base.SetValue(nameof(UNITCODE), value); }
        }
        public string JOB_NUMBER
        {
            get { return base.GetValue(nameof(JOB_NUMBER)); }
            set { base.SetValue(nameof(JOB_NUMBER), value); }
        }
        public int? BATCH_ID
        {
            get { return base.GetIntValue(nameof(BATCH_ID)); }
            set { base.SetValue(nameof(BATCH_ID), value); }
        }
        public string WITHDRAWAL_TYPE
        {
            get { return base.GetValue(nameof(WITHDRAWAL_TYPE)); }
            set { base.SetValue(nameof(WITHDRAWAL_TYPE), value); }
        }
        public string DO_NUMBER
        {
            get { return base.GetValue(nameof(DO_NUMBER)); }
            set { base.SetValue(nameof(DO_NUMBER), value); }
        }
        public string ISSUE_NO
        {
            get { return base.GetValue(nameof(ISSUE_NO)); }
            set { base.SetValue(nameof(ISSUE_NO), value); }
        }
        public string ITEM_CD
        {
            get { return base.GetValue(nameof(ITEM_CD)); }
            set { base.SetValue(nameof(ITEM_CD), value); }
        }

        public string PALLET_TAG_ID
        {
            get { return base.GetValue(nameof(PALLET_TAG_ID)); }
            set { base.SetValue(nameof(PALLET_TAG_ID), value); }
        }

        public string CHECK_REP_NO
        {
            get { return base.GetValue(nameof(CHECK_REP_NO)); }
            set { base.SetValue(nameof(CHECK_REP_NO), value); }

        }
        public string LOC_CD
        {
            get { return base.GetValue(nameof(LOC_CD)); }
            set { base.SetValue(nameof(LOC_CD), value); }
        }

        public string SHELF_NAME
        {
            get { return base.GetValue(nameof(SHELF_NAME)); }
            set { base.SetValue(nameof(SHELF_NAME), value); }
        }

        public int? SHELF_SETUPID
        {
            get { return base.GetIntValue(nameof(SHELF_SETUPID)); }
            set { base.SetValue(nameof(SHELF_SETUPID), value); }
        }

        public static FlexQR Parse(string qrcode)
        {
            var qr = new FlexQR();
            if (FlexQRBase.TryParse(qrcode, qr))
                return qr;
            else
                return null;
        }


        public string MACHINE_NO
        {
            get { return base.GetValue(nameof(MACHINE_NO)); }
            set { base.SetValue(nameof(MACHINE_NO), value); }
        }
    }
}
