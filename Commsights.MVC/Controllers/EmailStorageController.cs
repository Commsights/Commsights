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
using Commsights.Data.DataTransferObject;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using OfficeOpenXml;

namespace Commsights.MVC.Controllers
{
    public class EmailStorageController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmailStorageRepository _emailStorageRepository;

        public EmailStorageController(IHostingEnvironment hostingEnvironment, IEmailStorageRepository emailStorageRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _emailStorageRepository = emailStorageRepository;
        }
        private void Initialization(EmailStorage model)
        {
            if (string.IsNullOrEmpty(model.Note))
            {
                model.Note = "";
            }
            if (!string.IsNullOrEmpty(model.EmailTo))
            {
                model.EmailTo = model.EmailTo.Trim();
            }
            if (!string.IsNullOrEmpty(model.EmailFrom))
            {
                model.EmailFrom = model.EmailFrom.Trim();
            }
            if (!string.IsNullOrEmpty(model.EmailCC))
            {
                model.EmailCC = model.EmailCC.Trim();
            }
            if (!string.IsNullOrEmpty(model.EmailBCC))
            {
                model.EmailBCC = model.EmailBCC.Trim();
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int ID)
        {
            EmailStorage model = new EmailStorage();
            if(ID>0)
            {
                model = _emailStorageRepository.GetByID(ID);
            }    
            return View(model);
        }
        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _emailStorageRepository.Delete(ID);
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
