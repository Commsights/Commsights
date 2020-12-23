﻿using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public interface ICodeDataRepository
    {
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDAndEmployeeIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID, int employeeID);
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID);
        public List<Config> GetCategorySubByCategoryMainToList(string categoryMain);
        public string GetCompanyNameByTitle(string title);
        public string GetCompanyNameByURLCode(string uRLCode);
        public string GetProductNameByTitle(string title);
        public string GetProductNameByURLCode(string uRLCode);        
        public List<CodeDataReport> GetReportByDatePublishBeginAndDatePublishEndToList(DateTime datePublishBegin, DateTime datePublishEnd);
        public List<Membership> GetReportSelectByDatePublishBeginAndDatePublishEnd001ToList(DateTime datePublishBegin, DateTime datePublishEnd);
        public List<CodeData> GetReportByDateUpdatedAndHourAndIndustryIDAndCompanyNameAndIsCodingAndIsAnalysisToList(DateTime dateUpdated, int hour, int industryID, string companyName, bool isCoding, bool isAnalysis);
    }
}
