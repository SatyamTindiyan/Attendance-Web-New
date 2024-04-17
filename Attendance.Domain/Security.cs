using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Domain
{
    public class Security
    {
        private const string EncryptionKey = "LQH&#454@it2021$#";
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public static string encrypt(string encryptString)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public static string Decrypt(string cipherText)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string GetEncryptData(string textData)
        {
            string returnResult = "0";
            try
            {
                RijndaelManaged objrij = new RijndaelManaged();
                //set the mode for operation of the algorithm
                objrij.Mode = CipherMode.CBC;
                //set the padding mode used in the algorithm.
                objrij.Padding = PaddingMode.PKCS7;
                //set the size, in bits, for the secret key.
                objrij.KeySize = 0x80;
                //set the block size in bits for the cryptographic operation.
                objrij.BlockSize = 0x80;
                //set the symmetric key that is used for encryption & decryption.
                byte[] passBytes = Encoding.UTF8.GetBytes(EncryptionKey);
                //set the initialization vector (IV) for the symmetric algorithm
                byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                int len = passBytes.Length;
                if (len > EncryptionkeyBytes.Length)
                {
                    len = EncryptionkeyBytes.Length;
                }
                Array.Copy(passBytes, EncryptionkeyBytes, len);
                objrij.Key = EncryptionkeyBytes;
                objrij.IV = EncryptionkeyBytes;
                //Creates symmetric AES object with the current key and initialization vector IV.
                ICryptoTransform objtransform = objrij.CreateEncryptor();
                byte[] textDataByte = Encoding.UTF8.GetBytes(textData);
                //Final transform the test string.
                returnResult = Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
            }
            catch (Exception ex)
            {
                //WriteErrorLog(ex, errorMethodRoute, "GetEncryptData");
            }

            return returnResult;
        }

        public static string GetDecryptData(string EncryptedText)
        {
            string returnResult = "0";
            try
            {
                RijndaelManaged objrij = new RijndaelManaged();
                objrij.Mode = CipherMode.CBC;
                objrij.Padding = PaddingMode.PKCS7;
                objrij.KeySize = 0x80;
                objrij.BlockSize = 0x80;
                byte[] encryptedTextByte = Convert.FromBase64String(EncryptedText);
                byte[] passBytes = Encoding.UTF8.GetBytes(EncryptionKey);
                byte[] EncryptionkeyBytes = new byte[0x10];
                int len = passBytes.Length;
                if (len > EncryptionkeyBytes.Length)
                {
                    len = EncryptionkeyBytes.Length;
                }
                Array.Copy(passBytes, EncryptionkeyBytes, len);
                objrij.Key = EncryptionkeyBytes;
                objrij.IV = EncryptionkeyBytes;
                byte[] TextByte = objrij.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
                returnResult = Encoding.UTF8.GetString(TextByte);  //it will return readable string
            }
            catch (Exception ex)
            {
                //LogHandler log = new LogHandler();
                //log.WriteErrorLog(ex, errorMethodRoute, "GetDecryptData");
            }

            return returnResult;
        }

        public static SqlString GetRandomString(int length)
        {
            byte[] array = new byte[length];
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            rNGCryptoServiceProvider.GetBytes(array);
            return new SqlString(Convert.ToBase64String(array));
        }
        public static string GenerateRandomCryptographicKey(int keyLength)
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[keyLength];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public static string GetEncryptString(String plainText)
        {
            string encrypted = string.Empty;
            try
            {
                if (plainText.Trim() != String.Empty)
                {
                    byte[] inputBytes = ASCIIEncoding.ASCII.GetBytes(plainText);
                    byte[] pwdhash = null;
                    MD5CryptoServiceProvider hashmd5;

                    //generate an MD5 hash from the password.
                    //a hash is a one way encryption meaning once you generate
                    //the hash, you cant derive the password back from it.
                    hashmd5 = new MD5CryptoServiceProvider();
                    pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(EncryptionKey));
                    hashmd5 = null;

                    // Create a new TripleDES service provider 
                    TripleDESCryptoServiceProvider tdesProvider = new TripleDESCryptoServiceProvider();
                    tdesProvider.Key = pwdhash;
                    tdesProvider.Mode = CipherMode.ECB;

                    encrypted = Convert.ToBase64String(
                        tdesProvider.CreateEncryptor().TransformFinalBlock(inputBytes, 0, inputBytes.Length));
                }
            }
            catch (Exception e)
            {
                string str = e.Message;
                throw;
            }
            return encrypted;
        }

        public static String GetDecryptString(string encryptedString)
        {
            string decyprted = string.Empty;
            byte[] inputBytes = null;

            try
            {
                if (encryptedString.Trim() != string.Empty)
                {
                    string encryptedString_new = encryptedString.Replace(" ", "+");
                    int mod4 = encryptedString_new.Length % 4;
                    if (mod4 > 0)
                    {
                        encryptedString_new += new string('=', 4 - mod4);
                    }

                    inputBytes = Convert.FromBase64String(encryptedString_new);
                    //inputBytes = Convert.FromBase64String(encryptedString);
                    byte[] pwdhash = null;
                    MD5CryptoServiceProvider hashmd5;
                    //generate an MD5 hash from the password. 
                    //a hash is a one way encryption meaning once you generate
                    //the hash, you cant derive the password back from it.
                    hashmd5 = new MD5CryptoServiceProvider();
                    pwdhash = hashmd5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(EncryptionKey));
                    hashmd5 = null;

                    // Create a new TripleDES service provider 
                    TripleDESCryptoServiceProvider tdesProvider = new TripleDESCryptoServiceProvider();
                    tdesProvider.Key = pwdhash;
                    tdesProvider.Mode = CipherMode.ECB;

                    decyprted = ASCIIEncoding.ASCII.GetString(
                        tdesProvider.CreateDecryptor().TransformFinalBlock(inputBytes, 0, inputBytes.Length));
                }
            }
            catch (Exception e)
            {
                string str = e.Message;
                throw;
            }
            return decyprted;
        }

        public static string RandomString(int Length)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[Length];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < Length; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public static string GetOneWayEncryptMD5(string sPassword)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(sPassword);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToUpper());
            }
            return s.ToString();
        }

        public static string GetOneWayEncryptSHA256(string pasword)
        {
            byte[] arrbyte = new byte[pasword.Length];
            SHA256 hash = new SHA256CryptoServiceProvider();
            arrbyte = hash.ComputeHash(Encoding.UTF8.GetBytes(pasword));
            return Convert.ToBase64String(arrbyte);
        }

    }
}
