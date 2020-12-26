using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using Commsights.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Commsights.MVC.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
        }
        public IActionResult Index()
        {
            Membership model = new Membership();
            return View(model);
        }
    }
}
