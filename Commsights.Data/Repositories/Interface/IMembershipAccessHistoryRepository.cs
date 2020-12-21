using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IMembershipAccessHistoryRepository : IRepository<MembershipAccessHistory>
    {
        public List<Config> GetMenuSelectByMembershipIDAndCodeAndIsMenuLeftToList(int membershipID, string code, bool isMenuLeft);
        public Config GetMenuSelectByMembershipIDAndCodeAndIsViewAndControllerAndActionToList(int membershipID, string code, bool isView, string controller, string action);
        public Config GetMenuSelectByMembershipIDAndCodeAndControllerAndActionToList(int membershipID, string code, string controller, string action);
    }
}
