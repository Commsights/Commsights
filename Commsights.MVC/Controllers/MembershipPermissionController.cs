﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Commsights.Data.DataTransferObject;
using Commsights.Data.Enum;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using Commsights.Data.Repositories;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Commsights.MVC.Controllers
{
    public class MembershipPermissionController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IMembershipPermissionRepository _membershipPermissionRepository;
        public MembershipPermissionController(IHostingEnvironment hostingEnvironment, IMembershipPermissionRepository membershipPermissionRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _membershipPermissionRepository = membershipPermissionRepository;
        }
        private void Initialization()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Industry(int ID)
        {
            MembershipPermission model = new MembershipPermission();
            if (ID > 0)
            {
                model = _membershipPermissionRepository.GetByID(ID);
            }
            return View(model);
        }
        public IActionResult Product(int ID)
        {
            MembershipPermission model = new MembershipPermission();
            if (ID > 0)
            {
                model = _membershipPermissionRepository.GetByID(ID);
            }
            return View(model);
        }
        public ActionResult GetByMembershipIDToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetByMembershipIDToList(membershipID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferMembershipByIndustryIDAndCodeToList([DataSourceRequest] DataSourceRequest request, int industryID)
        {
            var data = _membershipPermissionRepository.GetDataTransferMembershipByIndustryIDAndCodeToList(industryID, AppGlobal.Industry);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferMembershipBySegmentIDAndProductToList([DataSourceRequest] DataSourceRequest request, int segmentID)
        {
            var data = _membershipPermissionRepository.GetDataTransferMembershipBySegmentIDAndCodeToList(segmentID, AppGlobal.Product);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferIndustryByParentIDToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _membershipPermissionRepository.GetDataTransferIndustryByParentIDToList(parentID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferIndustryByParentIDAndCodeToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _membershipPermissionRepository.GetDataTransferIndustryByParentIDAndCodeToList(parentID, AppGlobal.Industry);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferProductByParentIDAndCodeToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _membershipPermissionRepository.GetDataTransferProductByParentIDAndCodeToList(parentID, AppGlobal.Product);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferIndustryByMembershipIDAndIndustryToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetDataTransferIndustryByMembershipIDAndCodeToList(membershipID, AppGlobal.Industry);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferSegmentByMembershipIDAndProductToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetDataTransferSegmentByMembershipIDAndCodeToList(membershipID, AppGlobal.Product);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferSegmentByMembershipIDAndIndustryIDAndCodeToList([DataSourceRequest] DataSourceRequest request, int membershipID, int industryID)
        {
            var data = _membershipPermissionRepository.GetDataTransferSegmentByMembershipIDAndIndustryIDAndCodeToList(membershipID, industryID, AppGlobal.Product);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferContactByMembershipIDAndContactToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetDataTransferContactByMembershipIDAndCodeToList(membershipID, AppGlobal.Contact);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferContactByMembershipIDAndIndustryIDAndCodeToList([DataSourceRequest] DataSourceRequest request, int membershipID, int industryID)
        {
            var data = _membershipPermissionRepository.GetDataTransferContactByMembershipIDAndIndustryIDAndCodeToList(membershipID, industryID, AppGlobal.Contact);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferDailyReportSectionByMembershipIDAndDailyReportSectionToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetDataTransferDailyReportSectionByMembershipIDAndCodeToList(membershipID, AppGlobal.DailyReportSection);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferDailyReportSectionByMembershipIDAndIndustryIDAndCodeToList([DataSourceRequest] DataSourceRequest request, int membershipID, int industryID)
        {
            var data = _membershipPermissionRepository.GetDataTransferDailyReportSectionByMembershipIDAndIndustryIDAndCodeToList(membershipID, industryID, AppGlobal.DailyReportSection);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferDailyReportColumnByMembershipIDAndDailyReportColumnToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetDataTransferDailyReportColumnByMembershipIDAndCodeToList(membershipID, AppGlobal.DailyReportColumn);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferDailyReportColumnByMembershipIDAndIndustryIDAndCodeToList([DataSourceRequest] DataSourceRequest request, int membershipID, int industryID)
        {
            var data = _membershipPermissionRepository.GetDataTransferDailyReportColumnByMembershipIDAndIndustryIDAndCodeToList(membershipID, industryID, AppGlobal.DailyReportColumn);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferCompanyByMembershipIDAndCompetitorToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetDataTransferCompanyByMembershipIDAndCodeToList(membershipID, AppGlobal.Competitor);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferCompanyByMembershipIDAndIndustryIDAndCodeToList([DataSourceRequest] DataSourceRequest request, int membershipID, int industryID)
        {
            var data = _membershipPermissionRepository.GetDataTransferCompanyByMembershipIDAndIndustryIDAndCodeToList(membershipID, industryID, AppGlobal.Competitor);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByMembershipIDAndIndustryIDAndProductToList([DataSourceRequest] DataSourceRequest request, int membershipID, int industryID)
        {
            var data = _membershipPermissionRepository.GetByMembershipIDAndIndustryIDAndCodeToList(membershipID, industryID, AppGlobal.Product);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByMembershipIDAndProductToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetByMembershipIDAndCodeToList(membershipID, AppGlobal.Product);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByMembershipIDAndFileToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetByMembershipIDAndCodeToList(membershipID, AppGlobal.File);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByMembershipIDAndIndustryIDAndKeywordNegativeToList([DataSourceRequest] DataSourceRequest request, int membershipID, int industryID)
        {
            var data = _membershipPermissionRepository.GetByMembershipIDAndIndustryIDAndCodeToList(membershipID, industryID, AppGlobal.KeywordNegative);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByMembershipIDAndIndustryIDAndKeywordPositiveToList([DataSourceRequest] DataSourceRequest request, int membershipID, int industryID)
        {
            var data = _membershipPermissionRepository.GetByMembershipIDAndIndustryIDAndCodeToList(membershipID, industryID, AppGlobal.KeywordPositive);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByMembershipIDAndIndustryIDAndChannelToList([DataSourceRequest] DataSourceRequest request, int membershipID, int industryID)
        {
            var data = _membershipPermissionRepository.GetByMembershipIDAndIndustryIDAndCodeToList(membershipID, industryID, AppGlobal.WebsiteType);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetIndustryIDAndCodeToList([DataSourceRequest] DataSourceRequest request, int industryID)
        {
            var data = _membershipPermissionRepository.GetIndustryIDAndCodeToList(industryID, AppGlobal.Industry);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetMembershipContactToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetByMembershipIDAndCodeToList(membershipID, AppGlobal.Contact);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetProductToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipPermissionRepository.GetByProductCodeToList(AppGlobal.Product);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetMembershipProductToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetByMembershipIDAndCodeToList(membershipID, AppGlobal.Product);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult InitializationMenuPermission(int membershipID)
        {
            _membershipPermissionRepository.InitializationMenuPermission(membershipID, RequestUserID);
            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            return Json(note);
        }
        public IActionResult InitializationDailyReportSection(int membershipID)
        {
            _membershipPermissionRepository.InitializationDailyReportSection(membershipID, AppGlobal.DailyReportSection, RequestUserID);
            string note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            return Json(note);
        }
        public IActionResult InitializationChannel(int membershipID)
        {
            _membershipPermissionRepository.InitializationChannel(membershipID, AppGlobal.WebsiteType, RequestUserID);
            string note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            return Json(note);
        }
        public IActionResult InitializationDailyReportColumn(int membershipID)
        {
            _membershipPermissionRepository.InitializationDailyReportColumn(membershipID, AppGlobal.DailyReportColumn, RequestUserID);
            string note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            return Json(note);
        }
        public IActionResult InitializationDailyReportSectionByMembershipIDAndIndustryID(int membershipID, int industryID)
        {
            _membershipPermissionRepository.InitializationDailyReportSectionByMembershipIDAndIndustryID(membershipID, industryID, AppGlobal.DailyReportSection, RequestUserID);
            string note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            return Json(note);
        }
        public IActionResult InitializationDailyReportColumnByMembershipIDAndIndustryID(int membershipID, int industryID)
        {
            _membershipPermissionRepository.InitializationDailyReportColumnByMembershipIDAndIndustryID(membershipID, industryID, AppGlobal.DailyReportColumn, RequestUserID);
            string note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            return Json(note);
        }
        public IActionResult InitializationChannelByMembershipIDAndIndustryID(int membershipID, int industryID)
        {
            _membershipPermissionRepository.InitializationChannelByMembershipIDAndIndustryID(membershipID, industryID, AppGlobal.WebsiteType, RequestUserID);
            string note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            return Json(note);
        }
        public IActionResult SaveAllMenuPermission(int membershipID, bool isAll)
        {
            _membershipPermissionRepository.SaveAllMenuPermission(membershipID, isAll, RequestUserID);
            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            return Json(note);
        }
        public IActionResult CreateDataTransferContact(MembershipPermissionDataTransfer model, int membershipID, int industryID)
        {
            int result = 0;
            string note = AppGlobal.InitString;
            note = AppGlobal.Error + " - " + AppGlobal.CreateFail + ", " + AppGlobal.Error001;
            if ((membershipID > 0) && (industryID > 0))
            {
                List<string> listEmail = AppGlobal.SetEmailContact(model.Email);
                if (listEmail.Count == 0)
                {
                    listEmail.Add(model.Email);
                }
                foreach (string email in listEmail)
                {
                    MembershipPermission membershipPermission = new MembershipPermission();
                    membershipPermission.Code = AppGlobal.Contact;
                    membershipPermission.MembershipID = membershipID;
                    membershipPermission.IndustryID = industryID;
                    membershipPermission.CategoryID = model.ReportType.ID;
                    membershipPermission.FullName = model.FullName;
                    membershipPermission.Email = email;
                    membershipPermission.Phone = model.Phone;
                    membershipPermission.Initialization(InitType.Insert, RequestUserID);
                    result = result + _membershipPermissionRepository.Create(membershipPermission);
                }
                if (result > 0)
                {
                    note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
                }
                else
                {
                    note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
                }
            }
            return Json(note);
        }
        public IActionResult CreateDataTransferSegmentProduct(MembershipPermissionDataTransfer model, int membershipID, int industryID)
        {
            string note = AppGlobal.InitString;
            note = AppGlobal.Error + " - " + AppGlobal.CreateFail + ", " + AppGlobal.Error001;
            model.Code = AppGlobal.Product;
            model.MembershipID = membershipID;
            model.IndustryID = industryID;
            model.SegmentID = model.Segment.ID;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 1;
            if ((model.MembershipID > 0) && (model.IndustryID > 0) && (model.SegmentID > 0))
            {
                List<string> listProduct = AppGlobal.SetContentByDauChamPhay(model.ProductName);
                if (listProduct.Count == 0)
                {
                    listProduct.Add(model.ProductName);
                }
                foreach (string product in listProduct)
                {
                    MembershipPermission membershipPermission = new MembershipPermission();
                    membershipPermission.Code = AppGlobal.Product;
                    membershipPermission.MembershipID = membershipID;
                    membershipPermission.IndustryID = industryID;
                    membershipPermission.SegmentID = model.Segment.ID;
                    membershipPermission.ProductName = product.Trim();
                    membershipPermission.Initialization(InitType.Insert, RequestUserID);
                    result = result + _membershipPermissionRepository.Create(membershipPermission);
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
        public IActionResult CreateBrand(MembershipPermission model, int membershipID)
        {
            model.Code = AppGlobal.Brand;
            model.MembershipID = membershipID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _membershipPermissionRepository.Create(model);
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
        public IActionResult CreateCustomer(MembershipPermission model, int industryID)
        {
            model.IndustryID = industryID;
            model.ProductID = 0;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _membershipPermissionRepository.Create(model);
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
        public IActionResult CreateKeywordPositive(MembershipPermission model, int membershipID, int industryID)
        {

            model.MembershipID = membershipID;
            model.Code = AppGlobal.KeywordPositive;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 1;
            if ((membershipID > 0) && (industryID > 0))
            {
                List<string> listKeyword = AppGlobal.SetContentByDauChamPhay(model.ProductName);
                if (listKeyword.Count == 0)
                {
                    listKeyword.Add(model.ProductName);
                }
                foreach (string keyword in listKeyword)
                {
                    MembershipPermission membershipPermission = new MembershipPermission();
                    membershipPermission.Code = AppGlobal.KeywordPositive;
                    membershipPermission.MembershipID = membershipID;
                    membershipPermission.IndustryID = industryID;
                    membershipPermission.ProductName = keyword.Trim();
                    membershipPermission.Initialization(InitType.Insert, RequestUserID);
                    result = result + _membershipPermissionRepository.Create(membershipPermission);
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
        public IActionResult CreateKeywordNegative(MembershipPermission model, int membershipID, int industryID)
        {
            model.MembershipID = membershipID;
            model.Code = AppGlobal.KeywordNegative;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 1;
            if ((membershipID > 0) && (industryID > 0))
            {
                List<string> listKeyword = AppGlobal.SetContentByDauChamPhay(model.ProductName);
                if (listKeyword.Count == 0)
                {
                    listKeyword.Add(model.ProductName);
                }
                foreach (string keyword in listKeyword)
                {
                    MembershipPermission membershipPermission = new MembershipPermission();
                    membershipPermission.Code = AppGlobal.KeywordNegative;
                    membershipPermission.MembershipID = membershipID;
                    membershipPermission.IndustryID = industryID;
                    membershipPermission.ProductName = keyword.Trim();
                    membershipPermission.Initialization(InitType.Insert, RequestUserID);
                    result = result + _membershipPermissionRepository.Create(membershipPermission);
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
        public IActionResult Update(MembershipPermission model)
        {
            Initialization();
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _membershipPermissionRepository.Update(model.ID, model);
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
        public IActionResult CreateDataTransferIndustry(MembershipPermissionDataTransfer model, int membershipID)
        {
            string note = AppGlobal.InitString;
            note = AppGlobal.Error + " - " + AppGlobal.CreateFail + ", " + AppGlobal.Error001;
            model.Code = AppGlobal.Industry;
            model.MembershipID = membershipID;
            model.IndustryID = model.Industry.ID;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if ((model.MembershipID > 0) && (model.IndustryID > 0))
            {
                if (_membershipPermissionRepository.GetDataTransferIndustryByMembershipIDAndCodeToList(membershipID, AppGlobal.Industry).Count == 0)
                {
                    model.Active = true;
                }
                result = _membershipPermissionRepository.Create(model);
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
        public IActionResult CreateDataTransferIndustryByParentID(MembershipPermissionDataTransfer model, int parentID)
        {
            model.Code = AppGlobal.Industry;
            model.ParentID = parentID;
            model.IndustryID = model.Industry001.IndustryID;
            model.IndustryCompareID = model.IndustryCompare001.IndustryID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if ((model.ParentID > 0) && (model.IndustryID > 0) && (model.IndustryCompareID > 0))
            {
                result = _membershipPermissionRepository.Create(model);
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
        public IActionResult CreateDataTransferProductByParentID(MembershipPermissionDataTransfer model, int parentID)
        {
            model.Code = AppGlobal.Product;
            model.ParentID = parentID;
            model.ProductID = model.Product.ID;
            model.ProductCompareID = model.ProductCompare.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if ((model.ParentID > 0) && (model.ProductID > 0) && (model.ProductCompareID > 0))
            {
                result = _membershipPermissionRepository.Create(model);
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
        public IActionResult CreateDataTransferCompetitor(MembershipPermissionDataTransfer model, int membershipID, int industryID)
        {
            string note = AppGlobal.InitString;
            note = AppGlobal.Error + " - " + AppGlobal.CreateFail + ", " + AppGlobal.Error001;
            model.Code = AppGlobal.Competitor;
            model.MembershipID = membershipID;
            model.CompanyID = model.Company.ID;
            model.IndustryID = industryID;
            model.SegmentID = model.Segment.ID;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if ((model.MembershipID > 0) && (model.CompanyID > 0) && (model.IndustryID > 0) && (model.SegmentID > 0))
            {
                result = _membershipPermissionRepository.Create(model);
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
        public IActionResult CreateDataTransferMembership(MembershipPermissionDataTransfer model, int industryID)
        {
            model.Code = AppGlobal.Industry;
            model.IndustryID = industryID;
            model.MembershipID = model.Membership.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if ((model.MembershipID > 0) && (model.IndustryID > 0))
            {
                result = _membershipPermissionRepository.Create(model);
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
        public IActionResult CreateDataTransferMembershipSegment(MembershipPermissionDataTransfer model, int segmentID)
        {
            model.Code = AppGlobal.Product;
            model.SegmentID = segmentID;
            model.MembershipID = model.Membership.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if ((model.MembershipID > 0) && (model.SegmentID > 0))
            {
                result = _membershipPermissionRepository.Create(model);
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
        public IActionResult UpdateDataTransfer(MembershipPermissionDataTransfer model)
        {
            Initialization();
            model.IndustryID = model.Industry.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _membershipPermissionRepository.Update(model.ID, model);
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
        public IActionResult UpdateDataTransferIndustry(MembershipPermissionDataTransfer model)
        {
            Initialization();
            model.IndustryID = model.Industry.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _membershipPermissionRepository.Update(model.ID, model);
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
        public IActionResult UpdateDataTransferContact(MembershipPermissionDataTransfer model)
        {
            Initialization();
            model.CategoryID = model.ReportType.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = 0;
            if ((model.MembershipID > 0) && (model.CategoryID > 0))
            {
                result = _membershipPermissionRepository.Update(model.ID, model);
            }
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
        public IActionResult UpdateDataTransferSegmentProduct(MembershipPermissionDataTransfer model)
        {
            Initialization();
            model.SegmentID = model.Segment.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = 0;
            if ((model.MembershipID > 0) && (model.SegmentID > 0))
            {
                result = _membershipPermissionRepository.Update(model.ID, model);
            }
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
        public IActionResult UpdateDataTransferMembership(MembershipPermissionDataTransfer model)
        {
            Initialization();
            model.MembershipID = model.Membership.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = 0;
            if ((model.MembershipID > 0) && (model.IndustryID > 0))
            {
                result = _membershipPermissionRepository.Update(model.ID, model);
            }
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
        public IActionResult UpdateDataTransferMembershipSegment(MembershipPermissionDataTransfer model)
        {
            Initialization();
            model.MembershipID = model.Membership.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = 0;
            if ((model.MembershipID > 0) && (model.SegmentID > 0))
            {
                result = _membershipPermissionRepository.Update(model.ID, model);
            }
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
        public IActionResult UpdateDataTransferDailyReportSection(MembershipPermissionDataTransfer model)
        {
            Initialization();
            string note = AppGlobal.InitString;
            model.LanguageID = model.Language.ID;
            model.Initialization(InitType.Update, RequestUserID);
            int result = 0;
            if ((model.MembershipID > 0) && (model.CategoryID > 0))
            {
                result = _membershipPermissionRepository.Update(model.ID, model);
            }
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
        public IActionResult UpdateDataTransferDailyReportSectionOrColumn(MembershipPermissionDataTransfer model)
        {
            Initialization();
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = 0;
            if ((model.MembershipID > 0) && (model.CategoryID > 0))
            {
                result = _membershipPermissionRepository.Update(model.ID, model);
            }
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
        public IActionResult UpdateDataTransferCompetitor(MembershipPermissionDataTransfer model)
        {
            Initialization();
            model.CompanyID = model.Company.ID;
            model.IndustryID = model.Industry.ID;
            model.SegmentID = model.Segment.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = 0;
            if ((model.MembershipID > 0) && (model.CompanyID > 0) && (model.IndustryID > 0) && (model.SegmentID > 0))
            {
                result = _membershipPermissionRepository.Update(model.ID, model);
            }
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
        public IActionResult UpdateDataTransferCompany(MembershipPermissionDataTransfer model)
        {
            Initialization();
            model.CompanyID = model.Company.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _membershipPermissionRepository.Update(model.ID, model);
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
        public IActionResult UpdateDataTransferIndustry001(MembershipPermissionDataTransfer model)
        {
            Initialization();
            model.IndustryID = model.Industry001.IndustryID;
            model.IndustryCompareID = model.IndustryCompare001.IndustryID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = 0;
            if ((model.ParentID > 0) && (model.IndustryID > 0) && (model.IndustryCompareID > 0))
            {
                result = _membershipPermissionRepository.Update(model.ID, model);
            }
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
        public IActionResult UpdateDataTransferProduct(MembershipPermissionDataTransfer model)
        {
            Initialization();
            model.ProductID = model.Product.ID;
            model.ProductCompareID = model.ProductCompare.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = 0;
            if ((model.ParentID > 0) && (model.ProductID > 0) && (model.ProductCompareID > 0))
            {
                result = _membershipPermissionRepository.Update(model.ID, model);
            }
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
        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _membershipPermissionRepository.Delete(ID);
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
        public IActionResult SaveItems(MembershipPermission model)
        {
            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    List<MembershipPermission> list = new List<MembershipPermission>();
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        var file = Request.Form.Files[i];
                        if (file != null)
                        {
                            string fileExtension = Path.GetExtension(file.FileName);
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            if (!string.IsNullOrEmpty(model.FullName))
                            {
                                fileName = model.FullName;
                            }
                            fileName = AppGlobal.SetName(fileName);
                            fileName = fileName + "-" + AppGlobal.DateTimeCode + fileExtension;
                            var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.URLImagesCustomer, fileName);
                            using (var stream = new FileStream(physicalPath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                                MembershipPermission membershipPermission = new MembershipPermission();
                                membershipPermission.Initialization(InitType.Insert, RequestUserID);
                                membershipPermission.Code = AppGlobal.File;
                                membershipPermission.MembershipID = model.MembershipID;
                                membershipPermission.ProductName = model.ProductName;
                                membershipPermission.Email = fileName;
                                membershipPermission.Phone = fileExtension;
                                list.Add(membershipPermission);
                            }
                        }
                    }
                    _membershipPermissionRepository.Range(list);
                }
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("CustomerFiles", "Membership", new { ID = model.MembershipID });
        }
    }
}
