using System;
using System.Collections.Generic;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ProductPermissionDataTransfer : ProductPermission
    {
        public string EmployeeName { get; set; }
        public ModelTemplate Employee { get; set; }
    }
}
