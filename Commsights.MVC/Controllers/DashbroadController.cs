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
        private readonly IDashbroadRepository _dashbroadRepository;
        public DashbroadController(IDashbroadRepository dashbroadRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _dashbroadRepository = dashbroadRepository;
        }
        public IActionResult Overview()
        {
            DashbroadDataTransfer model = _dashbroadRepository.Overview();
            model.DatePublish = DateTime.Now;
            return View(model);
        }
        public ActionResult CustomerAndArticleCompanyCountToList()
        {
            List<DashbroadDataTransfer> list = _dashbroadRepository.CustomerAndArticleCompanyCountToList();
            return Json(list);
        }
        public ActionResult CustomerAndArticleCompanyCountByDatePublishToList()
        {
            List<DashbroadDataTransfer> list = _dashbroadRepository.CustomerAndArticleCompanyCountByDatePublishToList(DateTime.Now);
            return Json(list);
        }
        public ActionResult ProductAndArticleProductCountByDatePublishToList()
        {
            List<DashbroadDataTransfer> list = _dashbroadRepository.ProductAndArticleProductCountByDatePublishToList(DateTime.Now);
            return Json(list);
        }
        public ActionResult IndustryAndArticleIndustryCountByDatePublishToList()
        {
            List<DashbroadDataTransfer> list = _dashbroadRepository.IndustryAndArticleIndustryCountByDatePublishToList(DateTime.Now);
            return Json(list);
        }
        public ActionResult IndustryCustomerAndArticleIndustryCountByDatePublishToList()
        {
            List<DashbroadDataTransfer> list = _dashbroadRepository.IndustryCustomerAndArticleIndustryCountByDatePublishToList(DateTime.Now);
            return Json(list);
        }
        public ActionResult CustomerAndArticleCountByDatePublishToList()
        {
            List<DashbroadDataTransfer> list = _dashbroadRepository.CustomerAndArticleCountByDatePublishToList(DateTime.Now);
            return Json(list);
        }
    }
}
