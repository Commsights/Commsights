using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IMembershipRepository : IRepository<Membership>
    {
        public int IsByAccount(string account);
        public bool IsLoginByAccount(string account, string password);
        public bool IsLoginByID(int ID, string password);
        public bool IsExistPhone(string phone);
        public bool IsExistEmail(string email);
        public bool IsExistAccount(string account);
        public bool IsExistFullName(string fullName);
        public List<Membership> GetByCompanyToList();
        public List<Membership> GetByIndustryIDToList(int industryID);
        public List<Membership> GetCustomerToList();
        public List<Membership> GetCompetitorToList();
        public List<Membership> GetEmployeeToList();
        public Membership GetByAccount(string account);
    }
}
