using Commsights.Data.Models;
using Commsights.Data.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Commsights.Data.Helpers
{
    public class AppGlobal
    {
        #region Init
        public static DateTime InitDateTime => DateTime.Now;

        public static string InitString => string.Empty;

        public static string DateTimeCode => DateTime.Now.ToString("yyyyMMddHHmmss");

        public static string InitGuiCode => Guid.NewGuid().ToString();
        #endregion

        #region AppSettings 
        public static string Error001
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Error001").Value;
            }
        }
        public static int IndustryIDUnknown
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("IndustryIDUnknown").Value);
            }
        }
        public static int CountryID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("CountryID").Value);
            }
        }
        public static int LanguageID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("LanguageID").Value);
            }
        }
        public static int FrequencyID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("FrequencyID").Value);
            }
        }
        public static int ColorTypeID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("ColorTypeID").Value);
            }
        }
        public static int CompetitorID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("CompetitorID").Value);
            }
        }
        public static int WebsiteID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("WebsiteID").Value);
            }
        }
        public static int ParentID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("ParentID").Value);
            }
        }
        public static int ArticleTypeID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("ArticleTypeID").Value);
            }
        }
        public static int AssessID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("AssessID").Value);
            }
        }
        public static int ParentIDCompetitor
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("ParentIDCompetitor").Value);
            }
        }
        public static int ParentIDCustomer
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("ParentIDCustomer").Value);
            }
        }
        public static int ParentIDEmployee
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("ParentIDEmployee").Value);
            }
        }
        public static string Competitor
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Competitor").Value;
            }
        }
        public static string Contact
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Contact").Value;
            }
        }
        public static string ArticleType
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("ArticleType").Value;
            }
        }
        public static string AssessType
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("AssessType").Value;
            }
        }
        public static string PressList
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("PressList").Value;
            }
        }
        public static string Country
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Country").Value;
            }
        }
        public static string Language
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Language").Value;
            }
        }
        public static string Frequency
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Frequency").Value;
            }
        }
        public static string Color
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Color").Value;
            }
        }
        public static string Brand
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Brand").Value;
            }
        }
        public static string Product
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Product").Value;
            }
        }
        public static string ScanFinish
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("ScanFinish").Value;
            }
        }
        public static string Loading
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Loading").Value;
            }
        }
        public static string Logo
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Logo").Value;
            }
        }
        public static string Logo01
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Logo01").Value;
            }
        }
        public static string Logo160_160
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Logo160_160").Value;
            }
        }
        public static string URLImagesCustomer
        {
            get
            {
                string result = AppGlobal.Images + "/" + AppGlobal.Customer;
                return result;
            }
        }
        public static string Customer
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Customer").Value;
            }
        }
        public static string Images
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Images").Value;
            }
        }
        public static string LoadingURLFull
        {
            get
            {
                string result = AppGlobal.Domain + AppGlobal.Images + "/" + AppGlobal.Loading;
                return result;
            }
        }
        public static string LogoURLFull
        {
            get
            {
                string result = AppGlobal.Domain + AppGlobal.Images + "/" + AppGlobal.Logo;
                return result;
            }
        }
        public static string Logo01URLFull
        {
            get
            {
                string result = AppGlobal.Domain + AppGlobal.Images + "/" + AppGlobal.Logo01;
                return result;
            }
        }
        public static string Logo160_160URLFull
        {
            get
            {
                string result = AppGlobal.Domain + AppGlobal.Images + "/" + AppGlobal.Logo160_160;
                return result;
            }
        }
        public static string CRM
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CRM").Value;
            }
        }
        public static string Menu
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Menu").Value;
            }
        }
        public static string Website
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Website").Value;
            }
        }
        public static string MembershipType
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("MembershipType").Value;
            }
        }
        public static string WebsiteType
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("WebsiteType").Value;
            }
        }
        public static string EditSuccess
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("EditSuccess").Value;
            }
        }

        public static string EditFail
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("EditFail").Value;
            }
        }

        public static string CreateSuccess
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CreateSuccess").Value;
            }
        }

        public static string CreateFail
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CreateFail").Value;
            }
        }

        public static string DeleteSuccess
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("DeleteSuccess").Value;
            }
        }

        public static string DeleteFail
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("DeleteFail").Value;
            }
        }

        public static string Success
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Success").Value;
            }
        }

        public static string Error
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Error").Value;
            }
        }

        public static string RedirectDefault
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("RedirectDefault").Value;
            }
        }

        public static string LoginFail
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("LoginFail").Value;
            }
        }
        public static string URLDownloadExcel
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("URLDownloadExcel").Value;
            }
        }
        public static string URLScan
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("URLScan").Value;
            }
        }
        public static string FTPUploadExcel
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("FTPUploadExcel").Value;
            }
        }
        public static string ConectionString
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("ConectionString").Value;
            }
        }

        public static string Domain
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Domain").Value;
            }
        }

        public static string SitemapFTP
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("SitemapFTP").Value;
            }
        }

        public static string CMSTitle
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CMSTitle").Value;
            }
        }

        public static string MD5Key
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("MD5Key").Value;
            }
        }

        public static string PhoneContact
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("PhoneContact").Value;
            }
        }

        public static string EmailContact
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("EmailContact").Value;
            }
        }

        public static string AddressContact
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("AddressContact").Value;
            }
        }

        public static string Title
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Title").Value;
            }
        }

        public static string Facebook
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Facebook").Value;
            }
        }

        public static string Twitter
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Twitter").Value;
            }
        }

        public static string Youtube
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Youtube").Value;
            }
        }

        public static string FacebookTitle
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("FacebookTitle").Value;
            }
        }

        public static string TwitterTitle
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("TwitterTitle").Value;
            }
        }

        public static string YoutubeTitle
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("YoutubeTitle").Value;
            }
        }

        public static string TopMenuCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("TopMenuCode").Value;
            }
        }

        public static string MenuLeftCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("MenuLeftCode").Value;
            }
        }

        public static string CarouselCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CarouselCode").Value;
            }
        }

        public static int ProductPageSize
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("ProductPageSize").Value);
            }
        }

        public static string CategoryCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CategoryCode").Value;
            }
        }

        public static string PriceUnit
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("PriceUnit").Value;
            }
        }

        public static string MailUsername
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("MailUsername").Value;
            }
        }

        public static string MailPassword
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("MailPassword").Value;
            }
        }

        public static int MailSTMPPort
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("MailSTMPPort").Value);
            }
        }

        public static string TagCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("TagCode").Value;
            }
        }
        #endregion
        #region Functions
        public static string GetContentByURL(string url)
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            string html = webClient.DownloadString(url);
            string content = html;
            content = content.Replace(@"</body>", @"</body>~");
            content = content.Split('~')[0];
            content = content.Replace(@"</head>", @"~");
            if (content.Split('~').Length > 0)
            {
                content = content.Split('~')[1];
                content = content.Replace(@"<footer", @"~");
                content = content.Split('~')[0];
                content = content.Replace(@"</h1>", @"~");
                if (content.Split('~').Length > 0)
                {
                    content = content.Split('~')[1];
                }
            }
            content = RemoveHTMLTags(content);
            return content;
        }
        public static void GetParametersByURL(Product model)
        {
            model.Title = "";
            model.Description = "";
            model.Author = "";
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            string html = webClient.DownloadString(model.Urlcode);
            string title = html;
            title = title.Replace(@"</h1>", @"~");
            title = title.Split('~')[0];
            if (title.Split('>').Length > 0)
            {
                title = title.Split('>')[1];
            }
            if (string.IsNullOrEmpty(title))
            {
                title = html;
                title = title.Replace(@"""headline"":", @"~");
                if (title.Split('~').Length > 0)
                {
                    title = title.Split('~')[1];
                    title = title.Split(',')[0];
                    title = title.Replace(@""":", @"");
                }
            }
            if (!string.IsNullOrEmpty(title))
            {
                model.Title = title;
            }
            string description = html;
            description = description.Replace(@"""description"":", @"~");
            if (description.Split('~').Length > 0)
            {
                description = description.Split('~')[1];
                description = description.Split(',')[0];
                description = description.Replace(@""":", @"");
            }
            if (!string.IsNullOrEmpty(description))
            {
                model.Description = description;
            }
            string datePublished = html;
            datePublished = datePublished.Replace(@"""datePublished"":", @"~");
            if (datePublished.Split('~').Length > 0)
            {
                datePublished = datePublished.Split('~')[1];
                datePublished = datePublished.Split(',')[0];
                datePublished = datePublished.Replace(@""":", @"");
            }
            if (!string.IsNullOrEmpty(datePublished))
            {
                try
                {
                    model.DatePublish = DateTime.Parse(datePublished);
                }
                catch
                {
                }
            }
            string author = html;
            author = author.Replace(@"""author"":", @"~");
            if (author.Split('~').Length > 0)
            {
                author = author.Split('~')[1];
                author = author.Replace(@"""name"":", @"~");
                if (author.Split('~').Length > 0)
                {
                    author = author.Split('~')[1];
                    author = author.Split('}')[0];
                    author = author.Replace(@""":", @"");
                }
            }
            if (!string.IsNullOrEmpty(author))
            {
                model.Author = author;
            }
        }
        public static string SetDomainByURL(string url)
        {
            string domain = url;
            domain = domain.Replace(@"https://", @"");
            domain = domain.Replace(@"http://", @"");
            domain = domain.Split('/')[0];
            domain = domain.Replace(@"www.", @"");
            return domain;
        }
        public static string SetContent(string content)
        {
            content = content.Replace(@"</br>", @"");
            content = content.Replace(@"</a>", @"");
            content = content.Replace(@"<div>", @"");
            content = content.Replace(@"</div>", @"");
            content = content.Replace(@"<p>", @"");
            content = content.Replace(@"</p>", @"");
            content = content.Replace(@"/>", @"");
            content = content.Replace(@"content=""", @"");
            return content;
        }
        public static string RemoveHTMLTags(string content)
        {                       
            Regex regex = new Regex("\\<[^\\>]*\\>");
            content = regex.Replace(content, String.Empty);
            content = content.Trim();
            return content;
        }
        public static string SetName(string fileName)
        {
            string fileNameReturn = fileName;
            fileNameReturn = fileNameReturn.ToLower();
            fileNameReturn = fileNameReturn.Replace("’", "-");
            fileNameReturn = fileNameReturn.Replace("“", "-");
            fileNameReturn = fileNameReturn.Replace("--", "-");
            fileNameReturn = fileNameReturn.Replace("+", "-");
            fileNameReturn = fileNameReturn.Replace("/", "-");
            fileNameReturn = fileNameReturn.Replace(@"\", "-");
            fileNameReturn = fileNameReturn.Replace(":", "-");
            fileNameReturn = fileNameReturn.Replace(";", "-");
            fileNameReturn = fileNameReturn.Replace("%", "-");
            fileNameReturn = fileNameReturn.Replace("`", "-");
            fileNameReturn = fileNameReturn.Replace("~", "-");
            fileNameReturn = fileNameReturn.Replace("#", "-");
            fileNameReturn = fileNameReturn.Replace("$", "-");
            fileNameReturn = fileNameReturn.Replace("^", "-");
            fileNameReturn = fileNameReturn.Replace("&", "-");
            fileNameReturn = fileNameReturn.Replace("*", "-");
            fileNameReturn = fileNameReturn.Replace("(", "-");
            fileNameReturn = fileNameReturn.Replace(")", "-");
            fileNameReturn = fileNameReturn.Replace("|", "-");
            fileNameReturn = fileNameReturn.Replace("'", "-");
            fileNameReturn = fileNameReturn.Replace(",", "-");
            fileNameReturn = fileNameReturn.Replace(".", "-");
            fileNameReturn = fileNameReturn.Replace("?", "-");
            fileNameReturn = fileNameReturn.Replace("<", "-");
            fileNameReturn = fileNameReturn.Replace(">", "-");
            fileNameReturn = fileNameReturn.Replace("]", "-");
            fileNameReturn = fileNameReturn.Replace("[", "-");
            fileNameReturn = fileNameReturn.Replace(@"""", "-");
            fileNameReturn = fileNameReturn.Replace(@" ", "-");
            fileNameReturn = fileNameReturn.Replace("á", "a");
            fileNameReturn = fileNameReturn.Replace("à", "a");
            fileNameReturn = fileNameReturn.Replace("ả", "a");
            fileNameReturn = fileNameReturn.Replace("ã", "a");
            fileNameReturn = fileNameReturn.Replace("ạ", "a");
            fileNameReturn = fileNameReturn.Replace("ă", "a");
            fileNameReturn = fileNameReturn.Replace("ắ", "a");
            fileNameReturn = fileNameReturn.Replace("ằ", "a");
            fileNameReturn = fileNameReturn.Replace("ẳ", "a");
            fileNameReturn = fileNameReturn.Replace("ẵ", "a");
            fileNameReturn = fileNameReturn.Replace("ặ", "a");
            fileNameReturn = fileNameReturn.Replace("â", "a");
            fileNameReturn = fileNameReturn.Replace("ấ", "a");
            fileNameReturn = fileNameReturn.Replace("ầ", "a");
            fileNameReturn = fileNameReturn.Replace("ẩ", "a");
            fileNameReturn = fileNameReturn.Replace("ẫ", "a");
            fileNameReturn = fileNameReturn.Replace("ậ", "a");
            fileNameReturn = fileNameReturn.Replace("í", "i");
            fileNameReturn = fileNameReturn.Replace("ì", "i");
            fileNameReturn = fileNameReturn.Replace("ỉ", "i");
            fileNameReturn = fileNameReturn.Replace("ĩ", "i");
            fileNameReturn = fileNameReturn.Replace("ị", "i");
            fileNameReturn = fileNameReturn.Replace("ý", "y");
            fileNameReturn = fileNameReturn.Replace("ỳ", "y");
            fileNameReturn = fileNameReturn.Replace("ỷ", "y");
            fileNameReturn = fileNameReturn.Replace("ỹ", "y");
            fileNameReturn = fileNameReturn.Replace("ỵ", "y");
            fileNameReturn = fileNameReturn.Replace("ó", "o");
            fileNameReturn = fileNameReturn.Replace("ò", "o");
            fileNameReturn = fileNameReturn.Replace("ỏ", "o");
            fileNameReturn = fileNameReturn.Replace("õ", "o");
            fileNameReturn = fileNameReturn.Replace("ọ", "o");
            fileNameReturn = fileNameReturn.Replace("ô", "o");
            fileNameReturn = fileNameReturn.Replace("ố", "o");
            fileNameReturn = fileNameReturn.Replace("ồ", "o");
            fileNameReturn = fileNameReturn.Replace("ổ", "o");
            fileNameReturn = fileNameReturn.Replace("ỗ", "o");
            fileNameReturn = fileNameReturn.Replace("ộ", "o");
            fileNameReturn = fileNameReturn.Replace("ơ", "o");
            fileNameReturn = fileNameReturn.Replace("ớ", "o");
            fileNameReturn = fileNameReturn.Replace("ờ", "o");
            fileNameReturn = fileNameReturn.Replace("ở", "o");
            fileNameReturn = fileNameReturn.Replace("ỡ", "o");
            fileNameReturn = fileNameReturn.Replace("ợ", "o");
            fileNameReturn = fileNameReturn.Replace("ú", "u");
            fileNameReturn = fileNameReturn.Replace("ù", "u");
            fileNameReturn = fileNameReturn.Replace("ủ", "u");
            fileNameReturn = fileNameReturn.Replace("ũ", "u");
            fileNameReturn = fileNameReturn.Replace("ụ", "u");
            fileNameReturn = fileNameReturn.Replace("ư", "u");
            fileNameReturn = fileNameReturn.Replace("ứ", "u");
            fileNameReturn = fileNameReturn.Replace("ừ", "u");
            fileNameReturn = fileNameReturn.Replace("ử", "u");
            fileNameReturn = fileNameReturn.Replace("ữ", "u");
            fileNameReturn = fileNameReturn.Replace("ự", "u");
            fileNameReturn = fileNameReturn.Replace("é", "e");
            fileNameReturn = fileNameReturn.Replace("è", "e");
            fileNameReturn = fileNameReturn.Replace("ẻ", "e");
            fileNameReturn = fileNameReturn.Replace("ẽ", "e");
            fileNameReturn = fileNameReturn.Replace("ẹ", "e");
            fileNameReturn = fileNameReturn.Replace("ê", "e");
            fileNameReturn = fileNameReturn.Replace("ế", "e");
            fileNameReturn = fileNameReturn.Replace("ề", "e");
            fileNameReturn = fileNameReturn.Replace("ể", "e");
            fileNameReturn = fileNameReturn.Replace("ễ", "e");
            fileNameReturn = fileNameReturn.Replace("ệ", "e");
            fileNameReturn = fileNameReturn.Replace("đ", "d");
            fileNameReturn = fileNameReturn.Replace("--", "-");
            return fileNameReturn;
        }
        #endregion
    }
}
