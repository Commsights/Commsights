using Commsights.Data.Enum;
using Commsights.Data.Models;
using Commsights.Data.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Xml;

namespace Commsights.Data.Helpers
{
    public struct LinkItem
    {
        public string Href;
        public string Text;
    }
    public class DatePublish
    {
        public DateTime DateTime { get; set; }
        public string Time { get; set; }
        public string DateTimeString { get; set; }
        public int Count { get; set; }
        public bool IsTime { get; set; }
    }
    public class YearFinance
    {
        public int Display { get; set; }
        public YearFinance()
        {
        }
        public static List<YearFinance> GetAllToList()
        {
            List<YearFinance> list = new List<YearFinance>();
            for (int i = AppGlobal.DateBegin; i <= AppGlobal.DateEnd; i++)
            {
                YearFinance model = new YearFinance();
                model.Display = i;
                list.Add(model);
            }
            return list;
        }
    }

    public class MonthFinance
    {
        public int Display { get; set; }
        public MonthFinance()
        {
        }
        public static List<MonthFinance> GetAllToList()
        {
            List<MonthFinance> list = new List<MonthFinance>();
            for (int i = 1; i <= 12; i++)
            {
                MonthFinance model = new MonthFinance();
                model.Display = i;
                list.Add(model);
            }
            return list;
        }
    }
    public class HourFinance
    {
        public int Display { get; set; }
        public HourFinance()
        {
        }
        public static List<HourFinance> GetAllToList()
        {
            List<HourFinance> list = new List<HourFinance>();
            for (int i = 0; i < 24; i++)
            {
                HourFinance model = new HourFinance();
                model.Display = i;
                list.Add(model);
            }
            return list;
        }
    }
    public class SOE
    {
        public int Display { get; set; }
        public SOE()
        {
        }
        public static List<SOE> GetAllToList()
        {
            List<SOE> list = new List<SOE>();
            for (int i = 0; i <= 100; i = i + 5)
            {
                SOE model = new SOE();
                model.Display = i;
                list.Add(model);
            }
            return list;
        }
    }
    public class CorpCopy
    {
        public string Display { get; set; }
        public CorpCopy()
        {
        }
        public static List<CorpCopy> GetAllToList()
        {
            List<CorpCopy> list = new List<CorpCopy>();
            CorpCopy model = new CorpCopy();
            model.Display = "";
            list.Add(model);
            model = new CorpCopy();
            model.Display = "Origin";
            list.Add(model);
            return list;
        }
    }
    public class CodeDataValue
    {
        public int ID { get; set; }
        public string Display { get; set; }
        public CodeDataValue()
        {
        }
        public static List<CodeDataValue> GetAllToList()
        {
            List<CodeDataValue> list = new List<CodeDataValue>();
            CodeDataValue model = new CodeDataValue();
            model.ID = 0;
            model.Display = "";
            list.Add(model);
            model = new CodeDataValue();
            model.ID = 1;
            model.Display = "1";
            list.Add(model);
            return list;
        }
        public static List<CodeDataValue> GetItem0ToList()
        {
            List<CodeDataValue> list = new List<CodeDataValue>();
            CodeDataValue model = new CodeDataValue();
            model.ID = 0;
            model.Display = "";
            list.Add(model);
            return list;
        }
    }
    public class AppGlobal
    {
        #region Init
        public static DateTime InitDateTime => DateTime.Now;

        public static string InitString => string.Empty;

        public static string DateTimeCode => DateTime.Now.ToString("yyyyMMddHHmmss");
        public static string HourCode => DateTime.Now.ToString("yyyyMMddHH");
        public static string DateTimeCodeYearMonthDay => DateTime.Now.ToString("yyyyMMdd");
        public static string InitGuiCode => Guid.NewGuid().ToString();
        #endregion

        #region AppSettings    
        public static string ProductFeature
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("ProductFeature").Value;
            }
        }
        public static string IndustryCategory
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("IndustryCategory").Value;
            }
        }
        public static string TV
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("TV").Value;
            }
        }
        public static string Newspage
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Newspage").Value;
            }
        }
        public static string TotalSize
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("TotalSize").Value;
            }
        }
        public static string FTPScanFiles
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("FTPScanFiles").Value;
            }
        }
        public static string URLScanFiles
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("URLScanFiles").Value;
            }
        }
        public static int AdValue
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("AdValue").Value);
            }
        }
        public static int TierOtherID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("TierOtherID").Value);
            }
        }
        public static int TierPortalID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("TierPortalID").Value);
            }
        }
        public static int TierLocalMediaID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("TierLocalMediaID").Value);
            }
        }
        public static int TierIndustryID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("TierIndustryID").Value);
            }
        }
        public static int TierMassMediaID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("TierMassMediaID").Value);
            }
        }
        public static int FeatureID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("FeatureID").Value);
            }
        }
        public static int MentionID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("MentionID").Value);
            }
        }
        public static int DateBegin
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DateBegin").Value);
            }
        }
        public static int DateEnd
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DateEnd").Value);
            }
        }
        public static string IndustryKeyWord
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("IndustryKeyWord").Value;
            }
        }
        public static string KeyMessage
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("KeyMessage").Value;
            }
        }
        public static string CampaignKeyMessage
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CampaignKeyMessage").Value;
            }
        }
        public static string Campaign
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Campaign").Value;
            }
        }
        public static string CorpCopy
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CorpCopy").Value;
            }
        }
        public static string CategoryMain
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CategoryMain").Value;
            }
        }
        public static string CategorySub
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CategorySub").Value;
            }
        }
        public static string Feature
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Feature").Value;
            }
        }
        public static string Sentiment
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Sentiment").Value;
            }
        }
        public static int DailyReportColumnSegmentID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportColumnSegmentID").Value);
            }
        }
        public static int DailyReportColumnSubCatID
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("DailyReportColumnSubCatID").Value);
            }
        }
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
        public static string SourceAuto
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("SourceAuto").Value;
            }
        }
        public static string SourceGoogle
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("SourceGoogle").Value;
            }
        }
        public static string SourceScan
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("SourceScan").Value;
            }
        }
        public static string SourceAndi
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("SourceAndi").Value;
            }
        }
        public static string SourceYounet
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("SourceYounet").Value;
            }
        }
        public static string EmailSupport
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("EmailSupport").Value;
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
        public static string Tier
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Tier").Value;
            }
        }
        public static string MediaTier
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("MediaTier").Value;
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
        public static int AdvertisementValue
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("AdvertisementValue").Value);
            }
        }
        public static int TierID02
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return int.Parse(builder.Build().GetSection("AppSettings").GetSection("TierID02").Value);
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
        public static string CompanyName
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("CompanyName").Value;
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
        public static string EmailStorage
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("EmailStorage").Value;
            }
        }
        public static string EmailStorageCategory
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("EmailStorageCategory").Value;
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
        public static string URLImagesMembership
        {
            get
            {
                string result = AppGlobal.Images + "/" + AppGlobal.Membership;
                return result;
            }
        }
        public static string Membership
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("Membership").Value;
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
        public static string FTPDownloadReprotMonth
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("FTPDownloadReprotMonth").Value;
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
        public static string URLDownloadReprotMonth
        {
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build().GetSection("AppSettings").GetSection("URLDownloadReprotMonth").Value;
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
            content = content.Replace(@",", @";");
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
            content = content.Replace(@",", @";");
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
        public static string ToUpperFirstLetter(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                string result = "";
                string[] words = title.Split(' ');
                foreach (string word in words)
                {
                    if (word.Trim() != "")
                    {
                        if (word.Length > 1)
                            result += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                        else
                            result += word.ToUpper() + " ";
                    }
                }
                title = result;
            }
            return title;
        }
        public static string SetName(string fileName)
        {
            string fileNameReturn = fileName;
            if (!string.IsNullOrEmpty(fileNameReturn))
            {
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
            }
            return fileNameReturn;
        }
        public static void GetURLByURLAndi(Product model, List<ProductProperty> listProductPropertyURLCode, int RequestUserID)
        {
            model.URLCode = model.ImageThumbnail;
            string html = "";
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = System.Text.Encoding.UTF8;
                html = webClient.DownloadString(model.ImageThumbnail);
                if (html.Contains(@"andi.vn") == true)
                {
                    string html01 = html;
                    html = html.Replace(@"~", @"A");
                    model.IsVideo = false;
                    string content = html;
                    if (content.Contains(@"onclick=""showVideo('") == true)
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
                            if (url.Contains(@"http://") == true)
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
                }
            }
            catch (Exception e)
            {
                string message = e.Message;
            }
        }
        public static List<LinkItem> LinkFinder(string html, string urlRoot)
        {
            List<LinkItem> list = new List<LinkItem>();
            if (!string.IsNullOrEmpty(html))
            {
                Uri root = new Uri(urlRoot);
                string host = root.Host;
                string scheme = root.Scheme;
                string localPath = root.LocalPath;
                MatchCollection m1 = Regex.Matches(html, @"(<a.*?>.*?</a>)", RegexOptions.Singleline);
                foreach (Match m in m1)
                {
                    string value = m.Groups[1].Value;
                    LinkItem i = new LinkItem();
                    string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                    i.Text = t;
                    Match m2 = Regex.Match(value, @"href=\""(.*?)\""", RegexOptions.Singleline);
                    if (m2.Success)
                    {
                        i.Href = m2.Groups[1].Value;
                    }
                    else
                    {
                        string url = value.Replace(@"'", @"""");
                        url = url.Replace(@"href=""", @"~");
                        if (url.Split('~').Length > 1)
                        {
                            url = url.Split('~')[1];
                            url = url.Split('"')[0];
                            i.Href = url;
                        }
                    }
                    if ((!string.IsNullOrEmpty(i.Href)) && (!string.IsNullOrEmpty(i.Text)))
                    {

                        if ((i.Href.Contains(@"http") == false) && (i.Href.Contains(@"#") == false) && (i.Href.Contains(@";") == false) && (i.Href.Contains(@"(") == false) && (i.Href.Contains(@")") == false) && (i.Href.Contains(@"{") == false) && (i.Href.Contains(@"}") == false) && (i.Href.Contains(@"[") == false) && (i.Href.Contains(@"]") == false))
                        {
                            string firstlyChar = i.Href[0].ToString();
                            if (firstlyChar.Contains(@"/") == true)
                            {
                                i.Href = i.Href.Substring(1, i.Href.Length - 1);
                            }
                            if (localPath.Contains(@".") == true)
                            {
                                localPath = localPath.Split('.')[0];
                            }
                            string localPath001 = "";
                            for (int j = 0; j < localPath.Split('/').Length - 1; j++)
                            {
                                localPath001 = localPath001 + "/" + localPath.Split('/')[j];
                            }
                            i.Href = host + localPath001 + "/" + i.Href;
                            i.Href = i.Href.Replace(@"//", @"/");
                            i.Href = scheme + "://" + i.Href;
                        }
                        string extension = i.Href.Split('/')[i.Href.Split('/').Length - 1];
                        if ((i.Href.Contains(@"/tim-kiem") == false) && (i.Href.Contains(@"?tim-kiem") == false) && (i.Href.Contains(@"/tin-lien-quan") == false) && (i.Href.Contains(@"?tin-lien-quan") == false) && (i.Href.Contains(@"?tu-khoa") == false) && (i.Href.Contains(@"/tu-khoa") == false) && (i.Href.Contains(@"?search") == false) && (i.Href.Contains(@"/search") == false) && (i.Href.Contains(@"?tag") == false) && (i.Href.Contains(@"/tag") == false) && (i.Href.Contains(@"/blogs") == false) && (i.Href.Contains(@"/danh-muc") == false) && (i.Href.Contains(@"#") == false) && (i.Href.Contains(@";") == false) && (i.Href.Contains(@"(") == false) && (i.Href.Contains(@")") == false) && (i.Href.Contains(@"{") == false) && (i.Href.Contains(@"}") == false) && (i.Href.Contains(@"[") == false) && (i.Href.Contains(@"]") == false))
                        {
                            string year = DateTime.Now.Year.ToString();
                            i.Text = i.Text.Trim();
                            if (i.Text.Split(' ').Length > 1)
                            {
                                if ((i.Text.Length > 10) && (i.Text.Contains("/") == true) && (i.Text.Contains(year) == true))
                                {
                                }
                                else
                                {
                                    try
                                    {
                                        int number = int.Parse(i.Text);
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            DateTime date = DateTime.Parse(i.Text);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                DateTime date = new DateTime(int.Parse(i.Text.Split('/')[2]), int.Parse(i.Text.Split('/')[1]), int.Parse(i.Text.Split('/')[0]));
                                            }
                                            catch
                                            {
                                                if ((i.Text.Contains("{") == false) && (i.Text.Contains("[]") == false) && (i.Text.Contains("' trước") == false) && (i.Text.Contains("h trước") == false))
                                                {
                                                    Uri myUri = new Uri(i.Href);
                                                    if (myUri.Host == root.Host)
                                                    {
                                                        list.Add(i);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }
        public static void LinkFinder001(string urlCategory, string urlRoot, bool repeat, List<LinkItem> list)
        {
            try
            {
                int index = -1;
                Uri root = new Uri(urlRoot);
                Uri myUri = new Uri(urlRoot);
                string html = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlCategory);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }
                    html = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                }
                List<LinkItem> listCategory = new List<LinkItem>();
                if (!string.IsNullOrEmpty(html))
                {
                    index = -1;
                    MatchCollection m1 = Regex.Matches(html, @"(<a.*?>.*?</a>)", RegexOptions.Singleline);
                    foreach (Match m in m1)
                    {
                        index = index + 1;
                        string value = m.Groups[1].Value;
                        LinkItem i = new LinkItem();
                        Match m2 = Regex.Match(value, @"href=\""(.*?)\""", RegexOptions.Singleline);
                        if (m2.Success)
                        {
                            i.Href = m2.Groups[1].Value;
                        }
                        else
                        {
                            m2 = Regex.Match(value, @"href=\'(.*?)\'", RegexOptions.Singleline);
                            if (m2.Success)
                            {
                                i.Href = m2.Groups[1].Value;
                            }
                        }
                        if (!string.IsNullOrEmpty(i.Href))
                        {
                            bool checkHref = false;
                            try
                            {
                                myUri = new Uri(i.Href);
                                checkHref = true;
                            }
                            catch (Exception e)
                            {
                                string mes = e.Message;
                                i.Href = root.OriginalString + i.Href;
                                try
                                {
                                    myUri = new Uri(i.Href);
                                    checkHref = true;
                                }
                                catch (Exception e1)
                                {
                                    string mes1 = e1.Message;
                                }
                            }
                            if (checkHref == true)
                            {
                                if (myUri.Host == root.Host)
                                {
                                    string rootOriginalString = root.OriginalString + "/";
                                    if (myUri.OriginalString != rootOriginalString)
                                    {
                                        string localPath = myUri.LocalPath;
                                        if (localPath.Contains(@".") == true)
                                        {
                                            string extension = localPath.Split('.')[1];
                                            if ((extension.Contains(@"/") == false) && (extension.Contains(@"#") == false))
                                            {
                                                Match m3 = Regex.Match(value, @"title=\""(.*?)\""", RegexOptions.Singleline);
                                                if (m3.Success)
                                                {
                                                    i.Text = m3.Groups[1].Value;
                                                }
                                                else
                                                {
                                                    m3 = Regex.Match(value, @"title=\'(.*?)\'", RegexOptions.Singleline);
                                                    if (m3.Success)
                                                    {
                                                        i.Text = m3.Groups[1].Value;
                                                    }
                                                }
                                                if (string.IsNullOrEmpty(i.Text))
                                                {
                                                    string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                                                    i.Text = t;
                                                }
                                                if (!string.IsNullOrEmpty(i.Text))
                                                {
                                                    if (i.Text.Split(' ').Length > 4)
                                                    {
                                                        checkHref = false;
                                                        for (int j = 0; j < list.Count; j++)
                                                        {
                                                            if (list[j].Href == i.Href)
                                                            {
                                                                checkHref = true;
                                                                j = list.Count;
                                                            }
                                                        }
                                                        if (checkHref == false)
                                                        {
                                                            list.Add(i);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (repeat == true)
                                                        {
                                                            checkHref = false;
                                                            for (int j = 0; j < listCategory.Count; j++)
                                                            {
                                                                if (listCategory[j].Href == i.Href)
                                                                {
                                                                    checkHref = true;
                                                                    j = listCategory.Count;
                                                                }
                                                            }
                                                            if (checkHref == false)
                                                            {
                                                                listCategory.Add(i);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (repeat == true)
                                            {
                                                checkHref = false;
                                                for (int j = 0; j < listCategory.Count; j++)
                                                {
                                                    if (listCategory[j].Href == i.Href)
                                                    {
                                                        checkHref = true;
                                                        j = listCategory.Count;
                                                    }
                                                }
                                                if (checkHref == false)
                                                {
                                                    listCategory.Add(i);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (repeat == true)
                {
                    foreach (LinkItem item in listCategory)
                    {
                        LinkFinder001(item.Href, urlRoot, false, list);
                    }
                }
            }
            catch (Exception e1)
            {
                string mes1 = e1.Message;
            }
        }

        public static string LinkFinder002(string urlCategory, string urlRoot, bool repeat, List<LinkItem> list)
        {
            try
            {
                Uri root = new Uri(urlRoot);
                Uri myUri = new Uri(urlRoot);
                string html = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlCategory);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }
                    html = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                }
                List<LinkItem> listTrue = new List<LinkItem>();
                if (!string.IsNullOrEmpty(html))
                {
                    MatchCollection m1 = Regex.Matches(html, @"(<a.*?>.*?</a>)", RegexOptions.Singleline);
                    foreach (Match m in m1)
                    {
                        string value = m.Groups[1].Value;
                        LinkItem i = new LinkItem();
                        Match m2 = Regex.Match(value, @"href=\""(.*?)\""", RegexOptions.Singleline);
                        if (m2.Success)
                        {
                            i.Href = m2.Groups[1].Value;
                        }
                        else
                        {
                            m2 = Regex.Match(value, @"href=\'(.*?)\'", RegexOptions.Singleline);
                            if (m2.Success)
                            {
                                i.Href = m2.Groups[1].Value;
                            }
                        }
                        if (!string.IsNullOrEmpty(i.Href))
                        {
                            bool checkHref = false;
                            try
                            {
                                myUri = new Uri(i.Href);
                                checkHref = true;
                            }
                            catch (Exception e)
                            {
                                string mes = e.Message;
                                i.Href = root.OriginalString + "/" + i.Href;
                                i.Href = i.Href.Replace(@"://", @"~SOHU~");
                                i.Href = i.Href.Replace(@"//", @"/");
                                i.Href = i.Href.Replace(@"~SOHU~", @"://");
                                try
                                {
                                    myUri = new Uri(i.Href);
                                    checkHref = true;
                                }
                                catch (Exception e1)
                                {
                                    string mes1 = e1.Message;
                                }
                            }
                            if (checkHref == true)
                            {
                                if (myUri.Host == root.Host)
                                {
                                    string rootOriginalString = root.OriginalString + "/";
                                    if (myUri.OriginalString != rootOriginalString)
                                    {
                                        string localPath = myUri.LocalPath;
                                        if (localPath.Contains(@".") == true)
                                        {
                                            string extension = localPath.Split('.')[1];
                                            if ((extension.Contains(@"/") == false) || (extension.Contains(@"#") == false))
                                            {
                                                if (i.Href.Contains(@"#") == true)
                                                {

                                                }
                                                else
                                                {
                                                    checkHref = true;
                                                    for (int j = 0; j < list.Count; j++)
                                                    {
                                                        if (list[j].Href == i.Href)
                                                        {
                                                            checkHref = false;
                                                            j = list.Count;
                                                        }
                                                    }
                                                    if (checkHref == true)
                                                    {
                                                        list.Add(i);
                                                        if (repeat == true)
                                                        {
                                                            listTrue.Add(i);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (repeat == true)
                {
                    foreach (LinkItem item in listTrue)
                    {
                        LinkFinder002(item.Href, urlRoot, false, list);
                    }
                }
            }
            catch (Exception e)
            {
                string mes = e.Message;
            }
            return "";
        }
        public static void FinderContent(string html, Product product)
        {
            if (!string.IsNullOrEmpty(html))
            {
                MatchCollection m1 = Regex.Matches(html, @"(<p.*?>.*?</p>)", RegexOptions.Singleline);
                foreach (Match m in m1)
                {
                    string value = m.Groups[1].Value;
                    string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                    product.Description = product.Description + " " + t;
                    product.ContentMain = product.ContentMain + "<br/>" + t;
                }
            }
        }
        public static void FinderContentAndDatePublish(string html, Product product)
        {
            string url = product.URLCode;
            html = html.Replace(@"~", @"");
            if (!string.IsNullOrEmpty(html))
            {
                string htmlspan = html;
                MatchCollection m1;
                bool check = false;
                if (check == false)
                {
                    htmlspan = html;
                    htmlspan = htmlspan.Replace(@"<meta", @"~<meta");
                    for (int i = 0; i < htmlspan.Split('~').Length; i++)
                    {
                        string value = htmlspan.Split('~')[i];
                        if (value.Contains(@"published") == true)
                        {
                            string date = value.Replace(@"content=""", @"~");
                            if (date.Split('~').Length > 1)
                            {
                                date = date.Split('~')[1];
                                date = date.Split('"')[0];
                                date = date.Substring(0, 10);
                                date = date.Replace(@"-", @"/");
                                try
                                {
                                    DateTime datePublish = new DateTime(int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]), int.Parse(date.Split('/')[2]), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                                    if (product.DatePublish > datePublish)
                                    {
                                        product.DatePublish = datePublish;
                                        check = true;
                                        i = htmlspan.Split('~').Length;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
                if (check == false)
                {
                    htmlspan = html;
                    htmlspan = htmlspan.Replace(@"""datePublished"":", @"~");
                    if (htmlspan.Split('~').Length > 1)
                    {
                        htmlspan = htmlspan.Split('~')[1];
                        htmlspan = htmlspan.Trim();
                        htmlspan = htmlspan.Split(',')[0];
                        htmlspan = htmlspan.Replace(@"""", @"");
                        htmlspan = htmlspan.Substring(0, 10);
                        htmlspan = htmlspan.Replace(@"-", @"/");
                        string date = htmlspan;
                        try
                        {
                            DateTime datePublish = new DateTime(int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]), int.Parse(date.Split('/')[2]), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                            if (product.DatePublish > datePublish)
                            {
                                product.DatePublish = datePublish;
                                check = true;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                if (check == false)
                {
                    htmlspan = html;
                    htmlspan = htmlspan.Replace(@"~", @"");
                    htmlspan = htmlspan.Replace(@"</header>", @"~");
                    if (htmlspan.Split('~').Length > 1)
                    {
                        htmlspan = htmlspan.Split('~')[1];
                    }
                    m1 = Regex.Matches(htmlspan, @"(<dd.*?>.*?</dd>)", RegexOptions.Singleline);
                    for (int i = 0; i < m1.Count; i++)
                    {
                        string value = m1[i].Groups[1].Value;
                        string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                        t = t.Replace(@"-", @"/");
                        t = t.Replace(@".", @"/");
                        if ((!string.IsNullOrEmpty(t)) && (t.Contains(@"/") == true))
                        {
                            for (int j = 0; j < t.Split(' ').Length; j++)
                            {
                                string date = t.Split(' ')[j];
                                date = date.Trim();
                                date = date.Replace(@",", @"");
                                if ((date.Length > 7) && (date.Length < 11) && (date.Contains(@"/") == true))
                                {
                                    try
                                    {
                                        DateTime datePublish = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[1]), int.Parse(date.Split('/')[0]), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                                        if (product.DatePublish > datePublish)
                                        {
                                            product.DatePublish = datePublish;
                                            check = true;
                                            i = htmlspan.Split('~').Length;
                                        }
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            DateTime datePublish = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                                            if (product.DatePublish > datePublish)
                                            {
                                                product.DatePublish = datePublish;
                                                check = true;
                                                i = htmlspan.Split('~').Length;
                                            }
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    if (check == true)
                                    {
                                        i = m1.Count;
                                        j = t.Split(' ').Length;
                                    }
                                }
                            }
                        }
                    }
                }
                if (check == false)
                {
                    htmlspan = html;
                    htmlspan = htmlspan.Replace(@"~", @"");
                    htmlspan = htmlspan.Replace(@"</header>", @"~");
                    if (htmlspan.Split('~').Length > 1)
                    {
                        htmlspan = htmlspan.Split('~')[1];
                    }
                    m1 = Regex.Matches(htmlspan, @"(<dd.*?>.*?</dd>)", RegexOptions.Singleline);
                    for (int i = 0; i < m1.Count; i++)
                    {
                        string value = m1[i].Groups[1].Value;
                        string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                        if (((t.Contains(@"ngày") == true) && (t.Contains(@"tháng") == true)) || ((t.Contains(@"ng&#224;y") == true) && (t.Contains(@"th&#225;ng") == true)))
                        {
                            int day = 0;
                            int month = 0;
                            int year = 0;
                            int index = 0;
                            foreach (string item in t.Split(' '))
                            {
                                string date = item;
                                date = date.Trim();
                                date = date.Replace(@",", @"");
                                try
                                {
                                    int dateValue = int.Parse(date);
                                    switch (index)
                                    {
                                        case 0:
                                            day = dateValue;
                                            break;
                                        case 1:
                                            month = dateValue;
                                            break;
                                        case 2:
                                            year = dateValue;
                                            break;
                                    }
                                    index = index + 1;
                                }
                                catch
                                {
                                }
                            }
                            if (index == 3)
                            {
                                try
                                {
                                    DateTime datePublish = new DateTime(year, month, day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                    if (product.DatePublish > datePublish)
                                    {
                                        product.DatePublish = datePublish;
                                        check = true;
                                    }

                                }
                                catch
                                {
                                    try
                                    {
                                        DateTime datePublish = new DateTime(year, day, month, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                        if (product.DatePublish > datePublish)
                                        {
                                            product.DatePublish = datePublish;
                                            check = true;
                                        }

                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
                if (check == false)
                {
                    htmlspan = html;
                    htmlspan = htmlspan.Replace(@"~", @"");
                    htmlspan = htmlspan.Replace(@"</header>", @"~");
                    if (htmlspan.Split('~').Length > 1)
                    {
                        htmlspan = htmlspan.Split('~')[1];
                    }
                    m1 = Regex.Matches(htmlspan, @"(<time.*?>.*?</time>)", RegexOptions.Singleline);
                    for (int i = 0; i < m1.Count; i++)
                    {
                        string value = m1[i].Groups[1].Value;
                        string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                        t = t.Replace(@"-", @"/");
                        t = t.Replace(@".", @"/");
                        if ((!string.IsNullOrEmpty(t)) && (t.Contains(@"/") == true))
                        {
                            for (int j = 0; j < t.Split(' ').Length; j++)
                            {
                                string date = t.Split(' ')[j];
                                date = date.Trim();
                                date = date.Replace(@",", @"");
                                if ((date.Length > 7) && (date.Length < 11) && (date.Contains(@"/") == true))
                                {
                                    try
                                    {
                                        DateTime datePublish = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[1]), int.Parse(date.Split('/')[0]), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                        if (product.DatePublish > datePublish)
                                        {
                                            product.DatePublish = datePublish;
                                            check = true;
                                            i = htmlspan.Split('~').Length;
                                        }

                                    }
                                    catch
                                    {
                                        try
                                        {
                                            DateTime datePublish = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                            if (product.DatePublish > datePublish)
                                            {
                                                product.DatePublish = datePublish;
                                                check = true;
                                                i = htmlspan.Split('~').Length;
                                            }

                                        }
                                        catch
                                        {
                                        }
                                    }
                                    if (check == true)
                                    {
                                        i = m1.Count;
                                        j = t.Split(' ').Length;
                                    }
                                }
                            }
                        }
                    }
                }
                if (check == false)
                {
                    htmlspan = html;
                    htmlspan = htmlspan.Replace(@"~", @"");
                    htmlspan = htmlspan.Replace(@"</header>", @"~");
                    if (htmlspan.Split('~').Length > 1)
                    {
                        htmlspan = htmlspan.Split('~')[1];
                    }
                    m1 = Regex.Matches(htmlspan, @"(<time.*?>.*?</time>)", RegexOptions.Singleline);
                    for (int i = 0; i < m1.Count; i++)
                    {
                        string value = m1[i].Groups[1].Value;
                        string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                        if (((t.Contains(@"ngày") == true) && (t.Contains(@"tháng") == true)) || ((t.Contains(@"ng&#224;y") == true) && (t.Contains(@"th&#225;ng") == true)))
                        {
                            int day = 0;
                            int month = 0;
                            int year = 0;
                            int index = 0;
                            foreach (string item in t.Split(' '))
                            {
                                string date = item;
                                date = date.Trim();
                                date = date.Replace(@",", @"");
                                try
                                {
                                    int dateValue = int.Parse(date);
                                    switch (index)
                                    {
                                        case 0:
                                            day = dateValue;
                                            break;
                                        case 1:
                                            month = dateValue;
                                            break;
                                        case 2:
                                            year = dateValue;
                                            break;
                                    }
                                    index = index + 1;
                                }
                                catch
                                {
                                }
                            }
                            if (index == 3)
                            {
                                try
                                {
                                    DateTime datePublish = new DateTime(year, month, day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                    if (product.DatePublish > datePublish)
                                    {
                                        product.DatePublish = datePublish;
                                        check = true;
                                    }

                                }
                                catch
                                {
                                    try
                                    {
                                        DateTime datePublish = new DateTime(year, day, month, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                        if (product.DatePublish > datePublish)
                                        {
                                            product.DatePublish = datePublish;
                                            check = true;
                                        }

                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
                if (check == false)
                {
                    htmlspan = html;
                    htmlspan = htmlspan.Replace(@"~", @"");
                    htmlspan = htmlspan.Replace(@"</header>", @"~");
                    if (htmlspan.Split('~').Length > 1)
                    {
                        htmlspan = htmlspan.Split('~')[1];
                    }
                    m1 = Regex.Matches(htmlspan, @"(<span.*?>.*?</span>)", RegexOptions.Singleline);
                    for (int i = 0; i < m1.Count; i++)
                    {
                        string value = m1[i].Groups[1].Value;
                        string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                        t = t.Replace(@"-", @"/");
                        t = t.Replace(@".", @"/");
                        if ((!string.IsNullOrEmpty(t)) && (t.Contains(@"/") == true))
                        {
                            for (int j = 0; j < t.Split(' ').Length; j++)
                            {
                                string date = t.Split(' ')[j];
                                date = date.Trim();
                                date = date.Replace(@",", @"");
                                if ((date.Length > 7) && (date.Length < 11) && (date.Contains(@"/") == true))
                                {
                                    try
                                    {
                                        DateTime datePublish = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[1]), int.Parse(date.Split('/')[0]), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                        if (product.DatePublish > datePublish)
                                        {
                                            product.DatePublish = datePublish;
                                            check = true;
                                            i = htmlspan.Split('~').Length;
                                        }

                                    }
                                    catch
                                    {
                                        try
                                        {
                                            DateTime datePublish = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                            if (product.DatePublish > datePublish)
                                            {
                                                product.DatePublish = datePublish;
                                                check = true;
                                                i = htmlspan.Split('~').Length;
                                            }

                                        }
                                        catch
                                        {
                                        }
                                    }
                                    if (check == true)
                                    {
                                        i = m1.Count;
                                        j = t.Split(' ').Length;
                                    }
                                }
                            }
                        }
                    }
                }
                if (check == false)
                {
                    htmlspan = html;
                    htmlspan = htmlspan.Replace(@"~", @"");
                    htmlspan = htmlspan.Replace(@"</header>", @"~");
                    if (htmlspan.Split('~').Length > 1)
                    {
                        htmlspan = htmlspan.Split('~')[1];
                    }
                    m1 = Regex.Matches(htmlspan, @"(<span.*?>.*?</span>)", RegexOptions.Singleline);
                    for (int i = 0; i < m1.Count; i++)
                    {
                        string value = m1[i].Groups[1].Value;
                        string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                        if (((t.Contains(@"ngày") == true) && (t.Contains(@"tháng") == true)) || ((t.Contains(@"ng&#224;y") == true) && (t.Contains(@"th&#225;ng") == true)))
                        {
                            int day = 0;
                            int month = 0;
                            int year = 0;
                            int index = 0;
                            foreach (string item in t.Split(' '))
                            {
                                string date = item;
                                date = date.Trim();
                                date = date.Replace(@",", @"");
                                try
                                {
                                    int dateValue = int.Parse(date);
                                    switch (index)
                                    {
                                        case 0:
                                            day = dateValue;
                                            break;
                                        case 1:
                                            month = dateValue;
                                            break;
                                        case 2:
                                            year = dateValue;
                                            break;
                                    }
                                    index = index + 1;
                                }
                                catch
                                {
                                }
                            }
                            if (index == 3)
                            {
                                try
                                {
                                    DateTime datePublish = new DateTime(year, month, day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                    if (product.DatePublish > datePublish)
                                    {
                                        product.DatePublish = datePublish;
                                        check = true;
                                    }

                                }
                                catch
                                {
                                    try
                                    {
                                        DateTime datePublish = new DateTime(year, day, month, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                        if (product.DatePublish > datePublish)
                                        {
                                            product.DatePublish = datePublish;
                                            check = true;
                                        }

                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
                if (check == false)
                {
                    htmlspan = html;
                    htmlspan = htmlspan.Replace(@"~", @"");
                    htmlspan = htmlspan.Replace(@"</header>", @"~");
                    if (htmlspan.Split('~').Length > 1)
                    {
                        htmlspan = htmlspan.Split('~')[1];
                    }
                    m1 = Regex.Matches(htmlspan, @"(<div.*?>.*?</div>)", RegexOptions.Singleline);
                    for (int i = 0; i < m1.Count; i++)
                    {
                        string value = m1[i].Groups[1].Value;
                        string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                        t = t.Replace(@"-", @"/");
                        t = t.Replace(@".", @"/");
                        if ((!string.IsNullOrEmpty(t)) && (t.Contains(@"/") == true))
                        {
                            for (int j = 0; j < t.Split(' ').Length; j++)
                            {
                                string date = t.Split(' ')[j];
                                date = date.Trim();
                                date = date.Replace(@",", @"");
                                if ((date.Length > 7) && (date.Length < 11) && (date.Contains(@"/") == true))
                                {
                                    try
                                    {
                                        DateTime datePublish = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[1]), int.Parse(date.Split('/')[0]), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                        if (product.DatePublish > datePublish)
                                        {
                                            product.DatePublish = datePublish;
                                            check = true;
                                            i = htmlspan.Split('~').Length;
                                        }

                                    }
                                    catch
                                    {
                                        try
                                        {
                                            DateTime datePublish = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                            if (product.DatePublish > datePublish)
                                            {
                                                product.DatePublish = datePublish;
                                                check = true;
                                                i = htmlspan.Split('~').Length;
                                            }

                                        }
                                        catch
                                        {
                                        }
                                    }
                                    if (check == true)
                                    {
                                        i = m1.Count;
                                        j = t.Split(' ').Length;
                                    }
                                }
                            }
                        }
                    }
                }
                if (check == false)
                {
                    htmlspan = html;
                    htmlspan = htmlspan.Replace(@"~", @"");
                    htmlspan = htmlspan.Replace(@"</header>", @"~");
                    if (htmlspan.Split('~').Length > 1)
                    {
                        htmlspan = htmlspan.Split('~')[1];
                    }
                    m1 = Regex.Matches(htmlspan, @"(<div.*?>.*?</div>)", RegexOptions.Singleline);
                    for (int i = 0; i < m1.Count; i++)
                    {
                        string value = m1[i].Groups[1].Value;
                        string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                        if (((t.Contains(@"ngày") == true) && (t.Contains(@"tháng") == true)) || ((t.Contains(@"ng&#224;y") == true) && (t.Contains(@"th&#225;ng") == true)))
                        {
                            int day = 0;
                            int month = 0;
                            int year = 0;
                            int index = 0;
                            foreach (string item in t.Split(' '))
                            {
                                string date = item;
                                date = date.Trim();
                                date = date.Replace(@",", @"");
                                try
                                {
                                    int dateValue = int.Parse(date);
                                    switch (index)
                                    {
                                        case 0:
                                            day = dateValue;
                                            break;
                                        case 1:
                                            month = dateValue;
                                            break;
                                        case 2:
                                            year = dateValue;
                                            break;
                                    }
                                    index = index + 1;
                                }
                                catch
                                {
                                }
                            }
                            if (index == 3)
                            {
                                try
                                {
                                    DateTime datePublish = new DateTime(year, month, day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                    if (product.DatePublish > datePublish)
                                    {
                                        product.DatePublish = datePublish;
                                        check = true;
                                    }

                                }
                                catch
                                {
                                    try
                                    {
                                        DateTime datePublish = new DateTime(year, day, month, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                        if (product.DatePublish > datePublish)
                                        {
                                            product.DatePublish = datePublish;
                                            check = true;
                                        }

                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
                string htmlspan001 = html;
                Uri myUri = new Uri(product.URLCode);
                htmlspan001 = HTMLReplaceAndSplit(htmlspan001);
                m1 = Regex.Matches(htmlspan001, @"(<p.*?>.*?</p>)", RegexOptions.Singleline);
                for (int i = 0; i < m1.Count; i++)
                {
                    string value = m1[i].Groups[1].Value;
                    if ((value.Contains(@"<img") == true) || (value.Contains(@"</a>") == true) || (value.Contains(@"<a") == true) || (value.Contains(@"href") == true) || (value.Contains(@"function()") == true) || (value.Contains(@"$(") == true))
                    {

                    }
                    else
                    {
                        string t1 = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                        product.Description = product.Description + " " + t1;
                        product.ContentMain = product.ContentMain + "<br/>" + t1;
                    }
                    string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                    if (!string.IsNullOrEmpty(t))
                    {
                        if (check == false)
                        {
                            t = t.Replace(@"-", @"/");
                            t = t.Replace(@".", @"/");
                            for (int j = 0; j < t.Split(' ').Length; j++)
                            {
                                string date = t.Split(' ')[j];
                                date = date.Trim();
                                date = date.Replace(@",", @"");
                                if ((date.Length > 7) && (date.Length < 11) && (date.Contains(@"/") == true))
                                {
                                    try
                                    {
                                        DateTime datePublish = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[1]), int.Parse(date.Split('/')[0]), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                                        if (product.DatePublish > datePublish)
                                        {
                                            product.DatePublish = datePublish;
                                            check = true;
                                            i = htmlspan.Split('~').Length;
                                        }

                                    }
                                    catch
                                    {
                                        try
                                        {
                                            DateTime datePublish = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]), DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                                            if (product.DatePublish > datePublish)
                                            {
                                                product.DatePublish = datePublish;
                                                check = true;
                                                i = htmlspan.Split('~').Length;
                                            }

                                        }
                                        catch
                                        {
                                        }
                                    }
                                    if (check == true)
                                    {
                                        i = m1.Count;
                                        j = t.Split(' ').Length;
                                    }
                                }
                            }
                        }
                    }
                }
                product.Active = check;
            }
        }
        public static void DatePublish001(string html, string tagName, Product product)
        {
            DateTime datePublish = new DateTime(2019, 1, 1);
            string yearString = "";
            string monthString = "";
            string dayString = "";
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second;
            string htmlspan = html;
            htmlspan = htmlspan.Replace(@"~", @"");
            htmlspan = htmlspan.Replace(@"<body", @"~");
            if (htmlspan.Split('~').Length > 1)
            {
                htmlspan = htmlspan.Split('~')[1];
            }
            htmlspan = htmlspan.Replace(@"<main", @"~");
            if (htmlspan.Split('~').Length > 1)
            {
                htmlspan = htmlspan.Split('~')[1];
            }
            htmlspan = htmlspan.Replace(@"<header", @"~");
            if (htmlspan.Split('~').Length > 1)
            {
                htmlspan = htmlspan.Split('~')[1];
            }
            MatchCollection m1 = Regex.Matches(htmlspan, @"(<" + tagName + ".*?>.*?</" + tagName + ">)", RegexOptions.Singleline);
            for (int i = 0; i < m1.Count; i++)
            {
                string value = m1[i].Groups[1].Value;
                string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                if ((!string.IsNullOrEmpty(t)) && (t.Contains(@"/") == true))
                {
                    t = t.Replace(@"|", @"");
                    t = t.Replace(@"&nbsp;", @"");
                    t = t.Replace(@"-", @"/");
                    t = t.Replace(@".", @"/");
                    for (int j = 0; j < t.Split(' ').Length; j++)
                    {
                        string date = t.Split(' ')[j];
                        date = date.Trim();
                        date = date.Replace(@",", @"");
                        if ((date.Length > 7) && (date.Length < 11) && (date.Contains(@"/") == true))
                        {
                            try
                            {
                                yearString = date.Split('/')[2];
                                monthString = date.Split('/')[1];
                                dayString = date.Split('/')[0];
                                if (yearString.Length == 2)
                                {
                                    yearString = "20" + yearString;
                                }
                                datePublish = new DateTime(int.Parse(yearString), int.Parse(monthString), int.Parse(dayString), hour, minute, second);
                            }
                            catch
                            {
                                try
                                {
                                    yearString = date.Split('/')[2];
                                    monthString = date.Split('/')[1];
                                    dayString = date.Split('/')[0];
                                    if (yearString.Length == 2)
                                    {
                                        yearString = "20" + yearString;
                                    }
                                    datePublish = new DateTime(int.Parse(yearString), int.Parse(monthString), int.Parse(dayString), hour, minute, second);

                                }
                                catch
                                {
                                    try
                                    {
                                        yearString = date.Split('/')[0];
                                        monthString = date.Split('/')[1];
                                        dayString = date.Split('/')[2];
                                        if (yearString.Length == 2)
                                        {
                                            yearString = "20" + yearString;
                                        }
                                        datePublish = new DateTime(int.Parse(yearString), int.Parse(monthString), int.Parse(dayString), hour, minute, second);
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            if (product.DatePublish > datePublish)
                            {
                                product.DatePublish = datePublish;
                                product.Active = true;
                                i = m1.Count;
                                j = t.Split(' ').Length;
                            }
                        }
                    }
                }
            }
        }
        public static void DatePublish002(string html, string tagName, Product product)
        {
            DateTime datePublish = new DateTime(2019, 1, 1);
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second;
            string htmlspan = html;
            htmlspan = htmlspan.Replace(@"~", @"");
            htmlspan = htmlspan.Replace(@"<body", @"~");
            if (htmlspan.Split('~').Length > 1)
            {
                htmlspan = htmlspan.Split('~')[1];
            }
            htmlspan = htmlspan.Replace(@"<main", @"~");
            if (htmlspan.Split('~').Length > 1)
            {
                htmlspan = htmlspan.Split('~')[1];
            }
            htmlspan = htmlspan.Replace(@"<header", @"~");
            if (htmlspan.Split('~').Length > 1)
            {
                htmlspan = htmlspan.Split('~')[1];
            }
            MatchCollection m1 = Regex.Matches(htmlspan, @"(<" + tagName + ".*?>.*?</" + tagName + ">)", RegexOptions.Singleline);
            for (int i = 0; i < m1.Count; i++)
            {
                string value = m1[i].Groups[1].Value;
                value = value.Replace(@"|", @"");
                value = value.Replace(@"&nbsp;", @"");
                string t = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                if (((t.Contains(@"ngày") == true) && (t.Contains(@"tháng") == true)) || ((t.Contains(@"ng&#224;y") == true) && (t.Contains(@"th&#225;ng") == true)))
                {
                    int index = 0;
                    foreach (string item in t.Split(' '))
                    {
                        string date = item;
                        date = date.Trim();
                        date = date.Replace(@",", @"");
                        try
                        {
                            int dateValue = int.Parse(date);
                            switch (index)
                            {
                                case 0:
                                    day = dateValue;
                                    break;
                                case 1:
                                    month = dateValue;
                                    break;
                                case 2:
                                    year = dateValue;
                                    break;
                            }
                            index = index + 1;
                        }
                        catch
                        {
                        }
                    }
                    if (index == 3)
                    {
                        try
                        {
                            datePublish = new DateTime(year, month, day, hour, minute, second);
                        }
                        catch
                        {
                            try
                            {
                                datePublish = new DateTime(year, day, month, hour, minute, second);
                            }
                            catch
                            {

                            }
                        }
                        if (product.DatePublish > datePublish)
                        {
                            product.DatePublish = datePublish;
                            product.Active = true;
                            i = m1.Count;
                        }
                    }
                }
            }
        }
        public static void DatePublish003(string html, Product product)
        {
            string htmlspan = html;
            htmlspan = HTMLReplaceAndSplit(htmlspan);
            htmlspan = htmlspan.Replace(@"~", @"");
            htmlspan = htmlspan.Replace(@"<body", @"~");
            if (htmlspan.Split('~').Length > 1)
            {
                htmlspan = htmlspan.Split('~')[1];
            }
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            string content = reg.Replace(html, " ");
            content = HttpUtility.HtmlDecode(content);
            content = content.Trim();
            List<DatePublish> listDatePublish = new List<DatePublish>();
            DateTime dateTime = new DateTime();
            DateTime now = DateTime.Now;
            for (int k = 0; k < content.Split(' ').Length; k++)
            {
                string item = content.Split(' ')[k];
                if (!string.IsNullOrEmpty(item))
                {
                    string date = item;
                    date = Regex.Replace(date, @"\t|\n|\r", "");
                    date = date.Replace(@",", @"");
                    date = date.Replace(@"|", @"");
                    int timeCount = date.Count(f => f == '/');
                    if (timeCount == 0)
                    {
                        timeCount = date.Count(f => f == '-');
                    }
                    if (timeCount == 2)
                    {
                        if ((date.Length > 7) && (date.Length < 11))
                        {
                            string time = "";
                            bool timeCheck = false;
                            try
                            {
                                time = content.Split(' ')[k + 1];
                                if (time.Contains(@":") == true)
                                {
                                    if ((time.Length == 5) || (time.Length == 4))
                                    {
                                        timeCheck = true;
                                    }
                                }
                            }
                            catch
                            {
                                try
                                {
                                    time = content.Split(' ')[k - 1];
                                    if (time.Contains(@":") == true)
                                    {
                                        if ((time.Length == 5) || (time.Length == 4))
                                        {
                                            timeCheck = true;
                                        }
                                    }
                                }
                                catch
                                {
                                }
                            }
                            time = time.Trim();
                            try
                            {
                                dateTime = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[1]), int.Parse(date.Split('/')[0]), 0, 0, 0);
                                if (dateTime.Year > 2020)
                                {
                                    if (listDatePublish.Count == 0)
                                    {
                                        DatePublish datePublish = new DatePublish();
                                        datePublish.Time = "";
                                        datePublish.IsTime = false;
                                        datePublish.DateTime = dateTime;
                                        datePublish.Count = 1;
                                        if (timeCheck == true)
                                        {
                                            datePublish.Time = time;
                                            datePublish.IsTime = timeCheck;
                                        }
                                        listDatePublish.Add(datePublish);
                                    }
                                    else
                                    {
                                        bool check = true;
                                        DatePublish datePublish = new DatePublish();
                                        for (int i = 0; i < listDatePublish.Count; i++)
                                        {
                                            if ((listDatePublish[i].DateTime.Year == dateTime.Year) && (listDatePublish[i].DateTime.Month == dateTime.Month) && (listDatePublish[i].DateTime.Day == dateTime.Day))
                                            {
                                                listDatePublish[i].Count = listDatePublish[i].Count + 1;
                                                if (timeCheck == true)
                                                {
                                                    datePublish.Time = time;
                                                    datePublish.IsTime = timeCheck;
                                                }
                                                i = listDatePublish.Count;
                                                check = false;
                                            }
                                        }
                                        if (check == true)
                                        {
                                            datePublish.Time = "";
                                            datePublish.IsTime = false;
                                            datePublish.DateTime = dateTime;
                                            datePublish.Count = 1;
                                            if (timeCheck == true)
                                            {
                                                datePublish.Time = time;
                                                datePublish.IsTime = timeCheck;
                                            }
                                            listDatePublish.Add(datePublish);
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            DateTime productDatePublish = DateTime.Now;
            bool productActive = false;
            if (listDatePublish.Count == 1)
            {
                productDatePublish = listDatePublish[0].DateTime;
                productActive = true;
            }
            else
            {
                for (int i = 0; i < listDatePublish.Count; i++)
                {
                    DatePublish item = listDatePublish[i];
                    if (item.IsTime == true)
                    {
                        try
                        {
                            item.DateTime = new DateTime(item.DateTime.Year, item.DateTime.Month, item.DateTime.Day, int.Parse(item.Time.Split(':')[0]), int.Parse(item.Time.Split(':')[1]), 0);
                            if (item.DateTime < productDatePublish)
                            {
                                productDatePublish = item.DateTime;
                                productActive = true;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            if (productActive == true)
            {
                product.DatePublish = productDatePublish;
                product.Active = productActive;
            }
        }
        public static void DatePublish004(string html, Product product)
        {
            string htmlspan = html;
            htmlspan = HTMLReplaceAndSplit(htmlspan);
            htmlspan = htmlspan.Replace(@"~", @"");
            htmlspan = htmlspan.Replace(@"<body", @"~");
            if (htmlspan.Split('~').Length > 1)
            {
                htmlspan = htmlspan.Split('~')[1];
            }
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            string content = reg.Replace(html, " ");
            content = HttpUtility.HtmlDecode(content);
            content = content.Trim();
            List<DatePublish> listDatePublish = new List<DatePublish>();

            DateTime now = DateTime.Now;
            for (int k = 0; k < content.Split(' ').Length; k++)
            {
                string item = content.Split(' ')[k];
                if (!string.IsNullOrEmpty(item))
                {
                    string date = item;
                    date = Regex.Replace(date, @"\t|\n|\r", "");
                    date = date.Replace(@",", @"");
                    date = date.Replace(@"|", @"");
                    int timeCount = date.Count(f => f == '/');
                    if (timeCount == 0)
                    {
                        timeCount = date.Count(f => f == '-');
                    }
                    if (timeCount == 2)
                    {
                        if ((date.Length > 7) && (date.Length < 11))
                        {
                            try
                            {
                                DateTime dateTime = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[1]), int.Parse(date.Split('/')[0]), 0, 0, 0);
                                if (dateTime.Year > 2020)
                                {
                                    DatePublish datePublish = new DatePublish();
                                    datePublish.DateTimeString = date;
                                    if (date.Contains(@":") == true)
                                    {
                                        datePublish.IsTime = true;
                                    }
                                    listDatePublish.Add(datePublish);
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            DateTime productDatePublish = DateTime.Now;
            bool productActive = false;
            if (listDatePublish.Count == 1)
            {
                try
                {
                    productDatePublish = DateTime.Parse(listDatePublish[0].DateTimeString);
                    productActive = true;
                }
                catch
                {
                }
            }
            else
            {

            }

            if (productActive == true)
            {
                product.DatePublish = productDatePublish;
                product.Active = productActive;
            }
        }
        public static void FinderContent002(string html, string tagName, Product product)
        {
            string htmlspan = html;
            htmlspan = HTMLReplaceAndSplit(htmlspan);
            htmlspan = htmlspan.Replace(@"~", @"");
            htmlspan = htmlspan.Replace(@"<body", @"~");
            if (htmlspan.Split('~').Length > 1)
            {
                htmlspan = htmlspan.Split('~')[1];
            }
            htmlspan = htmlspan.Replace(@"<main", @"~");
            if (htmlspan.Split('~').Length > 1)
            {
                htmlspan = htmlspan.Split('~')[1];
            }
            MatchCollection m1 = Regex.Matches(htmlspan, @"(<" + tagName + ".*?>.*?</" + tagName + ">)", RegexOptions.Singleline);
            for (int i = 0; i < m1.Count; i++)
            {
                string value = m1[i].Groups[1].Value;
                if ((value.Contains(@"<img") == true) || (value.Contains(@"</a>") == true) || (value.Contains(@"</script>") == true) || (value.Contains(@"</noscript>") == true) || (value.Contains(@"</style>") == true))
                {

                }
                else
                {
                    string t1 = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                    if (!string.IsNullOrEmpty(t1))
                    {
                        product.Description = product.Description + " " + t1;
                        product.ContentMain = product.ContentMain + "<br/>" + t1;
                    }
                }
            }
        }
        public static string FinderContent003(string html, string tagName)
        {
            string description = "";
            string htmlspan = html;
            htmlspan = HTMLReplaceAndSplit(htmlspan);
            htmlspan = htmlspan.Replace(@"~", @"");
            htmlspan = htmlspan.Replace(@"<body", @"~");
            if (htmlspan.Split('~').Length > 1)
            {
                htmlspan = htmlspan.Split('~')[1];
            }
            htmlspan = htmlspan.Replace(@"<main", @"~");
            if (htmlspan.Split('~').Length > 1)
            {
                htmlspan = htmlspan.Split('~')[1];
            }
            MatchCollection m1 = Regex.Matches(htmlspan, @"(<" + tagName + ".*?>.*?</" + tagName + ">)", RegexOptions.Singleline);
            for (int i = 0; i < m1.Count; i++)
            {
                string value = m1[i].Groups[1].Value;
                if ((value.Contains(@"<img") == true) || (value.Contains(@"</a>") == true) || (value.Contains(@"</script>") == true) || (value.Contains(@"</noscript>") == true))
                {

                }
                else
                {
                    string t1 = Regex.Replace(value, @"\s*<.*?>\s*", "", RegexOptions.Singleline);
                    if (!string.IsNullOrEmpty(t1))
                    {
                        description = description + " " + t1;
                    }
                }
            }
            return description;
        }
        public static void FinderContentAndDatePublish001(string html, Product product)
        {
            if (!string.IsNullOrEmpty(html))
            {
                DateTime now = DateTime.Now;
                html = html.Replace(@"~", @"");
                string htmlspan = html;
                MatchCollection m1;
                product.Active = false;
                DateTime datePublish = new DateTime(2019, 1, 1);
                string yearString = "";
                string monthString = "";
                string dayString = "";
                int year = now.Year;
                int month = now.Month;
                int day = now.Day;
                int hour = now.Hour;
                int minute = now.Minute;
                int second = now.Second;
                product.DatePublish = now;
                if (product.Active == false)
                {
                    htmlspan = html;
                    m1 = Regex.Matches(htmlspan, @"(<meta.*?/>)", RegexOptions.Singleline);
                    for (int i = 0; i < m1.Count; i++)
                    {
                        string value = m1[i].Groups[1].Value;
                        if ((value.Contains(@"published") == true) || (value.Contains(@"pubdate") == true))
                        {
                            value = value.Replace(@"content=""", @"~");
                            value = value.Replace(@"content='", @"~");
                            if (value.Split('~').Length > 1)
                            {
                                value = value.Split('~')[1];
                                value = value.Replace(@"""", @"~");
                                value = value.Replace(@"'", @"~");
                                value = value.Split('~')[0];
                                value = value.Trim();
                                try
                                {
                                    datePublish = DateTime.Parse(value);
                                }
                                catch
                                {
                                    try
                                    {
                                        yearString = value.Split('-')[0];
                                        monthString = value.Split('-')[1];
                                        dayString = value.Split('-')[2];
                                        dayString = dayString.Substring(0, 2);
                                        datePublish = new DateTime(int.Parse(yearString), int.Parse(monthString), int.Parse(dayString), hour, minute, second);
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (product.DatePublish > datePublish)
                                {
                                    product.DatePublish = datePublish;
                                    product.Active = true;
                                    i = m1.Count;
                                }
                            }
                        }
                    }
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish001(htmlspan, "dd", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "dd", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish001(htmlspan, "time", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "time", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish001(htmlspan, "span", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "span", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish001(htmlspan, "div", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "div", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish001(htmlspan, "h1", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "h1", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish001(htmlspan, "h2", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "h2", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish001(htmlspan, "h3", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "h3", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish001(htmlspan, "h4", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "h4", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish001(htmlspan, "h5", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "h5", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish001(htmlspan, "h6", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "h6", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish001(htmlspan, "li", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "li", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish001(htmlspan, "em", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "em", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish001(htmlspan, "i", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "i", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish001(htmlspan, "p", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "p", product);
                }
                if (string.IsNullOrEmpty(product.Description) || product.Description.Length < 1000)
                {
                    htmlspan = html;
                    FinderContent002(htmlspan, "p", product);
                }
                if (string.IsNullOrEmpty(product.Description) || product.Description.Length < 500)
                {
                    htmlspan = html;
                    product.Description = "";
                    FinderContent002(htmlspan, "div", product);
                }
            }
        }
        public static void FinderContentAndDatePublish002(string html, Product product)
        {
            if (!string.IsNullOrEmpty(html))
            {
                DateTime now = DateTime.Now;
                html = html.Replace(@"~", @"");
                string htmlspan = html;
                MatchCollection m1;
                product.Active = false;
                DateTime datePublish = new DateTime(2019, 1, 1);
                string yearString = "";
                string monthString = "";
                string dayString = "";
                int year = now.Year;
                int month = now.Month;
                int day = now.Day;
                int hour = now.Hour;
                int minute = now.Minute;
                int second = now.Second;
                product.DatePublish = now;
                if (product.Active == false)
                {
                    htmlspan = html;
                    m1 = Regex.Matches(htmlspan, @"(<meta.*?/>)", RegexOptions.Singleline);
                    for (int i = 0; i < m1.Count; i++)
                    {
                        string value = m1[i].Groups[1].Value;
                        if ((value.Contains(@"published") == true) || (value.Contains(@"pubdate") == true) || (value.Contains(@"dateModified") == true) || (value.Contains(@"dateCreated") == true))
                        {
                            value = value.Replace(@"content=""", @"~");
                            value = value.Replace(@"content='", @"~");
                            if (value.Split('~').Length > 1)
                            {
                                value = value.Split('~')[1];
                                value = value.Replace(@"""", @"~");
                                value = value.Replace(@"'", @"~");
                                value = value.Split('~')[0];
                                value = value.Trim();
                                bool datePublishCheck = false;
                                try
                                {
                                    int count = value.Count(f => f == '-');
                                    if (count > 1)
                                    {
                                        datePublish = DateTime.Parse(value);
                                        datePublishCheck = true;
                                    }
                                }
                                catch
                                {
                                    try
                                    {
                                        yearString = value.Split('-')[0];
                                        monthString = value.Split('-')[1];
                                        dayString = value.Split('-')[2];
                                        dayString = dayString.Substring(0, 2);
                                        datePublish = new DateTime(int.Parse(yearString), int.Parse(monthString), int.Parse(dayString), hour, minute, second);
                                        datePublishCheck = true;
                                    }
                                    catch
                                    {
                                    }
                                }
                                if (datePublishCheck == true)
                                {
                                    product.DatePublish = datePublish;
                                    product.Active = true;
                                    i = m1.Count;
                                }
                            }
                        }
                    }
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish003(htmlspan, product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "abbr", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "i", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "em", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "dd", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "time", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "h1", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "h2", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "h3", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "h4", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "h5", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "h6", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "li", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "span", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "div", product);
                }
                if (product.Active == false)
                {
                    htmlspan = html;
                    DatePublish002(htmlspan, "p", product);
                }
                if (string.IsNullOrEmpty(product.Description) || product.Description.Length < 1000)
                {
                    htmlspan = html;
                    FinderContent002(htmlspan, "p", product);
                }
                if (string.IsNullOrEmpty(product.Description) || product.Description.Length < 500)
                {
                    htmlspan = html;
                    product.Description = "";
                    FinderContent002(htmlspan, "div", product);
                }
            }
        }
        public static void FinderContent004(string html, Product product)
        {
            if (!string.IsNullOrEmpty(html))
            {
                string htmlspan = "";
                if (string.IsNullOrEmpty(product.Description) || product.Description.Length < 1000)
                {
                    htmlspan = html;
                    FinderContent002(htmlspan, "p", product);
                }
                if (string.IsNullOrEmpty(product.Description) || product.Description.Length < 500)
                {
                    htmlspan = html;
                    product.Description = "";
                    FinderContent002(htmlspan, "div", product);
                }
            }
        }
        public static string FinderTitle(string url)
        {
            string title = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string html = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                html = html.Replace(@"~", @"");
                string htmlTitle = html;
                if ((htmlTitle.Contains(@"<meta property=""og:title"" content=""") == true) || (htmlTitle.Contains(@"<meta property='og:title' content='") == true))
                {
                    htmlTitle = htmlTitle.Replace(@"<meta property=""og:title"" content=""", @"~");
                    htmlTitle = htmlTitle.Replace(@"<meta property='og:title' content='", @"~");
                    if (htmlTitle.Split('~').Length > 1)
                    {
                        htmlTitle = htmlTitle.Split('~')[1];
                        htmlTitle = htmlTitle.Replace(@"""", @"~");
                        htmlTitle = htmlTitle.Replace(@"'", @"~");
                        htmlTitle = htmlTitle.Split('~')[0];
                        title = htmlTitle.Trim();
                    }
                }
                else
                {
                    MatchCollection m1 = Regex.Matches(htmlTitle, @"(<title>.*?</title>)", RegexOptions.Singleline);
                    if (m1.Count > 0)
                    {
                        string value = m1[m1.Count - 1].Groups[1].Value;
                        if (!string.IsNullOrEmpty(value))
                        {
                            value = value.Replace(@"<title>", @"");
                            value = value.Replace(@"</title>", @"");
                            title = value.Trim();
                        }
                    }
                }
                bool isUnicode = AppGlobal.ContainsUnicodeCharacter(title);
                if ((title.Contains(@"&#") == true) || (isUnicode == false))
                {
                    MatchCollection m1 = Regex.Matches(htmlTitle, @"(<title>.*?</title>)", RegexOptions.Singleline);
                    if (m1.Count > 0)
                    {
                        string value = m1[m1.Count - 1].Groups[1].Value;
                        if (!string.IsNullOrEmpty(value))
                        {
                            value = value.Replace(@"<title>", @"");
                            value = value.Replace(@"</title>", @"");
                            title = value.Trim();
                        }
                    }
                }
                if (title.Split('|').Length > 2)
                {
                    title = title.Split('|')[1];
                }
                if (title.Split('|').Length > 1)
                {
                    title = title.Split('|')[0];
                }
                title = title.Trim();
            }
            return title;
        }
        public static string FinderTitle001(string url)
        {
            string title = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                readStream = new StreamReader(receiveStream, Encoding.UTF8);
                string html = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                html = html.Replace(@"~", @"");
                string htmlTitle = html;
                MatchCollection m1 = Regex.Matches(htmlTitle, @"(<title>.*?</title>)", RegexOptions.Singleline);
                if (m1.Count > 0)
                {
                    string value = m1[m1.Count - 1].Groups[1].Value;
                    if (!string.IsNullOrEmpty(value))
                    {
                        value = value.Replace(@"<title>", @"");
                        value = value.Replace(@"</title>", @"");
                        title = value.Trim();
                    }
                }
                if (title.Split('|').Length > 2)
                {
                    title = title.Split('|')[1];
                }
                if (title.Split('|').Length > 1)
                {
                    title = title.Split('|')[0];
                }
                title = title.Trim();
            }
            return title;
        }
        public static string FinderHTMLContent(string url)
        {
            string html = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                readStream = new StreamReader(receiveStream, Encoding.UTF8);
                html = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
                html = html.Replace(@"~", @"");
            }
            return html;
        }
        public static List<LinkItem> ImgFinder(string html)
        {
            List<LinkItem> list = new List<LinkItem>();
            if (!string.IsNullOrEmpty(html))
            {
                MatchCollection m1 = Regex.Matches(html, @"(<img.*?>)", RegexOptions.Singleline);
                foreach (Match m in m1)
                {
                    LinkItem i = new LinkItem();
                    string value = m.Groups[1].Value;
                    Match m2 = Regex.Match(value, @"src=\""(.*?)\""", RegexOptions.Singleline);
                    if (m2.Success)
                    {
                        i.Href = m2.Groups[1].Value;
                        if (!string.IsNullOrEmpty(i.Href))
                        {
                            list.Add(i);
                        }
                    }
                }
            }
            return list;
        }
        public static bool ContainsUnicodeCharacter(string input)
        {
            const int MaxAnsiCode = 255;
            return input.Any(c => c > MaxAnsiCode);
        }
        public static string Decode(string input)
        {
            string html = System.Text.RegularExpressions.Regex.Replace(input, @"\\u[0-9A-F]{4}", match => ((char)int.Parse(match.Value.Substring(2), System.Globalization.NumberStyles.HexNumber)).ToString(), RegexOptions.IgnoreCase);
            string result = System.Net.WebUtility.HtmlDecode(html);
            return result;
        }
        private static char[] tcvnchars = {
        'µ', '¸', '¶', '·', '¹',
        '¨', '»', '¾', '¼', '½', 'Æ',
        '©', 'Ç', 'Ê', 'È', 'É', 'Ë',
        '®', 'Ì', 'Ð', 'Î', 'Ï', 'Ñ',
        'ª', 'Ò', 'Õ', 'Ó', 'Ô', 'Ö',
        '×', 'Ý', 'Ø', 'Ü', 'Þ',
        'ß', 'ã', 'á', 'â', 'ä',
        '«', 'å', 'è', 'æ', 'ç', 'é',
        '¬', 'ê', 'í', 'ë', 'ì', 'î',
        'ï', 'ó', 'ñ', 'ò', 'ô',
        '­', 'õ', 'ø', 'ö', '÷', 'ù',
        'ú', 'ý', 'û', 'ü', 'þ',
        '¡', '¢', '§', '£', '¤', '¥', '¦'
        };
        private static char[] unichars = {
        'à', 'á', 'ả', 'ã', 'ạ',
        'ă', 'ằ', 'ắ', 'ẳ', 'ẵ', 'ặ',
        'â', 'ầ', 'ấ', 'ẩ', 'ẫ', 'ậ',
        'đ', 'è', 'é', 'ẻ', 'ẽ', 'ẹ',
        'ê', 'ề', 'ế', 'ể', 'ễ', 'ệ',
        'ì', 'í', 'ỉ', 'ĩ', 'ị',
        'ò', 'ó', 'ỏ', 'õ', 'ọ',
        'ô', 'ồ', 'ố', 'ổ', 'ỗ', 'ộ',
        'ơ', 'ờ', 'ớ', 'ở', 'ỡ', 'ợ',
        'ù', 'ú', 'ủ', 'ũ', 'ụ',
        'ư', 'ừ', 'ứ', 'ử', 'ữ', 'ự',
        'ỳ', 'ý', 'ỷ', 'ỹ', 'ỵ',
        'Ă', 'Â', 'Đ', 'Ê', 'Ô', 'Ơ', 'Ư'
         };
        public static string TCVN3ToUnicode(string value)
        {
            char[] convertTable = new char[256];
            for (int i = 0; i < 256; i++)
            {
                convertTable[i] = (char)i;
            }
            for (int i = 0; i < tcvnchars.Length; i++)
            {
                convertTable[tcvnchars[i]] = unichars[i];
            }
            char[] chars = value.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] < (char)256)
                {
                    chars[i] = convertTable[chars[i]];
                }
            }
            return new string(chars);
        }
        public static string ConvertASCIIToUnicode(string input)
        {
            string result = Encoding.UTF8.GetString(Encoding.ASCII.GetBytes(input));
            return result;
        }
        public static string ConvertWind1252ToUnicode(string input)
        {
            string result = Encoding.UTF8.GetString(Encoding.GetEncoding(1252).GetBytes(input));
            return result;
        }
        public static string ConvertLatin1ToUnicode(string input)
        {
            string result = Encoding.UTF8.GetString(Encoding.GetEncoding("iso-8859-1").GetBytes(input));
            return result;
        }
        public static string GetDescription(string html)
        {
            string result = "";
            string htmlspan = html;
            result = FinderContent003(htmlspan, "p");
            if (string.IsNullOrEmpty(result) || (result.Length < 500))
            {
                htmlspan = html;
                result = FinderContent003(htmlspan, "div");
            }
            result = result.Replace(@"ä", @"a");
            result = result.Replace(@"�", @"á");
            return result;
        }
        public static string ConvertStringToUnicode(string input)
        {
            string result = "";
            if (input.Contains(@"&#") == true)
            {
                result = Decode(input);
            }
            else
            {
                Encoding encoding = Encoding.GetEncoding("us-ascii", new EncoderReplacementFallback("(unknown)"), new DecoderReplacementFallback("(error)"));
                byte[] encodedBytes = new byte[encoding.GetByteCount(input)];
                int numberOfEncodedBytes = encoding.GetBytes(input, 0, input.Length, encodedBytes, 0);
                string decodedString = encoding.GetString(encodedBytes);
                if (decodedString.Contains(@"unknown") == false)
                {
                    result = ConvertASCIIToUnicode(input);
                }
                else
                {
                    encoding = Encoding.GetEncoding("iso-8859-1", new EncoderReplacementFallback("(unknown)"), new DecoderReplacementFallback("(error)"));
                    encodedBytes = new byte[encoding.GetByteCount(input)];
                    numberOfEncodedBytes = encoding.GetBytes(input, 0, input.Length, encodedBytes, 0);
                    decodedString = encoding.GetString(encodedBytes);
                    if (decodedString.Contains(@"unknown") == false)
                    {
                        result = ConvertLatin1ToUnicode(input);
                    }
                    else
                    {
                        encoding = Encoding.GetEncoding(1252, new EncoderReplacementFallback("(unknown)"), new DecoderReplacementFallback("(error)"));
                        encodedBytes = new byte[encoding.GetByteCount(input)];
                        numberOfEncodedBytes = encoding.GetBytes(input, 0, input.Length, encodedBytes, 0);
                        decodedString = encoding.GetString(encodedBytes);
                        if (decodedString.Contains(@"unknown") == false)
                        {
                            result = ConvertWind1252ToUnicode(input);
                        }
                        else
                        {
                            result = Decode(input);
                        }
                    }
                }
            }
            result = TCVN3ToUnicode(result);
            return result;
        }
        public static string HTMLReplaceAndSplit(string htmlspan001)
        {
            bool check = false;
            if (check == false)
            {
                if (htmlspan001.Contains(@"Tin khác") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"Tin khác", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"Tin liên quan") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"Tin liên quan", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"Bài viết liên quan") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"Bài viết liên quan", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"Đọc thêm") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"Đọc thêm", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }

            if (check == false)
            {
                if (htmlspan001.Contains(@"Cùng chuyên mục") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"Cùng chuyên mục", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"tag"">") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"tag"">", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"tag'>") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"tag'>", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"class=""tag") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"class=""tag", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"class='tag") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"class='tag", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"-tags") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"-tags", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"</main>") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"</main>", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"related-post") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"related-post", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"<footer>") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"<footer>", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"class=""footer") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"class=""footer", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"class='footer") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"class='footer", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"footer"">") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"footer"">", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }
            if (check == false)
            {
                if (htmlspan001.Contains(@"footer'>") == true)
                {
                    htmlspan001 = htmlspan001.Replace(@"footer'>", @"~");
                    htmlspan001 = htmlspan001.Split('~')[0];
                    check = true;
                }
            }

            return htmlspan001;
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
