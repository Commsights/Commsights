using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Commsights.Data.Enum;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using Commsights.Data.Repositories;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Commsights.MVC.Controllers
{
    public class MembershipController : BaseController
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IConfigRepository _configResposistory;
        private ICompositeViewEngine _viewEngine;
        public MembershipController(IMembershipRepository membershipRepository, IConfigRepository configResposistory, ICompositeViewEngine viewEngine, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _membershipRepository = membershipRepository;
            _configResposistory = configResposistory;
            _viewEngine = viewEngine;
        }
        private void Initialization(Membership model, int action)
        {
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
                    Membership model001 = _membershipRepository.GetByID(model.Id);
                    if (model001 != null)
                    {
                        if (model.Password != model001.Password)
                        {
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
        public IActionResult Customer()
        {
            return View();
        }
        public IActionResult Employee()
        {
            return View();
        }
        public ActionResult GetAllToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetAllToList();
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetEmployeeToList([DataSourceRequest] DataSourceRequest request)
        {
            var data = _membershipRepository.GetByParentIDToList(151);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult CreateEmployee(Membership model)
        {
            Initialization(model, 0);
            model.ParentId = 151;
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
            int result = _membershipRepository.Update(model.Id, model);
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
            int result = _membershipRepository.Delete(ID);
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
    }
}
