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
using System.Threading.Tasks;

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
            if (item == null)
            {
                item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.Title.Equals(codeName));
            }
            if (item == null)
            {
                item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.Note.Equals(codeName));
            }
            return item;
        }
        public Config GetByGroupNameAndCodeAndParentID(string groupName, string code, int parentID)
        {
            Config item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.ParentID == parentID);
            return item;
        }
        public Config GetByGroupNameAndCodeAndParentIDAndTierID(string groupName, string code, int parentID, int tierID)
        {
            Config item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.ParentID == parentID && item.TierID == tierID);
            return item;
        }

        public Config GetByGroupNameAndCodeAndTitle(string groupName, string code, string title)
        {
            Config item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.Title.Equals(title));
            if (item == null)
            {
                item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.CodeName.Equals(title));
            }
            if (item == null)
            {
                item = _context.Set<Config>().FirstOrDefault(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.Note.Equals(title));
            }
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
        public List<Config> GetByParentIDAndGroupNameAndCodeToList(int parentID, string groupName, string code)
        {
            return _context.Config.Where(item => item.GroupName.Equals(AppGlobal.CRM) && item.Code.Equals(AppGlobal.Website) && item.ParentID == parentID).OrderBy(item => item.ID).ToList();
        }
        public List<Config> GetByGroupNameAndCodeAndParentIDAndIndustryIDToList(string groupName, string code, int parentID, int industryID)
        {
            return _context.Config.Where(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.ParentID == parentID && item.IndustryID == industryID).OrderBy(item => item.SortOrder).ToList();
        }
        public List<Config> GetByGroupNameAndCodeAndIndustryIDToList(string groupName, string code, int industryID)
        {
            List<Config> list = new List<Config>();
            if (industryID > 0)
            {
                list = _context.Config.Where(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.IndustryID == industryID).OrderBy(item => item.SortOrder).ToList();
            }
            return list;
        }
        public Config GetByGroupNameAndCodeAndIndustryIDAndCodeName(string groupName, string code, int industryID, string codeName)
        {
            Config model = new Config();
            if (industryID > 0)
            {
                model = _context.Config.Where(item => item.GroupName.Equals(groupName) && item.Code.Equals(code) && item.IndustryID == industryID && item.CodeName.Equals(codeName)).FirstOrDefault();
            }
            return model;
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
        public List<Config> GetSQLByGroupNameAndCodeAndIndustryIDToList(string groupName, string code, int industryID)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
                new SqlParameter("@IndustryID",industryID),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectByGroupNameAndCodeAndIndustryID", parameters);
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
        public List<Config> GetSQLCategorySubByGroupNameAndCodeAndIndustryIDToList(string groupName, string code, int industryID)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
                new SqlParameter("@IndustryID",industryID),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectCategorySubByGroupNameAndCodeAndIndustryID", parameters);
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
        public List<Config> GetSQLByGroupNameAndCodeAndIndustryIDAndParentIDToList(string groupName, string code, int industryID, int parentID)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
                new SqlParameter("@IndustryID",industryID),
                new SqlParameter("@ParentID",parentID),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectByGroupNameAndCodeAndIndustryIDAndParentID", parameters);
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
        public List<ConfigDataTransfer> GetDataTransferChildrenCountByGroupNameAndCodeAndActiveAndIsMenuLeftToList(string groupName, string code, bool active, bool isMenuLeft)
        {
            List<ConfigDataTransfer> list = new List<ConfigDataTransfer>();
            SqlParameter[] parameters =
                        {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
                new SqlParameter("@Active",active),
                new SqlParameter("@IsMenuLeft",isMenuLeft),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectDisplayByGroupNameAndCodeAndActiveAndIsMenuLeft", parameters);
            list = SQLHelper.ToList<ConfigDataTransfer>(dt);
            return list;

        }
        public List<ConfigDataTransfer> GetDataTransferTierByTierIDAndIndustryIDToList(int tierID, int industryID)
        {
            List<ConfigDataTransfer> list = new List<ConfigDataTransfer>();
            if (tierID > 0)
            {
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
        public List<Config> GetMenuSelectByMembershipIDAndCodeToList(int membershipID, string code)
        {
            List<Config> list = new List<Config>();
            if (membershipID > 0)
            {
                SqlParameter[] parameters =
                           {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@Code",code)
                 };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigMenuSelectByMembershipIDAndCode", parameters);
                list = SQLHelper.ToList<Config>(dt);
            }
            return list;
        }
        public List<Config> GetMenuSelectByMembershipIDAndCodeAndIsMenuLeftToList(int membershipID, string code, bool isMenuLeft)
        {
            List<Config> list = new List<Config>();
            if (membershipID > 0)
            {
                SqlParameter[] parameters =
                           {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@Code",code),
                new SqlParameter("@IsMenuLeft",isMenuLeft),
                 };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigMenuSelectByMembershipIDAndCodeAndIsMenuLeft", parameters);
                list = SQLHelper.ToList<Config>(dt);
            }
            return list;
        }
        public List<Config> GetMenuSelectByMembershipIDAndCodeAndIsMenuLeftAndIsViewToList(int membershipID, string code, bool isMenuLeft, bool isView)
        {
            List<Config> list = new List<Config>();
            if (membershipID > 0)
            {
                SqlParameter[] parameters =
                           {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@Code",code),
                new SqlParameter("@IsMenuLeft",isMenuLeft),
                new SqlParameter("@IsView",isView),
                 };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigMenuSelectByMembershipIDAndCodeAndIsMenuLeftAndIsView", parameters);
                list = SQLHelper.ToList<Config>(dt);
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
        public string DeleteByParentIDAndGroupNameAndCode(int parentID, string groupName, string code)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@ParentID",parentID),
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
            };
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ConfigDeleteByParentIDAndGroupNameAndCode", parameters);

            return result;
        }
        public string DeleteMenuByID(int ID)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@ID",ID),                
            };
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ConfigDeleteMenuByID", parameters);

            return result;
        }
        public List<Config> GetByIDListToList(string IDList)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@IDList",IDList),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectByIDList", parameters);
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
        public List<Config> GetSQLWebsiteByGroupNameAndCodeAndActiveToList(string groupName, string code, bool active)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
                new SqlParameter("@Active",active),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectWebsiteByGroupNameAndCodeAndActive", parameters);
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
        public List<Config> GetSQLWebsiteByGroupNameAndCodeToList(string groupName, string code)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectWebsiteByGroupNameAndCode", parameters);
            list = SQLHelper.ToList<Config>(dt);
            //foreach (Config item in list)
            //{
            //    if (item.Active == false)
            //    {
            //        bool check = false;
            //        if (string.IsNullOrEmpty(item.URLFull))
            //        {
            //            check = true;
            //        }
            //        else
            //        {
            //            if (check == false)
            //            {
            //                check = item.URLFull.Contains(@"@");
            //            }
            //            if (check == false)
            //            {
            //                check = item.URLFull.Contains(@".htm");
            //            }
            //            if (check == false)
            //            {
            //                check = item.URLFull.Contains(@"sms");
            //            }
            //            if (check == false)
            //            {
            //                check = item.URLFull.Contains(@"lich-thi-dau");
            //            }
            //            if (check == false)
            //            {
            //                check = item.URLFull.Contains(@"maito");
            //            }
            //            if (check == false)
            //            {
            //                check = item.URLFull.Contains(@"mailto");
            //            }
            //            if (check == false)
            //            {
            //                check = item.URLFull.Contains(@"tel");
            //            }
            //            if (check == false)
            //            {
            //                check = item.URLFull.Contains(@"#");
            //            }
            //            if (check == false)
            //            {
            //                check = item.URLFull.Contains(@"?");
            //            }
            //            if (check == false)
            //            {
            //                check = item.URLFull.Contains(@"+");
            //            }
            //            if (check == false)
            //            {
            //                check = item.URLFull.Contains(@"tag");
            //            }
            //            if (check == false)
            //            {
            //                check = item.URLFull.Contains(@"category");
            //            }
            //            if (check == false)
            //            {
            //                check = item.URLFull.Contains(@"2020");
            //            }
            //            if (check == false)
            //            {
            //                check = item.URLFull.Contains(@"javascript");
            //            }
            //            if (check == false)
            //            {
            //                int length = item.URLFull.Split('/').Length;
            //                if (length > 3)
            //                {
            //                    for (int i = 3; i < length; i++)
            //                    {
            //                        string last = item.URLFull.Split('/')[i];
            //                        if (!string.IsNullOrEmpty(last))
            //                        {
            //                            check = true;
            //                            i = length;
            //                        }
            //                    }
            //                }
            //            }
            //            if (check == false)
            //            {
            //                check = !item.URLFull.Contains(@".vn/");
            //            }
            //            if (check == false)
            //            {
            //                check = !item.URLFull.Contains(@".net/");
            //            }
            //            if (check == false)
            //            {
            //                check = !item.URLFull.Contains(@".info/");
            //            }
            //            if (check == false)
            //            {
            //                check = !item.URLFull.Contains(@".com/");
            //            }
            //        }
            //        if (check == true)
            //        {
            //            DeleteSingleItem(item.ID);
            //        }
            //    }
            //}
            
            return list;
        }
        public List<Config> GetSQLWebsiteByGroupNameAndCodeAndActiveAndRowBeginAndRowEndToList(string groupName, string code, bool active, int rowBegin, int rowEnd)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
                new SqlParameter("@Active",active),
                new SqlParameter("@RowBegin",rowBegin),
                new SqlParameter("@RowEnd",rowEnd),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectWebsiteByGroupNameAndCodeAndActiveAndRowBeginAndRowEnd", parameters);
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
        public List<Config> GetSQLWebsiteByGroupNameAndCodeAndActiveAndIsMenuLeftToList(string groupName, string code, bool active, bool isMenuLeft)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
                new SqlParameter("@Active",active),
                new SqlParameter("@IsMenuLeft",isMenuLeft),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectWebsiteByGroupNameAndCodeAndActiveAndIsMenuLeft", parameters);
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
        public List<Config> GetSQLWebsiteByGroupNameAndCodeAndActiveAndIsMenuLeftAndRowBeginAndRowEndToList(string groupName, string code, bool active, bool isMenuLeft, int rowBegin, int rowEnd)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
                new SqlParameter("@Active",active),
                new SqlParameter("@IsMenuLeft",isMenuLeft),
                new SqlParameter("@RowBegin",rowBegin),
                new SqlParameter("@RowEnd",rowEnd),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectWebsiteByGroupNameAndCodeAndActiveAndIsMenuLeftAndRowBeginAndRowEnd", parameters);
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
        public List<Config> GetSQLByGroupNameAndCodeToList(string groupName, string code)
        {
            List<Config> list = new List<Config>();
            SqlParameter[] parameters =
            {
                new SqlParameter("@GroupName",groupName),
                new SqlParameter("@Code",code),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectByGroupNameAndCode", parameters);
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
        public List<Config> GetWebsiteToList()
        {
            List<Config> list = new List<Config>();
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ConfigSelectWebsite");
            list = SQLHelper.ToList<Config>(dt);
            return list;
        }
        public async Task<string> AsyncInsertSingleItem(Config config)
        {

            SqlParameter[] parameters =
            {
new SqlParameter("@ID",config.ID),
new SqlParameter("@UserCreated",config.UserCreated),
new SqlParameter("@DateCreated",config.DateCreated),
new SqlParameter("@UserUpdated",config.UserUpdated),
new SqlParameter("@DateUpdated",config.DateUpdated),
new SqlParameter("@ParentID",config.ParentID),
new SqlParameter("@Note",config.Note),
new SqlParameter("@Active",config.Active),
new SqlParameter("@GroupName",config.GroupName),
new SqlParameter("@Code",config.Code),
new SqlParameter("@CodeName",config.CodeName),
new SqlParameter("@CodeNameSub",config.CodeNameSub),
new SqlParameter("@SortOrder",config.SortOrder),
new SqlParameter("@Icon",config.Icon),
new SqlParameter("@Controller",config.Controller),
new SqlParameter("@Action",config.Action),
new SqlParameter("@URLFull",config.URLFull),
new SqlParameter("@URLSub",config.URLSub),
new SqlParameter("@Title",config.Title),
new SqlParameter("@IsMenuLeft",config.IsMenuLeft),
new SqlParameter("@BlackWhite",config.BlackWhite),
new SqlParameter("@Color",config.Color),
new SqlParameter("@CountryID",config.CountryID),
new SqlParameter("@LanguageID",config.LanguageID),
new SqlParameter("@FrequencyID",config.FrequencyID),
new SqlParameter("@ColorTypeID",config.ColorTypeID),
new SqlParameter("@IndustryID",config.IndustryID),
new SqlParameter("@TierID",config.TierID),
};
            string result = await SQLHelper.ExecuteNonQueryAsync(AppGlobal.ConectionString, "sp_ConfigInsertSingleItem", parameters);
            return result;
        }
        public string UpdateSingleItem001(Config config)
        {

            SqlParameter[] parameters =
            {
new SqlParameter("@ID",config.ID),
new SqlParameter("@URLFull",config.URLFull),
new SqlParameter("@Title",config.Title),
new SqlParameter("@IsMenuLeft",config.IsMenuLeft),
new SqlParameter("@Color",config.Color),
new SqlParameter("@BlackWhite",config.BlackWhite),
new SqlParameter("@SortOrder",config.SortOrder),
};
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ConfigUpdateSingleItem001", parameters);
            return result;
        }
        public string DeleteSingleItem(int ID)
        {

            SqlParameter[] parameters =
            {
                new SqlParameter("@ID",ID),
            };
            string result = SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_ConfigDeleteSingleItemByID", parameters);
            return result;
        }
        public List<Config> GetProductPermissionDistinctIndustryByEmployeeIDToList(int employeeID)
        {
            List<Config> list = new List<Config>();
            if (employeeID > 0)
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@EmployeeID",employeeID),
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductPermissionSelectDistinctIndustryByEmployeeID", parameters);
                list = SQLHelper.ToList<Config>(dt);
            }
            return list;
        }

    }
}
