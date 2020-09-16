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
    public class DashbroadController : BaseController
    {
        public DashbroadController(IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
        }
        public IActionResult Overview()
        {
            return View();
        }
    }
}
