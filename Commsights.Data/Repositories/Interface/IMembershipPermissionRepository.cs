using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IMembershipPermissionRepository : IRepository<MembershipPermission>
    {
        public List<MembershipPermission> GetByMembershipIDToList(int membershipID);
        public List<MembershipPermission> GetBrandIDAndCodeToList(int brandID, string code);
        public List<MembershipPermission> GetByMembershipIDAndCodeToList(int membershipID, string code);
        public List<MembershipPermission> GetByMembershipIDAndBrandIDAndCodeToList(int membershipID, int brandID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferBrandByMembershipIDAndCodeToList(int membershipID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferMembershipByBrandIDAndCodeToList(int brandID, string code);
        public void InitializationMenuPermission(int membershipID, int requestUserID);
        public void SaveAllMenuPermission(int membershipID, bool isAll, int requestUserID);
    }
}
