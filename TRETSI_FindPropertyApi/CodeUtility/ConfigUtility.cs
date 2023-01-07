using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;


namespace EventOrganizerAPI.CodeUtility
{
    internal static class ConfigUtility {
        public const string SYSTEM_USER_ID = "system";

        //internal static string ConnString {
        //    get {
        //        return Program.Configuration.GetConnectionString("DefaultConnection");
        //    }
        //}

        public enum AppEnvironment
        {
            Production, Development, QA, Staging, UAT
        }
        internal static string CurrentUserId {
            get {
                try {
                    return Thread.CurrentPrincipal.Identity.Name.ToLowerInvariant();
                }
                catch {
                    return ConfigUtility.SYSTEM_USER_ID;
                }
            }
        }

        internal static string Env {
            get {
                switch (Environment) {
                    case "DEV":
                        return "local";

                    case "TEST":
                        return "staging";

                    case "PROD":
                        return "prod";

                    default:
                        return string.Empty;
                }
            }
        }

        internal static string Environment {
            get {
                string returnValue = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (!string.IsNullOrWhiteSpace(returnValue)) {
                    return "DEV";
                }
                return returnValue;
            }
        }

        public  static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        public static string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }


        internal static bool IsDeveloper {
            get {
                return string.Compare(ConfigUtility.CurrentUserId, "valluri", true, CultureInfo.InvariantCulture) == 0;
            }
        }

        internal static bool IsDevelopment {
            get {
                return string.Equals(ConfigUtility.Environment, "DEV", StringComparison.OrdinalIgnoreCase);
            }
        }

        internal static bool IsProduction {
            get {
                return string.Equals(ConfigUtility.Environment, "PROD", StringComparison.OrdinalIgnoreCase);
            }
        }

        internal static bool IsTest {
            get {
                return string.Equals(ConfigUtility.Environment, "TEST", StringComparison.OrdinalIgnoreCase);
            }
        }

        internal static bool IsUat {
            get {
                return string.Equals(ConfigUtility.Environment, "UAT", StringComparison.OrdinalIgnoreCase);
            }
        }
        internal static bool NetworkAvailable {
            get {
                try {
                    using (Ping ping = new Ping()) {
                        return ping.Send("208.67.222.220", 2000).Status == IPStatus.Success;
                    }
                }
                catch { }
                return false;
            }
        }
        internal static string AddWildCard(string input) {
            if (string.IsNullOrWhiteSpace(input)) {
                input = string.Empty;
            }
            const string WC = "%{0}%";

            return string.Format(CultureInfo.InvariantCulture, WC, input);
        }

        internal static string Base64Encode(string plainText) {
            byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        // Convert a byte array to an Object


        internal static string CleanUploadData(string requestData) {
            requestData = requestData.Replace("data:application/vnd.ms-excel;base64,", string.Empty, StringComparison.OrdinalIgnoreCase);
            requestData = requestData.Replace("data:application/octet-stream;base64,", string.Empty, StringComparison.OrdinalIgnoreCase);
            requestData = requestData.Replace(@"^data:image\/\w+;base64,/,", string.Empty, StringComparison.OrdinalIgnoreCase);
            return requestData;
        }

        internal static string FormatDate(string s, string format) {
            if (s == null) {
                return string.Empty;
            }

            try {
                string[] formats = new string[] {
          "MM/dd/yyyy HH:mm:ss",
          "dd-MM-yyyy HH:mm:ss",
          "yyyy-dd-MM HH:mm:ss",
          "yyyy-MM-ddTHH:mm:ssK",
          "yyyy-MM-dd"
        };
                return DateTime.ParseExact(s, formats, CultureInfo.InvariantCulture, DateTimeStyles.None).ToString(format, CultureInfo.InvariantCulture);
            }
            catch {
                return s;
            }
        }

        internal static string GenerateMacSha256(string salt, string data) {
            using (HMACSHA256 provider = new HMACSHA256(Encoding.ASCII.GetBytes(salt))) {
                byte[] hashArray = provider.ComputeHash(Encoding.ASCII.GetBytes(data));
                //return string.Concat(hashArray.Select(b => b.ToString("x2")));
                return BitConverter.ToString(hashArray).Replace("-", "").ToLower();
            }
        }

        internal static string Get10DigitPhoneNumber(string phoneNumber) {
            if (string.IsNullOrWhiteSpace(phoneNumber)) {
                return string.Empty;
            }
            if (phoneNumber.Length <= 10) {
                return phoneNumber;
            }
            return phoneNumber.Substring(phoneNumber.Length - 10);
        }

        internal static string Get12DigitPhoneNumber(string phoneNumber) {
            if (string.IsNullOrWhiteSpace(phoneNumber)) {
                return string.Empty;
            }
            return (phoneNumber.Length == 10) ? "91" + phoneNumber : phoneNumber;
        }

     
        internal static string GetBlankStringForNull(string s) {
            return string.IsNullOrWhiteSpace(s) ? string.Empty : s;
        }

        internal static decimal GetDecimalValueFromQueryString(NameValueCollection qs, string paramName) {
            decimal returnValue = 0;

            if (qs[paramName] != null) {
                try {
                    returnValue = XmlConvert.ToDecimal(qs[paramName]);
                }
                catch {
                }
            }
            return returnValue;
        }

     
        internal static string GetFormattedLabelValue(string label, string value) {
            const string STRING_FORMAT = "<br/><b>{0}:</b> {1}";
            return string.Format(STRING_FORMAT, label, value);
        }

        internal static string GetFormattedLabelValue(string label, decimal value) {
            return ConfigUtility.GetFormattedLabelValue(label, value.ToString("C", CultureInfo.CurrentCulture));
        }

        internal static string GetFormattedLabelValue(string label, double value) {
            return ConfigUtility.GetFormattedLabelValue(label, value.ToString("C", CultureInfo.CurrentCulture));
        }

        internal static string GetFormattedLabelValue(string label, int value) {
            return ConfigUtility.GetFormattedLabelValue(label, value.ToString(CultureInfo.CurrentCulture));
        }

       
        internal static string GetMd5Hash(string input) {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            using (MD5 md5Hash = MD5.Create()) {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++) {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        internal static string GetUniqueKey(int maxSize, bool useSpecialCharacters, bool onlyNumbers) {
            string characterSet = "abcdefghijklmnopqrstuvwxyz1234567890";
            if (useSpecialCharacters) {
                characterSet += "-[]@#!()";
            }
            if (onlyNumbers) {
                characterSet = "1234567890";
            }

            char[] chars = characterSet.ToCharArray();

            List<byte> byteList = new List<byte>();
            byteList.AddRange(Guid.NewGuid().ToByteArray());
            byteList.AddRange(Guid.NewGuid().ToByteArray());
            byteList.AddRange(Guid.NewGuid().ToByteArray());

            StringBuilder result = new StringBuilder(maxSize);
            for (int i = 0; i < maxSize; i++) {
                byte b = byteList[i];
                result.Append(chars[b % (chars.Length - 1)]);
            }
            return result.ToString();
        }

        

        internal static bool IsValidEmail(string emailAddress) {
            try {
                MailAddress m = new MailAddress(emailAddress);

                return true;
            }
            catch (FormatException) {
                return false;
            }
        }

        internal static string IsValidMobile(string mobileNumber) {
            if (mobileNumber.Length != 10) {
                return "Mobile Number should be 10 digits";
            }
            if (int.TryParse(mobileNumber.Substring(0, 1), out int firstDigit)) {
                if (firstDigit < 6) {
                    return "Mobile Number should start with 6,7,8 or 9";
                }
            }
            else {
                return "Invalid Mobile Number";
            }

            return string.Empty;
        }

        internal static bool MacValidateSha1(string salt, string dataForVerification, string mac) {
            using (HMACSHA1 provider = new HMACSHA1(Encoding.ASCII.GetBytes(salt))) {
                byte[] hashArray = provider.ComputeHash(Encoding.ASCII.GetBytes(dataForVerification));
                string calculatedMac = string.Concat(hashArray.Select(b => b.ToString("x2")));

                return calculatedMac == mac;
            }
        }

        // Convert an object to a byte array
        internal static byte[] ObjectToByteArray(object obj) {
            if (obj == null) {
                return null;
            }

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream()) {
                bf.Serialize(ms, obj);

                return ms.ToArray();
            }
        }

        internal static T ParseEnum<T>(string value) {
            return (T) Enum.Parse(typeof(T), value, true);
        }

        internal static T ParseEnum<T>(int value) {
            return (T) Enum.Parse(typeof(T), value.ToString(CultureInfo.InvariantCulture), true);
        }

        internal static T ParseEnum<T>(double value) {
            return (T) Enum.Parse(typeof(T), value.ToString(CultureInfo.InvariantCulture), true);
        }

        internal static bool IsValidEmailId(string emailAddress) {
            try {
                Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                bool isValid = regex.IsMatch(emailAddress.Trim());
                return isValid;
            }
            catch (FormatException) {
                return false;
            }
        }

        internal static bool IsValidData(string data, string type) {
            try {
                Regex regex = null;
                if (type != "PAN")
                    regex = new Regex(@"^\d+$");
                else
                    regex = new Regex(@"[A-Z][A-Z][A-Z][P][A-Z][0-9][0-9][0-9][0-9][A-Z]");
                bool isValid = regex.IsMatch(data.Trim());
                return isValid;
            }
            catch (FormatException) {
                return false;
            }
        }
    }
}
