using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IProductPropertyRepository : IRepository<ProductProperty>
    {
        public string UpdateItemsWithParentIDIsZero();
        public bool IsExistByGUICodeAndCodeAndCompanyID(string gUICode, string code, int companyID);
        public bool IsExistByGUICodeAndCodeAndIndustryID(string gUICode, string code, int industryID);
        public bool IsExistByGUICodeAndCodeAndIndustryIDAndSegmentID(string gUICode, string code, int industryID, int segmentID);
        public bool IsExistByGUICodeAndCodeAndProductID(string gUICode, string code, int productID);
        public List<ProductPropertyPropertyDataTransfer> GetDataTransferCompanyByParentIDToList(int parentID);
        public List<ProductPropertyPropertyDataTransfer> GetDataTransferIndustryByParentIDToList(int parentID);
        public List<ProductPropertyPropertyDataTransfer> GetDataTransferProductByParentIDToList(int parentID);
    }
}
