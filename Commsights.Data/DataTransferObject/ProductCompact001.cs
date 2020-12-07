 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ProductCompact001
    {
        public int ID { get; set; }       
        public string Title { get; set; }        
        public string URLCode { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
    }
}
