using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Commsights.Data.Repositories;
using Commsights.Data.Models;
using Commsights.Data.Helpers;
using Commsights.Data.Enum;
using Commsights.Data.DataTransferObject;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;
using System.Net;

namespace Commsights.MVC.Controllers
{
    public class ConfigController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfigRepository _configResposistory;

        public ConfigController(IWebHostEnvironment hostingEnvironment, IConfigRepository configResposistory, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _configResposistory = configResposistory;
        }
        private void Initialization(Config model)
        {
            if (string.IsNullOrEmpty(model.Controller))
            {
                model.Controller = "Config";
            }
            if (string.IsNullOrEmpty(model.Title))
            {
                model.Title = model.CodeName.Trim();
            }
            if (string.IsNullOrEmpty(model.Note))
            {
                model.Note = "";
            }
            if (!string.IsNullOrEmpty(model.Title))
            {
                model.Title = model.Title.Trim();
            }
            if (!string.IsNullOrEmpty(model.URLFull))
            {
                model.URLFull = model.URLFull.Trim();
            }
            if (!string.IsNullOrEmpty(model.CodeName))
            {
                model.CodeName = model.CodeName.Trim();
            }
            if (!string.IsNullOrEmpty(model.Note))
            {
                model.Note = model.Note.Trim();
            }
            if (string.IsNullOrEmpty(model.Icon))
            {
                model.Icon = "fa-circle";
            }
            if (string.IsNullOrEmpty(model.Action))
            {
                model.Action = "Index";
            }
            if (model.ParentID == null)
            {
                model.ParentID = 0;
            }
        }
        private void Initialization(ConfigDataTransfer model)
        {
            if (!string.IsNullOrEmpty(model.CodeName))
            {
                model.CodeName = model.CodeName.Trim();
            }
            if (!string.IsNullOrEmpty(model.Note))
            {
                model.Note = model.Note.Trim();
            }
            if (string.IsNullOrEmpty(model.Icon))
            {
                model.Icon = "fa fa-circle-o";
            }
            if (string.IsNullOrEmpty(model.Action))
            {
                model.Action = "Index";
            }
            if (model.Parent != null)
            {
                model.ParentID = model.Parent.ID;
            }
            if (model.Country != null)
            {
                model.CountryID = model.Country.ID;
            }
            if (model.Language != null)
            {
                model.LanguageID = model.Language.ID;
            }
            if (model.Frequency != null)
            {
                model.FrequencyID = model.Frequency.ID;
            }
            if (model.ColorType != null)
            {
                model.ColorTypeID = model.ColorType.ID;
            }
            model.Active = true;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MembershipType()
        {
            return View();
        }
        public IActionResult WebsiteType()
        {
            return View();
        }
        public IActionResult Menu()
        {
            return View();
        }
        public IActionResult WebsiteSite()
        {
            return View();
        }
        public IActionResult WebsiteCategory()
        {
            return View();
        }
        public IActionResult Scan()
        {
            return View();
        }
        public IActionResult Industry()
        {
            return View();
        }
        public IActionResult Segment()
        {
            return View();
        }
        public IActionResult ArticleType()
        {
            return View();
        }
        public IActionResult CategoryMainByIndustryID()
        {
            return View();
        }
        public IActionResult CategoryMain()
        {
            return View();
        }
        public IActionResult CategorySub()
        {
            return View();
        }
        public IActionResult CampaignName()
        {
            return View();
        }
        public IActionResult CampaignKeyMessage()
        {
            return View();
        }
        public IActionResult IndustryCategory()
        {
            return View();
        }
        public IActionResult ProductFeature()
        {
            return View();
        }
        public IActionResult IndustryKeyWord()
        {
            return View();
        }
        public IActionResult KeyMessage()
        {
            return View();
        }
        public IActionResult Feature()
        {
            return View();
        }
        public IActionResult CorpCopy()
        {
            return View();
        }
        public IActionResult AssessType()
        {
            return View();
        }
        public IActionResult Sentiment()
        {
            return View();
        }
        public IActionResult PressList001()
        {
            return View();
        }
        public IActionResult PressList()
        {
            return View();
        }
        public IActionResult Country()
        {
            return View();
        }
        public IActionResult Frequency()
        {
            return View();
        }
        public IActionResult Language()
        {
            return View();
        }
        public IActionResult Color()
        {
            return View();
        }
        public IActionResult Upload()
        {
            return View();
        }
        public IActionResult DailyReportColumn()
        {
            return View();
        }
        public IActionResult DailyReportSection()
        {
            return View();
        }
        public IActionResult ReportType()
        {
            return View();
        }
        public IActionResult EmailStorageCategory()
        {
            return View();
        }
        public IActionResult MediaTier()
        {
            return View();
        }
        public IActionResult MediaTierAndWebsite()
        {
            return View();
        }
        public IActionResult TotalSize()
        {
            return View();
        }
        public IActionResult PressListLogo()
        {
            Config model = new Config();
            return View(model);
        }
        public IActionResult WebsiteScan(int ID)
        {
            Config model = _configResposistory.GetByID(ID);
            model.Note = "";
            return View(model);
        }
        public ActionResult GetAll001ToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetAll001ToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetAllToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetAllToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetMediaToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetMediaToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetMediaFullToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetMediaFullToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetMediaByGroupNameAndActiveToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetMediaByGroupNameAndActiveToList(AppGlobal.CRM, true);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetMediaByGroupNameToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetMediaByGroupNameToList(AppGlobal.CRM);
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult GetByParentIDToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            List<Config> data = new List<Config>();
            if (parentID > 0)
            {
                data = _configResposistory.GetByParentIDToList(parentID);
            }
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetSegmentByParentID001ToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            List<Config> data = new List<Config>();
            if (parentID > 0)
            {
                data = _configResposistory.GetByParentIDToList(parentID).OrderBy(item => item.CodeName).ToList();
            }
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetWebsiteByParentIDAndGroupNameAndCodeToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            List<Config> data = new List<Config>();
            if (parentID > 0)
            {
                data = _configResposistory.GetByParentIDAndGroupNameAndCodeToList(parentID, AppGlobal.CRM, AppGlobal.Website);
            }
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetReportTypeToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.ReportType);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferTierByTierIDAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, int tierID, int industryID)
        {
            if (industryID != AppGlobal.TierID02)
            {
                industryID = 0;
            }
            var data = _configResposistory.GetDataTransferTierByTierIDAndIndustryIDToList(tierID, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetTotalSizeToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.TotalSize);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetColorToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Color).Where(item => item.ParentID == 0);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCountryToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Country).Where(item => item.ParentID == 0);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetFrequencyToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Frequency).Where(item => item.ParentID == 0);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetLanguageToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Language).Where(item => item.ParentID == 0);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetMenuToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Menu).OrderBy(model => model.SortOrder);
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult GetWebsiteTypeToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.WebsiteType);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDailyReportColumnToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.DailyReportColumn);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDailyReportSectionToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.DailyReportSection).Where(item => item.ParentID == 0);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetArticleTypeToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.ArticleType).Where(item => item.ParentID == 0);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCorpCopyToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.CorpCopy).Where(item => item.ParentID == 0);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetFeatureToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Feature).Where(item => item.ParentID == 0);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCategoryMainToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.CategoryMain).Where(item => item.IndustryID == 0 || item.IndustryID == null).OrderByDescending(item => item.Active);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCodeDataCategoryMainActiveToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.CategoryMain).Where(item => item.Active == true).OrderBy(item => item.SortOrder);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetSQLWebsiteByGroupNameAndCodeAndActiveAndIsMenuLeftToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetSQLWebsiteByGroupNameAndCodeAndActiveAndIsMenuLeftToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true, true);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetSegmentByParentIDToList([DataSourceRequest] DataSourceRequest request)
        {
            List<Config> data = new List<Config>();
            int parentID = 0;
            try
            {
                parentID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
                if (parentID > 0)
                {
                    data = _configResposistory.GetByParentIDToList(parentID);
                }
            }
            catch
            {
            }
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCampaignKeyMessageByCampaignNameAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, string campaignName)
        {
            int industryID = 0;
            try
            {
                industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
            }
            catch
            {
            }
            Config parent = new Config();
            if (!string.IsNullOrEmpty(campaignName))
            {
                parent = _configResposistory.GetByGroupNameAndCodeAndCodeName(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Campaign, campaignName);
            }
            var data = _configResposistory.GetByGroupNameAndCodeAndParentIDAndIndustryIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.CampaignKeyMessage, parent.ID, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCampaignKeyMessageByParentIDAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, int parentID, int industryID)
        {
            var data = _configResposistory.GetByGroupNameAndCodeAndParentIDAndIndustryIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.CampaignKeyMessage, parentID, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCategorySubByIndustryIDToList([DataSourceRequest] DataSourceRequest request)
        {
            int industryID = 0;
            try
            {
                industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
            }
            catch
            {
            }
            var data = _configResposistory.GetSQLCategorySubByGroupNameAndCodeAndIndustryIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.CategorySub, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCategorySubByCategoryMainAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, string categoryMain)
        {
            int parentID = 0;
            int industryID = 0;
            try
            {
                industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
            }
            catch
            {
            }
            Config categoryMain001 = _configResposistory.GetByGroupNameAndCodeAndIndustryIDAndCodeName(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.CategoryMain, industryID, categoryMain);
            if (categoryMain001 != null)
            {
                parentID = categoryMain001.ID;
            }
            var data = _configResposistory.GetSQLByGroupNameAndCodeAndIndustryIDAndParentIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.CategorySub, industryID, parentID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCategorySubByParentIDAndIndustryIDToList([DataSourceRequest] DataSourceRequest request, int parentID, int industryID)
        {
            var data = _configResposistory.GetByGroupNameAndCodeAndParentIDAndIndustryIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.CategorySub, parentID, industryID);
            return Json(data.ToDataSourceResult(request));
        }

        public ActionResult GetCampaignNameByIndustryIDToList([DataSourceRequest] DataSourceRequest request, int industryID)
        {
            var data = _configResposistory.GetByGroupNameAndCodeAndIndustryIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Campaign, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetKeyMessageByIndustryID001ToList([DataSourceRequest] DataSourceRequest request)
        {
            int industryID = 0;
            try
            {
                industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
            }
            catch
            {
            }
            var data = _configResposistory.GetSQLByGroupNameAndCodeAndIndustryIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.KeyMessage, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCampaignNameByIndustryID001ToList([DataSourceRequest] DataSourceRequest request)
        {
            int industryID = 0;
            try
            {
                industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
            }
            catch
            {
            }
            var data = _configResposistory.GetSQLByGroupNameAndCodeAndIndustryIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Campaign, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCategoryMainByIndustryID001ToList([DataSourceRequest] DataSourceRequest request)
        {
            int industryID = 0;
            try
            {
                industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
            }
            catch
            {
            }
            var data = _configResposistory.GetSQLByGroupNameAndCodeAndIndustryIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.CategoryMain, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCategoryMainByIndustryIDToList([DataSourceRequest] DataSourceRequest request, int industryID)
        {
            var data = _configResposistory.GetByGroupNameAndCodeAndIndustryIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.CategoryMain, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetIndustryKeyWordByIndustryIDToList([DataSourceRequest] DataSourceRequest request, int industryID)
        {
            var data = _configResposistory.GetByGroupNameAndCodeAndIndustryIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.IndustryKeyWord, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetIndustryCategoryByIndustryIDToList([DataSourceRequest] DataSourceRequest request, int industryID)
        {
            var data = _configResposistory.GetByGroupNameAndCodeAndIndustryIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.IndustryCategory, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProductFeatureByIndustryIDToList([DataSourceRequest] DataSourceRequest request, int industryID)
        {
            var data = _configResposistory.GetByGroupNameAndCodeAndIndustryIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.ProductFeature, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetIndustryCategoryByIndustryIDCookieToList([DataSourceRequest] DataSourceRequest request)
        {
            int industryID = 0;
            try
            {
                industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
            }
            catch
            {
            }
            var data = _configResposistory.GetByGroupNameAndCodeAndIndustryIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.IndustryCategory, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetKeyMessageByIndustryIDToList([DataSourceRequest] DataSourceRequest request, int industryID)
        {
            var data = _configResposistory.GetByGroupNameAndCodeAndIndustryIDToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.KeyMessage, industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCategorySubToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetSQLByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.CategorySub);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetMediaTierToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.MediaTier).Where(item => item.ParentID == 0);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferPressListByGroupNameAndCodeToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetDataTransferPressListByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.PressList);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetPressListToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.PressList);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetAssessTypeToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.AssessType);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetSentimentToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Sentiment);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetSegmentToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Segment);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetIndustryToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Industry).Where(item => item.ParentID == 0);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetWebisteToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetEmailStorageCategoryToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.EmailStorageCategory);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetMembershipTypeToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.MembershipType);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetWebisteAndActiveToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeAndActiveToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetWebisteAndActiveAndIsMenuLeftToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeAndActiveAndIsMenuLeftToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true, true);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferChildrenWebisteAndActiveToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetDataTransferChildrenCountByGroupNameAndCodeAndActiveToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferChildrenCountByGroupNameAndCodeAndActiveAndIsMenuLeftToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetDataTransferChildrenCountByGroupNameAndCodeAndActiveAndIsMenuLeftToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true, true);
            return Json(data.ToDataSourceResult(request));
        }
        public List<Config> GetWebsiteByGroupNameAndCodeAndActiveToList()
        {
            return _configResposistory.GetByGroupNameAndCodeAndActiveToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true);
        }
        public ActionResult GetSQLWebsiteByGroupNameAndCodeAndActiveToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetSQLWebsiteByGroupNameAndCodeAndActiveToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetSQLWebsiteByGroupNameAndCodeToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetSQLWebsiteByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetWebsiteByGroupNameAndCodeAndActiveToList001([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeAndActiveToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferParentByGroupNameAndCodeAndActiveToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetDataTransferParentByGroupNameAndCodeAndActiveToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferWebsiteByGroupNameAndCodeAndActiveToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetDataTransferWebsiteByGroupNameAndCodeAndActiveToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProductPermissionDistinctIndustryByEmployeeIDToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetProductPermissionDistinctIndustryByEmployeeIDToList(RequestUserID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetWebsiteScanByIDToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            List<Config> list = new List<Config>();
            Random random = new Random();
            Config config = _configResposistory.GetByID(parentID);
            if (config != null)
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = System.Text.Encoding.UTF8;
                string html = "";
                try
                {
                    html = webClient.DownloadString(config.URLFull);
                    List<LinkItem> listLinkItem = AppGlobal.LinkFinder(html, config.URLFull);
                    foreach (LinkItem linkItem in listLinkItem)
                    {
                        Config item = new Config();
                        item.Active = false;
                        item.ID = random.Next(1000000);
                        item.Title = linkItem.Text;
                        item.URLFull = linkItem.Href;
                        item.Note = item.Title + "~" + item.URLFull;
                        list.Add(item);
                    }
                }
                catch (Exception e)
                {

                }
            }
            var data = list.OrderBy(item => item.Title.Length);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult CreateTierByTierIDAndIndustryID(ConfigDataTransfer model, int tierID, int industryID)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Tier;
            model.ParentID = model.Parent.ID;
            model.TierID = tierID;
            model.IndustryID = industryID;
            if (industryID != AppGlobal.TierID02)
            {
                model.IndustryID = 0;
            }
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (model.ParentID > 0)
            {
                result = _configResposistory.Create(model);
            }
            else
            {
                model.Note = model.Note.Replace(@";", @",");
                foreach (string mediaTitle in model.Note.Split(','))
                {
                    Config parent = new Config();
                    string mediaTitle001 = mediaTitle.Trim();
                    if (!string.IsNullOrEmpty(mediaTitle001))
                    {
                        parent = _configResposistory.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, AppGlobal.Website, mediaTitle001);
                        if (parent == null)
                        {
                            parent = _configResposistory.GetByGroupNameAndCodeAndTitle(AppGlobal.CRM, AppGlobal.PressList, mediaTitle001);
                        }
                    }
                    if (parent != null)
                    {
                        Config config = new Config();
                        config = model;
                        config.ParentID = parent.ID;
                        config.Note = "";
                        config.ID = 0;
                        if (config.ParentID > 0)
                        {
                            result = _configResposistory.Create(config);
                        }
                    }
                }
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateEmailStorageCategory(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.EmailStorageCategory;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateTotalSize(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.TotalSize;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateReportType(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.ReportType;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateColor(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Color;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateCountry(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Country;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateFrequency(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Frequency;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateLanguage(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Language;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }

        public IActionResult CreateAssessType(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.AssessType;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateSentiment(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Sentiment;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateMediaTier(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.MediaTier;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreatePressList(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.PressList;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateArticleType(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.ArticleType;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateCorpCopy(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.CorpCopy;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateFeature(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Feature;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateCategoryMain(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.CategoryMain;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateCategoryMainByIndustryID(Config model, int industryID)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.CategoryMain;
            model.IndustryID = industryID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateIndustryCategory(Config model, int industryID)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.IndustryCategory;
            model.IndustryID = industryID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateProductFeature(Config model, int industryID)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.ProductFeature;
            model.IndustryID = industryID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateIndustryKeyWord(Config model, int industryID)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.IndustryKeyWord;
            model.IndustryID = industryID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateKeyMessage(Config model, int industryID)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.KeyMessage;
            model.IndustryID = industryID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateCampaignName(Config model, int industryID)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Campaign;
            model.IndustryID = industryID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateCampaignKeyMessage(Config model, int parentID, int industryID)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.CampaignKeyMessage;
            model.ParentID = parentID;
            model.IndustryID = industryID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateCategorySub(Config model, int parentID, int industryID)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.CategorySub;
            model.ParentID = parentID;
            model.IndustryID = industryID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateDailyReportSection(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.DailyReportSection;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateDailyReportColumn(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.DailyReportColumn;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateIndustry(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Industry;
            model.ParentID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateIndustrySub(Config model, int parentID)
        {
            Initialization(model);
            model.ParentID = parentID;
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Industry;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateSegment(Config model, int parentID)
        {
            Initialization(model);
            model.ParentID = parentID;
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Segment;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateMenu(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Menu;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateMembershipType(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.MembershipType;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateWebsiteType(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.WebsiteType;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateWebiste(Config model, int parentID)
        {
            Initialization(model);
            if (string.IsNullOrEmpty(model.Title))
            {
                model.Title = model.URLFull;
            }
            if (string.IsNullOrEmpty(model.URLFull))
            {
                model.URLFull = model.Title;
            }
            model.ParentID = parentID;
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Website;
            model.Active = false;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndURL(model.GroupName, model.Code, model.URLFull) == true)
            {
                model.ID = 0;
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }



        public IActionResult CreateWebisteDataTransfer(ConfigDataTransfer model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Website;
            model.Active = true;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndURL(model.GroupName, model.Code, model.URLFull) == true)
            {
                Uri myUri = new Uri(model.URLFull);
                model.Title = myUri.Host;
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult Create(Config model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult Update(Config model)
        {
            Initialization(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _configResposistory.Update(model.ID, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }
        public IActionResult UpdateSingleItem001(Config model)
        {
            string note = AppGlobal.InitString;
            _configResposistory.UpdateSingleItem001(model);
            int result = 1;
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }
        public IActionResult UpdateDataTransferPressList(ConfigDataTransfer model)
        {
            Initialization(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _configResposistory.Update(model.ID, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }
        public IActionResult CreateDataTransferPressList(ConfigDataTransfer model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.PressList;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndTitle(model.GroupName, model.Code, model.Title) == true)
            {
                result = _configResposistory.Create(model);
            }
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult UpdateMediaTier(ConfigDataTransfer model)
        {
            Initialization(model);
            string note = AppGlobal.InitString;
            model.ParentID = model.Parent.ID;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _configResposistory.Update(model.ID, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }
        public IActionResult UpdateDataTransfer(ConfigDataTransfer model)
        {
            Initialization(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            if (model.Code == AppGlobal.Website)
            {
                Uri myUri = new Uri(model.URLFull);
                model.Title = myUri.Host;
            }
            int result = _configResposistory.Update(model.ID, model);
            if (result > 0)
            {
                if (model.Code == AppGlobal.Website)
                {
                    List<Config> list = _configResposistory.GetByParentIDToList(model.ID);
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].IsMenuLeft = model.IsMenuLeft;
                    }
                    _configResposistory.UpdateRange(list);
                }
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }

        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _configResposistory.Delete(ID);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.DeleteFail;
            }
            return Json(note);
        }
        public IActionResult DeleteMenu(int ID)
        {
            string note = AppGlobal.InitString;
            int result = 1;
            _configResposistory.DeleteMenuByID(ID);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.DeleteFail;
            }
            return Json(note);
        }
        public IActionResult DeleteByParentIDAndGroupNameAndCode(int parentID)
        {
            string note = AppGlobal.InitString;
            note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            List<Config> list = _configResposistory.GetByParentIDAndGroupNameAndCodeToList(parentID, AppGlobal.CRM, AppGlobal.Website);
            _configResposistory.DeleteRange(list);
            return Json(note);
        }
        public void InitializationCategoryMainAndSubByIndustryID(int industryID)
        {
            Config model = new Config();
            model.CodeName = "Industry News";
            model.Note = "Tin ngành";
            model.SortOrder = 1;
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.CategoryMain;
            model.IndustryID = industryID;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = _configResposistory.Create(model);

            model = new Config();
            model.CodeName = "Corporate";
            model.Note = "Tin công ty";
            model.SortOrder = 2;
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.CategoryMain;
            model.IndustryID = industryID;
            model.Initialization(InitType.Insert, RequestUserID);
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                Config modelSub = new Config();
                modelSub.CodeName = "Retail & Distribution News";
                modelSub.Note = "Tin tức Bán lẻ & Phân phối";
                modelSub.SortOrder = 1;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);

                modelSub = new Config();
                modelSub.CodeName = "CSR News";
                modelSub.Note = "Tin cộng đồng";
                modelSub.SortOrder = 2;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);

                modelSub = new Config();
                modelSub.CodeName = "Financial news";
                modelSub.Note = "Bản tin tài chính";
                modelSub.SortOrder = 3;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);

                modelSub = new Config();
                modelSub.CodeName = "HR News";
                modelSub.Note = "Tin nhân sự";
                modelSub.SortOrder = 4;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);

                modelSub = new Config();
                modelSub.CodeName = "Investment/M&A News";
                modelSub.Note = "Tin đầu tư, mua bán, sát nhập";
                modelSub.SortOrder = 5;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);

                modelSub = new Config();
                modelSub.CodeName = "Brand & Sponsorship";
                modelSub.Note = "Thương hiệu & Tài trợ";
                modelSub.SortOrder = 6;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);

                modelSub = new Config();
                modelSub.CodeName = "Lawsuit News";
                modelSub.Note = "Tin kiện";
                modelSub.SortOrder = 7;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);

                modelSub = new Config();
                modelSub.CodeName = "Customer Service";
                modelSub.Note = "Dịch vụ khách hàng";
                modelSub.SortOrder = 8;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);

                modelSub = new Config();
                modelSub.CodeName = "General News";
                modelSub.Note = "Tin chung";
                modelSub.SortOrder = 9;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);

                modelSub = new Config();
                modelSub.CodeName = "Rewarding, Recognization";
                modelSub.Note = "Khen thưởng, công nhận";
                modelSub.SortOrder = 10;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);

                modelSub = new Config();
                modelSub.CodeName = "Other";
                modelSub.Note = "Tin khác";
                modelSub.SortOrder = 11;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);
            }

            model = new Config();
            model.CodeName = "Product & Service";
            model.Note = "Sản phẩm - Dịch vụ";
            model.SortOrder = 3;
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.CategoryMain;
            model.IndustryID = industryID;
            model.Initialization(InitType.Insert, RequestUserID);
            result = _configResposistory.Create(model);
            if (result > 0)
            {
                Config modelSub = new Config();
                modelSub.CodeName = "Product & Service";
                modelSub.Note = "Sản phẩm - Dịch vụ";
                modelSub.SortOrder = 1;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);

                modelSub = new Config();
                modelSub.CodeName = "Launching new product";
                modelSub.Note = "Ra mắt sản phẩm mới";
                modelSub.SortOrder = 2;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);

                modelSub = new Config();
                modelSub.CodeName = "Promotion News/Marketing News";
                modelSub.Note = "Tin khuyến mãi / Tin tiếp thị";
                modelSub.SortOrder = 3;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);

                modelSub = new Config();
                modelSub.CodeName = "Brand & Sponsorship";
                modelSub.Note = "Thương hiệu & Tài trợ";
                modelSub.SortOrder = 4;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);

                modelSub = new Config();
                modelSub.CodeName = "General News";
                modelSub.Note = "Tin chung";
                modelSub.SortOrder = 5;
                Initialization(modelSub);
                modelSub.GroupName = AppGlobal.CRM;
                modelSub.Code = AppGlobal.CategorySub;
                modelSub.IndustryID = industryID;
                modelSub.ParentID = model.ID;
                modelSub.Initialization(InitType.Insert, RequestUserID);
                _configResposistory.Create(modelSub);
            }
        }
        public IActionResult SaveWebsiteScanItems(int parentID, string listValue)
        {
            string note = AppGlobal.InitString;
            note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            foreach (string item in listValue.Split(';'))
            {
                if (item.Split('~').Length > 1)
                {
                    Config model = new Config();
                    model.Title = item.Split('~')[0];
                    model.URLFull = item.Split('~')[1];
                    Initialization(model);
                    if (string.IsNullOrEmpty(model.Title))
                    {
                        model.Title = model.URLFull;
                    }
                    if (string.IsNullOrEmpty(model.URLFull))
                    {
                        model.URLFull = model.Title;
                    }
                    model.ParentID = parentID;
                    model.GroupName = AppGlobal.CRM;
                    model.Code = AppGlobal.Website;
                    model.Active = false;
                    model.Initialization(InitType.Insert, RequestUserID);
                    if (_configResposistory.IsValidByGroupNameAndCodeAndURL(model.GroupName, model.Code, model.URLFull) == true)
                    {
                        _configResposistory.Create(model);
                    }
                }
            }
            return Json(note);
        }
        public ActionResult UploadPressList()
        {
            int result = 0;
            string action = "Upload";
            string controller = "Config";
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    if (file == null || file.Length == 0)
                    {
                    }
                    if (file != null)
                    {
                        string fileExtension = Path.GetExtension(file.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        fileName = AppGlobal.PressList;
                        fileName = fileName + "-" + AppGlobal.DateTimeCode + fileExtension;
                        var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.FTPUploadExcel, fileName);
                        using (var stream = new FileStream(physicalPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                            FileInfo fileLocation = new FileInfo(physicalPath);
                            if (fileLocation.Length > 0)
                            {
                                if ((fileExtension == ".xlsx") || (fileExtension == ".xls"))
                                {
                                    using (ExcelPackage package = new ExcelPackage(stream))
                                    {
                                        if (package.Workbook.Worksheets.Count > 0)
                                        {
                                            ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                                            if (workSheet != null)
                                            {
                                                int totalRows = workSheet.Dimension.Rows;
                                                //List<Config> list = _configResposistory.GetByGroupNameAndCodeAndActiveToList(AppGlobal.CRM, AppGlobal.Website, true);
                                                //for (int i = 1; i <= totalRows; i++)
                                                //{
                                                //    Config model = new Config();
                                                //    if (workSheet.Cells[i, 1].Value != null)
                                                //    {
                                                //        model.Title = workSheet.Cells[i, 1].Value.ToString().Trim();
                                                //    }
                                                //    if (workSheet.Cells[i, 2].Value != null)
                                                //    {
                                                //        model.Note = workSheet.Cells[i, 2].Value.ToString().Trim();
                                                //        model.Note = model.Note.Replace(@",", "");
                                                //        model.Note = model.Note.Replace(@".", "");
                                                //        try
                                                //        {
                                                //            model.Color = int.Parse(model.Note);
                                                //            foreach (Config item in list)
                                                //            {
                                                //                if (item.Title.ToLower() == model.Title.ToLower())
                                                //                {
                                                //                    item.Color = model.Color;
                                                //                    _configResposistory.Update(item.ID, item);
                                                //                }
                                                //            }
                                                //        }
                                                //        catch
                                                //        {
                                                //        }
                                                //    }
                                                //}

                                                List<Config> list = new List<Config>();
                                                for (int i = 2; i <= totalRows; i++)
                                                {
                                                    Config model = new Config();
                                                    model.GroupName = AppGlobal.CRM;
                                                    model.Code = AppGlobal.PressList;
                                                    model.Initialization(InitType.Insert, RequestUserID);
                                                    try
                                                    {
                                                        if (workSheet.Cells[i, 2].Value != null)
                                                        {
                                                            model.CodeName = workSheet.Cells[i, 2].Value.ToString().Trim();
                                                        }
                                                        if (workSheet.Cells[i, 3].Value != null)
                                                        {
                                                            string blackWhite = workSheet.Cells[i, 3].Value.ToString().Trim();
                                                            blackWhite = blackWhite.Replace(@",", "");
                                                            blackWhite = blackWhite.Replace(@".", "");
                                                            model.BlackWhite = int.Parse(blackWhite);
                                                        }
                                                        if (workSheet.Cells[i, 4].Value != null)
                                                        {
                                                            string color = workSheet.Cells[i, 4].Value.ToString().Trim();
                                                            color = color.Replace(@",", "");
                                                            color = color.Replace(@".", "");
                                                            model.Color = int.Parse(color);
                                                        }
                                                    }
                                                    catch
                                                    {

                                                    }
                                                    if (_configResposistory.IsValidByGroupNameAndCodeAndCodeName(model.GroupName, model.Code, model.CodeName))
                                                    {
                                                        list.Add(model);
                                                    }
                                                }
                                                result = _configResposistory.Range(list);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            if (result > 0)
            {
                action = "PressList";
                controller = "Config";
            }
            return RedirectToAction(action, controller);
        }


        public ActionResult UploadPressListLogo(Commsights.Data.Models.Config model)
        {
            Config config = _configResposistory.GetByID(model.ID);
            try
            {
                if (config != null)
                {
                    if (config.ID > 0)
                    {
                        if (Request.Form.Files.Count > 0)
                        {
                            var file = Request.Form.Files[0];
                            if (file == null || file.Length == 0)
                            {
                            }
                            if (file != null)
                            {
                                string fileExtension = Path.GetExtension(file.FileName);
                                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                                fileName = fileName + "-" + AppGlobal.DateTimeCode + fileExtension;
                                fileName = fileName.Replace(@"%", @"p");
                                string directoryDay = AppGlobal.DateTimeCodeYearMonthDay;
                                string mainPath = _hostingEnvironment.WebRootPath;
                                string url = AppGlobal.Domain;
                                url = url + AppGlobal.PressList + "/" + fileName;
                                string subPath = AppGlobal.PressList;
                                string fullPath = mainPath + @"\" + subPath;
                                if (!Directory.Exists(fullPath))
                                {
                                    Directory.CreateDirectory(fullPath);
                                }
                                var physicalPath = Path.Combine(mainPath, subPath, fileName);
                                using (var stream = new FileStream(physicalPath, FileMode.Create))
                                {
                                    file.CopyTo(stream);
                                    config.Note = fileName;
                                    config.Icon = url;
                                    config.Initialization(InitType.Update, RequestUserID);
                                    _configResposistory.Update(config.ID, config);
                                }
                            }

                        }
                    }
                }
            }
            catch
            {
            }
            return RedirectToAction("PressListLogo");
        }
    }
}
