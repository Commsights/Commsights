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
        public Config GetByGroupNameAndCodeAndParentID(string groupName, string code, int parentID)
        {
            Config item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.ParentID == parentID);
            return item;
        }
        public Config GetByGroupNameAndCodeAndTitle(string groupName, string code, string title)
        {
            Config item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.Title.Equals(title));
            return item;
        }
        public List<Config> GetByCodeToList(string code)
        {
            return _context.Config.Where(item => item.Code.Equals(code)).OrderBy(item => item.SortOrder).ToList();
        }
        public List<Config> GetByGroupNameAndCodeToList(string groupName, string code)
        {
            return _context.Config.Where(item => item.GroupName.Equals(groupName) && item.Code.Equals(code)).OrderBy(item => item.CodeName).OrderBy(item => item.SortOrder).ThenBy(item => item.CodeName).ToList();
        }
        public List<Config> GetMediaToList()
        {
            return _context.Config.Where(item => item.GroupName.Equals(AppGlobal.CRM) && (item.Code.Equals(AppGlobal.Website) || item.Code.Equals(AppGlobal.PressList))).OrderBy(item => item.Title).ToList();
        }
        public List<Config> GetMediaByGroupNameAndActiveToList(string groupName, bool active)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Active",active)
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectMediaByGroupNameAndActive", parameters);
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
        public List<Config> GetMediaByGroupNameToList(string groupName)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectMediaByGroupNameAndActive", parameters);
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
        public List<Config> GetMediaFullToList()
        {
            List<Config> list = new List<Config>();
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectMedia");
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
        public List<Config> GetByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active)
        {
            return _context.Config.Where(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.Active.Equals(active)).OrderBy(item => item.SortOrder).ToList();
        }
        public List<Config> GetByGroupNameAndCodeAndActiveAndIsMenuLeftToList(string groupName, string code, bool active, bool isMenuLeft)
        {
            return _context.Config.Where(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.Active.Equals(active) && item.IsMenuLeft.Equals(isMenuLeft)).OrderBy(item => item.ID).ToList();
        }
        public string UpdateByGroupNameAndCodeAndTitleAndColor(string groupName, string code, string title, int color)
        {
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
                new SqlParameter("@Title",title),
                new SqlParameter("@Color",color)
            };
            return SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ConfigUpdateByGroupNameAndCodeAndTitleAndColor", parameters);
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
        public List<Config> ToUpperFirstLetter()
        {
            List<Config> list = new List<Config>();
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigToUpperFirstLetter");
            list = SQLHelper.ToList<Config>(dt);
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
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectDataTransferWebisteByGroupNameAndCodeAndActive", parameters);
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
        public List<ConfigDataTransfer> GetDataTransferTierByTierIDAndIndustryIDToList(int tierID, int industryID)
        {
            List<ConfigDataTransfer> list = new List<ConfigDataTransfer>();
            SqlParameter[] parameters =
                        {
                new SqlParameter("@TierID",tierID),
                new SqlParameter("@IndustryID",industryID),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectDataTransferTierByTierIDAndIndustryID", parameters);
            list = SQLHelper.ToList<ConfigDataTransfer>(dt);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Parent = new ModelTemplate();
                list[i].Parent.ID = list[i].ParentID;
                list[i].Parent.TextName = list[i].ParentName;
            }
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
        public List<Config> GetAll001ToList()
        {
            List<Config> list = new List<Config>();            
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectAllItems");
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
    }
}
