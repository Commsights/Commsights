using System;
using System.Collections.Generic;
using System.Text;
using Commsights.Data.Models;

namespace Commsights.Data.DataTransferObject
{
    public class ConfigDataTransfer : Config
    {
        public ModelTemplate WebsiteType { get; set; }
    }
}
