﻿using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IProductSearchPropertyRepository : IRepository<ProductSearchProperty>
    {
        public List<ProductSearchPropertyDataTransfer> GetDataTransferProductSearchByProductSearchIDToList(int productSearchID);
    }
}
