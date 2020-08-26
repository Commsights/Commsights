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

namespace Commsights.MVC.Controllers
{
    public class ProductSearchController : BaseController
    {
        private readonly IProductSearchRepository _productSearchRepository;

        public ProductSearchController(IProductSearchRepository productSearchRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _productSearchRepository = productSearchRepository;
        }
        private void Initialization(ProductSearch model)
        {
        }
        public IActionResult Index()
        {
            ProductSearch model = new ProductSearch();
            DateTime now = DateTime.Now;
            model.DatePublishBegin = now;
            model.DatePublishEnd = now;
            return View(model);
        }
        public IActionResult Detail(int ID)
        {
            ProductSearch model = new ProductSearch();
            if (ID > 0)
            {
                model = _productSearchRepository.GetByID(ID);
            }
            return View(model);
        }
        public ActionResult GetByDateSearchBeginAndDateSearchEndToList([DataSourceRequest] DataSourceRequest request, DateTime dateSearchBegin, DateTime dateSearchEnd)
        {
            var data = _productSearchRepository.GetByDateSearchBeginAndDateSearchEndToList(dateSearchBegin, dateSearchEnd);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult SaveProductSearch(string search, DateTime datePublishBegin, DateTime datePublishEnd, bool isAll)
        {
            ProductSearch productSearch = _productSearchRepository.SaveProductSearch(search, datePublishBegin, datePublishEnd, RequestUserID, isAll);
            string result = AppGlobal.Domain + "ProductSearch/Detail/" + productSearch.ID;
            return Json(result);
        }
        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _productSearchRepository.Delete(ID);
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
