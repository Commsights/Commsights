using System;
using System.Collections.Generic;

namespace Commsights.Data.Models
{
    public partial class ProductPermission : BaseModel
    {

        public string Code { get; set; }
        public int? MembershipID { get; set; }
        public int? EmployeeID { get; set; }
        public int? IndustryID { get; set; }
        public int? RowBegin { get; set; }
        public int? RowEnd { get; set; }
        public int? SortOrder { get; set; }
        public int? RowPercent { get; set; }
        public DateTime? DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
    }
}
