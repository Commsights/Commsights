using Commsights.Data.Helpers;
using System;
using System.Collections.Generic;

namespace Commsights.Data.Models
{
    public partial class MembershipAccessHistory : BaseModel
    {
        public DateTime? DateTrack { get; set; }
        public int? MembershipId { get; set; }
        public string URLFull { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string QueryString { get; set; }
    }
}
