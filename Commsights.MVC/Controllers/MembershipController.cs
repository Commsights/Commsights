using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Hosting.Internal;

namespace Commsights.MVC.Controllers
{
    public class MembershipController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMembershipPermissionRepository _membershipPermissionRepository;
        private readonly IConfigRepository _configResposistory;

        public MembershipController(IWebHostEnvironment hostingEnvironment, IMembershipPermissionRepository membershipPermissionRepository, IMembershipRepository membershipRepository, IConfigRepository configResposistory, ICompositeViewEngine viewEngine, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _membershipRepository = membershipRepository;
            _membershipPermissionRepository = membershipPermissionRepository;
            _configResposistory = configResposistory;

        }
        private void Initialization(Membership model, int action)
        {
            if (!string.IsNullOrEmpty(model.ShortName))
            {
                model.ShortName = model.ShortName.Trim();
            }
            if (!string.IsNullOrEmpty(model.EnglishName))
            {
                model.EnglishName = model.EnglishName.Trim();
            }
            if (!string.IsNullOrEmpty(model.Address))
            {
                model.Address = model.Address.Trim();
            }
            if (!string.IsNullOrEmpty(model.Account))
            {
                model.Account = model.Account.Trim();
            }
            if (!string.IsNullOrEmpty(model.FullName))
            {
                model.FullName = model.FullName.Trim();
            }
            if (!string.IsNullOrEmpty(model.Email))
            {
                model.Email = model.Email.Trim();
            }
            if (!string.IsNullOrEmpty(model.Phone))
            {
                model.Phone = model.Phone.Trim();
            }
            if (string.IsNullOrEmpty(model.FullName))
            {
                model.FullName = model.Account;
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                model.Password = "0";
            }
            switch (action)
            {
                case 0:
                    model.InitDefaultValue();
                    model.EncryptPassword();
                    break;
                case 1:
                    Membership model001 = _membershipRepository.GetByID(model.ID);
                    if (model001 != null)
                    {
                        if (model.Password != model001.Password)
                        {
                            model.InitDefaultValue();
                            model.EncryptPassword();
                        }
                    }
                    break;
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CompanyToCustomer()
        {
            return View();
        }
        public IActionResult CompanyReplace()
        {
            return View();
        }
        public IActionResult Customer()
        {
            return View();
        }
        public IActionResult CustomerFiles(int ID)
        {
            MembershipPermission model = new MembershipPermission();
            model.Code = AppGlobal.File;
            model.MembershipID = ID;
            Membership membership = new Membership();
            if (ID > 0)
            {
                membership = _membershipRepository.GetByID(ID);
                if (membership != null)
                {
                    model.Note = membership.Account;
                    model.FullName = membership.FullName;
                }
            }
            return View(model);
        }
        public IActionResult CustomerDetail(int ID)
        {
            Membership membership = new Membership();
            if (ID > 0)
            {
                membership = _membershipRepository.GetByID(ID);
            }
            membership.CategoryID = ID;
            return View(membership);
        }
        public IActionResult CustomerDetailWindow(int ID)
        {
            Membership membership = new Membership();
            if (ID > 0)
            {
                membership = _membershipRepository.GetByID(ID);
            }
            return View(membership);
        }
        public IActionResult CompanyDetail(int ID)
        {
            Membership membership = new Membership();
            if (ID > 0)
            {
                membership = _membershipRepository.GetByID(ID);
            }
            membership.CategoryID = ID;
            return View(membership);
        }
        public IActionResult Employee()
        {
            return View();
        }
        public IActionResult Company()
        {
            return View();
        }
        public IActionResult BrandOfCustomer()
        {
            return View();
        }
        public IActionResult CompanyByIndustry()
        {
            return View();
        }
        public IActionResult CompanyByIndustry001()
        {
            return View();
        }
        public IActionResult CompanyBySegment()
        {
            return View();
        }
        public IActionResult IndustryByCompany()
        {
            return View();
        }
        public IActionResult ProductName()
        {
            return View();
        }
        public IActionResult CompetitorByCompany()
        {
            return View();
        }
        public IActionResult DailyReportSectionByCompany()
        {
            return View();
        }
        public IActionResult DailyReportColumnByCompany()
        {
            return View();
        }
        public ActionResult CustomerCancel()
        {
            return RedirectToAction("Customer");
        }
        public ActionResult CompanyCancel()
        {
            return RedirectToAction("Company");
        }
        public ActionResult CompanyPlus()
        {
            return RedirectToAction("CompanyDetail", new { ID = 0 });
        }
        public ActionResult CustomerPlus()
        {
            return RedirectToAction("CustomerDetail", new { ID = 0 });
        }
        public IActionResult GetByID(int ID)
        {
            Membership model = _membershipRepository.GetByID(ID);
            model.Note = "/Membership/CompanyDetail/" + ID;
            if (model.ParentID == AppGlobal.ParentIDCustomer)
            {
                model.Note = "/Membership/CustomerDetail/" + ID;
            }
            return Json(model);
        }
        public ActionResult GetByIndustryID001ToList([DataSourceRequest] DataSourceRequest request)
        {
            int industryID = 0;
            try
            {
                industryID = int.Parse(Request.Cookies["CodeDataIndustryID"]);
            }
            catch
            {

            }
            var data = _membershipRepository.GetByIndustryID001ToList(industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetAllCompany001ToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetAllCompany001ToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetAllCompany001ByActiveToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetAllCompany001ByActiveToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetAllToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetAllToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByCompetitorFullToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetByCompetitorFullToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByCompanyFullToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetByCompanyFullToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetAllCompanyToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetAllCompanyToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCompetitorToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetCompetitorToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetEmployeeToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetByParentIDToList(AppGlobal.ParentIDEmployee);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCustomerToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetCustomerToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCustomerFullToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetCustomerFullToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCustomerDataTransferToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetDataTransferByParentIDToList(AppGlobal.ParentIDCustomer);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetCompetitorDataTransferToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetDataTransferByParentIDToList(AppGlobal.ParentIDCompetitor);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByCompanyToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetByCompanyToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByIndustryIDToList([DataSourceRequest] DataSourceRequest request, int industryID)
        {
            var data = _membershipRepository.GetByIndustryIDToList(industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByIndustryIDWithIDAndAccountToList([DataSourceRequest] DataSourceRequest request, int industryID)
        {
            var data = _membershipRepository.GetByIndustryIDWithIDAndAccountToList(industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByIndustryIDAndParrentIDCustomerToList([DataSourceRequest] DataSourceRequest request, int industryID)
        {
            var data = _membershipRepository.GetByIndustryIDAndParrentIDToList(industryID, AppGlobal.ParentIDCustomer);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetByIndustryIDAndParrentIDToListToJSON(int industryID)
        {
            return Json(_membershipRepository.GetByIndustryIDAndParrentIDToList(industryID, AppGlobal.ParentIDCustomer));
        }

        [AcceptVerbs("Post")]
        public IActionResult SaveCompany(Membership model)
        {
            //model.ParentID = AppGlobal.ParentIDCompetitor;
            model.Active = true;
            if (model.ID > 0)
            {
                Initialization(model, 1);
                model.Initialization(InitType.Update, RequestUserID);
                _membershipRepository.Update(model.ID, model);
            }
            else
            {
                Initialization(model, 0);
                model.Initialization(InitType.Insert, RequestUserID);
                if (_membershipRepository.IsExistAccount(model.Account) == false)
                {
                    _membershipRepository.Create(model);
                }
            }
            return RedirectToAction("CompanyDetail", new { ID = model.ID });
        }
        [AcceptVerbs("Post")]
        public IActionResult SaveCustomer(Membership model)
        {
            //model.ParentID = AppGlobal.ParentIDCustomer;
            model.Active = true;
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                if (file != null)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    fileName = AppGlobal.SetName(fileName);
                    fileName = fileName + "-" + AppGlobal.DateTimeCode + fileExtension;
                    var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.URLImagesCustomer, fileName);
                    using (var stream = new FileStream(physicalPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        model.Avatar = fileName;
                    }
                }
            }
            if (model.ID > 0)
            {
                Initialization(model, 1);
                model.Initialization(InitType.Update, RequestUserID);

                //bool check = false;
                //int ID = _membershipRepository.IsByAccount(model.Account);
                //if (ID == 0)
                //{
                //    check = true;
                //}
                //else
                //{
                //    if (model.ID == ID)
                //    {
                //        check = true;
                //    }
                //}
                //if (check == true)
                //{
                //    _membershipRepository.Update(model.ID, model);
                //}
                _membershipRepository.Update(model.ID, model);
            }
            else
            {
                Initialization(model, 0);
                model.Initialization(InitType.Insert, RequestUserID);
                if (_membershipRepository.IsExistAccount(model.Account) == false)
                {
                    _membershipRepository.Create(model);
                }
            }
            return RedirectToAction("CustomerDetail", new { ID = model.ID });
        }
        public IActionResult CreateWithIndustryID(Membership model, int industryID)
        {
            Initialization(model, 0);
            model.ParentID = AppGlobal.ParentIDCompetitor;
            model.Active = true;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            model.Account = model.Account.Trim();
            Membership membership = _membershipRepository.GetByAccount(model.Account);
            if (membership.ID > 0)
            {
            }
            else
            {
                result = _membershipRepository.Create(model);
                membership = model;
            }
            if (membership.ID > 0)
            {
                MembershipPermission membershipPermission = new MembershipPermission();
                membershipPermission.MembershipID = model.ID;
                membershipPermission.IndustryID = industryID;
                membershipPermission.Code = AppGlobal.Industry;
                membershipPermission.Initialization(InitType.Insert, RequestUserID);
                _membershipPermissionRepository.Create(membershipPermission);
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }
        public IActionResult CreateCustomer(Membership model)
        {
            Initialization(model, 0);
            model.ParentID = AppGlobal.ParentIDCustomer;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_membershipRepository.IsExistEmail(model.Email) == false)
            {
                if (_membershipRepository.IsExistPhone(model.Phone) == false)
                {
                    result = _membershipRepository.Create(model);
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
        public IActionResult CreateEmployee(Membership model)
        {
            Initialization(model, 0);
            model.ParentID = AppGlobal.ParentIDEmployee;
            model.Active = true;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            if (_membershipRepository.IsExistEmail(model.Email) == false)
            {
                if (_membershipRepository.IsExistPhone(model.Email) == false)
                {
                    result = _membershipRepository.Create(model);
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
        public IActionResult Update(Membership model)
        {
            Initialization(model, 1);
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = 0;
            //Membership membership = _membershipRepository.GetByAccount(model.Account);
            //if (membership == null)
            //{
            //    check = true;
            //}
            //else
            //{
            //    if (membership.ID == model.ID)
            //    {
            //        check = true;
            //    }
            //}
            //if (check == true)
            //{
            //    result = _membershipRepository.Update(model.ID, model);
            //}
            result = _membershipRepository.Update(model.ID, model);
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
        public IActionResult Update001(MembershipCompanyDataTransfer model)
        {
            string note = AppGlobal.InitString;
            int result = 0;
            _membershipRepository.UpdateSingleItem001(model.ID, model.Active.Value, model.Account);
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
        public IActionResult Delete(Membership model)
        {
            model.Active = false;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _membershipRepository.Delete(model.ID);
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
        public IActionResult Logout()
        {
            Response.Cookies.Append("IsLogin", "false");
            Response.Cookies.Append("IsLogout", "true");
            Response.Cookies.Append("SaveLogin", "false");
            Response.Cookies.Delete("Password");
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ReplaceCompanyIDToCustomerID(int companyID, int customerID)
        {
            string note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            _membershipRepository.ReplaceCompanyIDToCustomerID(companyID, customerID);
            return Json(note);
        }
        public IActionResult ReplaceCompanyIDSourceToCompanyIDReplace(int companyIDSource, int companyIDReplace)
        {
            string note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            _membershipRepository.ReplaceCompanyIDSourceToCompanyIDReplace(companyIDSource, companyIDReplace);
            return Json(note);
        }
    }
}
