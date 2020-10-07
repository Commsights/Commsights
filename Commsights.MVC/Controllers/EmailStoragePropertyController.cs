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
using Commsights.MVC.Models;

namespace Commsights.MVC.Controllers
{
    public class EmailStoragePropertyController : BaseController
    {
        private readonly IEmailStoragePropertyRepository _emailStoragePropertyRepository;

        public EmailStoragePropertyController(IEmailStoragePropertyRepository emailStoragePropertyRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _emailStoragePropertyRepository = emailStoragePropertyRepository;
        }
        public ActionResult GetByParentIDToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _emailStoragePropertyRepository.GetByParentIDToList(parentID);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetParentIDAndFileToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _emailStoragePropertyRepository.GetParentIDAndCodeToList(parentID, AppGlobal.File);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetParentIDAndEmailStorageToList([DataSourceRequest] DataSourceRequest request, int parentID)
        {
            var data = _emailStoragePropertyRepository.GetParentIDAndCodeToList(parentID, AppGlobal.EmailStorage);
            return Json(data.ToDataSourceResult(request));
        }
        public ActionResult GetDataTransferByDatePublishBeginAndDatePublishEndToList([DataSourceRequest] DataSourceRequest request, DateTime datePublishBegin, DateTime datePublishEnd)
        {
            var data = _emailStoragePropertyRepository.GetDataTransferByDatePublishBeginAndDatePublishEndToList(datePublishBegin, datePublishEnd);
            return Json(data.ToDataSourceResult(request));
        }
        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _emailStoragePropertyRepository.Delete(ID);
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
