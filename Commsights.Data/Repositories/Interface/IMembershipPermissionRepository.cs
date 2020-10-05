﻿using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IMembershipPermissionRepository : IRepository<MembershipPermission>
    {
        public MembershipPermission GetByMembershipIDAndAndCodeAndActive(int membershipID, string code, bool active);
        public MembershipPermission GetByProductName(string productName);
        public List<MembershipPermission> GetByMembershipIDToList(int membershipID);
        public List<MembershipPermission> GetIndustryIDAndCodeToList(int industryID, string code);
        public List<MembershipPermission> GetByCodeToList(string code);
        public List<MembershipPermission> GetByProductCodeToList(string code);
        public List<MembershipPermission> GetByMembershipIDAndCodeToList(int membershipID, string code);
        public List<MembershipPermission> GetByMembershipIDAndIndustryIDAndCodeToList(int membershipID, int industryID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferIndustryByMembershipIDAndCodeToList(int membershipID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferIndustryByMembershipIDAndCodeAndActiveToList(int membershipID, string code, bool active);
        public List<MembershipPermissionDataTransfer> GetDataTransferSegmentByMembershipIDAndCodeToList(int membershipID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferContactByMembershipIDAndCodeToList(int membershipID, string code);

        public List<MembershipPermissionDataTransfer> GetDataTransferCompanyByMembershipIDAndCodeToList(int membershipID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferMembershipByIndustryIDAndCodeToList(int industryID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferMembershipBySegmentIDAndCodeToList(int segmentID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferDailyReportSectionByMembershipIDAndCodeToList(int industryID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferDailyReportColumnByMembershipIDAndCodeToList(int membershipID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferIndustryByParentIDToList(int parentID);
        public List<MembershipPermissionDataTransfer> GetDataTransferIndustryByParentIDAndCodeToList(int parentID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferProductByParentIDAndCodeToList(int parentID, string code);

        public List<MembershipPermissionDataTransfer> GetDataTransferContactByMembershipIDAndIndustryIDAndCodeToList(int membershipID, int industryID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferSegmentByMembershipIDAndIndustryIDAndCodeToList(int membershipID, int industryID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferCompanyByMembershipIDAndIndustryIDAndCodeToList(int membershipID, int industryID, string code);        
        public List<MembershipPermissionDataTransfer> GetDataTransferDailyReportSectionByMembershipIDAndIndustryIDAndCodeToList(int membershipID, int industryID, string code);
        public List<MembershipPermissionDataTransfer> GetDataTransferDailyReportColumnByMembershipIDAndIndustryIDAndCodeToList(int membershipID, int industryID, string code);
        public void InitializationMenuPermission(int membershipID, int requestUserID);
        public void SaveAllMenuPermission(int membershipID, bool isAll, int requestUserID);
        public void InitializationDailyReportSection(int membershipID, string code, int requestUserID);
        public void InitializationDailyReportColumn(int membershipID, string code, int requestUserID);
        public void InitializationChannel(int membershipID, string code, int requestUserID);
        public void InitializationDailyReportSectionByMembershipIDAndIndustryID(int membershipID, int industryID, string code, int requestUserID);
        public void InitializationDailyReportColumnByMembershipIDAndIndustryID(int membershipID, int industryID, string code, int requestUserID);
        public void InitializationChannelByMembershipIDAndIndustryID(int membershipID, int industryID, string code, int requestUserID);
        public List<MembershipPermissionDataTransfer> GetDataTransferSegmentByMembershipIDAndIndustryIDAndCode001ToList(int membershipID, int industryID, string code);
        public List<MembershipPermission> GetDailyReportColumnByMembershipIDAndIndustryIDAndCodeAndActiveToList(int membershipID, int industryID, string code, bool active);
        public List<MembershipPermission> GetDailyReportColumnByMembershipIDAndIndustryIDAndCodeAndActiveFormSQLToList(int membershipID, int industryID, string code, bool active);
    }
}
