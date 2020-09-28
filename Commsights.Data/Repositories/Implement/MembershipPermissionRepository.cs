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
        public List<MembershipPermission> GetByMembershipIDAndIndustryIDAndCodeToList(int membershipID, int industryID, string code)
        {
            return _context.MembershipPermission.Where(item => item.MembershipID == membershipID && item.IndustryID == industryID && item.Code.Equals(code)).OrderBy(item => item.DateUpdated).ToList();
        }
        public List<MembershipPermission> GetDailyReportColumnByMembershipIDAndIndustryIDAndCodeAndActiveToList(int membershipID, int industryID, string code, bool active)
        {
            return _context.MembershipPermission.Where(item => item.MembershipID == membershipID && item.IndustryID == industryID && item.Code.Equals(code) && item.Active == active).OrderBy(item => item.SortOrder).ToList();
        }
        public List<MembershipPermission> GetByCodeToList(string code)
        {
            return _context.MembershipPermission.Where(item => item.Code.Equals(code)).OrderBy(item => item.ProductName).ToList();
        }
        public List<MembershipPermission> GetByProductCodeToList(string code)
        {
            return _context.MembershipPermission.Where(item => item.Code.Equals(code) && !string.IsNullOrEmpty(item.ProductName)).OrderBy(item => item.ProductName).ToList();
        }
        public MembershipPermission GetByProductName(string productName)
        {
            return _context.MembershipPermission.FirstOrDefault(item => item.ProductName.Equals(productName));
        }
        public MembershipPermission GetByMembershipIDAndAndCodeAndActive(int membershipID, string code, bool active)
        {
            MembershipPermission model = _context.MembershipPermission.FirstOrDefault(item => item.MembershipID == membershipID && item.Code.Equals(code) && item.Active == active);
            if (model == null)
            {
                model = _context.MembershipPermission.FirstOrDefault(item => item.MembershipID == membershipID && item.Code.Equals(code));
            }
            return model;
        }
        public List<MembershipPermissionDataTransfer> GetDataTransferMembershipBySegmentIDAndCodeToList(int segmentID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if (segmentID > 0)
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@SegmentID",segmentID),
                new SqlParameter("@Code",code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferMembershipBySegmentIDAndCode", parameters);
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
        public List<MembershipPermissionDataTransfer> GetDataTransferContactByMembershipIDAndCodeToList(int membershipID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if (membershipID > 0)
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@Code",code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferContactByMembershipIDAndCode", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].ReportType = new ModelTemplate();
                    list[i].ReportType.ID = list[i].CategoryID;
                    list[i].ReportType.TextName = list[i].ReportTypeName;
                }
            }
            return list;
        }
        public List<MembershipPermissionDataTransfer> GetDataTransferContactByMembershipIDAndIndustryIDAndCodeToList(int membershipID, int industryID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if ((membershipID > 0) && (industryID > 0) && (!string.IsNullOrEmpty(code)))
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@MembershipID",membershipID),
                 new SqlParameter("@IndustryID",industryID),
                new SqlParameter("@Code",code)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferContactByMembershipIDAndIndustryIDAndCode", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].ReportType = new ModelTemplate();
                    list[i].ReportType.ID = list[i].CategoryID;
                    list[i].ReportType.TextName = list[i].ReportTypeName;
                }
            }
            return list;
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
        public List<MembershipPermissionDataTransfer> GetDataTransferIndustryByMembershipIDAndCodeAndActiveToList(int membershipID, string code, bool active)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if (membershipID > 0)
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@Code",code),
                new SqlParameter("@Active",active)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferIndustryByMembershipIDAndCodeAndActive", parameters);
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
        public List<MembershipPermissionDataTransfer> GetDataTransferSegmentByMembershipIDAndCodeToList(int membershipID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if (membershipID > 0)
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@Code",code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferSegmentByMembershipIDAndCode", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Segment = new ModelTemplate();
                    list[i].Segment.ID = list[i].SegmentID;
                    list[i].Segment.TextName = list[i].SegmentName;
                }
            }
            return list;
        }
        public List<MembershipPermissionDataTransfer> GetDataTransferSegmentByMembershipIDAndIndustryIDAndCodeToList(int membershipID, int industryID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if ((membershipID > 0) && (industryID > 0) && (!string.IsNullOrEmpty(code)))
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@IndustryID",industryID),
                new SqlParameter("@Code",code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferSegmentByMembershipIDAndIndustryIDAndCode", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Segment = new ModelTemplate();
                    list[i].Segment.ID = list[i].SegmentID;
                    list[i].Segment.TextName = list[i].SegmentName;
                }
            }
            return list;
        }
        public List<MembershipPermissionDataTransfer> GetDataTransferSegmentByMembershipIDAndIndustryIDAndCode001ToList(int membershipID, int industryID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if ((membershipID > 0) && (industryID > 0) && (!string.IsNullOrEmpty(code)))
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@IndustryID",industryID),
                new SqlParameter("@Code",code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferSegmentByMembershipIDAndIndustryIDAndCode001", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Segment = new ModelTemplate();
                    list[i].Segment.ID = list[i].SegmentID;
                    list[i].Segment.TextName = list[i].SegmentName;
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
            }
            return list;
        }
        public List<MembershipPermissionDataTransfer> GetDataTransferDailyReportSectionByMembershipIDAndIndustryIDAndCodeToList(int membershipID, int industryID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if ((membershipID > 0) && (industryID > 0) && (!string.IsNullOrEmpty(code)))
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@IndustryID",industryID),
                new SqlParameter("@Code",code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferDailyReportSectionByMembershipIDAndIndustryIDAndCode", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Language = new ModelTemplate();
                    list[i].Language.ID = list[i].LanguageID;
                    list[i].Language.TextName = list[i].LanguageName;
                }
            }
            return list;
        }
        public List<MembershipPermissionDataTransfer> GetDataTransferDailyReportColumnByMembershipIDAndCodeToList(int membershipID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if (membershipID > 0)
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@Code",code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferDailyReportColumnByMembershipIDAndCode", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
            }
            return list;
        }
        public List<MembershipPermissionDataTransfer> GetDataTransferDailyReportColumnByMembershipIDAndIndustryIDAndCodeToList(int membershipID, int industryID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if ((membershipID > 0) && (industryID > 0) && (!string.IsNullOrEmpty(code)))
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@IndustryID",industryID),
                new SqlParameter("@Code",code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferDailyReportColumnByMembershipIDAndIndustryIDAndCode", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
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
                    list[i].Industry = new ModelTemplate();
                    list[i].Industry.ID = list[i].IndustryID;
                    list[i].Industry.TextName = list[i].IndustryName;
                    list[i].Segment = new ModelTemplate();
                    list[i].Segment.ID = list[i].SegmentID;
                    list[i].Segment.TextName = list[i].SegmentName;
                }
            }
            return list;
        }
        public List<MembershipPermissionDataTransfer> GetDataTransferCompanyByMembershipIDAndIndustryIDAndCodeToList(int membershipID, int industryID, string code)
        {
            List<MembershipPermissionDataTransfer> list = new List<MembershipPermissionDataTransfer>();
            if ((membershipID > 0) && (membershipID > 0) && (!string.IsNullOrEmpty(code)))
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@MembershipID",membershipID),
                new SqlParameter("@IndustryID",industryID),
                new SqlParameter("@Code",code)
            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipPermissionSelectDataTransferCompanyByMembershipIDAndIndustryIDAndCode", parameters);
                list = SQLHelper.ToList<MembershipPermissionDataTransfer>(dt);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Company = new ModelTemplate();
                    list[i].Company.ID = list[i].CompanyID;
                    list[i].Company.TextName = list[i].CompanyName;
                    list[i].Industry = new ModelTemplate();
                    list[i].Industry.ID = list[i].IndustryID;
                    list[i].Industry.TextName = list[i].IndustryName;
                    list[i].Segment = new ModelTemplate();
                    list[i].Segment.ID = list[i].SegmentID;
                    list[i].Segment.TextName = list[i].SegmentName;
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
        public void InitializationDailyReportSection(int membershipID, string code, int requestUserID)
        {
            List<MembershipPermission> listMembershipPermission = _context.MembershipPermission.Where(item => item.MembershipID == membershipID && item.Code.Equals(code)).ToList();
            _context.MembershipPermission.RemoveRange(listMembershipPermission);
            List<Config> listConfig = _context.Config.Where(item => item.Code.Equals(code)).ToList();
            listMembershipPermission = new List<MembershipPermission>();
            foreach (Config config in listConfig)
            {
                MembershipPermission model = new MembershipPermission();
                model.MembershipID = membershipID;
                model.Code = code;
                model.CategoryID = config.ID;
                model.Hour = AppGlobal.Hour;
                model.Minute = -1;
                model.SortOrder = -1;
                model.Active = false;
                model.Email = config.CodeName;
                model.Phone = config.Note;
                model.Initialization(InitType.Insert, requestUserID);
                listMembershipPermission.Add(model);
            }
            _context.MembershipPermission.AddRange(listMembershipPermission);
            _context.SaveChangesAsync();
        }
        public void InitializationDailyReportSectionByMembershipIDAndIndustryID(int membershipID, int industryID, string code, int requestUserID)
        {
            if ((membershipID > 0) && (industryID > 0) && (!string.IsNullOrEmpty(code)))
            {
                List<MembershipPermission> listMembershipPermission = _context.MembershipPermission.Where(item => item.MembershipID == membershipID && item.IndustryID == industryID && item.Code.Equals(code)).ToList();
                _context.MembershipPermission.RemoveRange(listMembershipPermission);
                List<Config> listConfig = _context.Config.Where(item => item.Code.Equals(code)).ToList();
                listMembershipPermission = new List<MembershipPermission>();
                foreach (Config config in listConfig)
                {
                    MembershipPermission model = new MembershipPermission();
                    model.LanguageID = AppGlobal.LanguageID;
                    model.MembershipID = membershipID;
                    model.IndustryID = industryID;
                    model.Code = code;
                    model.CategoryID = config.ID;
                    model.Hour = AppGlobal.Hour;
                    model.Minute = -1;
                    model.SortOrder = -1;
                    model.Active = false;
                    model.Email = config.CodeName;
                    model.Phone = config.Note;
                    model.Initialization(InitType.Insert, requestUserID);
                    listMembershipPermission.Add(model);
                }
                _context.MembershipPermission.AddRange(listMembershipPermission);
                _context.SaveChangesAsync();
            }
        }
        public void InitializationDailyReportColumn(int membershipID, string code, int requestUserID)
        {
            List<MembershipPermission> listMembershipPermission = _context.MembershipPermission.Where(item => item.MembershipID == membershipID && item.Code.Equals(code)).ToList();
            _context.MembershipPermission.RemoveRange(listMembershipPermission);
            List<Config> listConfig = _context.Config.Where(item => item.GroupName.Equals(AppGlobal.CRM) && item.Code.Equals(code)).ToList();
            listMembershipPermission = new List<MembershipPermission>();
            foreach (Config config in listConfig)
            {
                MembershipPermission model = new MembershipPermission();
                model.LanguageID = AppGlobal.LanguageID;
                model.MembershipID = membershipID;
                model.Code = code;
                model.SortOrder = 0;
                model.Active = false;
                model.CategoryID = config.ID;
                model.Email = config.CodeName;
                model.Phone = config.Note;
                model.Initialization(InitType.Insert, requestUserID);
                listMembershipPermission.Add(model);
            }
            _context.MembershipPermission.AddRange(listMembershipPermission);
            _context.SaveChangesAsync();
        }
        public void InitializationDailyReportColumnByMembershipIDAndIndustryID(int membershipID, int industryID, string code, int requestUserID)
        {
            if ((membershipID > 0) && (industryID > 0) && (!string.IsNullOrEmpty(code)))
            {
                List<MembershipPermission> listMembershipPermission = _context.MembershipPermission.Where(item => item.MembershipID == membershipID && item.IndustryID == industryID && item.Code.Equals(code)).ToList();
                _context.MembershipPermission.RemoveRange(listMembershipPermission);
                List<Config> listConfig = _context.Config.Where(item => item.GroupName.Equals(AppGlobal.CRM) && item.Code.Equals(code)).ToList();
                listMembershipPermission = new List<MembershipPermission>();
                foreach (Config config in listConfig)
                {
                    MembershipPermission model = new MembershipPermission();
                    model.MembershipID = membershipID;
                    model.IndustryID = industryID;
                    model.Code = code;
                    model.SortOrder = 0;
                    model.Active = false;
                    model.CategoryID = config.ID;
                    model.Email = config.CodeName;
                    model.Phone = config.Note;
                    model.Initialization(InitType.Insert, requestUserID);
                    listMembershipPermission.Add(model);
                }
                _context.MembershipPermission.AddRange(listMembershipPermission);
                _context.SaveChangesAsync();
            }
        }
        public void InitializationChannel(int membershipID, string code, int requestUserID)
        {
            List<MembershipPermission> listMembershipPermission = _context.MembershipPermission.Where(item => item.MembershipID == membershipID && item.Code.Equals(code)).ToList();
            _context.MembershipPermission.RemoveRange(listMembershipPermission);
            List<Config> listConfig = _context.Config.Where(item => item.GroupName.Equals(AppGlobal.CRM) && item.Code.Equals(code)).ToList();
            listMembershipPermission = new List<MembershipPermission>();
            foreach (Config config in listConfig)
            {
                MembershipPermission model = new MembershipPermission();
                model.MembershipID = membershipID;
                model.Code = code;
                model.SortOrder = 0;
                model.Active = false;
                model.CategoryID = config.ID;
                model.Email = config.CodeName;
                model.Phone = config.Note;
                model.Initialization(InitType.Insert, requestUserID);
                listMembershipPermission.Add(model);
            }
            _context.MembershipPermission.AddRange(listMembershipPermission);
            _context.SaveChangesAsync();
        }
        public void InitializationChannelByMembershipIDAndIndustryID(int membershipID, int industryID, string code, int requestUserID)
        {
            if ((membershipID > 0) && (industryID > 0) && (!string.IsNullOrEmpty(code)))
            {
                List<MembershipPermission> listMembershipPermission = _context.MembershipPermission.Where(item => item.MembershipID == membershipID && item.IndustryID == industryID && item.Code.Equals(code)).ToList();
                _context.MembershipPermission.RemoveRange(listMembershipPermission);
                List<Config> listConfig = _context.Config.Where(item => item.GroupName.Equals(AppGlobal.CRM) && item.Code.Equals(code)).ToList();
                listMembershipPermission = new List<MembershipPermission>();
                foreach (Config config in listConfig)
                {
                    MembershipPermission model = new MembershipPermission();
                    model.MembershipID = membershipID;
                    model.IndustryID = industryID;
                    model.Code = code;
                    model.SortOrder = 0;
                    model.Active = false;
                    model.CategoryID = config.ID;
                    model.FullName = config.CodeName;
                    model.Email = config.CodeName;
                    model.Phone = config.Note;
                    model.Initialization(InitType.Insert, requestUserID);
                    listMembershipPermission.Add(model);
                }
                _context.MembershipPermission.AddRange(listMembershipPermission);
                _context.SaveChangesAsync();
            }
        }
    }
}
