using System;
using System.Collections.Generic;

namespace Commsights.Data.Models
{
    public partial class Config : BaseModel
    {        
        public string GroupName { get; set; }
        public string Code { get; set; }
        public string CodeName { get; set; }
        public string CodeNameSub { get; set; }
        public int? SortOrder { get; set; }
        public string Icon { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Urlfull { get; set; }
        public string Urlsub { get; set; }
        public string Title { get; set; }
        public bool? IsMenuLeft { get; set; }
    }
}
