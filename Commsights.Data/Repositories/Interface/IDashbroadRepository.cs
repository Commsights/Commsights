using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IDashbroadRepository
    {
        public DashbroadDataTransfer Overview();
        public List<DashbroadDataTransfer> CustomerAndArticleCompanyCountToList();
        public List<DashbroadDataTransfer> CustomerAndArticleCompanyCountByDatePublishToList(DateTime datePublish);
        public List<DashbroadDataTransfer> CompanyAndArticleCompanyCountByDatePublishToList(DateTime datePublish);
        public List<DashbroadDataTransfer> IndustryAndArticleIndustryCountByDatePublishToList(DateTime datePublish);
        public List<DashbroadDataTransfer> IndustryCustomerAndArticleIndustryCountByDatePublishToList(DateTime datePublish);
        public List<DashbroadDataTransfer> ProductAndArticleProductCountByDatePublishToList(DateTime datePublish);
        public List<DashbroadDataTransfer> ProductCustomerAndArticleProductCountByDatePublishToList(DateTime datePublish);
        public List<DashbroadDataTransfer> CustomerAndArticleCountByDatePublishToList(DateTime datePublish);
    }
}
