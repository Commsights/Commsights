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
        public List<CodeData> GetByDatePublishBeginAndDatePublishEndAndIndustryIDToList(DateTime datePublishBegin, DateTime datePublishEnd, int industryID);
        public List<Config> GetCategorySubByCategoryMainToList(string categoryMain);
        public string GetCompanyNameByTitle(string title);
        public string GetCompanyNameByURLCode(string uRLCode);
        public string GetProductNameByTitle(string title);
        public string GetProductNameByURLCode(string uRLCode);
    }
}
