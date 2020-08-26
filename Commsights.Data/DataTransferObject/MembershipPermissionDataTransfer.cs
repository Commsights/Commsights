using System;
using System.Collections.Generic;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class MembershipPermissionDataTransfer : MembershipPermission
    {
        public string BrandName { get; set; }
        public string MembershipName { get; set; }
        public ModelTemplate Brand { get; set; }
        public ModelTemplate Membership { get; set; }
    }
}
