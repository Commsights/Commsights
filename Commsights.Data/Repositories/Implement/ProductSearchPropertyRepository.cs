﻿using Commsights.Data.DataTransferObject;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public class ProductSearchPropertyRepository : Repository<ProductSearchProperty>, IProductSearchPropertyRepository
    {
        private readonly CommsightsContext _context;
        public ProductSearchPropertyRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public List<ProductSearchPropertyDataTransfer> GetDataTransferProductSearchByProductSearchIDToList(int productSearchID)
        {
            List<ProductSearchPropertyDataTransfer> listProductSearchPropertyDataTransfer = new List<ProductSearchPropertyDataTransfer>();
            SqlParameter[] parameters =
                       {
                new SqlParameter("@ProductSearchID",productSearchID),
            };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_ProductSearchPropertySelectProductSearchByProductSearchID", parameters);
            listProductSearchPropertyDataTransfer = SQLHelper.ToList<ProductSearchPropertyDataTransfer>(dt);
            foreach (var item in listProductSearchPropertyDataTransfer)
            {
                item.ArticleType = new ModelTemplate();
                item.ArticleType.ID = item.ArticleTypeID;
                item.ArticleType.TextName = item.ArticleTypeName;
            }
            return listProductSearchPropertyDataTransfer;
        }
    }
}
