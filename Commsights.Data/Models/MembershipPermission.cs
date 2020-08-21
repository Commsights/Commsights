using System;
using System.Collections.Generic;

namespace Commsights.Data.Models
{
    public partial class MembershipPermission : BaseModel
    {      
        public int? MembershipId { get; set; }
        public int? MenuId { get; set; }
        public int? CustomerId { get; set; }
        public bool? IsView { get; set; }
    }
}
