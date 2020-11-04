using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commsights.Data.Models
{
    public partial class ReportMonthlyProperty : BaseModel
    {
        public int? ProductID { get; set; }
        public int? ProductPropertyID { get; set; }
    }
}
