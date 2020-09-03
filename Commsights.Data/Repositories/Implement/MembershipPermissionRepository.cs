using Commsights.Data.DataTransferObject;
using Commsights.Data.Enum;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
namespace Commsights.Data.Repositories
{
    public class MembershipPermissionRepository : Repository<MembershipPermission>, IMembershipPermissionRepository
    {
        private readonly CommsightsContext _context;

        public MembershipPermissionRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public List<MembershipPermission> GetByMembershipIDToList(int membershipID)
        {
            return _context.MembershipPermission.Where(item => item.MembershipID == membershipID).OrderBy(item => item.ID).ToList();
        }
        public List<MembershipPermission> GetIndustryIDAndCodeToList(int industryID, string code)
        {
            return _context.MembershipPermission.Where(item => item.IndustryID == industryID && item.Code.Equals(code)).OrderBy(item => item.ID).ToList();
        }
        public List<MembershipPermission> GetByMembershipIDAndCodeToList(int membershipID, string code)
        {
            return _context.MembershipPermission.Where(item => item.MembershipID == membershipID && item.Code.Equals(code)).OrderBy(item => item.DateUpdated).ToList();
        }
        public List<MembershipPermissionDataTransfer> GetDataTransferIndustryByMembershipIDAndCodeToList(int membershipID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if (membershipID > 0)
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@Code",code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferIndustryByMembershipIDAndCode", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Industry = new ModelTemplate();
                    list[i].Industry.ID = list[i].IndustryID;
                    list[i].Industry.TextName = list[i].IndustryName;
                }
            }
            return list;
        }
        public List<MembershipPermissionDataTransfer> GetDataTransferDailyReportSectionByMembershipIDAndCodeToList(int membershipID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if (membershipID > 0)
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@Code",code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferDailyReportSectionByMembershipIDAndCode", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].DailyReportSection = new ModelTemplate();
                    list[i].DailyReportSection.ID = list[i].CategoryID;
                    list[i].DailyReportSection.TextName = list[i].DailyReportSectionName;
                }
            }
            return list;
        }
        public List<MembershipPermissionDataTransfer> GetDataTransferMembershipByIndustryIDAndCodeToList(int industryID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if (industryID > 0)
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@IndustryID", industryID),
                new SqlParameter("@Code", code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferMembershipByIndustryIDAndCode", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Membership = new ModelTemplate();
                    list[i].Membership.ID = list[i].MembershipID;
                    list[i].Membership.TextName = list[i].MembershipName;
                }
            }
            return list;
        }
        public List<MembershipPermissionDataTransfer> GetDataTransferCompanyByMembershipIDAndCodeToList(int membershipID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if (membershipID > 0)
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@Code",code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferCompanyByMembershipIDAndCode", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Company = new ModelTemplate();
                    list[i].Company.ID = list[i].CompanyID;
                    list[i].Company.TextName = list[i].CompanyName;
                }
            }
            return list;
        }
        public List<MembershipPermissionDataTransfer> GetDataTransferIndustryByParentIDToList(int parentID)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if (parentID > 0)
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@ParentID",parentID)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferIndustryByParentID", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Industry = new ModelTemplate();
                    list[i].Industry.ID = list[i].IndustryID;
                    list[i].Industry.TextName = list[i].IndustryName;
                    list[i].IndustryCompare = new ModelTemplate();
                    list[i].IndustryCompare.ID = list[i].IndustryCompareID;
                    list[i].IndustryCompare.TextName = list[i].IndustryCompareName;
                }
            }
            return list;
        }

        public List<MembershipPermissionDataTransfer> GetDataTransferIndustryByParentIDAndCodeToList(int parentID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if (parentID > 0)
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@ParentID",parentID),
                new SqlParameter("@Code",code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferIndustryByParentIDAndCode", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Industry001 = new ModelTemplateIndustry();
                    list[i].Industry001.IndustryID = list[i].IndustryID;
                    list[i].Industry001.TextName = list[i].IndustryName;
                    list[i].IndustryCompare001 = new ModelTemplateIndustry();
                    list[i].IndustryCompare001.IndustryID = list[i].IndustryCompareID;
                    list[i].IndustryCompare001.TextName = list[i].IndustryCompareName;
                }
            }
            return list;
        }
        public List<MembershipPermissionDataTransfer> GetDataTransferProductByParentIDAndCodeToList(int parentID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if (parentID > 0)
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@ParentID",parentID),
                new SqlParameter("@Code",code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferProductByParentIDAndCode", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Product = new ModelTemplate();
                    list[i].Product.ID = list[i].ProductID;
                    list[i].Product.TextName = list[i].ProductSourceName;
                    list[i].ProductCompare = new ModelTemplate();
                    list[i].ProductCompare.ID = list[i].ProductCompareID;
                    list[i].ProductCompare.TextName = list[i].ProductCompareName;
                }
            }
            return list;
        }
        public List<MembershipPermission> GetByMembershipIDAndIndustryIDAndCodeToList(int membershipID, int industryID, string code)
        {
            List<MembershipPermission> list = new List<MembershipPermission>();
            if (membershipID > 0)
            {
                list = _context.MembershipPermission.Where(item => item.MembershipID == membershipID && item.IndustryID == industryID && item.Code.Equals(code)).OrderBy(item => item.DateUpdated).ToList();
            }
            return list;
        }
        public void InitializationMenuPermission(int membershipID, int requestUserID)
        {
            List<MembershipPermission> list = _context.MembershipPermission.Where(item => item.MembershipID == membershipID && item.MenuID > 0).ToList();
            _context.MembershipPermission.RemoveRange(list);
            List<Config> listMenu = _context.Config.Where(item => item.GroupName.Equals(AppGlobal.CRM) && item.Code.Equals(AppGlobal.Menu)).OrderBy(item => item.SortOrder).ToList();
            foreach (Config menu in listMenu)
            {
                MembershipPermission membershipPermission = new MembershipPermission();
                membershipPermission.MembershipID = membershipID;
                membershipPermission.MenuID = menu.ID;
                membershipPermission.IsView = false;
                membershipPermission.Initialization(InitType.Insert, requestUserID);
                _context.MembershipPermission.Add(membershipPermission);
            }
            _context.SaveChanges();
        }
        public void SaveAllMenuPermission(int membershipID, bool isAll, int requestUserID)
        {
            List<MembershipPermission> list = _context.MembershipPermission.Where(item => item.MembershipID == membershipID && item.MenuID > 0).ToList();
            foreach (MembershipPermission membershipPermission in list)
            {
                membershipPermission.IsView = isAll;
                membershipPermission.Initialization(InitType.Update, requestUserID);
                _context.MembershipPermission.Update(membershipPermission);
            }
            _context.SaveChanges();
        }
        public bool IsExistByMembershipIDAndBrandID(int membershipID, int industryID)
        {
            var membershipPermission = _context.MembershipPermission.FirstOrDefault(item => item.MembershipID == membershipID && item.IndustryID == industryID);
            if (membershipPermission != null)
            {
                return true;
            }
            return false;
        }
    }
}
