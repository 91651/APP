using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Security.Cryptography;

namespace APP.Framework.Security
{
    public class Cryptography
    {
        public Cryptography()
        {
        }
        /// <summary> 
        ///  生成MD5摘要 
        /// </summary> 
        public static string Md5(string str)
        {
            byte[] inBytes = Encoding.UTF8.GetBytes(str);
            byte[] hashBytes = MD5.Create().ComputeHash(inBytes);
            //StringBuilder sb = new StringBuilder();
            //foreach (byte b in hashBytes)
            //{
            //    sb.AppendFormat("{0:X2}", b);
            //}
            //var outStr = sb.ToString();
            var outStr = string.Concat(hashBytes.Select(b => b.ToString("X2")));
            return outStr;
        }
        /// <summary> 
        ///  生成HMACMD5摘要 
        /// </summary> 
        public static string HmacMd5(string str, string key = "")
        {
            var inBytes = Encoding.UTF8.GetBytes(str);
            var keyBytes = Encoding.UTF8.GetBytes(key);
            var hmacmd5 = new HMACMD5 { Key = keyBytes };
            var hashBytes = hmacmd5.ComputeHash(inBytes);
            var outStr = string.Concat(hashBytes.Select(b => b.ToString("X2")));
            return outStr;
        }
        /// <summary> 
        ///  AES加密 
        /// </summary> 
        public static string AesEncrypt(string str, string key, string iv, CipherMode mode = CipherMode.CBC)
        {
            byte[] inBytes = Encoding.UTF8.GetBytes(str);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
            Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = ivBytes;
            aes.Mode = mode;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inBytes, 0, inBytes.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        /// <summary> 
        ///  AES解密 
        /// </summary>
        public static string AesDecrypt(string str, string key, string iv, CipherMode mode = CipherMode.CBC)
        {
            byte[] inBytes = Convert.FromBase64String(str); ;
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] ivBytes = Encoding.UTF8.GetBytes(iv);
            Aes aes = Aes.Create();
            aes.Key = keyBytes;
            aes.IV = ivBytes;
            aes.Mode = mode;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inBytes, 0, inBytes.Length);
            cs.FlushFinalBlock();
            return Encoding.UTF8.GetString(ms.ToArray()); ;
        }
    }
}
