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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DateCreated { get; set; }
        public int? UserUpdated { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? DateUpdated { get; set; }
        public int? ParentID { get; set; }
        public string Note { get; set; }
        public bool? Active { get; set; }
    }
}
