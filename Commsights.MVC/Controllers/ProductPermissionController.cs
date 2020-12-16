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
using System.Xml;
using System.Text;
using Commsights.MVC.Models;
using System.Net;
using System.IO;
using Commsights.Data.DataTransferObject;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Diagnostics.Eventing.Reader;
using Commsights.Service.Mail;
using System.Drawing;
using Microsoft.AspNetCore.Http;

namespace Commsights.MVC.Controllers
{
    public class ProductPermissionController : BaseController
    {
        private readonly IProductPermissionRepository _productPermissionRepository;
        public ProductPermissionController(IProductPermissionRepository productPermissionRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _productPermissionRepository = productPermissionRepository;
        }
        public IActionResult ProductPermission()
        {
            return View();
        }
        public ActionResult GetProductPermissionDataTransferByIndustryIDToList([DataSourceRequest] DataSourceRequest request, int industryID)
        {
            var data = _productPermissionRepository.GetProductPermissionDataTransferByIndustryIDToList(industryID);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult CreateDataTransfer(ProductPermissionDataTransfer model, int industryID)
        {
            string note = AppGlobal.InitString;
            model.IndustryID = industryID;
            model.EmployeeID = model.Employee.ID;
            model.MembershipID = RequestUserID;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = _productPermissionRepository.Create(model);
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
        public IActionResult Update(ProductPermissionDataTransfer model)
        {
            string note = AppGlobal.InitString;
            model.EmployeeID = model.Employee.ID;
            model.MembershipID = RequestUserID;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _productPermissionRepository.Update(model.ID, model);
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
            int result = _productPermissionRepository.Delete(ID);
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
