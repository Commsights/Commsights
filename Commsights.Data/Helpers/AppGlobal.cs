using Commsights.Data.Enum;
using Commsights.Data.Models;
using Commsights.Data.Repositories;
using Microsoft.Extensions.Caching.Memory;
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
        public static string HourCode => DateTime.Now.ToString("yyyyMMddHH");

        public static string InitGuiCode => Guid.NewGuid().ToString();
        #endregion

        #region AppSettings 
        public static int DailyReportColumnDatePublishID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportColumnDatePublishID").Value);
            }
        }
        public static int DailyReportColumnCategoryID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportColumnCategoryID").Value);
            }
        }
        public static int DailyReportColumnCompanyID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportColumnCompanyID").Value);
            }
        }
        public static int DailyReportColumnSentimentID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportColumnSentimentID").Value);
            }
        }

        public static int DailyReportColumnHeadlineVietnameseID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportColumnHeadlineVietnameseID").Value);
            }
        }
        public static int DailyReportColumnHeadlineEnglishID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportColumnHeadlineEnglishID").Value);
            }
        }
        public static int DailyReportColumnMediaTitleID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportColumnMediaTitleID").Value);
            }
        }
        public static int DailyReportColumnMediaTypeID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportColumnMediaTypeID").Value);
            }
        }
        public static int DailyReportColumnPageID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportColumnPageID").Value);
            }
        }
        public static int DailyReportColumnAdvertisementID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportColumnAdvertisementID").Value);
            }
        }
        public static int DailyReportColumnSummaryID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportColumnSummaryID").Value);
            }
        }
        public static int DailyReportSectionDataID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportSectionDataID").Value);
            }
        }
        public static int DailyReportSectionSummaryID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportSectionSummaryID").Value);
            }
        }
        public static int DailyReportSectionExtraID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportSectionExtraID").Value);
            }
        }
        public static string WebsiteHTML
        {
            get
            {
                return "<a target='_blank' href='" + CommsightsWebsite + "' title='" + CommsightsWebsiteDisplay + "'>" + CommsightsWebsiteDisplay + "</a>";
            }
        }
        public static string PhoneReportHTML
        {
            get
            {
                return "<a target='_blank' href='" + PhoneReportURLFUll + "' title='" + PhoneReport + "'>" + PhoneDisplay + "</a>";
            }
        }
        public static string EmailReportHTML
        {
            get
            {
                return "<a target='_blank' href='" + EmailReportURLFUll + "' title='" + EmailReport + "'>" + EmailReport + "</a>";
            }
        }
        public static string FacebookHTML
        {
            get
            {
                return "<a target='_blank' href='" + FacebookURLFUll + "' title='" + Facebook + "'>" + Facebook + "</a>";
            }
        }
        public static string GoogleMapHTML
        {
            get
            {
                return "<a target='_blank' href='" + GoogleMapURLFUll + "' title='" + AddressReport + "'>" + AddressReport + "</a>";
            }
        }
        public static string CommsightsWebsite
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CommsightsWebsite").Value;
            }
        }
        public static string CommsightsWebsiteDisplay
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CommsightsWebsiteDisplay").Value;
            }
        }
        public static string URLCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("URLCode").Value;
            }
        }
        public static string CompanyTitleEnglish
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CompanyTitleEnglish").Value;
            }
        }
        public static string TaxCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("TaxCode").Value;
            }
        }
        public static string PhoneDisplay
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("PhoneDisplay").Value;
            }
        }
        public static string PhoneReport
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("PhoneReport").Value;
            }
        }
        public static string EmailReport
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("EmailReport").Value;
            }
        }
        public static string AddressReport
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("AddressReport").Value;
            }
        }
        public static string EmailReportURLFUll
        {
            get
            {
                string result = "https://mail.google.com/mail/u/0/?view=cm&fs=1&to=" + EmailReport + "&su=Hi_CommSights&body=https://www.commsights.com/&tf=1" + EmailReport;
                return result;
            }
        }
        public static string PhoneReportURLFUll
        {
            get
            {
                string result = "tel:" + PhoneReport;
                return result;
            }
        }
        public static string FacebookURLFUll
        {
            get
            {
                string result = "https://www.facebook.com/" + Facebook;
                return result;
            }
        }
        public static string GoogleMapURLFUll
        {
            get
            {
                string result = "https://www.google.com/maps/d/u/0/viewer?mid=" + GoogleMapID;
                return result;
            }
        }
        public static string Background_Logo_Opacity10_1400_1000URLFUll
        {
            get
            {
                string result = Domain + Images + "/" + Background_Logo_Opacity10_1400_1000;
                return result;
            }
        }
        public static string GoogleMapID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("GoogleMapID").Value;
            }
        }
        public static string ReportDailyTitle
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("ReportDailyTitle").Value;
            }
        }
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
        public static int DailyReportDataID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportDataID").Value);
            }
        }
        public static int DailyReportSummaryID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportSummaryID").Value);
            }
        }
        public static int DailyReportExtraID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportExtraID").Value);
            }
        }
        public static int PositiveID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("PositiveID").Value);
            }
        }
        public static int NeutralID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("NeutralID").Value);
            }
        }
        public static int NegativeID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("NegativeID").Value);
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
        public static int CompanyFeatureID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("CompanyFeatureID").Value);
            }
        }
        public static int CompanyMentionID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("CompanyMentionID").Value);
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
        public static string Channel
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Channel").Value;
            }
        }
        public static string KeywordNegative
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("KeywordNegative").Value;
            }
        }
        public static string KeywordPositive
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("KeywordPositive").Value;
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
        public static string File
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("File").Value;
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
        public static string SendMailSuccess
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("SendMailSuccess").Value;
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
        public static string Background_Logo_Opacity10_1400_1000
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Background_Logo_Opacity10_1400_1000").Value;
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
        public static string FTPDownloadReprotDaily
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("FTPDownloadReprotDaily").Value;
            }
        }
        public static string URLDownloadReprotDaily
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("URLDownloadReprotDaily").Value;
            }
        }
        public static string HTML
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("HTML").Value;
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
                string result = AppGlobal.DomainMain + AppGlobal.Images + "/" + AppGlobal.Loading;
                return result;
            }
        }
        public static string LogoURLFull
        {
            get
            {
                string result = AppGlobal.DomainMain + AppGlobal.Images + "/" + AppGlobal.Logo;
                return result;
            }
        }
        public static string Logo01URLFull
        {
            get
            {
                string result = AppGlobal.DomainMain + AppGlobal.Images + "/" + AppGlobal.Logo01;
                return result;
            }
        }
        public static string Logo160_160URLFull
        {
            get
            {
                string result = AppGlobal.DomainMain + AppGlobal.Images + "/" + AppGlobal.Logo160_160;
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
        public static string DomainMain
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("DomainMain").Value;
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
        public static string DomainSub
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("DomainSub").Value;
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
        public static string TagCode
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("TagCode").Value;
            }
        }
        public static string SMTPServer
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("SMTPServer").Value;
            }
        }

        public static int SMTPPort
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("SMTPPort").Value);
            }
        }

        public static int IsMailUsingSSL
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("IsMailUsingSSL").Value);
            }
        }
        public static string IsMailBodyHtml
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("IsMailBodyHtml").Value;
            }
        }
        public static string MasterEmailUser
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("MasterEmailUser").Value;
            }
        }
        public static string MasterEmailPassword
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("MasterEmailPassword").Value;
            }
        }
        public static string MasterEmailDisplay
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("MasterEmailDisplay").Value;
            }
        }
        public static string MasterEmailSubject
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("MasterEmailSubject").Value;
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
        public static int CheckContentAndKeyword(string content, string keyword)
        {
            int check = 0;
            int checkSub = 0;
            int index = content.IndexOf(keyword);
            if (index == 0)
            {
                checkSub = checkSub + 1;
            }
            else
            {
                string word01 = content[index - 1].ToString();
                if ((word01 == " ") || (word01 == "(") || (word01 == "[") || (word01 == "{") || (word01 == "<"))
                {
                    checkSub = checkSub + 1;
                }
            }
            int index001 = index + keyword.Length;
            if (index001 < content.Length)
            {
                string word02 = content[index001].ToString();
                if ((word02 != " ") && (word02 != ",") && (word02 != ".") && (word02 != ";") && (word02 != ")") && (word02 != "]") && (word02 != "}") && (word02 != ">"))
                {
                }
                else
                {
                    checkSub = checkSub + 1;
                }
            }
            if (checkSub == 2)
            {
                check = 1;
            }
            return check;
        }
        public static string GetContentByURL(string url, int ParentID)
        {
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            string html = "";
            try
            {
                html = webClient.DownloadString(url);
            }
            catch
            {

            }
            string content = html;
            if (!string.IsNullOrEmpty(content))
            {
                content = content.Replace(@"</body>", @"</body>~");
                content = content.Split('~')[0];
                content = content.Replace(@"</head>", @"~");
                if (content.Split('~').Length > 1)
                {
                    content = content.Split('~')[1];
                    content = content.Replace(@"<footer", @"~");
                    content = content.Split('~')[0];
                    switch (ParentID)
                    {
                        default:
                            content = content.Replace(@"</h1>", @"~");
                            int length = content.Split('~').Length;
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 492:
                            content = content.Replace(@"</h1>", @"~");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[2];
                            }
                            break;
                        case 168:
                            content = content.Replace(@"(ANTV)", @"~(ANTV)");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 187:
                            content = content.Replace(@"<div class=""article-body cmscontents"">", @"~<div class=""article-body cmscontents"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 196:
                            content = content.Replace(@"<div id=""content_detail_news"">", @"~<div id=""content_detail_news"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 229:
                            content = content.Replace(@"<!-- main content -->", @"~");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 278:
                            content = content.Replace(@"<div id=""ArticleContent""", @"~<div id=""ArticleContent""");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 294:
                            content = content.Replace(@"<div data-check-position=""af_detail_body_start"">", @"~<div data-check-position=""af_detail_body_start"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 295:
                            content = content.Replace(@"<div class=""sapo_news"">", @"~<div class=""sapo_news"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 296:
                            content = content.Replace(@"<div class=""article-content"">", @"~<div class=""article-content"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 301:
                            content = content.Replace(@"<div class=""article-detail"">", @"~<div class=""article-detail"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 343:
                            content = content.Replace(@"<div class=""boxdetail"">", @"~<div class=""boxdetail"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 363:
                            content = content.Replace(@"<div id=""divContents""", @"~<div id=""divContents""");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 378:
                            content = content.Replace(@"<div class=""main-content"">", @"~<div class=""main-content"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 394:
                            content = content.Replace(@"<strong class=""d_Txt"">", @"~<strong class=""d_Txt"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 420:
                            content = content.Replace(@"<div id=""sevenBoxNewContentInfo"" class=""sevenPostContentCus"">", @"~<div id=""sevenBoxNewContentInfo"" class=""sevenPostContentCus"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 421:
                            content = content.Replace(@"<div class=""detail-content"">", @"~<div class=""detail-content"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 454:
                            content = content.Replace(@"<span id=""ctl00_mainContent_bodyContent_lbBody"">", @"~<span id=""ctl00_mainContent_bodyContent_lbBody"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 530:
                            content = content.Replace(@"<div id=""admwrapper"">", @"~<div id=""admwrapper"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 851:
                            content = content.Replace(@"<div id=""wrap-detail"" class=""cms-body"">", @"~<div id=""wrap-detail"" class=""cms-body"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 855:
                            content = content.Replace(@"<div class=""knc-content"" id=""ContentDetail"">", @"~<div class=""knc-content"" id=""ContentDetail"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 859:
                            content = content.Replace(@"<div type=""RelatedOneNews"" class=""VCSortableInPreviewMode"">", @"~<div type=""RelatedOneNews"" class=""VCSortableInPreviewMode"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 865:
                            content = content.Replace(@"<div type=""abdf"">", @"~<div type=""abdf"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 870:
                            content = content.Replace(@"<div class=""entry-content"">", @"~<div class=""entry-content"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 969:
                            content = content.Replace(@"<div id=""cotent_detail"" class=""pkg"">", @"~<div id=""cotent_detail"" class=""pkg"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 1002:
                            content = content.Replace(@"<!-- BEGIN .shortcode-content -->", @"~");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 1052:
                            content = content.Replace(@"<article class=""article-content"" itemprop=""articleBody"">", @"~<article class=""article-content"" itemprop=""articleBody"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 1177:
                            content = content.Replace(@"(Tieudung.vn)", @"~(Tieudung.vn)");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 1289:
                            content = content.Replace(@"<div class=""entry-content"">", @"~<div class=""entry-content"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 1307:
                            content = content.Replace(@"<div class=""entry-content"">", @"~<div class=""entry-content"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 1352:
                            content = content.Replace(@"<div class=""inline-image-caption", @"~<div class=""inline-image-caption");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 1357:
                            content = content.Replace(@"<div class=""content article-body cms-body AdAsia""", @"~<div class=""content article-body cms-body AdAsia""");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 1383:
                            content = content.Replace(@"<div class=""article-content""", @"~<div class=""article-content""");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 1398:
                            content = content.Replace(@"<div class=""article__body""", @"~<div class=""article__body""");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                        case 1403:
                            content = content.Replace(@"<div class=""edittor-content box-cont clearfix"" itemprop=""articleBody"">", @"~<div class=""edittor-content box-cont clearfix"" itemprop=""articleBody"">");
                            if (content.Split('~').Length > 1)
                            {
                                content = content.Split('~')[1];
                            }
                            break;
                    }
                    content = content.Replace(@"<div class=""VnnAdsPos clearfix"" data-pos=""vt_article_bottom"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<ul class=""list-news hidden"" data-campaign=""Box-Related"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<!-- Begin Dable In_article Widget / For inquiries, visit http://dable.io -->", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""tagandnetwork"" id=""tagandnetwork"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"</em></p><div class=""inner-article"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""print_back"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""func-bot"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""news-other row10""><div class=""cate-header"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""sharing_tool atbottom"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""CustomContentObject LinkInlineObject"" data-type=""insertlinkbottom"" contenteditable=""false"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""news_relate_one d-flex mb30"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div id=""adsctl00_mainContent_AdsHome1"" class=""qc simplebanner"" data-position=""PC_below_Author"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""VCSortableInPreviewMode link-content-footer IMSCurrentEditorEditObject"" type=""link"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""inner-article"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""ads_detail"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<!-- Composite Start -->", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""attachmentsContainer"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""news-share article-social clearfix"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""tags-wp"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div id=""plhMain_NewsDetail1_divTags"" class=""keysword"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<p style=""text-align: center;""><iframe allowfullscreen="""" frameborder=""0"" height=""400px""", @"~<p style=""text-align: center;""><iframe allowfullscreen="""" frameborder=""0"" height=""400px""");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""related-news"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""tag_detail"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""tag_detail"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""over-dk"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""share_bt"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""w640right"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""link-source-wrapper is-web clearfix fr"" id=""urlSourceCafeF"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<center style=""margin:10px 0px; float:left;width:100%;margin:auto;"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<!-- CAND-detail-338x280 -->", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""dtContentTxtAuthor"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""explus_related_1404022217 explus_related_1404022217_bottom _tlq"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""VCSortableInPreviewMode link-content-footer"" type=""link"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div>Bạn đang đọc bài viết", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<p><strong><span style=""color: rgb(51, 51, 255);"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div data-check-position=""vnb_detail_body_end"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div id=""ContentPlaceHolder1_Detail1_pnTlq"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""social pkg""  style=""margin-top:20px;clear:both"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<h2 class=""related-news-title red-title"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""like_share"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<!--CBV1 -->", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<!-- END .shortcode-content -->", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""related-inline-story clearfix cms-relate"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div itemprop=""publisher"" itemscope", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div itemprop=""publisher"" itemscope", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<span style=""text-decoration:underline;""><strong>Xem thêm:", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""_MB_ITEM_SOURCE_URL"" align=""right"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<i class=""social-sticky-stop""></i>", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""vnnews-ft-post"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""vnnews-ft-post"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""topic"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<p class=""pSource"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""social-btn"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""sharemxhbot"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""article__foot"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""ads-item lh0"" data-zone=""native_1"">", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"Mời quý độc giả theo dõi các chương trình đã phát sóng của Đài Truyền hình Việt Nam", @"~");
                    content = content.Split('~')[0];
                    content = content.Replace(@"<div class=""like-share""><div class=""like"">", @"~");
                    content = content.Split('~')[0];
                }
                content = RemoveHTMLTags(content);
            }
            return content;
        }
        public static void GetParametersByURL(Product model)
        {
            model.Title = "";
            model.Description = "";
            model.Author = "";
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            string html = webClient.DownloadString(model.URLCode);
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
        public static List<string> SetEmailContact(string content)
        {
            List<string> list = new List<string>();
            foreach (string contact in content.Split(';'))
            {
                string email = "";
                if (contact.Split('<').Length > 1)
                {
                    email = contact.Split('<')[1];
                    email = email.Replace(@">", @"");

                }
                else
                {
                    email = contact.Trim();
                }
                if (!string.IsNullOrEmpty(email))
                {
                    list.Add(email.Trim());
                }
            }
            return list;
        }
        public static List<string> SetContentByDauChamPhay(string content)
        {
            List<string> list = new List<string>();
            foreach (string item in content.Split(';'))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    list.Add(item.Trim());
                }
            }
            return list;
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
        public static void GetURLByURLAndi(Product model, List<ProductProperty> listProductPropertyURLCode, int RequestUserID)
        {
            string html = "";
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = System.Text.Encoding.UTF8;
                html = webClient.DownloadString(model.ImageThumbnail);
                if (html.Contains(@"andi.vn"))
                {
                    model.IsVideo = false;
                    string content = html;
                    if (content.Contains(@"onclick=""showVideo('"))
                    {
                        content = content.Replace(@"onclick=""showVideo('", @"~");
                        if (content.Split('~').Length > 1)
                        {
                            content = content.Split('~')[1];
                            content = content.Replace(@"'", @"~");
                            content = content.Split('~')[0];
                            model.Image = "http://video.andi.vn/" + content;
                            model.IsVideo = true;
                        }
                    }
                    html = html.Replace(@"<div style=""text-align:center;"">", @"~");
                    if (html.Split('~').Length > 1)
                    {
                        html = html.Split('~')[1];
                        html = html.Replace(@"</div>", @"~");
                        html = html.Split('~')[0];
                        html = html.Replace(@"src='", @"~");
                        html = html.Replace(@"'", @"~");
                        foreach (string url in html.Split('~'))
                        {
                            if (url.Contains(@"http://"))
                            {
                                ProductProperty productProperty = new ProductProperty();
                                productProperty.GUICode = model.GUICode;
                                productProperty.Note = url;
                                productProperty.ProductID = 0;
                                productProperty.Code = AppGlobal.URLCode;
                                productProperty.Initialization(InitType.Insert, RequestUserID);
                                listProductPropertyURLCode.Add(productProperty);
                            }
                        }
                    }
                }
                else
                {

                    html = html.Replace(@"rel=""canonical""", @"~");
                    if (html.Split('~').Length > 1)
                    {
                        html = html.Split('~')[1];
                        html = html.Replace(@"href=""", @"~");
                        if (html.Split('~').Length > 1)
                        {
                            html = html.Split('~')[1];
                            html = html.Split('"')[0];
                            model.URLCode = html.Trim();
                        }
                    }
                }
            }
            catch
            {
            }
        }
        public void GetAuthorFromURL(Product product)
        {
            string html = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(product.URLCode);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            html = reader.ReadToEnd();
                        }
                        string author = html;
                        switch (product.ParentID)
                        {
                            case 1:
                                author = author.Replace(@"</article>", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"</h1>", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[1];
                                    author = author.Replace(@"<p class=""Normal"" style=""text-align:right;"">", @"~");
                                    author = author.Split('~')[author.Split('~').Length - 1];
                                    author = AppGlobal.RemoveHTMLTags(author);
                                }
                                break;
                            case 5:
                                author = author.Replace(@"<script type=""application/ld+json"">", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"dable:author", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[1];
                                    author = AppGlobal.SetContent(author);
                                    author = author.Replace(@"""", @"");
                                }
                                break;
                            case 6:
                                author = author.Replace(@"<div class=""tagandnetwork"" id=""tagandnetwork"">", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"<div class=""author"">", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[1];
                                    author = AppGlobal.RemoveHTMLTags(author);
                                }
                                break;
                            case 8:
                                author = author.Replace(@"<div class=""inner-article"">", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"<div class=""VnnAdsPos clearfix"" data-pos=""vt_article_bottom"">", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"<div class=""bold ArticleLead"">", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[1];
                                    author = author.Replace(@"<span class=""bold"">", @"~");
                                    author = author.Split('~')[author.Split('~').Length - 1];
                                    author = author.Replace(@"<p align=""justify"">", @"~");
                                    author = author.Split('~')[author.Split('~').Length - 1];
                                    author = author.Replace(@"</table>", @"~");
                                    author = author.Split('~')[author.Split('~').Length - 1];
                                    author = AppGlobal.RemoveHTMLTags(author);
                                }
                                break;
                            case 263:
                                author = author.Replace(@"<time class=""op-published""", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"<b  data-field=""author"" >", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[1];
                                    author = AppGlobal.RemoveHTMLTags(author);
                                    author = author.Replace(@"|", @"");
                                }
                                break;
                            case 278:
                                author = author.Replace(@"<div class=""inner-article"">", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"<strong>", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[author.Split('~').Length - 1];
                                    author = AppGlobal.RemoveHTMLTags(author);
                                }
                                break;
                            case 294:
                                author = author.Replace(@"<span class=""afcba-source"">", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"data-role=""author"">", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[author.Split('~').Length - 1];
                                    author = AppGlobal.RemoveHTMLTags(author);
                                    author = author.Replace(@",", @"");
                                }
                                break;
                            case 295:
                                author = author.Replace(@"<div class=""soucre_news"">", @"~");
                                author = author.Split('~')[0];
                                author = author.Replace(@"<p style=""text-align: right;"">", @"~");
                                if (author.Split('~').Length > 1)
                                {
                                    author = author.Split('~')[author.Split('~').Length - 1];
                                    author = AppGlobal.RemoveHTMLTags(author);
                                }
                                break;
                        }
                        author = author.Trim();
                        product.Author = author;
                    }
                }
            }
            catch
            {
            }
        }
        public static void GetURL(Product product)
        {
            string url = product.URLCode;
            string f0 = "";
            string f1 = "";
            switch (product.ParentID)
            {
                case 295:
                    f0 = url.Split('-')[url.Split('-').Length - 1];
                    f1 = f0;
                    f1 = f1.Replace(@".html", @".htm");
                    f1 = f1.Replace(@"n", @"");
                    url = url.Replace(f0, f1);
                    product.URLCode = url;
                    break;
                case 182:
                    url = "https://baobinhphuoc.com.vn/Content/" + url;
                    product.URLCode = url;
                    break;
                case 395:
                    url = url.Replace(@".vn", @".vn/");
                    product.URLCode = url;
                    break;
                case 506:
                    f0 = url.Split('=')[url.Split('=').Length - 1];
                    url = "https://danang.gov.vn/web/guest/chi-tiet?id=" + f0;
                    product.URLCode = url;
                    break;
            }
        }
        #endregion
    }
}
