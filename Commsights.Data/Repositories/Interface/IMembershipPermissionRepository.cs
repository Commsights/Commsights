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
        public List<MembershipPermissionDataTransfer> GetDataTransferSegmentByMembershipIDAndCodeToList(int membershipID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferContactByMembershipIDAndCodeToList(int membershipID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferCompanyByMembershipIDAndCodeToList(int membershipID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferMembershipByIndustryIDAndCodeToList(int industryID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferDailyReportSectionByMembershipIDAndCodeToList(int industryID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferDailyReportColumnByMembershipIDAndCodeToList(int membershipID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferIndustryByParentIDToList(int parentID);
        public List<MembershipPermissionDataTransfer> GetDataTransferIndustryByParentIDAndCodeToList(int parentID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferProductByParentIDAndCodeToList(int parentID, string code);
        public void InitializationMenuPermission(int membershipID, int requestUserID);
        public void SaveAllMenuPermission(int membershipID, bool isAll, int requestUserID);
        public void InitializationDailyReportSection(int membershipID, string code, int requestUserID);
        public void InitializationDailyReportColumn(int membershipID, string code, int requestUserID);
    }
}
