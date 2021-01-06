using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public interface IConfigRepository : IRepository<Config>
    {
        public List<Config> ToUpperFirstLetter();
        public string UpdateByGroupNameAndCodeAndTitleAndColor(string groupName, string code, string title, int color);
        public bool IsValidByGroupNameAndCodeAndURL(string groupName, string code, string url);
        public bool IsValidByGroupNameAndCodeAndTitle(string groupName, string code, string title);
        public bool IsValidByGroupNameAndCodeAndCodeName(string groupName, string code, string codeName);
        public Config GetByGroupNameAndCodeAndCodeName(string groupName, string code, string codeName);
        public Config GetByGroupNameAndCodeAndTitle(string groupName, string code, string title);
        public Config GetByGroupNameAndCodeAndParentID(string groupName, string code, int parentID);
        public List<Config> GetByCodeToList(string code);
        public List<Config> GetByGroupNameAndCodeToList(string groupName, string code);
        public List<Config> GetMediaByGroupNameToList(string groupName);
        public List<Config> GetMediaByGroupNameAndActiveToList(string groupName, bool active);
        public List<Config> GetByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active);
        public List<Config> GetByGroupNameAndCodeAndActiveAndIsMenuLeftToList(string groupName, string code, bool active, bool isMenuLeft);
        public List<ConfigDataTransfer> GetDataTransferParentByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active);
        public List<ConfigDataTransfer> GetDataTransferChildrenCountByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active);
        public List<ConfigDataTransfer> GetDataTransferChildrenCountByGroupNameAndCodeAndActiveAndIsMenuLeftToList(string groupName, string code, bool active, bool isMenuLeft);
        public List<ConfigDataTransfer> GetDataTransferPressListByGroupNameAndCodeToList(string groupName, string code);
        public List<ConfigDataTransfer> GetDataTransferWebsiteByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active);
        public List<Config> GetMediaToList();
        public List<Config> GetMediaFullToList();
        public List<Config> GetAll001ToList();
        public List<ConfigDataTransfer> GetDataTransferTierByTierIDAndIndustryIDToList(int tierID, int industryID);
        public List<Config> GetByParentIDAndGroupNameAndCodeToList(int parentID, string groupName, string code);
        public string DeleteByParentIDAndGroupNameAndCode(int parentID, string groupName, string code);
        public List<Config> GetByIDListToList(string IDList);
        public Task<string> AsyncInsertSingleItem(Config config);
        public Config GetByGroupNameAndCodeAndParentIDAndTierID(string groupName, string code, int parentID, int tierID);
        public List<Config> GetSQLWebsiteByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active);
        public List<Config> GetSQLWebsiteByGroupNameAndCodeAndActiveAndIsMenuLeftToList(string groupName, string code, bool active, bool isMenuLeft);
        public List<Config> GetWebsiteToList();
        public string UpdateSingleItem001(Config config);
        public List<Config> GetSQLByGroupNameAndCodeToList(string groupName, string code);
        public List<Config> GetByGroupNameAndCodeAndParentIDAndIndustryIDToList(string groupName, string code, int parentID, int industryID);
        public List<Config> GetByGroupNameAndCodeAndIndustryIDToList(string groupName, string code, int industryID);
        public List<Config> GetSQLByGroupNameAndCodeAndIndustryIDToList(string groupName, string code, int industryID);
        public List<Config> GetSQLCategorySubByGroupNameAndCodeAndIndustryIDToList(string groupName, string code, int industryID);
        public Config GetByGroupNameAndCodeAndIndustryIDAndCodeName(string groupName, string code, int industryID, string codeName);
        public List<Config> GetSQLByGroupNameAndCodeAndIndustryIDAndParentIDToList(string groupName, string code, int industryID, int parentID);
        public List<Config> GetMenuSelectByMembershipIDAndCodeToList(int membershipID, string code);
        public List<Config> GetMenuSelectByMembershipIDAndCodeAndIsMenuLeftToList(int membershipID, string code, bool isMenuLeft);
        public List<Config> GetMenuSelectByMembershipIDAndCodeAndIsMenuLeftAndIsViewToList(int membershipID, string code, bool isMenuLeft, bool isView);
        public List<Config> GetSQLWebsiteByGroupNameAndCodeAndActiveAndRowBeginAndRowEndToList(string groupName, string code, bool active, int rowBegin, int rowEnd);
        public List<Config> GetSQLWebsiteByGroupNameAndCodeAndActiveAndIsMenuLeftAndRowBeginAndRowEndToList(string groupName, string code, bool active, bool isMenuLeft, int rowBegin, int rowEnd);
        public List<Config> GetProductPermissionDistinctIndustryByEmployeeIDToList(int employeeID);
        public List<Config> GetSQLWebsiteByGroupNameAndCodeToList(string groupName, string code);
        public string DeleteMenuByID(int ID);
    }
}
