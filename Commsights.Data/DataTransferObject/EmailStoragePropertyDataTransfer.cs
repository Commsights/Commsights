using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class EmailStoragePropertyDataTransfer : EmailStorageProperty
    {        
        public string CompanyName { get; set; }
        public string CategoryName { get; set; }
        public string IndustryName { get; set; }
    }
}
