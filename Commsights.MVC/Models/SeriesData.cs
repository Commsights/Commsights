using Commsights.Data.DataTransferObject;
using Commsights.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commsights.MVC.Models
{
    public class SeriesData
    {
        public string Name { get; set; }        
        public List<int?> Data { get; set; }
    }
}
