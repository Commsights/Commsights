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
    public class ProductSearchPropertyController : BaseController
    {
        private readonly IProductSearchPropertyRepository _productSearchPropertyRepository;

        public ProductSearchPropertyController(IProductSearchPropertyRepository productSearchPropertyRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _productSearchPropertyRepository = productSearchPropertyRepository;
        }
        private void Initialization(ProductSearch model)
        {
        }
       
        public ActionResult GetDataTransferProductSearchByProductSearchIDToList([DataSourceRequest] DataSourceRequest request, int productSearchID)
        {
            var data = _productSearchPropertyRepository.GetDataTransferProductSearchByProductSearchIDToList(productSearchID);
            return Json(data.ToDataSourceResult(request));
        }
    }
}
