using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IConfigRepository : IRepository<Config>
    {
        public bool IsValidByGroupNameAndCodeAndURL(string groupName, string code, string url);
        public bool IsValidByGroupNameAndCodeAndTitle(string groupName, string code, string title);
        public bool IsValidByGroupNameAndCodeAndCodeName(string groupName, string code, string codeName);
        public List<Config> GetByCodeToList(string code);
        public List<Config> GetByGroupNameAndCodeToList(string groupName, string code);
        public List<Config> GetByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active);
    }
}
