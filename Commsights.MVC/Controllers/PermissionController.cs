﻿using System;
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
        public IActionResult CreateWebsiteScan()
        {
            List<Config> list = _configResposistory.GetByGroupNameAndCodeAndActiveToList(AppGlobal.CRM, AppGlobal.Website, true).OrderBy(item => item.Title).ToList();
            foreach (Config config in list)
            {
                if (config != null)
                {
                    try
                    {
                        string html = "";
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(config.URLFull);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Stream receiveStream = response.GetResponseStream();
                            StreamReader readStream = null;
                            if (String.IsNullOrWhiteSpace(response.CharacterSet))
                            {
                                readStream = new StreamReader(receiveStream);
                            }
                            else
                            {
                                readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                            }
                            html = readStream.ReadToEnd();
                            response.Close();
                            readStream.Close();
                        }
                        else
                        {
                            if (config.URLFull.Contains(@"http:") == true)
                            {
                                config.URLFull = config.URLFull.Replace(@"http:", @"https:");
                            }
                            else
                            {
                                config.URLFull = config.URLFull.Replace(@"https:", @"http:");
                            }
                            request = (HttpWebRequest)WebRequest.Create(config.URLFull);
                            response = (HttpWebResponse)request.GetResponse();
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                _configResposistory.Update(config.ID, config);
                                Stream receiveStream = response.GetResponseStream();
                                StreamReader readStream = null;
                                if (String.IsNullOrWhiteSpace(response.CharacterSet))
                                {
                                    readStream = new StreamReader(receiveStream);
                                }
                                else
                                {
                                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                                }
                                html = readStream.ReadToEnd();
                                response.Close();
                                readStream.Close();
                            }
                            else
                            {
                                _configResposistory.Delete(config.ID);
                            }
                        }
                        List<LinkItem> listLinkItem = AppGlobal.LinkFinder(html, config.URLFull);
                        foreach (LinkItem linkItem in listLinkItem)
                        {
                            Config item = new Config();
                            item.ParentID = config.ID;
                            item.GroupName = AppGlobal.CRM;
                            item.Code = AppGlobal.Website;
                            item.Active = false;
                            item.Title = linkItem.Text;
                            item.URLFull = linkItem.Href;
                            item.Initialization(InitType.Insert, RequestUserID);
                            if (_configResposistory.IsValidByGroupNameAndCodeAndURL(item.GroupName, item.Code, item.URLFull) == true)
                            {
                                try
                                {
                                    _configResposistory.Create(item);
                                }
                                catch (Exception e)
                                {
                                    item.Title = "";
                                    try
                                    {
                                        _configResposistory.Create(item);
                                    }
                                    catch (Exception e1)
                                    {
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
            string note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            return Json(note);
        }
    }
}
