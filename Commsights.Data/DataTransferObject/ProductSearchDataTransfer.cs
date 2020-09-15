using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ProductSearchDataTransfer : ProductSearch
    {       
        public string CompanyName { get; set; }       
        public ModelTemplate Company { get; set; }        
    }
}
