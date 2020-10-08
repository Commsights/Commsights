using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IProductPropertyRepository : IRepository<ProductProperty>
    {
        public bool IsExist(ProductProperty model);
        public string InsertItemsByID(int ID);
        public string UpdateItemsWithParentIDIsZero();
        public bool IsExistByProductIDAndCodeAndCompanyID(int productID, string code, int companyID);
        public bool IsExistByGUICodeAndCodeAndCompanyID(string gUICode, string code, int companyID);
        public bool IsExistByGUICodeAndCodeAndIndustryID(string gUICode, string code, int industryID);
        public bool IsExistByGUICodeAndCodeAndIndustryIDAndSegmentID(string gUICode, string code, int industryID, int segmentID);
        public bool IsExistByGUICodeAndCodeAndProductID(string gUICode, string code, int productID);
        public List<ProductPropertyDataTransfer> GetDataTransferCompanyByParentIDToList(int parentID);
        public List<ProductPropertyDataTransfer> GetDataTransferIndustryByParentIDToList(int parentID);
        public List<ProductPropertyDataTransfer> GetDataTransferProductByParentIDToList(int parentID);
        public List<ProductProperty> GetByParentIDAndCodeToList(int parentID, string code);
        public ProductProperty GetByProductIDAndCodeAndCompanyID(int productID, string code, int companyID);
        public string Initialization();
        public ProductProperty GetByID001(int ID);
    }
}
