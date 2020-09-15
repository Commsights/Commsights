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
    public class ReportController : BaseController
    {
        private readonly IProductSearchRepository _productSearchRepository;
        private readonly IProductRepository _productRepository;
        public ReportController(IProductRepository productRepository, IProductSearchRepository productSearchRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _productSearchRepository = productSearchRepository;
            _productRepository = productRepository;
        }
        public IActionResult Daily()
        {
            BaseViewModel model = new BaseViewModel();
            model.DatePublish = DateTime.Now;
            return View(model);
        }
        public IActionResult Daily02(int ID)
        {
            ProductSearchDataTransfer model = new ProductSearchDataTransfer();
            if (ID > 0)
            {
                model = _productSearchRepository.GetDataTransferByID(ID);
            }
            return View(model);
        }
        public ActionResult InitializationByDatePublishToList([DataSourceRequest] DataSourceRequest request, DateTime datePublish)
        {
            var data = _productSearchRepository.InitializationByDatePublishToList(datePublish);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult ReportDailyByDatePublishAndCompanyIDToList([DataSourceRequest] DataSourceRequest request, DateTime datePublish, int companyID)
        {
            var data = _productRepository.ReportDailyByDatePublishAndCompanyIDToList(datePublish, companyID);
            return Json(data.ToDataSourceResult(request));
        }
    }
}
