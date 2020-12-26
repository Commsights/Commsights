using Commsights.Data.Helpers;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Commsights.Data.Repositories
{
    public class MembershipAccessHistoryRepository : Repository<MembershipAccessHistory>, IMembershipAccessHistoryRepository
    {
        private readonly CommsightsContext _context;

        public MembershipAccessHistoryRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public List<Config> GetMenuSelectByMembershipIDAndCodeAndIsMenuLeftToList(int membershipID, string code, bool isMenuLeft)
        {
            List<Config> list = new List<Config>();
            if (membershipID > 0)
            {
                SqlParameter[] parameters =
                           {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@Code",code),
                new SqlParameter("@IsMenuLeft",isMenuLeft),
                 };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigMenuSelectByMembershipIDAndCodeAndIsMenuLeft", parameters);
                list = SQLHelper.ToList<Config>(dt);
            }
            return list;
        }
        public Config GetMenuSelectByMembershipIDAndCodeAndIsViewAndControllerAndActionToList(int membershipID, string code, bool isView, string controller, string action)
        {
            Config item = new Config();
            if (membershipID > 0)
            {
                SqlParameter[] parameters =
                           {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@Code",code),
                new SqlParameter("@IsView",isView),
                new SqlParameter("@Controller",controller),
                new SqlParameter("@Action",action),
                 };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigMenuSelectByMembershipIDAndCodeAndIsViewAndControllerAndAction", parameters);
                if (dt.Rows.Count > 0)
                {
                    item = SQLHelper.ToList<Config>(dt).FirstOrDefault();
                }
            }
            return item;
        }
        public Config GetMenuSelectByMembershipIDAndCodeAndControllerAndActionToList(int membershipID, string code, string controller, string action)
        {
            Config item = new Config();
            if (membershipID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@MembershipID",membershipID),
                    new SqlParameter("@Code",code),                
                    new SqlParameter("@Controller",controller),
                    new SqlParameter("@Action",action),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigMenuSelectByMembershipIDAndCodeAndControllerAndAction", parameters);
                if (dt.Rows.Count > 0)
                {
                    item = SQLHelper.ToList<Config>(dt).FirstOrDefault();
                }
            }
            return item;
        }
    }
}
