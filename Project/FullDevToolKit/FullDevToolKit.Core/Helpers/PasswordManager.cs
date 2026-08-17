using System;
using System.Collections.Generic;
using System.Text;

namespace FullDevToolKit.Helpers
{
    public static class PasswordManager
    {

        public static string EncryptPassword(string textpwd, string key)
        {
            string ret = string.Empty;
                        
            textpwd = MD5.BuildMD5(textpwd);  
            var encryptor = new Encryptor();
            encryptor.Key = key;    
            ret = encryptor.Encrypt(textpwd);

            return ret;

        }

        public static PasswordCode GetChangePasswordCode()
        {
            PasswordCode ret = new PasswordCode();
            string code = Utilities.GenerateCode(6);
            ret.Code = code;
                        
            return ret;
        }

        public static bool ValidatePassword(string savedpassword, 
             string passwordfromuser, string key)
        {
            bool ret = false;

            string aux = EncryptPassword(passwordfromuser, key);

            if (aux == savedpassword)
            {
                ret = true;
            }

            return ret;
        }

        public static bool ValidatePasswordCode(string usercode, string savedcode)
        {
            bool ret = false;

            PasswordCode pcode = new PasswordCode();
            pcode.Code = usercode;

            if (pcode.EncriptedCode == savedcode)
            { 
                ret = true;
            }

            return ret;
        }

        public static string InvertText(string text)
        {
            string ret = string.Empty;
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            ret = new string(array);
            return ret;
        }         

    }

    public class PasswordCode
    {
        public string Code { get; set; }
        public string EncriptedCode
        {
            get
            {
                string ret = string.Empty;                
                var encryptor = new Encryptor();
                encryptor.Key = PasswordManager.InvertText(Code);
                ret = encryptor.Encrypt(Code);

                return ret;
            }        
        }
    }
}

 