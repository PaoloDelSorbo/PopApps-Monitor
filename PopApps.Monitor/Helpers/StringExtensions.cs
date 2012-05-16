using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO.Compression;
using System.IO;
using System.Text.RegularExpressions;

namespace PopApps.Monitor.Helpers
{
    public static class StringExtensions
    {
        public static string Null2Empty(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            return s;
        }

        public static byte[] Compress(this string value)
        {
            //Transform string into byte[]  
            byte[] byteArray = new byte[value.Length];
            int indexBA = 0;
            foreach (char item in value.ToCharArray())
            {
                byteArray[indexBA++] = (byte)item;
            }

            //Prepare for compress
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream sw = new GZipStream(ms, CompressionMode.Compress))
                {

                    //Compress
                    sw.Write(byteArray, 0, byteArray.Length);
                    //Close, DO NOT FLUSH cause bytes will go missing...
                }
                //Transform byte[] zip data to string
                byteArray = ms.ToArray();
            }
            return byteArray;
        }

        public static string Decompress(byte[] gzBuffer)
        {
            string s = "";
            using (MemoryStream ms = new MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                byte[] buffer = new byte[msgLength];

                ms.Position = 0;
                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }

                s = Encoding.UTF8.GetString(buffer);
            }
            return s;
        }
        public static string GenerateFilenameSlug(this string fileName)
        {
            try
            {
                string fileExtension = Path.GetExtension(fileName);
                fileName = fileName.Replace(fileExtension, "").GenerateSlug();
                return fileName + fileExtension;
            }
            catch (Exception)
            {
                return "foo";
            }
        }
        public static string GenerateSlug(this string value)
        {
            if (value == null) return "";
            var toLower = true;
            var normalised = value.Normalize(NormalizationForm.FormKD);

            const int maxlen = 80;
            int len = normalised.Length;
            bool prevDash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = normalised[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    if (prevDash)
                    {
                        sb.Append('-');
                        prevDash = false;
                    }
                    sb.Append(c);
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    if (prevDash)
                    {
                        sb.Append('-');
                        prevDash = false;
                    }
                    // tricky way to convert to lowercase
                    if (toLower)
                        sb.Append((char)(c | 32));
                    else
                        sb.Append(c);
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' || c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevDash && sb.Length > 0)
                    {
                        prevDash = true;
                    }
                }
                else
                {
                    string swap = ConvertEdgeCases(c, toLower);

                    if (swap != null)
                    {
                        if (prevDash)
                        {
                            sb.Append('-');
                            prevDash = false;
                        }
                        sb.Append(swap);
                    }
                }

                if (i == maxlen) break;
            }
 

            return sb.ToString();
        }

        static string ConvertEdgeCases(char c, bool toLower)
        {
            string swap = null;
            switch (c)
            {
                case 'ı':
                    swap = "i";
                    break;
                case 'ł':
                    swap = "l";
                    break;
                case 'Ł':
                    swap = toLower ? "l" : "L";
                    break;
                case 'đ':
                    swap = "d";
                    break;
                case 'ß':
                    swap = "ss";
                    break;
                case 'ø':
                    swap = "o";
                    break;
                case 'Þ':
                    swap = "th";
                    break;
            }
            return swap;
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public static string TruncateAtWord(this string input, int length)
        {
            return TruncateAtWord(input, length, true);
        }


        public static string Truncate(this string str, int maxLength, bool ellipsis)
        {
            if (str == null) return null;
            return str.Substring(0, Math.Min(maxLength, str.Length)) + (ellipsis && maxLength < str.Length ? "..." : "");
        }
        public static string Truncate(this string str, int maxLength)
        {
            return Truncate(str, maxLength, false);
        }

        public static string TruncateAtWord(this string input, int length, bool ellipsis)
        {
            if (input == null || input.Length < length)
                return input;
            int iNextSpace = input.LastIndexOf(" ", length);
            return string.Format("{0}" + (ellipsis ? "..." : ""), input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
        }
        public static string RemoveHTMLTags(this string source)
        {
            string expn = "<.*?>";
            if (string.IsNullOrEmpty(source))
                return "";
            source = Regex.Replace(source, expn, string.Empty);
            source = HttpUtility.HtmlDecode(source);
            return source;
        }
        public static string TruncateHtml(this string input, int length)
        {
            return TruncateHtml(input, length, true);
        }
        public static string TruncateHtml(this string input, int length, bool ellipsis)
        {
            return input.RemoveHTMLTags().TruncateAtWord(length, ellipsis);
        }

        public static string MD5(this string password)
        {
            var textBytes = Encoding.Default.GetBytes(password);
            var cryptHandler = new MD5CryptoServiceProvider();
            var hash = cryptHandler.ComputeHash(textBytes);
            var ret = "";
            foreach (var a in hash)
            {
                ret += a.ToString("x2");
            }
            return ret;
        }

        public static string ToUpperFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }

    }
}