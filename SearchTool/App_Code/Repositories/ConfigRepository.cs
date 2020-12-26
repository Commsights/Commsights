using Commsights.Data.Helpers;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public class ConfigRepository
    {
        public ConfigRepository()
        {

        }
        public static List<Config> GetSQLWebsiteByGroupNameAndCodeAndActiveAndIsMenuLeftToList(string groupName, string code, bool active, bool isMenuLeft)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
                new SqlParameter("@Active",active),
                new SqlParameter("@IsMenuLeft",isMenuLeft),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectWebsiteByGroupNameAndCodeAndActiveAndIsMenuLeft", parameters);
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
    }
}
