﻿using System;
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

namespace Commsights.MVC.Controllers
{
    public class ConfigController : BaseController
    {
        private readonly IConfigRepository _configResposistory;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ConfigController(IHostingEnvironment hostingEnvironment, IConfigRepository configResposistory, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _configResposistory = configResposistory;
        }
        private void Initialization(Config model)
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
            model.ParentID = model.Parent.ID;
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
        public IActionResult WebsiteCategory()
        {
            return View();
        }
        public IActionResult Scan()
        {
            return View();
        }
        public IActionResult Brand()
        {
            return View();
        }
        public IActionResult BrandSub()
        {
            return View();
        }
        public IActionResult ArticleType()
        {
            return View();
        }
        public IActionResult AssessType()
        {
            return View();
        }
        public IActionResult PressList()
        {
            return View();
        }
        public IActionResult Upload()
        {
            return View();
        }
        public ActionResult GetAllToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetAllToList();
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
        public ActionResult GetArticleTypeToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.ArticleType).Where(item => item.ParentID == 0);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetPressListToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.PressList).Where(item => item.ParentID == 0);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetAssessTypeToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.AssessType).Where(item => item.ParentID == 0);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetBrandToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Brand).Where(item => item.ParentID == 0);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetWebisteToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website);
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
        public ActionResult GetDataTransferChildrenWebisteAndActiveToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetDataTransferChildrenCountByGroupNameAndCodeAndActiveToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferParentByGroupNameAndCodeAndActiveToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _configResposistory.GetDataTransferParentByGroupNameAndCodeAndActiveToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.Website, true);
            return Json(data.ToDataSourceResult(request));
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
        public IActionResult CreateBrand(Config model)
        {
            Initialization(model);
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Brand;
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
        public IActionResult CreateBrandSub(Config model, int parentID)
        {
            Initialization(model);
            model.ParentID = parentID;
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Brand;
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
            model.ParentID = parentID;
            model.GroupName = AppGlobal.CRM;
            model.Code = AppGlobal.Website;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_configResposistory.IsValidByGroupNameAndCodeAndURL(model.GroupName, model.Code, model.URLFull) == true)
            {
                //Config parent = _configResposistory.GetByID(parentID);
                //if (parent != null)
                //{
                //    model.IsMenuLeft = parent.IsMenuLeft;
                //}
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
        public IActionResult UpdateDataTransfer(ConfigDataTransfer model)
        {
            Initialization(model);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
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
        [AcceptVerbs("Post")]
        public ActionResult UploadPressList()
        {
            int result = 0;
            string action = "Upload";
            string controller = "Config";
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
                        file.CopyToAsync(stream);
                    }
                    FileInfo fileLocation = new FileInfo(physicalPath);
                    if (fileLocation.Length > 0)
                    {
                        if (fileExtension == ".xlsx")
                        {
                            using (ExcelPackage package = new ExcelPackage(fileLocation))
                            {
                                if (package.Workbook.Worksheets.Count > 0)
                                {
                                    ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                                    if (workSheet != null)
                                    {
                                        int totalRows = workSheet.Dimension.Rows;
                                        for (int i = 2; i <= totalRows; i++)
                                        {
                                            result = 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (result > 0)
            {
                action = "PressList";
                controller = "Config";
            }
            return RedirectToAction(action, controller);
        }
    }
}
