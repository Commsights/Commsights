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
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Diagnostics.Eventing.Reader;
using Commsights.Service.Mail;
using System.Drawing;

namespace Commsights.MVC.Controllers
{
    public class PermissionController : BaseController
    {
        private readonly IConfigRepository _configResposistory;
        private readonly IProductRepository _productRepository;
        public PermissionController(IConfigRepository configResposistory, IProductRepository productRepository, IMembershipAccessHistoryRepository membershipAccessHistoryRepository) : base(membershipAccessHistoryRepository)
        {
            _configResposistory = configResposistory;
            _productRepository = productRepository;
        }
        public IActionResult Setup()
        {
            return View();
        }
        public IActionResult UpdatePressList()
        {
            List<Config> list = _configResposistory.GetByGroupNameAndCodeToList(Commsights.Data.Helpers.AppGlobal.CRM, Commsights.Data.Helpers.AppGlobal.PressList);
            foreach (Config item in list)
            {
                item.Title = AppGlobal.ToUpperFirstLetter(item.Title);
                _configResposistory.Update(item.ID, item);
            }
            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            return Json(note);
        }
        public IActionResult UpdateProduct()
        {
            List<Product> list = _productRepository.GetAllToList();
            foreach (Product item in list)
            {
                item.Title = AppGlobal.ToUpperFirstLetter(item.Title);
                _productRepository.Update(item.ID, item);
            }
            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            return Json(note);
        }
    }
}
