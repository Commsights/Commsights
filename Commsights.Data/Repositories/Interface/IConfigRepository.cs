using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
        public List<Config> GetByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active);
        public List<Config> GetByGroupNameAndCodeAndActiveAndIsMenuLeftToList(string groupName, string code, bool active, bool isMenuLeft);
        public List<ConfigDataTransfer> GetDataTransferParentByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active);
        public List<ConfigDataTransfer> GetDataTransferChildrenCountByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active);
        public List<ConfigDataTransfer> GetDataTransferPressListByGroupNameAndCodeToList(string groupName, string code);
        public List<ConfigDataTransfer> GetDataTransferWebsiteByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active);
        public List<Config> GetMediaToList();
        public List<ConfigDataTransfer> GetDataTransferTierByTierIDAndIndustryIDToList(int tierID, int industryID);
    }
}
