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
    }
}
