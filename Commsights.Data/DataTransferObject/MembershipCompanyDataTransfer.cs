using System;
using System.Collections.Generic;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class MembershipCompanyDataTransfer
    {
        public int ID { get; set; }
        public bool? Active { get; set; }
        public string Account { get; set; }
        public string FullName { get; set; }

    }
}
