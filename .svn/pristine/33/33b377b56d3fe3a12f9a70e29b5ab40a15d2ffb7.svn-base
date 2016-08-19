
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using HtmlAgilityPack;
using System.Threading;
using System.Drawing.Imaging;
using System.Globalization;

namespace Lemia.Common
{
    public class Utils
    {
        #region Variables
        private static Mutex mut = new Mutex();
        static string _className = "Utils";
        public const int PRODUCT_4_RENT = 49;
        public const int PRODUCT_4_SELL = 38;
        public static RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled;
        #endregion

        #region Methods
        public static string PathForLogs()
        {
            return @"\Logs\";
        }
        public static string EncryptByMD5(string input)
        {
            string strEnd = string.Empty;
            try
            {
                if (!String.IsNullOrEmpty(input))
                {
                    var uEncode = new UnicodeEncoding();
                    byte[] hashedBytes = uEncode.GetBytes(input);
                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                    byte[] hash = md5.ComputeHash(hashedBytes);
                    strEnd = Convert.ToBase64String(hash);
                }
            }
            catch
            {
                strEnd = string.Empty;
            }
            return strEnd;
        }
        public static string GetHtmlFromUrl(string url)
        {
            string sHTML = GetHtmlFromUrl_ProxyAll(url);
            return sHTML;
        }
        //Editted by NhatHD: chỉ sử dụng proxy.
        public static string GetHtmlFromUrl_ProxyAll(string url)
        {
            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            //request.UserAgent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
            //request.Referer = url;
            ////request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:38.0) Gecko/20100101 Firefox/38.0";
            //request.KeepAlive = true;
            //request.Timeout = 60000;
            //WebResponse response = request.GetResponse();
            //Stream stream = response.GetResponseStream();
            //StreamReader sr = new StreamReader(stream);
            //string text = sr.ReadToEnd();
            //text = Utils.Normalization(text);
            //return text;

            int dem = 0;
            int MinHtmlLength = 500;
            string html = string.Empty;
            Random ranProxy = new Random(1);

            while ((string.IsNullOrEmpty(html) || html.Length < MinHtmlLength) && (dem < 5))
            {
                dem = dem + 1;
                //Lấy random proxy từ danh sách. Truyền tham số Random từ ngoài vào. Tránh trường hợp, mỗi lần vào hàm RandomProxy đều được khởi tạo giống nhau.
                string sDomainProxy = RandomDomainProxy(ranProxy);
                //Get html sử dụng proxy.
                html = GetHtmlFromUrl_ProxyDetail(url, sDomainProxy, 8080);
                if (string.IsNullOrEmpty(html))
                {
                    html = DownloadString(url);
                }
                if ((string.IsNullOrEmpty(html) || html.Length < MinHtmlLength) && (dem < 5))
                {
                    //Nếu get không thành công, thì tạm dừng 1 giây.
                    Thread.Sleep(1 * 1 * 1000);
                }
            }
            return html;
        }
        public static string GetHtmlFromUrl_ProxyDetail(string url, string DomainOrIPProxy, int portDomainOrIPProxy)
        {
            string text = string.Empty;
            string ForceIP = string.Empty;
            try
            {

                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
                request.Referer = url;
                //Dung Proxy
                WebProxy myproxy = new WebProxy(DomainOrIPProxy, portDomainOrIPProxy);
                myproxy.BypassProxyOnLocal = false;
                request.Proxy = myproxy;
                //***************//
                if (url.ToLower().Contains("vatgia."))
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(new Uri(url), new Cookie("login_name", "darklight"));
                    request.CookieContainer.Add(new Uri(url), new Cookie("PHPSESSD", "7404e73dfb301c0884f8eff603d34d2a"));
                }
                //***************//
                //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:38.0) Gecko/20100101 Firefox/38.0";
                request.KeepAlive = true;
                request.Timeout = 60000;
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                text = sr.ReadToEnd();
                text = Utils.Normalization(text);
                return text;
            }
            catch (Exception ex)
            {
                //resend Request
                return string.Empty;
            }
        }
        public static string GetHtmlFromUrl(string url, string DomainOrIPProxy, int portDomainOrIPProxy)
        {
            string text = string.Empty;
            string ForceIP = string.Empty;
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.UserAgent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";
                request.Referer = url;
                //Dung Proxy
                WebProxy myproxy = new WebProxy(DomainOrIPProxy, portDomainOrIPProxy);
                myproxy.BypassProxyOnLocal = false;
                request.Proxy = myproxy;

                //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:38.0) Gecko/20100101 Firefox/38.0";
                request.KeepAlive = true;
                request.Timeout = 60000;
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                text = sr.ReadToEnd();
                text = Utils.Normalization(text);
                return text;
            }
            catch (Exception ex)
            {
                //resend Request
                return string.Empty;
            }
        }
        public static string RandomDomainProxy(Random rnd)
        {
            string Domain = "";
            int month = rnd.Next(1, 5);
            if (month == 1)
            {
                Domain = "mayanhcu.vn";
            }
            else if (month == 2)
            {
                Domain = "quanaoredep.vn";
            }
            else if (month == 3)
            {
                Domain = "donoithatdep.vn";
            }
            else if (month == 4)
            {
                Domain = "xemaycugiare.com";
            }
            else if (month == 5)
            {
                Domain = "maytinhbanggiatot.vn";
            }
            return Domain;
        }
        public static string DownloadString(string link, CookieCollection cookies = null)
        {
            try
            {
                // Request 1 use HttpRequest
                var resp = MakeRequestGet(link, cookies);
                if (resp != null)
                {
                    var encoding = Encoding.UTF8;
                    var stream = resp.GetResponseStream();
                    if (stream != null)
                    {
                        var respStream = new StreamReader(stream, encoding);
                        var htmlData = respStream.ReadToEnd();

                        resp.Close();
                        respStream.Close();
                        return htmlData;
                    }
                }
                // Request 2 use WebClient
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.CacheControl] = "max-age=0";
                    client.Headers[HttpRequestHeader.AcceptEncoding] = "gzip,deflate,sdch";
                    client.Headers[HttpRequestHeader.AcceptCharset] = "windows-1258,utf-8;q=0.7,*;q=0.3";
                    client.Headers[HttpRequestHeader.Accept] = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                    client.Headers[HttpRequestHeader.AcceptLanguage] = "vi-VN,vi;q=0.8,fr-FR;q=0.6,fr;q=0.4,en-US;q=0.2,en;q=0.2";
                    client.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                    return client.DownloadString(link);
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        private static HttpWebResponse MakeRequestGet(string link, CookieCollection cookie = null)
        {
            // Create request
            var req = (HttpWebRequest)WebRequest.Create(link);
            // Cookie
            req.CookieContainer = new CookieContainer();
            if (cookie != null)
            {
                req.CookieContainer.Add(new Uri(link), cookie);
            }

            // Headers
            req.Method = "GET";
            req.Timeout = 600 * 1000;
            req.AllowAutoRedirect = true;
            req.Headers = new WebHeaderCollection
                                  {
                                      { HttpRequestHeader.CacheControl,"max-age=0"},
                                      { HttpRequestHeader.AcceptEncoding, "gzip,deflate,sdch" },
                                      { HttpRequestHeader.AcceptCharset, "windows-1258,utf-8;q=0.7,*;q=0.3"},
                                      { HttpRequestHeader.AcceptLanguage, "vi-VN,vi;q=0.8,fr-FR;q=0.6,fr;q=0.4,en-US;q=0.2,en;q=0.2" }
                                  };
            req.KeepAlive = true;
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            // Response
            try
            {
                return (HttpWebResponse)req.GetResponse();
            }
            catch (WebException ex)
            {
                return ex.Response != null ? (HttpWebResponse)ex.Response : null;
            }
        }
        public static string Normalization(string text)
        {
            text = text.Replace("\r\n", string.Empty).Replace("\t", " ");
            text = Regex.Replace(text, @"\s+", " ");
            return text;
        }
        public static bool DateIsEqual(DateTime d1, DateTime d2)
        {
            if (d1.Year == d2.Year && d1.Month == d2.Month && d1.Day == d2.Day)
            {
                return true;
            }
            return false;
        }
        public static string ExtractValueUsingRegex(string text, string regexp, string splitValue)
        {
            if (string.IsNullOrEmpty(text))
            { return string.Empty; }
            string value = string.Empty;
            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled;
            Regex r = new Regex(regexp, options);
            MatchCollection collect = r.Matches(text);
            foreach (Match m in collect)
            {
                if (m.Success)
                {
                    //return m.Value;
                    if (string.IsNullOrEmpty(value))
                    {
                        value = m.Groups["value"].Value;
                    }
                    else
                    {
                        value = value + splitValue + m.Groups["value"].Value;
                    }
                }
            }
            return value;
        }
        public static string ExtractValueUsingXpath(string text, string xPath)
        {
            try
            {
                string result = string.Empty;
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(text);
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(xPath);
                if (nodes != null && nodes.Count > 0)
                {
                    foreach (HtmlNode node in nodes)
                    {
                        if (node != null)
                        {
                            result = string.IsNullOrEmpty(result) ? node.InnerHtml : result + " <br /> " + node.InnerHtml;
                        }
                    }
                }
                return result;
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string ExtractEmails(string text)
        {
            string result = string.Empty; ;
            try
            {
                RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
                //Regex r = new Regex(@"[_a-zA-Z\d\-\.]+@[_a-zA-Z\d\-]+(\.[_a-zA-Z\d\-]+)+", options);
                Regex r = new Regex(@"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})", options);
                MatchCollection matches = r.Matches(text);
                foreach (Match match in matches)
                {
                    string s = match.Value.ToLower().Trim();
                    if (!result.Contains(s.Trim()) && !s.Contains("webmaster"))
                    {
                        result = result + ";" + s;
                    }
                }
            }
            catch
            {

            }
            return result;
        }
        public static List<string> ExtractItems(string html, string xpath)
        {
            List<string> lstItems = new List<string>();
            try
            {
                HtmlDocument htmldoc = new HtmlDocument();
                htmldoc.LoadHtml(html);
                if (htmldoc != null && htmldoc.DocumentNode != null)
                {
                    HtmlNodeCollection collectNode = htmldoc.DocumentNode.SelectNodes(xpath);
                    if (collectNode != null)
                    {
                        foreach (HtmlNode htmlChild in collectNode)
                        {
                            try
                            {
                                lstItems.Add(htmlChild.OuterHtml);
                            }
                            catch (Exception ex)
                            {
                                Utils.WriteLog(ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
            return lstItems;
        }
        public static List<string> ExtractItemsByAttribute(string html, string xpath, string attribute)
        {
            List<string> lstItems = new List<string>();
            try
            {
                if(string.IsNullOrEmpty(xpath))
                {
                    return lstItems;
                }
                HtmlDocument htmldoc = new HtmlDocument();
                htmldoc.LoadHtml(html);
                if (htmldoc != null && htmldoc.DocumentNode != null)
                {
                    HtmlNodeCollection collectNode = htmldoc.DocumentNode.SelectNodes(xpath);
                    if (collectNode != null)
                    {
                        foreach (HtmlNode htmlChild in collectNode)
                        {
                            try
                            {
                                if (htmlChild.Attributes[attribute] != null && !string.IsNullOrEmpty(htmlChild.Attributes[attribute].Value))
                                {
                                    lstItems.Add(htmlChild.Attributes[attribute].Value);
                                }
                            }
                            catch (Exception ex)
                            {
                                Utils.WriteLog(ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
            return lstItems;
        }
        public static List<string> ExtractValueUsingRegex(string text, string regexp)
        {
            List<string> lstValues = new List<string>();
            try
            {
                RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
                Regex r = new Regex(regexp, options);
                MatchCollection collect = r.Matches(text);
                foreach (Match m in collect)
                {
                    try
                    {
                        if (m.Success)
                        {
                            lstValues.Add(m.Groups["value"].Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        Utils.WriteLog(ex.Message);
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.WriteLog(ex.Message);
            }
            return lstValues;
        }
        public static int Download(string fromUrl, string toFolder, ref bool downloaded, string imageName, string imagePath, bool isGetPhonenumber = false)
        {
            downloaded = true;
            try
            {
                string ext = Path.GetExtension(fromUrl);
                toFolder = toFolder.Replace("/", "\\");
                string path = imagePath + toFolder + imageName;
                FileInfo file = new FileInfo(path);
                if (!Directory.Exists(file.DirectoryName))
                {
                    Directory.CreateDirectory(file.DirectoryName);
                }
                System.Drawing.Image _tmpImage;
                string filePath = string.Empty;
                switch (file.Extension)
                {
                    case ".gif":
                        //filePath = file.FullName.Substring(0, file.FullName.LastIndexOf(".")) + ".jpg";
                        filePath = file.FullName;
                        break;
                    default:
                        filePath = file.FullName;
                        break;
                }
                //New download methods
                HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(fromUrl);
                imageRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:39.0) Gecko/20100101 Firefox/39.0";
                HttpWebResponse imageResponse = (HttpWebResponse)imageRequest.GetResponse();
                imageResponse = (HttpWebResponse)imageRequest.GetResponse();
                Stream responseStream = imageResponse.GetResponseStream();
                _tmpImage = System.Drawing.Image.FromStream(responseStream);
                //Nếu không phải là get số điện thoại, thì check kích thước ảnh.
                if (!isGetPhonenumber)
                {
                    if (_tmpImage.Width < 140 || _tmpImage.Height < 100)
                    {
                        //ảnh bé quá thì không lấy.
                        downloaded = false;
                        return 0;
                    }
                }
                _tmpImage.Save(filePath, ImageFormat.Jpeg);
                responseStream.Close();
                imageResponse.Close();

                //using (WebClient client = new WebClient())
                //{
                //    using (Stream rawStream = client.OpenRead(fromUrl))
                //    {
                //        _tmpImage = Image.FromStream(rawStream);
                //        if (_tmpImage.Width < 140 || _tmpImage.Height < 100)
                //        {
                //            //ảnh bé quá thì không lấy.
                //            downloaded = false;
                //            return 0;
                //        }
                //        _tmpImage.Save(filePath, ImageFormat.Jpeg);
                //        rawStream.Close();
                //    }
                //}

                //byte[] lnBuffer;
                //byte[] lnFile;

                //HttpWebRequest lxRequest = (HttpWebRequest)WebRequest.Create(fromUrl);

                //// returned values are returned as a stream, then read into a string
                //using (HttpWebResponse lxResponse = (HttpWebResponse)lxRequest.GetResponse())
                //{
                //    using (BinaryReader lxBR = new BinaryReader(lxResponse.GetResponseStream()))
                //    {
                //        using (MemoryStream lxMS = new MemoryStream())
                //        {
                //            lnBuffer = lxBR.ReadBytes(1024);
                //            while (lnBuffer.Length > 0)
                //            {
                //                lxMS.Write(lnBuffer, 0, lnBuffer.Length);
                //                lnBuffer = lxBR.ReadBytes(1024);
                //            }
                //            lnFile = new byte[(int)lxMS.Length];
                //            lxMS.Position = 0;
                //            lxMS.Read(lnFile, 0, lnFile.Length);

                //            _tmpImage = Image.FromStream(lxMS);
                //            if (!isGetPhonenumber)
                //            {
                //                if (_tmpImage.Width < 140 || _tmpImage.Height < 100)
                //                {
                //                    //ảnh bé quá thì không lấy.
                //                    downloaded = false;
                //                    return 0;
                //                }
                //            }
                //            _tmpImage.Save(filePath, ImageFormat.Jpeg);
                //            lxMS.Close();
                //        }
                //    }
                //}

                return 0;
            }
            catch (Exception ex)
            {
                Utils.WriteLog(ex.Message);
                downloaded = false;
                return 0;
            }
        }
        public static string BuildImageUrl(string detailUrlValue, string domainLink)
        {
            // Trả về luôn nếu là ảnh base64
            if (detailUrlValue.Contains("data:image/"))
            {
                return detailUrlValue;
            }

            string url = string.Empty;

            try
            {
                if (detailUrlValue.Contains("http:") || detailUrlValue.Contains("https:"))
                {
                    return detailUrlValue;
                }
                string host = domainLink.Replace("www.", string.Empty).Replace("http:", string.Empty).Replace("//", string.Empty);
                if (detailUrlValue.Contains(host))
                {
                    if (!detailUrlValue.Contains("http://"))
                    {
                        detailUrlValue = "http://" + detailUrlValue;
                    }
                    return detailUrlValue;
                }
                else
                {
                    if (detailUrlValue.StartsWith("/"))
                    {
                        if (host.EndsWith("/"))
                        {
                            url = host.Substring(0, host.Length - 1) + detailUrlValue;
                        }
                        else
                        {
                            url = host + detailUrlValue;
                        }
                    }
                    else
                    {
                        if (host.EndsWith("/"))
                        {
                            url = host + detailUrlValue;
                        }
                        else
                        {
                            url = host + "/" + detailUrlValue;
                        }
                    }
                }
                url = "http://" + url;
            }
            catch (Exception ex)
            {
                url = string.Empty;
                Utils.WriteLog(ex.Message);
            }
            return url;
        }
        public static string ConvertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static string NormalizationPhoneNumber(string s)
        {
            s = s.Replace(" ", string.Empty);
            string[] arrPhones = s.Split('-');
            string result = string.Empty;
            string tempPhone = string.Empty;
            if (arrPhones != null)
            {
                if (arrPhones.Length == 1)
                {
                    tempPhone = Utils.ExtractPhones(s);
                    if (!string.IsNullOrEmpty(tempPhone))
                    {
                        return tempPhone;
                    }
                }
                foreach (string ss in arrPhones)
                {
                    if (string.IsNullOrEmpty(ss))
                    {
                        continue;
                    }
                    tempPhone = ExtractPhones(ss);//ss.Replace(" ", string.Empty).Replace(".", string.Empty);
                    if (!result.Contains(tempPhone))
                    {
                        if (tempPhone.Length == 10 || tempPhone.Length == 11)
                        {
                            if (string.IsNullOrEmpty(result))
                            {
                                result = tempPhone.Trim();
                            }
                            else
                            {
                                result = result + ";" + tempPhone.Trim();
                            }
                        }
                    }
                }
            }
            return result;
        }
        public static string RemoveAllHtmlTag(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                source = RemoveAllJavaScript(source);
                source = source.Replace("\r\n", string.Empty).Replace("\t", " ");
                source = Regex.Replace(source, @"\s+", " ");
                source = source.Replace("&nbsp;", " ");
                return System.Net.WebUtility.HtmlDecode(Regex.Replace(source, "<.*?>", " "));
            }
            else
            {
                return string.Empty;
            }
        }
        public static string RemoveHtmlTag_RemoveAllATag(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                source = RemoveAllJavaScript(source);
                source = source.Replace("\r\n", string.Empty).Replace("\t", " ");
                source = Regex.Replace(source, @"\s+", " ");
                source = source.Replace("&nbsp;", " ");
                source = System.Net.WebUtility.HtmlDecode(Regex.Replace(source, "<a.*?>", " "));
                return System.Net.WebUtility.HtmlDecode(Regex.Replace(source, "</a>", " "));
            }
            else
            {
                return string.Empty;
            }
        }
        public static string RemoveAllJavaScript(string source)
        {
            string sTemp = string.Empty;
            if (!string.IsNullOrEmpty(source))
            {
                try
                {
                    RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
                    sTemp = Regex.Replace(source, "<script.*?>.*?</script>", string.Empty, options);
                }
                catch (Exception ex)
                {
                    Utils.WriteLog(ex.Message);
                }
                source = sTemp;
            }
            return source;
        }
        public static string RemoveHtmlTag_ForDescription(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                //(?i)<(?!p|/p|div|/div|br|span|/span).*?>
                string regex = "(?i)<(?!p|/p|div|/div|br|span|/span|table|/table|img).*?>";
                source = RemoveAllJavaScript(source);
                source = source.Replace("\r\n", string.Empty).Replace("\t", " ");
                source = Regex.Replace(source, @"\s+", " ");
                source = source.Replace("&nbsp;", string.Empty);
                //Remove all các thẻ, trừ p, div, br, span.
                source = Regex.Replace(source, regex, string.Empty);
                //Remove all các thuộc tính thẻ div
                source = Regex.Replace(source, "<div.*?>", "<div>");
                //Remove all các thuộc tính thẻ p
                source = Regex.Replace(source, "<p.*?>", "<p>");
                //Remove all các thuộc tính thẻ span
                source = Regex.Replace(source, "<span.*?>", "<span>");
                source = Regex.Replace(source, "<tr.*?>", "<tr>");
                source = Regex.Replace(source, "<td.*?>", "<td>");
                return System.Net.WebUtility.HtmlDecode(source);
            }
            else
            {
                return string.Empty;
            }
        }
        public static string BuildUrlDetail(string detailUrlValue, string domainLink)
        {
            try
            {
                string url = string.Empty;
                string http = domainLink.Contains("https:") ? "https" : "http";
                string host = domainLink.Replace("www.", string.Empty).Replace("http:", string.Empty).Replace("//", string.Empty);
                host = host.Replace("www.", string.Empty).Replace("https:", string.Empty).Replace("//", string.Empty);

                if (detailUrlValue.Contains(host))
                {
                    if (!detailUrlValue.Contains("http://") && !detailUrlValue.Contains("https://"))
                    {
                        detailUrlValue = http + "://" + detailUrlValue;
                    }
                    return detailUrlValue;
                }

                if (detailUrlValue.StartsWith("/"))
                {
                    if (host.EndsWith("/"))
                    {
                        url = host.Substring(0, host.Length - 1) + detailUrlValue;
                    }
                    else
                    {
                        url = host + detailUrlValue;
                    }
                }
                else
                {
                    if (host.EndsWith("/"))
                    {
                        url = host + detailUrlValue;
                    }
                    else
                    {
                        url = host + "/" + detailUrlValue;
                    }
                }
                url = http + "://" + url;
                return url;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static string ExtractValueByAttribute(string text, string xPath, string attributeName)
        {
            try
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(text);
                HtmlNode node = doc.DocumentNode.SelectSingleNode(xPath);
                if (node != null)
                {
                    return node.Attributes[attributeName].Value;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
           
            return string.Empty;
        }
        #endregion

        #region Log
        public static void WriteLog(string value, string fileName)
        {
            mut.WaitOne();
            try
            {

                string path = PathForLogs() + DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString() + @"\" + DateTime.Now.Day.ToString() + @"\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string logPath = string.Empty;
                logPath = !string.IsNullOrEmpty(fileName) ? path + @"\" + fileName + @"_Log.txt" : path + @"\" + @"Log.txt";
                using (FileStream fs = new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        //sw.WriteLine("----------" + DateTime.Now.ToString() + "----------");
                        sw.WriteLine(value);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }
        public static void WriteLog(string value)
        {
            mut.WaitOne();
            try
            {
                string path = PathForLogs() + DateTime.Today.ToShortDateString().Replace('/', '-');
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string logPath = path + @"\" + string.Format(@"Log_{0}.txt", DateTime.Now.ToString("yyyyMMddHH"));
                using (FileStream fs = new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        //sw.WriteLine("----------" + DateTime.Now.ToString() + "----------");
                        sw.WriteLine(value);
                    }
                }
            }
            catch
            {
            }
            finally
            {
                mut.ReleaseMutex();
            }
        }
        #endregion

        #region phone
        public static string ExtractPhones(string text)
        {
            string result = string.Empty;
            string temp = string.Empty;
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
            Regex rr = new Regex(@"[0-90]{10,11}|[0-9o]{2}[\s-.:,_']?[0-9o]{2}[\s-.:,_']?[0-9o]{2}[\s-.:,_']?[0-9o]{2}[\s-.:,_']?[0-9o]{2}|[0-9o]{3}[\s-.:,_'][0-9o]{2}[\s-.:,_'][0-9o]{2}[\s-.:,_'][0-9o]{2}[\s-.:,_'][0-9o]{2}[\s-.:,_']|[0-9o]{4}[\s-.:,_'][0-9o]{3}[\s-.:,_'][0-9o]{4}|[0-9o]{4}[\s-.:,_'][0-9o]{3}[\s-.:,_'][0-9o]{3}|[0-9o]{3}[\s-.:,_'][0-9o]{4}[\s-.:,_'][0-9o]{3}|[0-9o]{4}[\s-.:,_'][0-9o]{4}[\s-.:,_'][0-9o]{3}", options);
            MatchCollection matches = rr.Matches(text);
            result = IsPhone(matches, result);

            //if (matches == null || matches.Count == 0)
            {
                rr = new Regex(@"[0-9o]{4}[.\s-]?[0-9o]{2}[.\s-]?[0-9o]{2}[.\s-]?[0-9o]{2}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }

            //if (matches == null || matches.Count == 0)
            {
                rr = new Regex(@"[0-9o]{4}[.\s-]?[0-9o]{3}[.\s-]?[0-9o]{3}|[0-9o]{3}[.\s-]?[0-9o]{3}[.\s-]?[0-9o]{2}[.\s-]?[0-9o]{3}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            //if (matches == null || matches.Count == 0)
            {
                rr = new Regex(@"[0-9o]{4}[\s-.:,_]?[0-9o]{2}[\s-.:,_]?[0-9o]{2}[\s-.:,_]?[0-9o]{3}[\s-.:,_]?", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            //if (matches == null || matches.Count == 0)
            {
                rr = new Regex(@"[0-9o]{3}[\s-.:,_]?[0-9o]{2}[\s-.:,_]?[0-9o]{2}[\s-.:,_]?[0-9o]{3}[\s-.:,_]?", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            //if (matches == null || matches.Count == 0)
            {
                rr = new Regex(@"[0-9o]{3,4}[\s-.:,_]?[0-9o]{2,4}[\s-.:,_]?[0-9o]{2,5}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            //if (matches == null || matches.Count == 0)
            {
                rr = new Regex(@"[0-9o]{2,4}[\s-.:,_]?[0-9o]{2,4}[\s-.:,_]?[0-9o]{2,5}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            //if (matches == null || matches.Count == 0)
            {
                rr = new Regex(@"[0-9o]{5}[\s-.:,_]?[0-9o]{3}[\s-.:,_]?[0-9o]{3}|[0-9o]{5}[\s-.:,_]?[0-9o]{4}[\s-.:,_]?[0-9o]{2}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            {
                rr = new Regex(@"[0-9o]{3}[.\s-:,_]?[0-9o]{2}[.\s-:,_]?[0-9o]{3}[.\s-:,_]?[0-9o]{2}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            {
                rr = new Regex(@"[0-9o]{4}[.\s-:,_]+?[0-9o]{3}[.\s-:,_]+?[0-9o]{3}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            {
                rr = new Regex(@"[0-9o]{4}[.\s-:,_]+?[0-9o]{3}[.\s-:,_]+?[0-9o]{4}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            {
                rr = new Regex(@"[0-9o]{2}[.\s-:,_]+?[0-9o]{3}[.\s-:,_]+?[0-9o]{3}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            {
                rr = new Regex(@"[0-9o]{4}[.\s-:,_]+?[0-9o]{2}[.\s-:,_]+?[0-9o]{2}[.\s-:,_]+?[0-9o]{2}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            {
                rr = new Regex(@"[0-9o]{3}[.\s-:,_]+?[0-9o]{1}[.\s-:,_]+?[0-9o]{6}|[0-9o]{2}[.\s-:,_]+?[0-9o]{3}[.\s-:,_]+?[0-9o]{2}[.\s-:,_]+?[0-9o]{3}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            {
                rr = new Regex(@"[0-9o]{3}[.\s-:,_]+?[0-9o]{1}[.\s-:,_]+?[0-9o]{3}[.\s-:,_]+?[0-9o]{3}|[0-9o]{3}[.\s-:,_]+?[0-9o]{3}[.\s-:,_]+?[0-9o]{2}[.\s-:,_]+?[0-9o]{2}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            {
                rr = new Regex(@"[0-9o]{2}[.\s-:,_]+?[0-9o]{5}[.\s-:,_]+?[0-9o]{3}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            {
                rr = new Regex(@"[0-9o]{4}[.\s-:,_]+?[0-9o]{1}[.\s-:,_]+?[0-9o]{3}[.\s-:,_]+?[0-9o]{3}|[0-9o]{5}[.\s-:,_]+?[0-9o]{2}[.\s-:,_]+?[0-9o]{4}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            {
                rr = new Regex(@"[0-9o]{5}[.\s-:,_]+?[0-9o]{2}[.\s-:,_]+?[0-9o]{2}[.\s-:,_]+?[0-9o]{2}|[0-9o]{3}[.\s-:,_]+?[0-9o]{3}[.\s-:,_]+?[0-9o]{4}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            {
                rr = new Regex(@"[0-9o]{4}[.\s-:,_]+?[0-9o]{3}[.\s-:,_]+?[0-9o]{1}[.\s-:,_]+?[0-9o]{2}|[0-9o]{5}[.\s-:,_]+?[0-9o]{3}[.\s-:,_]+?[0-9o]{3}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            {
                rr = new Regex(@"[0-9o]{4}[.\s-:,_]+?[0-9o]{3}[.\s-:,_]+?[0-9o]{2}[.\s-:,_]+?[0-9o]{2}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            {
                rr = new Regex(@"[0-9o]{5}[.\s-:,_]+?[0-9o]{1}[.\s-:,_]+?[0-9o]{4}", options);
                matches = rr.Matches(text);
                temp = IsPhone(matches, result);
                if (temp != null && temp != string.Empty)
                {
                    result = temp;
                }
            }
            return result;
        }
        private static string IsPhone(MatchCollection matches, string result)
        {
            //TODO: Lấy số điện thoại bàn.
            if (matches == null || matches.Count == 0)
            { return string.Empty; }
            string tempPhone = string.Empty;
            foreach (Match match in matches)
            {
                //Replace O by 0
                tempPhone = match.Value.ToLower().Replace('o', '0');
                //tempPhone = match.Value.Replace(" ", string.Empty).Replace(".", string.Empty).Replace("-", string.Empty);
                tempPhone = Regex.Replace(tempPhone, @"[^\d]", "");
                //if phone number not start with zero number so it is not phone number type.
                //if (!tempPhone.StartsWith("0"))
                //{ continue; }
                if (tempPhone.StartsWith("011"))
                {
                    continue;
                }
                if (tempPhone.StartsWith("00"))
                {
                    tempPhone = tempPhone.Substring(1, tempPhone.Length - 1);
                }
                if (tempPhone.StartsWith("84"))
                {
                    tempPhone = tempPhone.Substring(2, tempPhone.Length - 2);
                    tempPhone = "0" + tempPhone;
                }
                if (!tempPhone.StartsWith("09"))
                {
                    if (!tempPhone.StartsWith("01"))
                    {
                        if (!tempPhone.StartsWith("009") | !tempPhone.StartsWith("001"))
                        {
                            continue;
                        }
                    }
                }
                if (tempPhone.Length == 12)
                {
                    string sub = tempPhone.Substring(0, 2);
                    if (sub == "00")
                    {
                        tempPhone = tempPhone.Substring(1, tempPhone.Length - 1);
                    }
                }
                if (tempPhone.StartsWith("09"))
                {
                    if (tempPhone.Length != 10)
                    {
                        continue;
                    }
                }
                if (tempPhone.StartsWith("01"))
                {
                    if (tempPhone.Length != 11)
                    {
                        continue;
                    }
                }
                if (!result.Contains(tempPhone))
                {
                    if (string.IsNullOrEmpty(result))
                    {
                        result = tempPhone.Trim();
                    }
                    else
                    {
                        result = result + ";" + tempPhone.Trim();
                    }
                }
            }
            return result;
        }
        public static string Phones_OnlyNumber(string phones)
        {
            try
            {
                if (string.IsNullOrEmpty(phones) == false && phones.Length > 0)
                {
                    RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline;
                    var matNumberPattern = Regex.Matches(phones, "[0-9]{1,}", options);
                    if (matNumberPattern != null && matNumberPattern.Count > 0)
                    {
                        string phoneOK = string.Empty;
                        foreach (var item in matNumberPattern)
                        {
                            if (string.IsNullOrEmpty(item.ToString()) == false && item.ToString().Length > 0)
                            {
                                phoneOK += item.ToString().Trim();
                            }
                        }
                        return phoneOK;
                    }
                }
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }
        public static string Phone_StandardOfVN_Mobiles(string aPhoneNumber)
        {
            if (string.IsNullOrEmpty(aPhoneNumber))
            {
                return string.Empty;
            }
            try
            {
                #region Proccessing
                //Chuyển chữ thành số
                aPhoneNumber = aPhoneNumber.ToLower().Trim();
                aPhoneNumber = aPhoneNumber.Replace("một", "1");
                aPhoneNumber = aPhoneNumber.Replace("hai", "2");
                aPhoneNumber = aPhoneNumber.Replace("ba", "3");
                aPhoneNumber = aPhoneNumber.Replace("bốn", "4");
                aPhoneNumber = aPhoneNumber.Replace("năm", "5");
                aPhoneNumber = aPhoneNumber.Replace("sáu", "6");
                aPhoneNumber = aPhoneNumber.Replace("bảy", "7");
                aPhoneNumber = aPhoneNumber.Replace("tám", "8");
                aPhoneNumber = aPhoneNumber.Replace("chín", "9");

                aPhoneNumber = aPhoneNumber.Replace("one", "1");
                aPhoneNumber = aPhoneNumber.Replace("two", "2");
                aPhoneNumber = aPhoneNumber.Replace("three", "3");
                aPhoneNumber = aPhoneNumber.Replace("four", "4");
                aPhoneNumber = aPhoneNumber.Replace("five", "5");
                aPhoneNumber = aPhoneNumber.Replace("six", "6");
                aPhoneNumber = aPhoneNumber.Replace("seven", "7");
                aPhoneNumber = aPhoneNumber.Replace("eight", "8");
                aPhoneNumber = aPhoneNumber.Replace("nine", "9");
                //Chỉ lấy số
                aPhoneNumber = Phones_OnlyNumber(aPhoneNumber);
                //Chuẩn hóa về bắt đầu bằng số 0
                if (aPhoneNumber.StartsWith("+849"))
                {
                    aPhoneNumber = aPhoneNumber.Replace("+849", "09");
                }
                if (aPhoneNumber.StartsWith("849"))
                {
                    aPhoneNumber = aPhoneNumber.Replace("849", "09");
                }

                if (aPhoneNumber.StartsWith("+841"))
                {
                    aPhoneNumber = aPhoneNumber.Replace("+841", "01");
                }
                if (aPhoneNumber.StartsWith("841"))
                {
                    aPhoneNumber = aPhoneNumber.Replace("841", "01");
                }

                if (aPhoneNumber.StartsWith("+848"))
                {
                    aPhoneNumber = aPhoneNumber.Replace("+848", "08");
                }
                if (aPhoneNumber.StartsWith("848"))
                {
                    aPhoneNumber = aPhoneNumber.Replace("848", "08");
                }
                //908 927 264
                if ((aPhoneNumber.StartsWith("9") || aPhoneNumber.StartsWith("8")) && aPhoneNumber.Length == 9)
                {
                    aPhoneNumber = "0" + aPhoneNumber;
                }
                //1689 484 147
                if ((aPhoneNumber.StartsWith("1")) && aPhoneNumber.Length == 10)
                {
                    aPhoneNumber = "0" + aPhoneNumber;
                }

                //Trả về đúng số điện thoại chuẩn đầu 08 09
                if ((aPhoneNumber.StartsWith("08") || aPhoneNumber.StartsWith("09")) && aPhoneNumber.Length >= 10)
                {
                    if (aPhoneNumber.Length == 10)
                    {
                        return aPhoneNumber;
                    }
                    else if (aPhoneNumber.Length > 10)
                    {
                        return aPhoneNumber.Substring(0, 10);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                //Trả về đúng số điện thoại chuẩn đầu 01
                if (aPhoneNumber.StartsWith("01") && aPhoneNumber.Length >= 11)
                {
                    if (aPhoneNumber.Length == 11)
                    {
                        return aPhoneNumber;
                    }
                    else if (aPhoneNumber.Length > 11)
                    {
                        return aPhoneNumber.Substring(0, 11);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }

                return string.Empty;
                #endregion
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static bool is_MobilesVN_PhoneNumber(string aPhoneNumber)
        {
            if (string.IsNullOrEmpty(aPhoneNumber))
            {
                return false;
            }
            else
            {

                aPhoneNumber = aPhoneNumber.Trim();
                if (aPhoneNumber.StartsWith("+849"))
                {
                    aPhoneNumber = aPhoneNumber.Replace("+849", "09");
                }
                if (aPhoneNumber.StartsWith("849"))
                {
                    aPhoneNumber = aPhoneNumber.Replace("849", "09");
                }
                if (aPhoneNumber.StartsWith("+841"))
                {
                    aPhoneNumber = aPhoneNumber.Replace("+841", "01");
                }
                if (aPhoneNumber.StartsWith("841"))
                {
                    aPhoneNumber = aPhoneNumber.Replace("841", "01");
                }
                if (aPhoneNumber.StartsWith("+848"))
                {
                    aPhoneNumber = aPhoneNumber.Replace("+848", "08");
                }
                if (aPhoneNumber.StartsWith("848"))
                {
                    aPhoneNumber = aPhoneNumber.Replace("848", "08");
                }

                if (aPhoneNumber.StartsWith("01") && aPhoneNumber.Length == "01689484147".Length)
                {
                    return true;
                }
                else if (aPhoneNumber.StartsWith("09") && aPhoneNumber.Length == "0979193397".Length)
                {
                    return true;
                }
                else if (aPhoneNumber.StartsWith("08") && aPhoneNumber.Length == "0868996687".Length)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region Price
        public static MatchCollection TryParsePrice_GetMatches(string text, string regex)
        {
            MatchCollection matchs = null;
            try
            {
                matchs = Regex.Matches(text, regex, options);
                return matchs;
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, _className);
            }
            return matchs;
        }
        public static int TryParsePrice(string text, long productId = 0)
        {
            // Bắt giá ưu tiên có keyword
            int _price = -1;
            //WriteLog("Bắt đầu gọi hàm bắt giá cũ:: productId or propertyId=" + productId, _className);
            try
            {
                List<int> lstPrices = new List<int>();
                List<string> lstResults = new List<string>();
                long price = 0;
                Match match = null;
                string input = string.Empty;
                string value = string.Empty;
                MatchCollection matches = null;
                string tiento = "(giá|giá cần bán|giá mong muốn|giá cho ra đi|giá bán|Gía|GIÁ CHỈ CÓ)";
                //string inputRegex = @"[0-9]{1,}(triệu|tr|\.|,|tỷ|\stỷ|\s|t)?([0-9]+|\s|\.|,)?([0-9]+|\s|\.|,)?([0-9]+)?(tr|triệu|đ|k|t)?";
                string inputRegex = @"[0-9]{1,}\s?(triệu|tr|\.|,|tỷ|\stỷ|\s|t)?([0-9]+|\s|\.|,)?([0-9]+|\s|\.|,)?([0-9]+)?(tr|triệu|đ|k|t)?";
                string pattern_1trieu200K = string.Empty;
                //Trường hợp đầu tiên: giá 4,000,000
                string firstRegex = @"(\s|,|\.|-|_)(giá bán|giá)(\s:|:\s|\s|:)[0-9,\.]+(\s|\.|$)(triệu|tr|k|vnd|vnđ)?";
                matches = Regex.Matches(text, firstRegex, options);
                lstResults = AddPrice(matches, lstResults);
                //Giá 1 triệu 200k
                pattern_1trieu200K = @"(giá|giá rẻ)[\s|:]*?[0-9]{1,}\s*?(Triệu|tr|trieu)\s*?[0-9]{1,}\s*?(K|k|nghìn)";
                matches = Regex.Matches(text, pattern_1trieu200K, options);
                lstResults = AddPrice(matches, lstResults);
                //Giá 6tr6.
                pattern_1trieu200K = @"(Giá|gia)(\.|\s|:)*?[0-9]{1,}(tr|triệu)[0-9]{1,}|\s[0-9]{1,}[\.,]?[0-9]{1,}?\s?(tr\s|tr\.|triệu|t[^\w]|tr[^\w])";
                matches = Regex.Matches(text, pattern_1trieu200K, options);
                lstResults = AddPrice(matches, lstResults);
                pattern_1trieu200K = @"[0-9]{1,}\s?(triệu|tr|t)\s?[0-9]{1,}(\s|$)";
                matches = Regex.Matches(text, pattern_1trieu200K, options);
                lstResults = AddPrice(matches, lstResults);
                //14,2 triệu
                pattern_1trieu200K = @"(^|\s|:|Giá|Giá:)[0-9]{1,}(,|\.)[0-9]{1,}\s*?(triệu|vnđ)";
                matches = Regex.Matches(text, pattern_1trieu200K, options);
                lstResults = AddPrice(matches, lstResults);
                //Đoạn này của HoaDV, kệ đê.
                if (lstResults == null && lstResults.Count == 0)
                {
                    if (matches == null || matches.Count == 0)
                    {
                        pattern_1trieu200K = @"(giá)[^.]+[0-9]{1,}\s*?(tr)(\s|\.|,)";
                        matches = Regex.Matches(text, pattern_1trieu200K, options);
                        if (matches != null && matches.Count > 0)
                        {
                            foreach (var item in matches)
                            {
                                string PatternPriceNumber = @"[0-9]{1,}\s*?(tr)";
                                var match_Price = Regex.Matches(item.ToString().Trim(), PatternPriceNumber, options);
                                if (match_Price != null && match_Price.Count > 0)
                                {
                                    foreach (var itemPrice in match_Price)
                                    {
                                        int gia = ProccessingDataType.ToInt32_hoadv(itemPrice.ToString().ToLower().Replace("tr", "").Trim() + "000");
                                        if (gia > 50)
                                        {
                                            return gia;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                //Xử lý chung.
                string priceRegex = tiento + @"(giá|giá cần bán|giá mong muốn|giá cho ra đi|giá bán|Gía|GIÁ CHỈ CÓ)[\s:]+?[0-9]{1,}(triệu|tr|tỷ|\stỷ|t|\.|,|\s)?([0-9]+|\s|\.|,)?([0-9]+|\s|\.|,)?([0-9]+)?(trăm nghìn|tr|triệu|đ|k|t)?|[0-9]{1,}[\.,0-9]?(triệu|tr|t|tỷ|\stỷ)[0-9]{1,}?[\sk,\.]|([0-9]{1,}k[^\w])|[0-9]{1,}\s?triệu|[0-9\.]+(vnd|vnđ|đ)";
                matches = Regex.Matches(text, priceRegex, options);
                lstResults = AddPrice(matches, lstResults);

                if (lstResults != null && lstResults.Count > 0)
                {
                    foreach (string s in lstResults)
                    {
                        try
                        {
                            value = s;
                            if (!string.IsNullOrEmpty(value))
                            {
                                value = value.Trim();
                                match = Regex.Match(value, inputRegex, options);
                                if (match != null)
                                {
                                    input = match.Value;
                                    input = input.Replace("tr đ", "tr");
                                    input = input.TrimEnd('.');
                                    input = input.Replace("tr.", "tr");
                                    input = input.Replace("tr..", "tr");
                                    input = input.Replace("tr .", "tr");
                                    input = input.Replace("tr,", "tr");
                                    input = input.Replace("tr,", "tr");
                                    input = input.Replace("triệu.", "tr");
                                    price = ExtractPrice(input + " ");
                                    if (price > 0)
                                    {
                                        price = price / 1000;
                                        if (price > 0)
                                        {
                                            //return int.Parse(price.ToString());
                                            lstPrices.Add(int.Parse(price.ToString()));
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            WriteLog("TryParsePrice::" + value + "::" + ex.Message, _className);
                        }
                    }
                    //Lấy giá to nhất
                    if (lstPrices != null && lstPrices.Count > 0)
                    {
                        int MaxPrice = lstPrices.Max();
                        //Nếu giá dưới 10k, thì không lấy.
                        return (MaxPrice < 10 ? 0 : MaxPrice);
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, _className);
            }
            return 0;
        }
        public static long ExtractPriceByKeyword(string s)
        {
            s = s.ToLower();
            long num = 0;
            string ty = "ti|ty";
            string trieu = "trieu|tr|t";
            string ngan = @"k|\s|\.|,|;|nghin|ngan|ng";
            // truong hop 123 ty 456 trieu 789 nghin
            MatchCollection result = Regex.Matches(s, @"^([0-9]*)(\s?)(" + ty + @")(\s?)([0-9]*)(\s?)(" + trieu + @")(\s?)([0-9]*)(\s?)(" + ngan + @") $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string[] temp = Regex.Split(match.Value, ty);
                    num = Int64.Parse(temp[0]) * 1000000000;
                    string[] temp1 = Regex.Split(temp[1], trieu);
                    num += Int64.Parse(temp1[0]) * 1000000;
                    temp1[1] = Regex.Replace(temp1[1], @"\D+", "");
                    num += Int64.Parse(temp1[1]) * 1000;
                }
                return num;
            }
            // 1. truong hop 123 ty 456 trieu
            result = Regex.Matches(s, @"^[0-9]*([\s]*)?(" + ty + @")([\s]*)?[0-9]*([\s]*)?(" + trieu + @") $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string[] temp = Regex.Split(match.Value, ty);
                    num = Int64.Parse(temp[0]) * 1000000000;

                    temp[1] = Regex.Replace(temp[1], @"\D+", "");
                    num += Int64.Parse(temp[1]) * 1000000;
                    //string s1 = Regex.Replace(temp[0], @"\s+", "");                    
                }
                return num;
            }
            // 2. truong hop 123 ty 4
            result = Regex.Matches(s, @"^[0-9]*([\s]*)?(" + ty + @")([\s]*)?[0-9]*([\s]?) $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string[] temp = Regex.Split(match.Value, ty);
                    num = Int64.Parse(temp[0]) * 1000000000;

                    temp[1] = Regex.Replace(temp[1], @"\D+", "");
                    int tl = temp[1].Length;
                    for (int i = 9; i > tl; i--)
                    {
                        temp[1] += '0';
                    }
                    num += Int64.Parse(temp[1]);

                    //string s1 = Regex.Replace(temp[0], @"\s+", "");                    
                }
                return num;
            }
            // 3. truong hop 1.2 tỷ
            result = Regex.Matches(s, @"^([0-9]*)(\s?)[,\.](\s?)([0-9]*)(\s?)(" + ty + ") $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string[] temp = Regex.Split(match.Value, ty);
                    temp[0] = Regex.Replace(temp[0], @"\s+", "");
                    temp[0] = Regex.Replace(temp[0], @",", ".");
                    num = (long)(Double.Parse(temp[0]) * 1000000000);
                }
                return num;
            }
            // 4. truong hop 456 trieu 789 nghin
            result = Regex.Matches(s, @"^([0-9]*)(\s?)(" + trieu + @")(\s?)([0-9]*)(\s?)(" + ngan + ") $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string[] temp1 = Regex.Split(match.Value, trieu);
                    num += Int64.Parse(temp1[0]) * 1000000;

                    temp1[1] = Regex.Replace(temp1[1], @"\D+", "");
                    num += Int64.Parse(temp1[1]) * 1000;
                }
                return num;
            }
            // 5. truong hop 123 trieu 4
            result = Regex.Matches(s, @"^[0-9]*([\s]*)?(" + trieu + @")([\s]*)?[0-9]*([\s]?) $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string[] temp = Regex.Split(match.Value, trieu);
                    num = Int64.Parse(temp[0]) * 1000000;

                    temp[1] = Regex.Replace(temp[1], @"\D+", "");
                    int tl = temp[1].Length;
                    //for (int i = 9; i > tl; i--)
                    for (int i = 6; i > tl; i--)//Editted by NhatHD: với phần phía sau, max chỉ là 6 số 0.
                        temp[1] += '0';
                    num += Int64.Parse(temp[1]);
                }
                return num;
            }
            // 6. truong hop 1.2 trieu
            double num1 = 0;
            result = Regex.Matches(s, @"^([0-9]*)(\s?)[,\.](\s?)([0-9]*)(\s?)(" + trieu + ") $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    //float.Parse("0" + a);
                    string[] temp = Regex.Split(match.Value, trieu);
                    temp[0] = Regex.Replace(temp[0], @"\s+", "");
                    temp[0] = Regex.Replace(temp[0], @",", ".");
                    num1 = double.Parse(temp[0], CultureInfo.InvariantCulture);
                    num1 *= 1000000;
                }
                return (long)num1;
            }
            // 7. truong hop 100k
            result = Regex.Matches(s, @"^([0-9]*)(\s?)(" + "k|nghin|ngan|ng" + @") $|^([0-9]*)(\s?)((\.|,|\s)[0-9]*)?(" + "k|nghin|ngan|ng" + ") $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string temp = Regex.Replace(match.Value, @"\D+", "");
                    for (int i = 0; i < 3; i++)
                    {
                        temp += '0';
                    }
                    num = (long)(Double.Parse(temp));
                }
                return num;
            }
            // truong hop 123.345.456.678
            result = Regex.Matches(s, @"[1-9]*[\s|\.|,]?[0-9]*[\s|\.|,]?[0-9]*[\s|\.|,]?[0-9]*[\s|\.|,]?");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string temp = Regex.Replace(match.Value, @"\D+", "");
                    if (temp != " " && !string.IsNullOrEmpty(temp.Trim()))
                    {
                        if (Int64.TryParse(temp, out num))
                        {
                            num = Int64.Parse(temp);
                        }
                    }
                }
                return num;
            }
            return 0;
        }
        public static long ExtractPrice(string s)
        {
            s = s.ToLower();
            long num = 0;
            string ty = "ti|ty|tỉ|tỷ";
            string trieu = "triệu|triêu|trieu|tr|t";
            string ngan = @"k|\s|\.|,|;|nghin|nghìn|ngàn|ng";
            // truong hop 123 ty 456 trieu 789 nghin
            MatchCollection result = Regex.Matches(s, @"^([0-9]*)(\s?)(" + ty + @")(\s?)([0-9]*)(\s?)(" + trieu + @")(\s?)([0-9]*)(\s?)(" + ngan + @") $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string[] temp = Regex.Split(match.Value, ty);
                    num = Int64.Parse(temp[0]) * 1000000000;
                    string[] temp1 = Regex.Split(temp[1], trieu);
                    num += Int64.Parse(temp1[0]) * 1000000;
                    temp1[1] = Regex.Replace(temp1[1], @"\D+", "");
                    num += Int64.Parse(temp1[1]) * 1000;
                }
                return num;
            }
            // 1. truong hop 123 ty 456 trieu
            result = Regex.Matches(s, @"^[0-9]*([\s]*)?(" + ty + @")([\s]*)?[0-9]*([\s]*)?(" + trieu + @") $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string[] temp = Regex.Split(match.Value, ty);
                    num = Int64.Parse(temp[0]) * 1000000000;

                    temp[1] = Regex.Replace(temp[1], @"\D+", "");
                    num += Int64.Parse(temp[1]) * 1000000;
                    //string s1 = Regex.Replace(temp[0], @"\s+", "");                    
                }
                return num;
            }
            // 2. truong hop 123 ty 4
            result = Regex.Matches(s, @"^[0-9]*([\s]*)?(" + ty + @")([\s]*)?[0-9]*([\s]?) $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string[] temp = Regex.Split(match.Value, ty);
                    num = Int64.Parse(temp[0]) * 1000000000;

                    temp[1] = Regex.Replace(temp[1], @"\D+", "");
                    int tl = temp[1].Length;
                    for (int i = 9; i > tl; i--)
                    {
                        temp[1] += '0';
                    }
                    num += Int64.Parse(temp[1]);

                    //string s1 = Regex.Replace(temp[0], @"\s+", "");                    
                }
                return num;
            }
            // 3. truong hop 1.2 tỷ
            result = Regex.Matches(s, @"^([0-9]*)(\s?)[,\.](\s?)([0-9]*)(\s?)(" + ty + ") $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string[] temp = Regex.Split(match.Value, ty);
                    temp[0] = Regex.Replace(temp[0], @"\s+", "");
                    temp[0] = Regex.Replace(temp[0], @",", ".");
                    num = (long)(Double.Parse(temp[0]) * 1000000000);
                }
                return num;
            }
            // 4. truong hop 456 trieu 789 nghin
            result = Regex.Matches(s, @"^([0-9]*)(\s?)(" + trieu + @")(\s?)([0-9]*)(\s?)(" + ngan + ") $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string[] temp1 = Regex.Split(match.Value, trieu);
                    num += Int64.Parse(temp1[0]) * 1000000;

                    temp1[1] = Regex.Replace(temp1[1], @"\D+", "");
                    num += Int64.Parse(temp1[1]) * 1000;
                }
                return num;
            }
            // 5. truong hop 123 trieu 4
            result = Regex.Matches(s, @"^[0-9]*([\s]*)?(" + trieu + @")([\s]*)?[0-9]*([\s]?) $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string[] temp = Regex.Split(match.Value, trieu);
                    num = Int64.Parse(temp[0]) * 1000000;

                    temp[1] = Regex.Replace(temp[1], @"\D+", "");
                    int tl = temp[1].Length;
                    //for (int i = 9; i > tl; i--)
                    for (int i = 6; i > tl; i--)//Editted by NhatHD: với phần phía sau, max chỉ là 6 số 0.
                        temp[1] += '0';
                    num += Int64.Parse(temp[1]);
                }
                return num;
            }
            // 6. truong hop 1.2 trieu
            double num1 = 0;
            result = Regex.Matches(s, @"^([0-9]*)(\s?)[,\.](\s?)([0-9]*)(\s?)(" + trieu + ") $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    //float.Parse("0" + a);
                    string[] temp = Regex.Split(match.Value, trieu);
                    temp[0] = Regex.Replace(temp[0], @"\s+", "");
                    temp[0] = Regex.Replace(temp[0], @",", ".");
                    num1 = double.Parse(temp[0], CultureInfo.InvariantCulture);
                    num1 *= 1000000;
                }
                return (long)num1;
            }
            // 7. truong hop 100k
            result = Regex.Matches(s, @"^([0-9]*)(\s?)(" + "k|nghin|nghìn|ngàn|ng" + @") $|^([0-9]*)(\s?)((\.|,|\s)[0-9]*)?(" + "k|nghin|nghìn|ngàn|ng" + ") $");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string temp = Regex.Replace(match.Value, @"\D+", "");
                    for (int i = 0; i < 3; i++)
                    {
                        temp += '0';
                    }
                    num = (long)(Double.Parse(temp));
                }
                return num;
            }
            // truong hop 123.345.456.678
            result = Regex.Matches(s, @"[1-9]*[\s|\.|,]?[0-9]*[\s|\.|,]?[0-9]*[\s|\.|,]?[0-9]*[\s|\.|,]?");
            if (result.Count > 0)
            {
                foreach (Match match in result)
                {
                    string temp = Regex.Replace(match.Value, @"\D+", "");
                    if (temp != " " && !string.IsNullOrEmpty(temp.Trim()))
                    {
                        if (Int64.TryParse(temp, out num))
                        {
                            num = Int64.Parse(temp);
                        }
                    }
                }
                return num;
            }
            return 0;
        }
        public static List<string> AddPrice(MatchCollection matches, List<string> lstResults)
        {

            if (matches != null && matches.Count > 0)
            {
                foreach (Match m in matches)
                {
                    try
                    {
                        lstResults.Add(m.Value);
                    }
                    catch (Exception ex)
                    {
                        WriteLog(ex.Message, _className);
                    }
                }
            }

            return lstResults;
        }
        #endregion

        public static string BuildAlias_VN(string cateName)
        {
            string alias = string.Empty;
            alias = ConvertToUnSign(cateName);
            alias = Regex.Replace(alias, "[^0-9a-zA-Z]+", " ");
            alias = alias.Replace("\r\n", string.Empty).Replace("\t", " ");
            alias = Regex.Replace(alias, @"\s+", " ");
            alias = alias.Trim();
            alias = Regex.Replace(alias, @"\s", "-");
            return alias.ToLower().Trim();
        }        
    }
}
