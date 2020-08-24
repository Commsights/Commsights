using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IMembershipPermissionRepository : IRepository<MembershipPermission>
    {
        public List<MembershipPermission> GetByMembershipIDToList(int membershipID);
        public List<MembershipPermission> GetBrandOfCustomerToList(int membershipID);
        public List<MembershipPermission> GetCustomerOfBrandToList(int brandId);
        public void InitializationMenuPermission(int membershipID, int requestUserID);
        public void SaveAllMenuPermission(int membershipID, bool isAll, int requestUserID);
    }
}
