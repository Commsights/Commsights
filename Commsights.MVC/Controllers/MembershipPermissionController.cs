using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commsights.Data.DataTransferObject;
using Commsights.Data.Enum;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using Commsights.Data.Repositories;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace Commsights.MVC.Controllers
{
    public class MembershipPermissionController : BaseController
    {
        private readonly IMembershipPermissionRepository _membershipPermissionRepository;
        public MembershipPermissionController(IMembershipPermissionRepository membershipPermissionRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _membershipPermissionRepository = membershipPermissionRepository;
        }
        private void Initialization()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IndustryAndSegmentAndProduct()
        {
            return View();
        }

        public ActionResult GetByMembershipIDAndToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetByMembershipIDToList(membershipID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetMembershipIDAndBrandToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetByMembershipIDAndCodeToList(membershipID, AppGlobal.Brand);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferBrandByMembershipIDAndCodeToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetDataTransferBrandByMembershipIDAndCodeToList(membershipID, AppGlobal.Brand);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferCompanyByMembershipIDAndCodeToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetDataTransferCompanyByMembershipIDAndCodeToList(membershipID, AppGlobal.Competitor);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferMembershipByBrandIDAndCodeToList([DataSourceRequest] DataSourceRequest request, int brandID)
        {
            var data = _membershipPermissionRepository.GetDataTransferMembershipByBrandIDAndCodeToList(brandID, AppGlobal.Brand);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByMembershipIDAndBrandIDAndProductToList([DataSourceRequest] DataSourceRequest request, int membershipID, int brandID)
        {
            var data = _membershipPermissionRepository.GetByMembershipIDAndBrandIDAndCodeToList(membershipID, brandID, AppGlobal.Product);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetBrandIDAndCodeToList([DataSourceRequest] DataSourceRequest request, int brandID)
        {
            var data = _membershipPermissionRepository.GetBrandIDAndCodeToList(brandID, AppGlobal.Brand);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetMembershipContactToList([DataSourceRequest] DataSourceRequest request, int membershipID)
        {
            var data = _membershipPermissionRepository.GetByMembershipIDAndCodeToList(membershipID, AppGlobal.Contact);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult InitializationMenuPermission(int membershipID)
        {
            _membershipPermissionRepository.InitializationMenuPermission(membershipID, RequestUserID);
            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            return Json(note);
        }
        public IActionResult SaveAllMenuPermission(int membershipID, bool isAll)
        {
            _membershipPermissionRepository.SaveAllMenuPermission(membershipID, isAll, RequestUserID);
            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            return Json(note);
        }
        public IActionResult CreateContact(MembershipPermission model, int membershipID)
        {
            string note = AppGlobal.InitString;
            note = AppGlobal.Error + " - " + AppGlobal.CreateFail + ", " + AppGlobal.Error001;
            if (membershipID > 0)
            {
                model.MembershipID = membershipID;
                model.Code = AppGlobal.Contact;

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
            }
            return Json(note);
        }
        public IActionResult CreateProduct(MembershipPermission model, int membershipID, int brandID)
        {
            model.Code = AppGlobal.Product;
            model.MembershipID = membershipID;
            model.BrandID = brandID;
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
        public IActionResult CreateCustomer(MembershipPermission model, int brandID)
        {
            model.BrandID = brandID;
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
        public IActionResult CreateDataTransferBrand(MembershipPermissionDataTransfer model, int membershipID)
        {
            model.Code = AppGlobal.Brand;
            model.MembershipID = membershipID;
            model.BrandID = model.Brand.ID;
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
        public IActionResult CreateDataTransferCompetitor(MembershipPermissionDataTransfer model, int membershipID)
        {
            model.Code = AppGlobal.Competitor;
            model.MembershipID = membershipID;
            model.CompanyID = model.Company.ID;
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
        public IActionResult CreateDataTransferMembership(MembershipPermissionDataTransfer model, int brandID)
        {
            model.Code = AppGlobal.Brand;
            model.BrandID = brandID;
            model.MembershipID = model.Membership.ID;
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
        public IActionResult UpdateDataTransfer(MembershipPermissionDataTransfer model)
        {
            Initialization();
            model.BrandID = model.Brand.ID;
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
        public IActionResult UpdateDataTransferMembership(MembershipPermissionDataTransfer model)
        {
            Initialization();
            model.MembershipID = model.Membership.ID;
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
    }
}
