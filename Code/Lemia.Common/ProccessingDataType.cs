
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lemia.Common
{
    public class ProccessingDataType
    {
        #region Variables
        public static RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline;
        private const string uniChars =
           "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
        private const string KoDauChars =
            "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";
        #endregion

        #region Common method
        public static int UnicodeToUTF8(byte[] dest, int maxDestBytes, string source, int sourceChars)
        {
            int i, count;
            int c, result;

            result = 0;
            if ((source != null && source.Length == 0))
                return result;
            count = 0;
            i = 0;
            if (dest != null)
            {
                while ((i < sourceChars) && (count < maxDestBytes))
                {
                    c = (int)source[i++];
                    if (c <= 0x7F)
                        dest[count++] = (byte)c;
                    else if (c > 0x7FF)
                    {
                        if ((count + 3) > maxDestBytes)
                            break;
                        dest[count++] = (byte)(0xE0 | (c >> 12));
                        dest[count++] = (byte)(0x80 | ((c >> 6) & 0x3F));
                        dest[count++] = (byte)(0x80 | (c & 0x3F));
                    }
                    else
                    {
                        //  0x7F < source[i] <= 0x7FF
                        if ((count + 2) > maxDestBytes)
                            break;
                        dest[count++] = (byte)(0xC0 | (c >> 6));
                        dest[count++] = (byte)(0x80 | (c & 0x3F));
                    }
                }
                if (count >= maxDestBytes)
                    count = maxDestBytes - 1;
                dest[count] = (byte)(0);
            }
            else
            {
                while (i < sourceChars)
                {
                    c = (int)(source[i++]);
                    if (c > 0x7F)
                    {
                        if (c > 0x7FF)
                            count++;
                        count++;
                    }
                    count++;
                }
            }
            result = count + 1;
            return result;
        }
        public static int UTF8ToUnicode(char[] dest, int maxDestChars, byte[] source, int sourceBytes)
        {
            int i, count;
            int c, result;
            int wc;

            if (source == null)
            {
                result = 0;
                return result;
            }
            result = (int)(-1);
            count = 0;
            i = 0;
            if (dest != null)
            {
                while ((i < sourceBytes) && (count < maxDestChars))
                {
                    wc = (int)(source[i++]);
                    if ((wc & 0x80) != 0)
                    {
                        if (i >= sourceBytes)
                            return result;
                        wc = wc & 0x3F;
                        if ((wc & 0x20) != 0)
                        {
                            c = (byte)(source[i++]);
                            if ((c & 0xC0) != 0x80)
                                return result;
                            if (i >= sourceBytes)
                                return result;
                            wc = (wc << 6) | (c & 0x3F);
                        }
                        c = (byte)(source[i++]);
                        if ((c & 0xC0) != 0x80)
                            return result;
                        dest[count] = (char)((wc << 6) | (c & 0x3F));
                    }
                    else
                        dest[count] = (char)wc;
                    count++;
                }
                if (count > maxDestChars)
                    count = maxDestChars - 1;
                dest[count] = (char)(0);
            }
            else
            {
                while (i < sourceBytes)
                {
                    c = (byte)(source[i++]);
                    if ((c & 0x80) != 0)
                    {
                        if (i >= sourceBytes)
                            return result;
                        c = c & 0x3F;
                        if ((c & 0x20) != 0)
                        {
                            c = (byte)(source[i++]);
                            if ((c & 0xC0) != 0x80)
                                return result;
                            if (i >= sourceBytes)
                                return result;
                        }
                        c = (byte)(source[i++]);
                        if ((c & 0xC0) != 0x80)
                            return result;
                    }
                    count++;
                }
            }
            result = count + 1;
            return result;
        }
        public static byte[] UTF8Encode(string ws)
        {
            int l;
            byte[] temp, result;

            result = null;
            if ((ws != null && ws.Length == 0))
                return result;
            temp = new byte[ws.Length * 3];
            l = UnicodeToUTF8(temp, temp.Length + 1, ws, ws.Length);
            if (l > 0)
            {
                result = new byte[l - 1];
                Array.Copy(temp, 0, result, 0, l - 1);
            }
            else
            {
                result = new byte[ws.Length];
                for (int i = 0; i < result.Length; i++)
                    result[i] = (byte)(ws[i]);
            }
            return result;
        }
        public static string UTF8Decode(byte[] s)
        {
            int l;
            char[] temp;
            string result;

            result = String.Empty;
            if (s == null)
                return result;
            temp = new char[s.Length + 1];
            l = UTF8ToUnicode(temp, temp.Length, s, s.Length);
            if (l > 0)
            {
                result = "";
                for (int i = 0; i < l - 1; i++)
                    result += temp[i];
            }
            else
            {
                result = "";
                for (int i = 0; i < s.Length; i++)
                    result += (char)(s[i]);
            }
            return result;
        }
        public static string RemoveSpecialCharacter(string orig)
        {
            string rv;

            // replacing with space allows the camelcase to work a little better in most cases.
            rv = orig.Replace("\\", " ");
            rv = rv.Replace("(", " ");
            rv = rv.Replace(")", " ");
            rv = rv.Replace("/", " ");
            //rv = rv.Replace("-", " ");
            rv = rv.Replace(",", " ");
            rv = rv.Replace(">", " ");
            rv = rv.Replace("<", " ");
            rv = rv.Replace("&", " ");
            rv = rv.Replace("!", " ");
            rv = rv.Replace("@", " ");
            rv = rv.Replace("#", " ");
            rv = rv.Replace("$", " ");
            rv = rv.Replace("%", " ");
            rv = rv.Replace("^", " ");
            rv = rv.Replace("*", " ");
            rv = rv.Replace("+", "__");
            rv = rv.Replace("|", " ");
            rv = rv.Replace("[", " ");
            rv = rv.Replace("]", " ");
            rv = rv.Replace("{", " ");
            rv = rv.Replace("}", " ");
            rv = rv.Replace(":", " ");
            rv = rv.Replace(";", " ");
            rv = rv.Replace("?", " ");
            rv = rv.Replace("~", " ");
            rv = rv.Replace(",", " ");
            //rv = rv.Replace(".", " ");
            rv = rv.Replace("\"", "");
            // single quotes shouldn't result in CamelCase variables like Patient's -> PatientS
            // "smart" forward quote
            rv = rv.Replace("'", "");

            // make sure to get rid of double spaces.
            rv = rv.Replace("   ", " ");
            rv = rv.Replace("  ", " ");

            rv = rv.Trim(' '); // Remove leading and trailing spaces.

            return (rv);
        }
        public static string UnicodeToKoDau(string s)
        {
            string retVal = String.Empty;
            if (s == null)
                return retVal;
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += KoDauChars[pos];
                else
                    retVal += s[i];
            }
            return retVal;
        }
        public static string KoDauChiLayChuCaiVaSo(string s)
        {
            if (s.Length < 1)
            {
                return s;
            }
            else
            {
                s = ProccessingDataType.UnicodeToKoDau(s);
                s = ProccessingDataType.ChiLayChuCaiVaSo(s);
                s = s.Replace(" ", "");
                return s;
            }
        }
        public static string ChiLayChuCaiVaSo(string s)
        {
            if (s.Length < 1)
            {
                return s;
            }
            else
            {
                s = s.Replace("~", "");
                s = s.Replace("`", "");
                s = s.Replace("!", "");
                s = s.Replace("@", "");
                s = s.Replace("#", "");
                s = s.Replace("$", "");

                s = s.Replace("%", "");
                s = s.Replace("^", "");
                s = s.Replace("&", "");
                s = s.Replace("*", "");
                s = s.Replace("(", "");

                s = s.Replace(")", "");
                s = s.Replace("_", "");
                //  s = s.Replace("-", "");
                s = s.Replace("+", "");
                s = s.Replace("=", "");

                s = s.Replace("|", "");
                s = s.Replace("\\", "");
                s = s.Replace("{", "");
                s = s.Replace("[", "");
                s = s.Replace("}", "");

                s = s.Replace("]", "");
                s = s.Replace("\"", "");
                s = s.Replace("'", "");
                s = s.Replace(":", "");
                s = s.Replace(";", "");

                s = s.Replace("?", "");
                s = s.Replace("/", "");
                s = s.Replace(">", "");
                s = s.Replace("<", "");
                s = s.Replace(",", "");

                s = s.Replace(".", "");
                return s;
            }
        }
        public static string UnicodeToWindows1252(string s)
        {
            string retVal = String.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                int ord = (int)s[i];
                if (ord > 191)
                    retVal += "&#" + ord.ToString() + ";";
                else
                    retVal += s[i];
            }
            return retVal;
        }
        public static string UnicodeToISO8859(string src)
        {
            Encoding iso = Encoding.GetEncoding("iso8859-1");
            Encoding unicode = Encoding.UTF8;
            byte[] unicodeBytes = unicode.GetBytes(src);
            return iso.GetString(unicodeBytes);
        }
        public static string ISO8859ToUnicode(string src)
        {
            Encoding iso = Encoding.GetEncoding("iso8859-1");
            Encoding unicode = Encoding.UTF8;
            byte[] isoBytes = iso.GetBytes(src);
            return unicode.GetString(isoBytes);
        }
        public static string SqlInjection(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                input = input.Replace("'", "")
                         .Replace(";", "")
                         .Replace("--", "")
                         .Replace("/*", "")
                         .Replace("*/", "")
                         .Replace("xp_", "")
                         .Replace("[", "")
                         .Replace("]", "")
                         .Replace("%", "")
                         .Replace(".", "")
                         .Replace("_", "");
            }

            return input;
        }
        public static string layNKyTuDauTien(string s, int n)
        {
            if (n > s.Length)
            {
                return s;
            }
            else
            {
                try
                {
                    s = s.Substring(0, n - 1);
                    int vitriCach = s.LastIndexOf(" ");
                    s = s.Substring(0, vitriCach);
                    s = s.Trim();
                }
                catch { }
                return s;
            }
        }
        public static string layNKyTuDauTien_(string s, int n)
        {
            if (n > s.Length)
            {
                return s;
            }
            else
            {
                try
                {
                    s = s.Substring(0, n - 1);
                    int vitriCach = s.LastIndexOf("-");
                    s = s.Substring(0, vitriCach);
                    s = s.Trim();
                }
                catch { }
                return s;
            }
        }
        public static bool IsNumber(string text)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(text);
        }
        public static string XoaCachTrang(string s, int SoCachTrangConLai)
        {
            string sCachTrang = "";
            if (SoCachTrangConLai > 0)
            {

                for (int i = 1; i <= SoCachTrangConLai; i++)
                {
                    sCachTrang += " ";
                }
            }
            for (int i = s.Length; i > SoCachTrangConLai; i--)
            {
                //ProccessingDataType.LaySoCachTrang(i);
                s = s.Replace(ProccessingDataType.LaySoCachTrang(i), sCachTrang);
            }
            return s;
        }
        public static string LaySoCachTrang(int n)
        {
            string s = "";
            if (n > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    s += " ";
                }
            }
            return s;
        }
        public static int ToInt32_hoadv(object obj)
        {
            int retVal = 0;

            try
            {
                retVal = Convert.ToInt32(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }
        public static long ToLong_hoadv(string s)
        {
            long retVal = 0;

            try
            {
                long.TryParse(s, out retVal);
            }
            catch
            {

            }

            return retVal;
        }
        public static Decimal ToDecimal_hoadv(object obj)
        {
            Decimal retVal = 0;

            try
            {
                retVal = Convert.ToDecimal(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }
        public static double ToDouble_hoadv(object obj)
        {
            double retVal = 0;

            try
            {
                retVal = Convert.ToDouble(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }
        public static bool ToBool_hoadv(object obj)
        {
            bool retVal = false;

            try
            {
                retVal = Convert.ToBoolean(obj);
            }
            catch
            {
                //  retVal = false;
            }

            return retVal;
        }
        public static Guid ToGuid_hoadv(object obj)
        {
            Guid retVal = new Guid();

            try
            {
                retVal = (Guid)obj;
            }
            catch
            {
                //  retVal = false;

            }

            return retVal;
        }
        public static DateTime ToDateTime_hoadv(object obj)
        {
            DateTime retVal = DateTime.Now;

            try
            {
                retVal = Convert.ToDateTime(obj);
            }
            catch
            {
                //  retVal = false;
            }

            return retVal;
        }
        public static string ToString_hoadv(object obj)
        {
            string retVal;

            try
            {
                retVal = Convert.ToString(obj);
            }
            catch
            {
                retVal = String.Empty;
            }

            return retVal;
        }
        public static string Them_0_TruocChuoi(string s, int n)
        {
            if (s.Length == n)
            {
                return s;
            }
            else
            {
                string s0 = s;
                try
                {
                    for (int i = 0; i < n; i++)
                    {
                        s0 = "0" + s0;
                    }

                    int lenS0 = s0.Length;
                    s0 = s0.Substring(lenS0 - n, n);
                }
                catch { }
                return s0;
            }
        }
        public static string delSpecialSymbol(string s)
        {
            if (s.Length == 0)
            {
                return s;
            }
            else
            {
                s = s.Replace(":", "");
                s = s.Replace(",", "");
                s = s.Replace(".", "");

                s = s.Replace("/", "");
                s = s.Replace("?", "");
                s = s.Replace(">", "");
                s = s.Replace("<", "");
                s = s.Replace("(", "");
                s = s.Replace(")", "");
                s = s.Replace(";", "");
                s = s.Replace("\"", "");
                s = s.Replace("'", "");

                s = s.Replace("]", "");
                s = s.Replace("[", "");
                s = s.Replace("}", "");
                s = s.Replace("{", "");
                s = s.Replace("|", "");
                s = s.Replace("\\", "");
                s = s.Replace("=", "");
                s = s.Replace("+", "");

                s = s.Replace("_", "");
                s = s.Replace("*", "");
                s = s.Replace("&", "");
                s = s.Replace(":", "");
                s = s.Replace("^", "");
                s = s.Replace("%", "");
                s = s.Replace("$", "");
                s = s.Replace("#", "");
                s = s.Replace("@", "");
                s = s.Replace("!", "");
                s = s.Replace("~", "");
                s = s.Replace("`", "");



                s = s.Trim().ToLower().Replace(" ", "-");
                s = s.Replace("----------", "-");
                s = s.Replace("---------", "-");
                s = s.Replace("--------", "-");
                s = s.Replace("-------", "-");
                s = s.Replace("------", "-");
                s = s.Replace("-----", "-");
                s = s.Replace("----", "-");
                s = s.Replace("---", "-");
                s = s.Replace("--", "-");
                s = ProccessingDataType.layNKyTuDauTien_(s, 75);

                return s;



            }

        }
        public static List<string> RemoveDuplicate_InArray(List<string> lstBienThe)
        {
            List<string> lstNoDuplicateModel = new List<string>();
            try
            {
                if (lstBienThe != null && lstBienThe.Count > 0)
                {
                    int dem = 0;
                    do
                    {
                        if (dem == 0)
                        {
                            lstNoDuplicateModel.Add(lstBienThe[dem]);
                        }
                        else
                        {
                            bool IsDuplicate = false;
                            int vt = 0;
                            foreach (string itemBienThe in lstNoDuplicateModel)
                            {
                                if ((itemBienThe.ToLower().Trim() == lstBienThe[dem].ToLower().Trim()))
                                {

                                    IsDuplicate = true;
                                    break;
                                }
                                vt++;
                            }
                            if (!IsDuplicate)
                            {
                                lstNoDuplicateModel.Add(lstBienThe[dem]);
                            }
                        }
                        dem++;
                    } while (dem < lstBienThe.Count);

                }
            }
            catch (Exception ex)
            {
                //  Utils.WriteLog("Loi:" + ex.Message, "BienTheMayAnh_RemoveDuplicateModel_BienThe");
            }
            return lstNoDuplicateModel;
        }
        public static List<string> RemoveDuplicate(List<string> lstBienThe)
        {
            List<string> lstNoDuplicateModel = new List<string>();
            string sLog = ",";
            try
            {
                if (lstBienThe != null && lstBienThe.Count > 0)
                {
                    int dem = 0;
                    do
                    {

                        if (dem == 0)
                        {
                            lstNoDuplicateModel.Add(lstBienThe[dem]);
                        }
                        else
                        {


                            if (!sLog.Contains(string.Format(",{0},", lstBienThe[dem])))
                            {
                                lstNoDuplicateModel.Add(lstBienThe[dem]);
                            }
                        }
                        sLog += lstBienThe[dem] + ",";
                        dem++;
                    } while (dem < lstBienThe.Count);

                }
            }
            catch (Exception ex)
            {
                //  Utils.WriteLog("Loi:" + ex.Message, "BienTheMayAnh_RemoveDuplicateModel_BienThe");
            }
            return lstNoDuplicateModel;
        }
        public static string GetModelSub(string model)
        {
            string[] aModel = model.Replace("(", "").Replace(")", "").Split('|');
            string sRet = "";
            if (aModel != null && aModel.Length > 0)
            {
                foreach (string item in aModel)
                {
                    string item1 = item.Replace("AgfaPhoto", "Agfa Photo");

                    var matchNumber = Regex.Matches(item1, "[0-9]{1,}");
                    if (matchNumber != null && matchNumber.Count > 0)
                    {
                        string sModelItem = "";
                        string[] aWord = item1.Split(' ');
                        int start = 0;
                        for (int i = 0; i < aWord.Length; i++)
                        {

                            matchNumber = Regex.Matches(aWord[i], "[0-9]{1,}");
                            if (matchNumber != null && matchNumber.Count > 0)
                            {
                                start = i;
                            }
                            else
                            {
                                continue;
                            }

                        }

                        sModelItem = aWord[0];
                        if (start > 0)
                        {
                            for (int i = start; i < aWord.Length; i++)
                            {
                                sModelItem += " " + aWord[i];
                            }
                        }

                        sRet += sModelItem + "|";
                    }
                    else
                    {
                        sRet += item + "|";
                    }
                }
            }
            sRet = "(" + sRet.Substring(0, sRet.Length - 1) + ")";
            return sRet;
        }
        public static float ConvertStringToFloat_Hoadv(string s)
        {
            float so = 0;
            try
            {
                bool kq = float.TryParse(s, out so);
                if (so == 0)
                {
                    kq = float.TryParse(s.Replace(".", ","), out so);
                }
            }
            catch (Exception ex)
            {
                Utils.WriteLog(s + "\r\n" + ex.Message, "Loi_ConvertStringToFloat_Hoadv_");
            }
            return so;
        }
        public static int StringToArrayInt_GetMax(string array)
        {
            if (string.IsNullOrEmpty(array))
            {
                return 0;
            }
            else
            {
                try
                {
                    array = array.Replace(" ", "");
                    string[] ArrayNumber = array.Split(',');
                    int[] aResult = new int[ArrayNumber.Length];
                    for (int i = 0; i < ArrayNumber.Length; i++)
                    {
                        aResult[i] = int.Parse("0" + ArrayNumber[i]);
                    }
                    return MaxArrayInt(aResult);
                }
                catch (Exception ex)
                {
                    Utils.WriteLog("Loi:" + ex.Message, "Loi_StringToArrayInt_GetMax");
                    return 0;
                }



            }
        }
        public static int MaxArrayInt(int[] array)
        {
            int so = 0;
            try
            {
                if (array != null && array.Length > 0)
                {
                    if (array.Length == 1)
                    {
                        return array[0];
                    }
                    else
                    {
                        int max = 1;
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (max < array[i])
                            {
                                max = array[i];
                            }
                            //for (int j = 1; j < array.Length; j++)
                            //{
                            //    if (array[i] > array[j])
                            //    {
                            //        int tam = array[i];
                            //        array[i] = array[j];
                            //        array[j] = tam;
                            //    }
                            //}
                        }
                        return max;
                    }
                }
            }
            catch (Exception)
            {

            }
            return so;
        }
        public static int MinArrayInt(int[] array)
        {
            int so = 0;
            try
            {
                if (array != null && array.Length > 0)
                {
                    if (array.Length == 1)
                    {
                        return array[0];
                    }
                    else
                    {
                        int min = array[0];
                        for (int i = 0; i < array.Length; i++)
                        {
                            if (min > array[i])
                            {
                                min = array[i];
                            }
                        }
                        return min;
                    }
                }
            }
            catch (Exception)
            {

            }
            return so;
        }
        public static float MaxArrayFloat(float[] array)
        {
            float so = 0;
            try
            {
                if (array != null && array.Length > 0)
                {
                    if (array.Length == 1)
                    {
                        return array[0];
                    }
                    else
                    {
                        for (int i = 0; i < array.Length - 1; i++)
                        {
                            for (int j = 1; j < array.Length; j++)
                            {
                                if (array[i] > array[j])
                                {
                                    float tam = array[i];
                                    array[i] = array[j];
                                    array[j] = tam;
                                }
                            }
                        }
                        return array[array.Length - 1];
                    }
                }
            }
            catch (Exception)
            {

            }
            return so;
        }
        public static string ToUpper_StringCheckLeng(string s)
        {
            if (string.IsNullOrEmpty(s) == true || s.Length < 1)
            {
                return s;
            }
            return s.ToUpper().Trim();
        }
        public static string ToLower_StringCheckLeng(string s)
        {
            if (string.IsNullOrEmpty(s) == true || s.Length < 1)
            {
                return s;
            }
            return s.ToLower().Trim();
        }
        #endregion
    }
}
