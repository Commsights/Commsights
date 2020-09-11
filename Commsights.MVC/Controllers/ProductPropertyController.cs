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
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using Commsights.Data.DataTransferObject;
using System.Diagnostics.Eventing.Reader;

namespace Commsights.MVC.Controllers
{
    public class ProductPropertyController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductPropertyRepository _productPropertyRepository;
        public ProductPropertyController(IProductRepository productRepository, IProductPropertyRepository productPropertyRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _productRepository = productRepository;
            _productPropertyRepository = productPropertyRepository;
        }
        public IActionResult Company(int ID)
        {
            Product model = new Product();
            if (ID > 0)
            {
                model = _productRepository.GetByID(ID);
            }
            return View(model);
        }
        public IActionResult Industry(int ID)
        {
            Product model = new Product();
            if (ID > 0)
            {
                model = _productRepository.GetByID(ID);
            }
            return View(model);
        }
        public IActionResult Product(int ID)
        {
            Product model = new Product();
            if (ID > 0)
            {
                model = _productRepository.GetByID(ID);
            }
            return View(model);
        }
        public ActionResult GetDataTransferCompanyByParentIDToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _productPropertyRepository.GetDataTransferCompanyByParentIDToList(parentID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferIndustryByParentIDToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _productPropertyRepository.GetDataTransferIndustryByParentIDToList(parentID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferProductByParentIDToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _productPropertyRepository.GetDataTransferProductByParentIDToList(parentID);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult UpdateDataTransfer(ProductPropertyPropertyDataTransfer model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            model.AssessID = model.AssessType.ID;
            int result = _productPropertyRepository.Update(model.ID, model);
            if (result > 0)
            {
                Product product = _productRepository.GetByID(model.ParentID.Value);
                if (product != null)
                {
                    switch (model.Code)
                    {
                        case "Industry":
                            if (product.IndustryID == model.IndustryID)
                            {
                                product.AssessID = model.AssessID;
                            }
                            break;
                        case "Product":
                            if (product.ProductID == model.ProductID)
                            {
                                product.AssessID = model.AssessID;
                            }
                            break;
                        case "Segment":
                            if (product.SegmentID == model.SegmentID)
                            {
                                product.AssessID = model.AssessID;
                            }
                            break;
                        case "Company":
                            if (product.CompanyID == model.CompanyID)
                            {
                                product.AssessID = model.AssessID;
                            }
                            break;
                    }
                    product.Initialization(InitType.Update, RequestUserID);
                    _productRepository.Update(product.ID, product);

                }
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Error + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }
    }
}
