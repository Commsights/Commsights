using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commsights.Data.Models
{
    public partial class EmailStorage : BaseModel
    {
        public int? CategoryID { get; set; }
        public int? IndustryID { get; set; }
        public int? CompanyID { get; set; }
        public int? SMTPPort { get; set; }
        public string SMTPServer { get; set; }
        public string EmailFrom { get; set; }
        public string Password { get; set; }
        public string Display { get; set; }
        public string Subject { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public string EmailBody { get; set; }
        public DateTime? DateSend { get; set; }
        public DateTime? DateRead { get; set; }
    }
}
