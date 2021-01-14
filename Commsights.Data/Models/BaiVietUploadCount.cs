using System;
using System.Collections.Generic;

namespace Commsights.Data.Models
{
    public partial class BaiVietUploadCount : BaseModel
    {
        public int? IndustryID { get; set; }
        public int? Count { get; set; }        
    }
}
