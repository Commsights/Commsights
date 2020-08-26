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
            return View();
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
        public IActionResult SaveProductSearch(string search, DateTime datePublishBegin, DateTime datePublishEnd)
        {
            ProductSearch productSearch = _productSearchRepository.SaveProductSearch(search, datePublishBegin, datePublishEnd, RequestUserID);
            string result = AppGlobal.Domain + "ProductSearch/Detail/" + productSearch.ID;
            return Json(result);
        }
    }
}
