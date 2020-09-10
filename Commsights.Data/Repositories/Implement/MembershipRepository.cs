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
        public List<Membership> GetByCompanyToList()
        {
            return _context.Membership.Where(item => (item.ParentID == AppGlobal.ParentIDCustomer || item.ParentID == AppGlobal.ParentIDCompetitor) && (item.Active == true)).OrderBy(item => item.Account).ToList();
        }
        public List<Membership> GetCustomerToList()
        {
            return _context.Membership.Where(item => (item.ParentID == AppGlobal.ParentIDCustomer) && (item.Active == true)).OrderBy(item => item.Account).ToList();
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
                membership = _context.Membership.FirstOrDefault(item => item.Account.Equals(account));
            }
            return membership;
        }
    }
}
