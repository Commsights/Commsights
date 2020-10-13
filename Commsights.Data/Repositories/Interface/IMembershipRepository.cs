using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IMembershipRepository : IRepository<Membership>
    {
        public string ReplaceCompanyIDSourceToCompanyIDReplace(int companyIDSource, int companyIDReplace);
        public string ReplaceCompanyIDToCustomerID(int companyID, int customerID);
        public int IsByAccount(string account);
        public bool IsLoginByAccount(string account, string password);
        public bool IsLoginByID(int ID, string password);
        public bool IsExistPhone(string phone);
        public bool IsExistEmail(string email);
        public bool IsExistAccount(string account);
        public bool IsExistFullName(string fullName);
        public List<Membership> GetByCompanyToList();
        public List<Membership> GetByCompanyFullToList();        
        public List<Membership> GetByIndustryIDToList(int industryID);
        public List<Membership> GetCustomerToList();
        public List<Membership> GetCompetitorToList();
        public List<Membership> GetByCompetitorFullToList();
        public List<Membership> GetEmployeeToList();
        public Membership GetByAccount(string account);
        public List<Membership> GetByIndustryIDAndParrentIDToList(int industryID, int parentID);
        public List<Membership> GetByIndustryIDWithIDAndAccountToList(int industryID);
        public List<MembershipDataTransfer> GetAllCompanyToList();
        public List<MembershipDataTransfer> GetDataTransferByParentIDToList(int parentID);
    }
}
