using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class EmailStorageDataTransfer : EmailStorage
    {        
        public string CompanyName { get; set; }        
    }
}
