using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commsights.Data.Models
{
    public partial class EmailStorageProperty : BaseModel
    {
        public int? CategoryID { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
    }
}
