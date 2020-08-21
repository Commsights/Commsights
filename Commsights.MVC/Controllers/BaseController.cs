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

namespace Commsights.MVC.Controllers
{
    public class BaseController : Controller, IActionFilter
    {
        private readonly IMembershipAccessHistoryRepository _membershipAccessHistoryRepository;
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
        public bool IsUserAllow(string Controller = "", string Action = "", string QueryString = "")
        {
            MembershipAccessHistory membershipAccessHistory = new MembershipAccessHistory();
            membershipAccessHistory.Initialization(InitType.Insert, RequestUserID);
            membershipAccessHistory.DateTrack = DateTime.Now;
            membershipAccessHistory.MembershipId = RequestUserID;
            membershipAccessHistory.Controller = Controller;
            membershipAccessHistory.Action = Action;
            membershipAccessHistory.QueryString = QueryString;
            _membershipAccessHistoryRepository.Create(membershipAccessHistory);
            return true;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string Controller = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ControllerName;
            string Action = ((ControllerBase)context.Controller).ControllerContext.ActionDescriptor.ActionName;
            string QueryString = context.HttpContext.Request.QueryString.ToString();
            if (IsUserAllow(Controller, Action, QueryString) == false)
            {
                context.Result = new RedirectResult("/Home/Index");
            }
            else
            {
            }
        }
    }
}
