using System;
using System.Text;
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
using Commsights.MVC.Models;
using Commsights.Service.Mail;

namespace Commsights.MVC.Controllers
{
    public class EmailStorageController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IEmailStorageRepository _emailStorageRepository;
        private readonly IEmailStoragePropertyRepository _emailStoragePropertyRepository;
        private readonly IMailService _mailService;
        public EmailStorageController(IWebHostEnvironment hostingEnvironment, IMailService mailService, IEmailStorageRepository emailStorageRepository, IEmailStoragePropertyRepository emailStoragePropertyRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _emailStorageRepository = emailStorageRepository;
            _emailStoragePropertyRepository = emailStoragePropertyRepository;
            _mailService = mailService;
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
            BaseViewModel model = new BaseViewModel();
            model.DatePublishBegin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            model.DatePublishEnd = DateTime.Now;
            return View(model);
        }
        public IActionResult List()
        {
            BaseViewModel model = new BaseViewModel();
            model.DatePublishBegin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            model.DatePublishEnd = DateTime.Now;
            return View(model);
        }
        public IActionResult ReadView()
        {
            BaseViewModel model = new BaseViewModel();
            model.DatePublishBegin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            model.DatePublishEnd = DateTime.Now;
            return View(model);
        }
        public IActionResult Detail(int ID)
        {
            EmailStorage model = new EmailStorage();
            model.Display = AppGlobal.MasterEmailDisplay;
            model.EmailFrom = AppGlobal.MasterEmailUser;
            model.Password = AppGlobal.MasterEmailPassword;
            if (ID > 0)
            {
                model = _emailStorageRepository.GetByID(ID);
            }
            return View(model);
        }
        public IActionResult Preview(int ID)
        {
            EmailStorage model = new EmailStorage();
            if (ID > 0)
            {
                model = _emailStorageRepository.GetByID(ID);
                StringBuilder txt = new StringBuilder();
                List<EmailStorageProperty> list = _emailStoragePropertyRepository.GetParentIDAndCodeToList(model.ID, AppGlobal.File);
                string url001 = Commsights.Data.Helpers.AppGlobal.Domain + "EmailStorage/PDF?ID=" + model.ID + "&email=" + model.EmailTo;
                txt.AppendLine(@"<a target='_blank' href='" + url001 + "' title='PDF'>PDF</a>");
                foreach (EmailStorageProperty item in list)
                {
                    string url = Commsights.Data.Helpers.AppGlobal.Domain + "EmailStorage/Read?ID=" + item.ID + "&email=" + model.EmailTo;
                    txt.AppendLine(@"<a target='_blank' href='" + url + "' title='" + model.Subject + "'>" + model.Subject + "</a>");
                }
                model.Note = txt.ToString();
            }
            return View(model);
        }
        public IActionResult Read(int ID, string email)
        {
            string url = Commsights.Data.Helpers.AppGlobal.Domain + "EmailStorage/";
            EmailStorageProperty model = new EmailStorageProperty();
            if (ID > 0)
            {
                model = _emailStoragePropertyRepository.GetByID(ID);
                url = url + model.FileName;
                EmailStorageProperty read = new EmailStorageProperty();
                read.Email = email;
                read.DateRead = DateTime.Now;
                read.ParentID = model.ParentID;
                read.Code = AppGlobal.EmailStorage;
                read.Initialization(InitType.Insert, RequestUserID);
                _emailStoragePropertyRepository.Create(read);
            }
            return Redirect(url);
        }
        public IActionResult PDF(int ID, string email)
        {
            string url = Commsights.Data.Helpers.AppGlobal.Domain + "EmailStorage/Preview?ID=" + ID;
            EmailStorageProperty model = new EmailStorageProperty();
            if (ID > 0)
            {
                EmailStorageProperty read = new EmailStorageProperty();
                read.DateRead = DateTime.Now;
                read.Email = email;
                read.ParentID = ID;
                read.Code = AppGlobal.EmailStorage;
                read.Initialization(InitType.Insert, RequestUserID);
                _emailStoragePropertyRepository.Create(read);
            }
            return Redirect(url);
        }
        public void Hiden(int ID, string email)
        {
            EmailStorageProperty model = new EmailStorageProperty();
            if (ID > 0)
            {
                EmailStorageProperty read = new EmailStorageProperty();
                read.DateRead = DateTime.Now;
                read.Email = email;
                read.ParentID = ID;
                read.Code = AppGlobal.EmailStorage;
                read.Initialization(InitType.Insert, RequestUserID);
                _emailStoragePropertyRepository.Create(read);
            }
        }
        public IActionResult SendMailByID(int ID)
        {
            EmailStorage model = _emailStorageRepository.GetByID(ID);
            if (model != null)
            {
                StringBuilder body = new StringBuilder();
                StringBuilder txt = new StringBuilder();
                string url001 = Commsights.Data.Helpers.AppGlobal.Domain + "EmailStorage/PDF?ID=" + model.ID + "&email=" + model.EmailTo;
                txt.AppendLine(@"<a target='_blank' href='" + url001 + "' title='PDF'>PDF</a>");
                List<EmailStorageProperty> list = _emailStoragePropertyRepository.GetParentIDAndCodeToList(model.ID, AppGlobal.File);
                foreach (EmailStorageProperty item in list)
                {
                    string url = Commsights.Data.Helpers.AppGlobal.Domain + "EmailStorage/Read?ID=" + item.ID + "&email=" + model.EmailTo;
                    txt.AppendLine(@"<a target='_blank' href='" + url + "' title='" + model.Subject + "'>" + model.Subject + "</a>");
                }
                body.AppendLine(@"<p><b>Download: " + txt.ToString() + "</b></p>");
                body.AppendLine(@"" + model.EmailBody);
                body.AppendLine(@"<hr />");
                body.AppendLine(@"<div style='line-height:20px;'>");
                body.AppendLine(@"<img src='" + Commsights.Data.Helpers.AppGlobal.Logo01URLFull + "' width='30%' height='30%' title='" + Commsights.Data.Helpers.AppGlobal.CompanyTitleEnglish + "' alt='" + Commsights.Data.Helpers.AppGlobal.CompanyTitleEnglish + "' />");
                body.AppendLine(@"<br />");
                body.AppendLine(@"" + Commsights.Data.Helpers.AppGlobal.GoogleMapHTML);
                body.AppendLine(@"<br />");
                body.AppendLine(@"Tel: " + Commsights.Data.Helpers.AppGlobal.PhoneReportHTML + " - Email: " + Commsights.Data.Helpers.AppGlobal.EmailReportHTML);
                body.AppendLine(@"<br />");
                body.AppendLine(@"Facebook: " + Commsights.Data.Helpers.AppGlobal.FacebookHTML + " - Website: " + Commsights.Data.Helpers.AppGlobal.WebsiteHTML);
                body.AppendLine(@"</div>");
                string url002 = Commsights.Data.Helpers.AppGlobal.Domain + "EmailStorage/Hiden?ID=" + model.ID + "&email=" + model.EmailTo;
                body.AppendLine(@"<img src='" + url002 + "' height='0' width='0' title='" + Commsights.Data.Helpers.AppGlobal.CompanyTitleEnglish + "' alt='" + Commsights.Data.Helpers.AppGlobal.CompanyTitleEnglish + "'></img>");
                Commsights.Service.Mail.Mail mail = new Service.Mail.Mail();
                mail.Initialization();
                mail.Content = body.ToString();
                mail.Subject = model.Subject;
                mail.ToMail = model.EmailTo;
                mail.CCMail = model.EmailCC;
                mail.BCCMail = model.EmailBCC;
                mail.FromMail = model.EmailFrom;
                mail.Username = model.EmailFrom;
                mail.Password = model.Password;
                mail.Display = model.Display;
                try
                {
                    _mailService.Send(mail);
                    model.DateSend = DateTime.Now;
                    model.Initialization(InitType.Update, RequestUserID);
                    _emailStorageRepository.Update(model.ID, model);
                }
                catch (Exception e)
                {
                }
            }
            string note = AppGlobal.Success + " - " + AppGlobal.SendMailSuccess;
            return Json(note);
        }
        public ActionResult GetDataTransferByDatePublishBeginAndDatePublishEndToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd)
        {
            var data = _emailStorageRepository.GetDataTransferByDatePublishBeginAndDatePublishEndToList(datePublishBegin, datePublishEnd);
            return Json(data.ToDataSourceResult(request));
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
                model.Initialization(InitType.Update, RequestUserID);
                _emailStorageRepository.Update(model.ID, model);
            }
            else
            {
                model.Initialization(InitType.Insert, RequestUserID);
                _emailStorageRepository.Create(model);
            }
            if (model.ID > 0)
            {
                emailStorageProperty.Code = AppGlobal.File;
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
