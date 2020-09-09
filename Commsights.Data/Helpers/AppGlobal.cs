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
        public static int Hour
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("Hour").Value);
            }
        }
        public static int IndustryID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("IndustryID").Value);
            }
        }
        public static int SegmentID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("SegmentID").Value);
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
        public static int TinDoanhNghiepID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("TinDoanhNghiepID").Value);
            }
        }
        public static int TinNganhID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("TinNganhID").Value);
            }
        }
        public static int TinSanPhamID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("TinSanPhamID").Value);
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
        public static string ReportType
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("ReportType").Value;
            }
        }
        public static string DailyReportSection
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("DailyReportSection").Value;
            }
        }
        public static string DailyReportColumn
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("DailyReportColumn").Value;
            }
        }
        public static string Company
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Company").Value;
            }
        }
        public static string Industry
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Industry").Value;
            }
        }
        public static string Segment
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Segment").Value;
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
        public static List<int> InitializationHourToList()
        {
            List<int> list = new List<int>();
            for (int i = 1; i < 25; i++)
            {
                list.Add(i);
            }
            return list;
        }
        public static bool CheckContentAndKeyword(string content, string keyword)
        {
            bool check = false;
            int index = content.IndexOf(keyword);
            if (index == 0)
            {
                check = true;
            }
            else
            {
                if (content[index - 1].ToString() == " ")
                {
                    check = true;
                }                
            }
            int index001 = index + keyword.Length;
            if (index001 < content.Length - 1)
            {
                if ((content[index001].ToString() != " ") || (content[index001].ToString() != ",") || (content[index001].ToString() != ".") || (content[index001].ToString() != ";"))
                {
                    check = false;
                }
            }
            return check;
        }
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
                    content = content.Replace(@"<!--/.article-body-->", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<ul class=""list-news hidden"" data-campaign=""Box-Related"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div id=""box_tag"" class=""width_common mb30"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<!--end text-right-tt-->", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div id=""admzonek1broje0"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""print_back"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""tag_detail"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""soucre_news"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""func-bot"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""attachmentsContainer"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""news-share article-social clearfix"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""social-plugins"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<center style=""margin:10px 0px;float:left;width:100%;margin:auto;"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""news-other row10"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<p class=""keywords"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""block-interesting-news"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""news_relate_one d-flex mb30"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div id=""adsctl00_mainContent_AdsHome1""", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div id=""plhMain_NewsDetail1_divTags"" class=""keysword"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""sharefb"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""article-dmca"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div id=""stickysocialbot"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""over-dk"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div data-check-position=""body_end"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""link-source-wrapper is-web clearfix fr"" id=""urlSourceCafeF"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<!-- CAND-detail-336x280 -->", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<!-- CAND-detail-338x280 -->", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""tag-header"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""explus_related_1404022217 explus_related_1404022217_bottom _tlq"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""boxsamethread"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""art-social pkg"" style=""padding: 10px 10px 0;"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<span style=""color: rgb(51, 51, 255);"">Đọc thêm", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""VCSortableInPreviewMode link-content-footer IMSCurrentEditorEditObject"" type=""link"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div id=""ContentPlaceHolder1_Detail1_pnTlq"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"div class=""social pkg""  style=""margin-top:20px;clear:both"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<h2 class=""related-news-title red-title"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""inner-article"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""like_share"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<!--CBV1 -->", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""wp_rp_wrap  wp_rp_vertical_s"" id=""wp_rp_first"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""dtContentTxtAuthor"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""module-tags d-flex flex-wrap align-items-center border-top-3 mt-4 pt-3"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<!-- Begin Dable In_article Widget / For inquiries, visit http://dable.io -->", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""banner fyi-banner-inner fyi--InnerArticle mobile-hidden"" style=""display: none;"" position=""Mobile_AdsArticleAfter3"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class='share_bottom'>", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""like-share-mail bottom"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<span style=""text-decoration:underline;""><strong>Xem thêm:</strong>", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""tagandnetwork"" id=""tagandnetwork"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""_MB_ITEM_SOURCE_URL"" align=""right"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""vnbcb-author bottom"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""news-tags"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""vnnews-ft-post"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""details__content"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""vnbcb-author bottom"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<p class=""pSource"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""social-btn"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""sharemxhbot"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""footer-content  width_common"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""like-share"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""tags"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div type=""RelatedOneNews"" class=""VCSortableInPreviewMode"">", @"~");
                    content = content.Split('~')[0];

                    //content = content.Replace(@"<section id=""ADS_153_15s_container""", @"~<section id=""ADS_153_15s_container""");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""share-buttons"">", @"~<div class=""share-buttons"">");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<section class=""art-tag"" style=""margin-bottom:20px"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<span class=""terms-label"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""abd_vidinpage"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""single-post-banner"" style=""width: 100%;"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<span class=""icon-box-interested-main""></span>Tin liên quan", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<ul class=""share-social hidden-print"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""HotBlock"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<span class=""btn-copy-link-source2"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""posts-nav-link"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""ads_detail"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""wpdevar_comment_3"" style=""width:100%;text-align:left;"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<span class=""post-date small text-secondary"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""rmp-rating-widget__icons"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""entry-terms post-tags clearfix style-24"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""print_back"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""tag_detail"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- AddThis Advanced Settings above via filter on the_content -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""productDetailGallery"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""productDetailGallery"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""recommend"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<ul class=""share-social hidden-print"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""likeShare left w100pt"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""soucre_news"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""func-bot"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""attachmentsContainer"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""post-related section-container clearfix"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<section id=""PC_POSTARTICLE_ATGT_container""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""social-share"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""padT15"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""jeg_sharelist"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""blog-share text-center"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""news-share article-social clearfix"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""bawmrp"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""sharing clearfix"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""VCSortableInPreviewMode noCaption active"" type=""Photo"" style="""">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""post-related"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<center style=""margin:10px 0px;float:left;width:100%;margin:auto;"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- end post-author -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""article-tool"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""article-event-relate"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""box-feedback""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""tool-like"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""news-other row10"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<p class=""pAuthor"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- <div id=""newsauthor""></div>", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""rlink2"" class=""rlink"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""file_attachment"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""td-g-rec td-g-rec-id-content_bottom "">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""addthis_inline_share_toolbox"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""linkshare"" data=""200"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""ctl00_bodyContent_ND_CM1_pnComment"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<p class=""keywords"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<ul class=""the-article-share"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<ul class=""kbwscwlr-list"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""CustomContentObject LinkInlineObject"" data-type=""insertlinkbottom"" contenteditable=""false"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<ins class=""ads-by-google"" data-ad-name=""PC_Detail_TextLink""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""keywords"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""news_relate_one d-flex mb30"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""newsRight"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""col-xs-12 detail-sharelike"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""others-new"" class=""others-new row"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""SEOtool clear"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""share-post"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!--/.sharrre-container-->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""related-posts"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""tags""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""clear mgBt5"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""boxothers"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- list tag -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<iframe rel=""nofollow""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""messenger_single"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""related-tags"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""wpdevar_comment_1""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<li style=""float:left;list-style:none;margin-right: 10px"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""td-post-source-tags"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""related-post margin-top-20 clearfix"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""ads-inline-buttom"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<p class=""news-tag-list"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""article-tool"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""tagBox left w100pt"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""relatedthemeposts clearfix"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""pen_name""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""news-source-bottom""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""linkshare"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div style=""font-size: 20px; margin-top: 15px""><b>Mời bạn đọc thêm:</b>", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""abh_box abh_box_down abh_box_fancy"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""image_widget2-18"" class=""widget widget_sp_image"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""quads-location quads-ad5"" id=quads-ad5", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""print"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- #content -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""monkey-group-like-comment-fb-bic"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""article__tag clearfix"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""rticle-tags mg-b-10"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<ul style=""list-style-type: disc;"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""post-tags"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""ctl00_ContentPlaceHolder1_Adv1_idAdv"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""adsctl00_mainContent_AdsHome1""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""a-single a-450"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""entry-tags mh-clearfix"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""pt-comment"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""commentDetail"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<ul class=""clearfix tag-list"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<table class=""rl center"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<td id=""lanxem"" width=""100%"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""row_view_select"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<table class=""__mb_article_in_image"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""comment-content"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""myshow"" style=""display:none"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- end div.author -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""flex-s-s"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- b-maincontent -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""block_tag marginbottom10 margintop10"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- tags -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""authorDetails"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- Detail end -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!--end content_single-->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""linkshare"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""margin-bottom-lg"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""plhMain_NewsDetail1_divTags"" class=""keysword"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""cp-post-share-tags"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""commentbox"" style=""display: block;"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""edn_socialPrintWrapper"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- BVN response -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""td-a-rec td-a-rec-id-content_bottom "">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""wrap-adv-56"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<span itemprop=""publisher""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""news-other"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""titleDdtail"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class='heateor_sss_sharing_container heateor_sss_horizontal_sharing'", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""category tags clearfix""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<blockquote class=""wp-embedded-content""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div align=""right"" class=""comments"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<hr class=""wp-block-separator is-style-wide""/>", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""item-rating"" class=""rating col-ex col-2"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""article-dmca"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- end article section -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""footer-social"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""single-tag-wrap"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!--Tin tức bình thường(Có Media)-->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""article-social"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<span class=""starRating"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""news_column panel panel-default newsdetailbox"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""UISocialShare"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""addtoany_share_save_container addtoany_content addtoany_content_bottom"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""post-item-metadata entry-meta"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""article_related padB10"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""stickysocialbot"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""keywords"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""share-like bottom"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""end-post-share"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""my-3"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- In_Article_Video_Bottom -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""link-source"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""box1""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class='list_tag_trend'", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""hot-tag-of-detail"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!--  ad tags Size: 300x250 ZoneId:1474826-->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""commentbox"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""block-league-news news-related"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class='art_author'>", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- Composite Start -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<script>!function(f,b,e,v,n,t,s)", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- Bài liên quan -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""jeg_share_bottom_container"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""kksr-stars"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""keyreference"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""over-dk"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""p-author"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""link-source-wrapper is-web clearfix fr"" id=""urlSourceCafeF"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""anfz_", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- Latest compiled and minified JavaScript -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id='wpd-post-rating'", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""box-widget socialshare"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!--						<div class=""comFB"">-->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<span id=""btn-quan-tam"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""button_bookmark""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<span id=""tuongphan"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""share-star"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""social-plugins"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<p class=""bawpvc-ajax-counter""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div style=""display: inline-block; width: 100%; border-top: 3px solid #e0e0e0; margin-top: 12px; margin-bottom: 12px;"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""likebox"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""relate-ct-gr"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""social_share width_common""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""box-sharing "">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""tt-detail mt20 mb20"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""noprint"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""tptn_counter""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""col-sm-12 col-md-6 fLeft padL0 padB0 marginB0"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<p id=""stringrating"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""ctl00_cplhLoadMainContent_pnlRelate"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""bottom_content"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""tag-header"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""fb-like""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- .post-content -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!--Facebook Comment-->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""fb-root"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""nv-source"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""tinlienquan"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<p class=""credit"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- the-content -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""a2a_kit a2a_kit_size_32 addtoany_list""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!--<div class=""details__follow hide - mobile"">-->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""post-tag hide"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<table class=""__mb_article_in_image __mb_article_in_image_large"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""banner-detail _MB_BANNER_DETAIL clearfix"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""ctl00_mainContent_articleView_pnShowImageOtherArticle"" class=""pnOtherArticle"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<span class=""sharer-action""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<ul id=""ContentPlaceHolder1_ctl01_svpagelist"" class=""pagelist"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""others-new"" class=""other row15"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""chi-tiet-footer"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""width_common space_bottom_10"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""news-other-related""", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<section class=""dgx-related-section"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- .entry-content -->", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div id=""xds-155"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""share-other"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class='ads-quangcao'>", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""dt-social-bt"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""post-footer"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""side-block border-block mt-15 tieu-diem-trong-ngay"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<div class=""single-bottom"">", @"~");
                    //content = content.Split('~')[0];
                    //content = content.Replace(@"<!-- tab news other -->", @"~");
                    //content = content.Split('~')[0];
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
