using System;
using System.Collections.Generic;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class MembershipDataTransfer : Membership
    {
        public string TransferName { get; set; }
        
    }
}
