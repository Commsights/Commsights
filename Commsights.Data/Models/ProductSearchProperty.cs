using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commsights.Data.Models
{
    public partial class ProductSearchProperty : BaseModel
    {
        public int? ProductSearchID { get; set; }
        public int? ProductID { get; set; }
        public int? ArticleTypeID { get; set; }
        public int? CompanyID { get; set; }
        public int? AssessID { get; set; }        
    }
}
