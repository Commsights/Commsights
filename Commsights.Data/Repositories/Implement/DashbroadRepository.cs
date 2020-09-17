using Commsights.Data.DataTransferObject;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Commsights.Data.Repositories
{
    public class DashbroadRepository : IDashbroadRepository
    {
        public DashbroadRepository()
        {
        }
        public DashbroadDataTransfer Overview()
        {
            DashbroadDataTransfer model = new DashbroadDataTransfer();
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_DashbroadOverview");
            var list = SQLHelper.ToList<DashbroadDataTransfer>(dt);
            if (list.Count > 0)
            {
                model = list[0];
            }
            return model;
        }
        public List<DashbroadDataTransfer> CustomerAndArticleCompanyCountToList()
        {
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_DashbroadCustomerAndArticleCompanyCount");
            var list = SQLHelper.ToList<DashbroadDataTransfer>(dt);
            return list;
        }
        public List<DashbroadDataTransfer> CustomerAndArticleCompanyCountByDatePublishToList(DateTime datePublish)
        {
            List<DashbroadDataTransfer> list = new List<DashbroadDataTransfer>();
            if (datePublish.Year > 2019)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_DashbroadCustomerAndArticleCompanyCountByDatePublish", parameters);
                list = SQLHelper.ToList<DashbroadDataTransfer>(dt);
            }
            return list;
        }
        public List<DashbroadDataTransfer> CompanyAndArticleCompanyCountByDatePublishToList(DateTime datePublish)
        {
            List<DashbroadDataTransfer> list = new List<DashbroadDataTransfer>();
            if (datePublish.Year > 2019)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_DashbroadCompanyAndArticleCompanyCountByDatePublish", parameters);
                list = SQLHelper.ToList<DashbroadDataTransfer>(dt);
            }
            return list;
        }
        public List<DashbroadDataTransfer> IndustryAndArticleIndustryCountByDatePublishToList(DateTime datePublish)
        {
            List<DashbroadDataTransfer> list = new List<DashbroadDataTransfer>();
            if (datePublish.Year > 2019)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_DashbroadIndustryAndArticleIndustryCountByDatePublish", parameters);
                list = SQLHelper.ToList<DashbroadDataTransfer>(dt);
            }
            return list;
        }
        public List<DashbroadDataTransfer> IndustryCustomerAndArticleIndustryCountByDatePublishToList(DateTime datePublish)
        {
            List<DashbroadDataTransfer> list = new List<DashbroadDataTransfer>();
            if (datePublish.Year > 2019)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_DashbroadIndustryCustomerAndArticleIndustryCountByDatePublish", parameters);
                list = SQLHelper.ToList<DashbroadDataTransfer>(dt);
            }
            return list;
        }
        public List<DashbroadDataTransfer> ProductAndArticleProductCountByDatePublishToList(DateTime datePublish)
        {
            List<DashbroadDataTransfer> list = new List<DashbroadDataTransfer>();
            if (datePublish.Year > 2019)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_DashbroadProductAndArticleProductCountByDatePublish", parameters);
                list = SQLHelper.ToList<DashbroadDataTransfer>(dt);
            }
            return list;
        }
        public List<DashbroadDataTransfer> ProductCustomerAndArticleProductCountByDatePublishToList(DateTime datePublish)
        {
            List<DashbroadDataTransfer> list = new List<DashbroadDataTransfer>();
            if (datePublish.Year > 2019)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_DashbroadProductCustomerAndArticleProductCountByDatePublish", parameters);
                list = SQLHelper.ToList<DashbroadDataTransfer>(dt);
            }
            return list;
        }
        public List<DashbroadDataTransfer> CustomerAndArticleCountByDatePublishToList(DateTime datePublish)
        {
            List<DashbroadDataTransfer> list = new List<DashbroadDataTransfer>();
            if (datePublish.Year > 2019)
            {
                SqlParameter[] parameters =
                       {
                new SqlParameter("@DatePublish",datePublish)
                };
                DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_DashbroadCustomerAndArticleCountByDatePublish", parameters);
                list = SQLHelper.ToList<DashbroadDataTransfer>(dt);
            }
            return list;
        }
    }
}
