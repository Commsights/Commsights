using Commsights.Data.DataTransferObject;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Commsights.Data.Repositories
{
    public class ConfigRepository : Repository<Config>, IConfigRepository
    {
        private readonly CommsightsContext _context;

        public ConfigRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public bool IsValidByGroupNameAndCodeAndURL(string groupName, string code, string url)
        {
            Config item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.URLFull.Equals(url));
            return item == null ? true : false;
        }
        public bool IsValidByGroupNameAndCodeAndTitle(string groupName, string code, string title)
        {
            Config item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.Title.Equals(title));
            return item == null ? true : false;
        }
        public bool IsValidByGroupNameAndCodeAndCodeName(string groupName, string code, string codeName)
        {
            Config item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.CodeName.Equals(codeName));
            return item == null ? true : false;
        }
        public Config GetByGroupNameAndCodeAndCodeName(string groupName, string code, string codeName)
        {            
            Config item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.CodeName.Equals(codeName));
            return item;
        }
        public Config GetByGroupNameAndCodeAndTitle(string groupName, string code, string title)
        {
            Config item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.Title.Equals(title));
            return item;
        }
        public List<Config> GetByCodeToList(string code)
        {
            return _context.Config.Where(item => item.Code.Equals(code)).ToList();
        }
        public List<Config> GetByGroupNameAndCodeToList(string groupName, string code)
        {
            return _context.Config.Where(item => item.GroupName.Equals(groupName) && item.Code.Equals(code)).OrderBy(item => item.CodeName).ToList();
        }
        public List<Config> GetByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active)
        {
            return _context.Config.Where(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.Active.Equals(active)).OrderBy(item => item.Title).ToList();
        }
        public List<Config> GetByGroupNameAndCodeAndActiveAndIsMenuLeftToList(string groupName, string code, bool active, bool isMenuLeft)
        {
            return _context.Config.Where(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.Active.Equals(active) && item.IsMenuLeft.Equals(isMenuLeft)).OrderBy(item => item.URLFull).ToList();
        }
        public List<ConfigDataTransfer> GetDataTransferParentByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active)
        {
            List<ConfigDataTransfer> list = new List<ConfigDataTransfer>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
                new SqlParameter("@Active",active)
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectParentByGroupNameAndCodeAndActive", parameters);
            list = SQLHelper.ToList<ConfigDataTransfer>(dt);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Parent = new ModelTemplate();
                list[i].Parent.ID = list[i].ParentID;
                list[i].Parent.TextName = list[i].ParentName;
            }
            return list;
        }
        public List<ConfigDataTransfer> GetDataTransferWebsiteByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active)
        {
            List<ConfigDataTransfer> list = new List<ConfigDataTransfer>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
                new SqlParameter("@Active",active)
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectDataTransferWebisteByGroupNameAndCode", parameters);
            list = SQLHelper.ToList<ConfigDataTransfer>(dt);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Country = new ModelTemplate();
                list[i].Country.ID = list[i].CountryID;
                list[i].Country.TextName = list[i].CountryName;
                list[i].Parent = new ModelTemplate();
                list[i].Parent.ID = list[i].ParentID;
                list[i].Parent.TextName = list[i].ParentName;
                list[i].Language = new ModelTemplate();
                list[i].Language.ID = list[i].LanguageID;
                list[i].Language.TextName = list[i].LanguageName;
                list[i].Frequency = new ModelTemplate();
                list[i].Frequency.ID = list[i].FrequencyID;
                list[i].Frequency.TextName = list[i].FrequencyName;
                list[i].ColorType = new ModelTemplate();
                list[i].ColorType.ID = list[i].ColorTypeID;
                list[i].ColorType.TextName = list[i].ColorTypeName;
            }
            return list;
        }
        public List<ConfigDataTransfer> GetDataTransferChildrenCountByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active)
        {
            List<ConfigDataTransfer> list = new List<ConfigDataTransfer>();
            SqlParameter[] parameters =
                        {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
                new SqlParameter("@Active",active)
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectDisplayByGroupNameAndCodeAndActive", parameters);
            list = SQLHelper.ToList<ConfigDataTransfer>(dt);
            return list;

        }
        public List<ConfigDataTransfer> GetDataTransferPressListByGroupNameAndCodeToList(string groupName, string code)
        {
            List<ConfigDataTransfer> list = new List<ConfigDataTransfer>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code)
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectDataTransferPressListByGroupNameAndCode", parameters);
            list = SQLHelper.ToList<ConfigDataTransfer>(dt);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Country = new ModelTemplate();
                list[i].Country.ID = list[i].CountryID;
                list[i].Country.TextName = list[i].CountryName;
                list[i].Parent = new ModelTemplate();
                list[i].Parent.ID = list[i].ParentID;
                list[i].Parent.TextName = list[i].ParentName;
                list[i].Language = new ModelTemplate();
                list[i].Language.ID = list[i].LanguageID;
                list[i].Language.TextName = list[i].LanguageName;
                list[i].Frequency = new ModelTemplate();
                list[i].Frequency.ID = list[i].FrequencyID;
                list[i].Frequency.TextName = list[i].FrequencyName;
                list[i].ColorType = new ModelTemplate();
                list[i].ColorType.ID = list[i].ColorTypeID;
                list[i].ColorType.TextName = list[i].ColorTypeName;
            }
            return list;
        }
    }
}
