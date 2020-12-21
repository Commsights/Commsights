using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commsights.Data.Enum;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using Commsights.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Commsights.MVC.Controllers
{
    public class BaseController : Controller, IActionFilter
    {
        private readonly IMembershipAccessHistoryRepository _membershipAccessHistoryRepository;
        private readonly IConfigRepository _configResposistory;
        public BaseController(IMembershipAccessHistoryRepository membershipAccessHistoryRepository)
        {
            _membershipAccessHistoryRepository = membershipAccessHistoryRepository;
        }
        public int RequestUserID
        {
            get
            {
                int.TryParse(Request.Cookies["UserID"]?.ToString(), out int result);
                return result;
            }
        }
        public bool IsUserAllow(string controller = "", string action = "", string QueryString = "")
        {
            MembershipAccessHistory membershipAccessHistory = new MembershipAccessHistory();
            membershipAccessHistory.Initialization(InitType.Insert, RequestUserID);
            membershipAccessHistory.DateTrack = DateTime.Now;
            membershipAccessHistory.MembershipId = RequestUserID;
            membershipAccessHistory.Controller = controller;
            membershipAccessHistory.Action = action;
            membershipAccessHistory.QueryString = QueryString;
            _membershipAccessHistoryRepository.Create(membershipAccessHistory);

            bool result = true;
            Config item = _membershipAccessHistoryRepository.GetMenuSelectByMembershipIDAndCodeAndControllerAndActionToList(RequestUserID, AppGlobal.Menu, controller, action);
            if (item != null)
            {
                if (item.ID > 0)
                {
                    result = item.IsMenuLeft.Value;
                }
            }
            return result;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string controller = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ControllerName;
            string action = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ActionName;
            string queryString = context.HttpContext.Request.QueryString.ToString();
            if ((controller.Equals("Home")) && (action.Equals("Index")))
            {
            }
            else
            {
                if (this.RequestUserID > 0)
                {
                    if (IsUserAllow(controller, action, queryString) == false)
                    {
                        context.Result = new RedirectResult("/Home/Index");
                    }
                }
                else
                {
                    if (((controller.Equals("Membership")) && (action.Equals("Login"))) || ((controller.Equals("Product")) && (action.Equals("ViewContent"))))
                    {
                    }
                    else
                    {
                        context.Result = new RedirectResult("/Home/Index");
                    }
                }
            }
        }
    }
}
