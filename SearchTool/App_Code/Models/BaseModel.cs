using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Commsights.Data.Models
{
    public class BaseModel
    {
        public int ID { get; set; }
        public int? UserCreated { get; set; }       
        public DateTime? DateCreated { get; set; }
        public int? UserUpdated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int? ParentID { get; set; }
        public string Note { get; set; }
        public bool? Active { get; set; }
    }
}
