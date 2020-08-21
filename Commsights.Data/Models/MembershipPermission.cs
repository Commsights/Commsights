using System;
using System.Collections.Generic;

namespace Commsights.Data.Models
{
    public partial class MembershipPermission : BaseModel
    {
        public int? MembershipId { get; set; }
        public int? MenuId { get; set; }        
        public bool? IsView { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int? ProductId { get; set; }
        public string Product { get; set; }
    }
}
