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
        private readonly IEmailStoragePropertyRepository _emailStoragePropertyRepository;

        public EmailStorageController(IHostingEnvironment hostingEnvironment, IEmailStorageRepository emailStorageRepository, IEmailStoragePropertyRepository emailStoragePropertyRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _emailStorageRepository = emailStorageRepository;
            _emailStoragePropertyRepository = emailStoragePropertyRepository;
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
            model.IndustryID = AppGlobal.IndustryID;
            model.Display = AppGlobal.MasterEmailDisplay;
            model.EmailFrom = AppGlobal.MasterEmailUser;
            model.Password = AppGlobal.MasterEmailPassword;
            if (ID > 0)
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
        [AcceptVerbs("Post")]
        public IActionResult Save(EmailStorage model)
        {
            EmailStorageProperty emailStorageProperty = new EmailStorageProperty();
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files[0];
                if (file != null)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    fileName = AppGlobal.SetName(fileName);
                    fileName = fileName + "-" + AppGlobal.DateTimeCode + fileExtension;
                    var physicalPath = Path.Combine(_hostingEnvironment.WebRootPath, AppGlobal.EmailStorage, fileName);
                    using (var stream = new FileStream(physicalPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    emailStorageProperty.FileName = fileName;
                }
            }
            Initialization(model);
            if (model.ID > 0)
            {
                _emailStorageRepository.Update(model.ID, model);
            }
            else
            {
                model.Initialization(InitType.Insert, RequestUserID);
                _emailStorageRepository.Create(model);
            }
            if (model.ID > 0)
            {
                emailStorageProperty.Title = model.Subject;
                emailStorageProperty.ParentID = model.ID;
                emailStorageProperty.Initialization(InitType.Insert, RequestUserID);
                if (!string.IsNullOrEmpty(emailStorageProperty.FileName))
                {
                    _emailStoragePropertyRepository.Create(emailStorageProperty);
                }
            }
            return RedirectToAction("Detail", new { ID = model.ID });
        }
    }
}
