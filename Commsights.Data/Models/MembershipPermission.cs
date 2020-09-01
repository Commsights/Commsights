using System;
using System.Collections.Generic;

namespace Commsights.Data.Models
{
    public partial class MembershipPermission : BaseModel
    {
        public int? MembershipID { get; set; }
        public int? MenuID { get; set; }        
        public bool? IsView { get; set; }
        public int? CategoryID { get; set; }
        public int? IndustryID { get; set; }
        public int? ProductID { get; set; }
        public int? CompanyID { get; set; }
        public string Product { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
