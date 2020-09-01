﻿using System;
using System.Collections.Generic;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class MembershipPermissionDataTransfer : MembershipPermission
    {
        public string IndustryName { get; set; }
        public string MembershipName { get; set; }
        public string CompanyName { get; set; }        
        public string IndustryCompetitorName { get; set; }
        public string ProductCustomerName { get; set; }
        public string ProductCompetitorName { get; set; }
        public ModelTemplate Industry { get; set; }
        public ModelTemplate Membership { get; set; }
        public ModelTemplate Company { get; set; }        
        public ModelTemplate IndustryCompetitor { get; set; }
        public ModelTemplate ProductCustomer { get; set; }
        public ModelTemplate ProductCompetitor { get; set; }
    }
}
