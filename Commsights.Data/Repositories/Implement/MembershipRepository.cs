﻿using Commsights.Data.DataTransferObject;
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
    public class MembershipRepository : Repository<Membership>, IMembershipRepository
    {
        private readonly CommsightsContext _context;

        public MembershipRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public Membership GetByCodeAndFullName(string code, string fullName)
        {
            Membership model = null;
            MembershipPermission membershipPermission = _context.MembershipPermission.FirstOrDefault(item => item.Code.Equals(code) && item.FullName.Equals(fullName));
            if (membershipPermission != null)
            {
                model = _context.Membership.FirstOrDefault(item => item.ID == membershipPermission.MembershipID);
            }
            return model;
        }
        public string ReplaceCompanyIDToCustomerID(int companyID, int customerID)
        {

            SqlParameter[] parameters =
                  {
                new SqlParameter("@CompanyID",companyID),
                new SqlParameter("@CustomerID",customerID),
            };
            return SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_MembershipReplaceCompanyIDToCustomerID", parameters);
        }
        public string ReplaceCompanyIDSourceToCompanyIDReplace(int companyIDSource, int companyIDReplace)
        {

            SqlParameter[] parameters =
                  {
                new SqlParameter("@CompanyIDSource",companyIDSource),
                new SqlParameter("@CompanyIDReplace",companyIDReplace),
            };
            return SQLHelper.ExecuteNonQuery(AppGlobal.ConectionString, "sp_MembershipReplaceCompanyIDSourceToCompanyIDReplace", parameters);
        }
        public List<Membership> GetByIndustryIDToList(int industryID)
        {
            List<Membership> list = new List<Membership>();
            if (industryID > 0)
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@IndustryID",industryID)

            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipSelectByIndustryID", parameters);
                list = SQLHelper.ToList<Membership>(dt);
            }
            return list;
        }
        public List<Membership> GetByIndustryIDWithIDAndAccountToList(int industryID)
        {
            List<Membership> list = new List<Membership>();
            if (industryID > 0)
            {
                SqlParameter[] parameters =
                      {
                new SqlParameter("@IndustryID",industryID)

            };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipSelectByIndustryIDWithIDAndAccount", parameters);
                list = SQLHelper.ToList<Membership>(dt);
            }
            return list;
        }
        public List<Membership> GetByIndustryIDAndParrentIDToList(int industryID, int parentID)
        {
            List<Membership> list = new List<Membership>();
            if ((industryID > 0) && (parentID > 0))
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@IndustryID",industryID),
                new SqlParameter("@ParentID",parentID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipSelectByIndustryIDAndParrentID", parameters);
                list = SQLHelper.ToList<Membership>(dt);
            }
            return list;
        }
        public List<MembershipDataTransfer> GetDataTransferByParentIDToList(int parentID)
        {
            List<MembershipDataTransfer> list = new List<MembershipDataTransfer>();
            if (parentID > 0)
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@ParentID",parentID)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipSelectDataTransferByParentID", parameters);
                list = SQLHelper.ToList<MembershipDataTransfer>(dt);
            }
            return list;
        }
        public List<MembershipDataTransfer> GetAllCompanyToList()
        {
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_MembershipSelectAllCompany");
            return SQLHelper.ToList<MembershipDataTransfer>(dt);
        }
        public List<Membership> GetByCompanyToList()
        {
            return _context.Membership.Where(item => (item.ParentID == AppGlobal.ParentIDCustomer || item.ParentID == AppGlobal.ParentIDCompetitor) && (item.Active == true)).OrderBy(item => item.Account).ToList();
        }
        public List<Membership> GetByCompanyFullToList()
        {
            return _context.Membership.Where(item => (item.ParentID == AppGlobal.ParentIDCustomer || item.ParentID == AppGlobal.ParentIDCompetitor)).OrderBy(item => item.Account).ToList();
        }
        public List<Membership> GetByCompetitorFullToList()
        {
            return _context.Membership.Where(item => (item.ParentID == AppGlobal.ParentIDCompetitor)).OrderBy(item => item.Account).ToList();
        }
        public List<Membership> GetCustomerToList()
        {
            return _context.Membership.Where(item => (item.ParentID == AppGlobal.ParentIDCustomer) && (item.Active == true)).OrderBy(item => item.Account).ToList();
        }
        public List<Membership> GetCustomerFullToList()
        {
            return _context.Membership.Where(item => (item.ParentID == AppGlobal.ParentIDCustomer)).OrderBy(item => item.Account).ToList();
        }
        public List<Membership> GetCompetitorToList()
        {
            return _context.Membership.Where(item => (item.ParentID == AppGlobal.ParentIDCompetitor) && (item.Active == true)).OrderBy(item => item.Account).ToList();
        }
        public List<Membership> GetEmployeeToList()
        {
            return _context.Membership.Where(item => item.ParentID == AppGlobal.ParentIDEmployee).OrderBy(item => item.Account).ToList();
        }
        public bool IsExistAccount(string account)
        {
            var membership = _context.Membership.FirstOrDefault(user => user.Account.Equals(account));
            if (membership != null)
            {
                return true;
            }
            return false;
        }
        public bool IsExistFullName(string fullName)
        {
            var membership = _context.Membership.FirstOrDefault(user => user.FullName.Equals(fullName));
            if (membership != null)
            {
                return true;
            }
            return false;
        }
        public bool IsExistEmail(string email)
        {
            var membership = _context.Membership.FirstOrDefault(user => user.Email.Equals(email));
            if (membership != null)
            {
                return true;
            }
            return false;
        }
        public bool IsExistPhone(string phone)
        {
            var membership = _context.Membership.FirstOrDefault(user => user.Phone.Equals(phone));
            if (membership != null)
            {
                return true;
            }
            return false;
        }
        public bool IsLoginByAccount(string account, string password)
        {
            var membership = _context.Membership.FirstOrDefault(user => user.Email.Equals(account));
            if (membership == null)
            {
                membership = _context.Membership.FirstOrDefault(user => user.Phone.Equals(account));
            }
            if (membership != null)
            {
                if (SecurityHelper.Decrypt(membership.Guicode, membership.Password).Equals(password))
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsLoginByID(int ID, string password)
        {
            var membership = _context.Membership.FirstOrDefault(user => user.ID == ID);
            if (membership != null)
            {
                if (SecurityHelper.Decrypt(membership.Guicode, membership.Password).Equals(password))
                {
                    return true;
                }
            }
            return false;
        }
        public Membership GetByAccount(string account)
        {
            Membership membership = new Membership();
            if (!string.IsNullOrEmpty(account))
            {
                membership = _context.Membership.FirstOrDefault(item => item.Account.Contains(account));
                if (membership == null)
                {
                    membership = _context.Membership.FirstOrDefault(item => item.FullName.Contains(account));
                }
                if (membership == null)
                {
                    membership = _context.Membership.FirstOrDefault(item => item.Website.Contains(account));
                }
                if (membership == null)
                {
                    membership = _context.Membership.FirstOrDefault(item => item.ShortName.Contains(account));
                }
                if (membership == null)
                {
                    membership = _context.Membership.FirstOrDefault(item => item.EnglishName.Contains(account));
                }
                if (membership == null)
                {
                    MembershipPermission membershipPermission = _context.MembershipPermission.FirstOrDefault(item => item.Code.Equals(AppGlobal.CompanyName) && item.FullName.Equals(account));
                    if (membershipPermission == null)
                    {
                        membershipPermission = _context.MembershipPermission.FirstOrDefault(item => item.Code.Equals(AppGlobal.CompanyName) && item.FullName.Contains(account));
                    }
                    if (membershipPermission != null)
                    {
                        membership = _context.Membership.FirstOrDefault(item => item.ID == membershipPermission.MembershipID);
                    }
                }
            }
            return membership;
        }
        public int IsByAccount(string account)
        {
            int ID = 0;
            if (!string.IsNullOrEmpty(account))
            {
                ID = _context.Membership.FirstOrDefault(item => item.Account.Equals(account)).ID;
            }
            return ID;
        }
    }
}
