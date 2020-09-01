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
        public List<MembershipPermission> GetIndustryIDAndCodeToList(int industryID, string code);
        public List<MembershipPermission> GetByMembershipIDAndCodeToList(int membershipID, string code);
        public List<MembershipPermission> GetByMembershipIDAndIndustryIDAndCodeToList(int membershipID, int industryID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferIndustryByMembershipIDAndCodeToList(int membershipID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferCompanyByMembershipIDAndCodeToList(int membershipID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferMembershipByIndustryIDAndCodeToList(int industryID, string code);
        public void InitializationMenuPermission(int membershipID, int requestUserID);
        public void SaveAllMenuPermission(int membershipID, bool isAll, int requestUserID);
    }
}
