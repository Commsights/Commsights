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

namespace Commsights.MVC.Controllers
{
    public class ProductSearchPropertyController : BaseController
    {
        private readonly IProductSearchPropertyRepository _productSearchPropertyRepository;

        public ProductSearchPropertyController(IProductSearchPropertyRepository productSearchPropertyRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _productSearchPropertyRepository = productSearchPropertyRepository;
        }
        private void Initialization(ProductSearchProperty model)
        {
        }
        public IActionResult Index(int ID)
        {
            ProductSearchProperty model = new ProductSearchProperty();
            if (ID > 0)
            {
                model = _productSearchPropertyRepository.GetByID(ID);
            }
            return View(model);
        }
        public ActionResult GetDataTransferProductSearchByProductSearchIDToList([DataSourceRequest] DataSourceRequest request, int productSearchID)
        {
            var data = _productSearchPropertyRepository.GetDataTransferProductSearchByProductSearchIDToList(productSearchID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferByParentIDToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _productSearchPropertyRepository.GetDataTransferByParentIDToList(parentID);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult CreateWithParentID(ProductSearchPropertyDataTransfer model, int parentID)
        {
            Initialization(model);
            model.ParentID = parentID;
            model.CompanyID = model.Company.ID;
            model.AssessID = model.AssessType.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = 0;
            result = _productSearchPropertyRepository.Create(model);
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
        public IActionResult Update(ProductSearchPropertyDataTransfer model)
        {
            Initialization(model);            
            model.CompanyID = model.Company.ID;
            model.AssessID = model.AssessType.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _productSearchPropertyRepository.Update(model.ID, model);
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
        public IActionResult UpdateReportDataTransfer(ProductSearchPropertyDataTransfer model)
        {
            Initialization(model);            
            model.AssessID = model.AssessType.ID;
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _productSearchPropertyRepository.Update(model.ID, model);
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
            int result = _productSearchPropertyRepository.Delete(ID);
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
