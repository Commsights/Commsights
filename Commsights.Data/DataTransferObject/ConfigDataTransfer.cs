using System;
using System.Collections.Generic;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ConfigDataTransfer : Config
    {
        public int CountChildren { get; set; }
        public string DisplayName
        {
            get
            {
                return Title + " (" + CountChildren + ")";
            }
        }
        public string ParentName { get; set; }
        public ModelTemplate Parent { get; set; }
    }
}
