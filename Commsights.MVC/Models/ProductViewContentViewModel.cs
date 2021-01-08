using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commsights.MVC.Models
{
    public class ProductViewContentViewModel
    {
        public Config Parent { get; set; }
        public Product Product { get; set; }
        public List<ProductSearchProperty> ListProductSearchProperty { get; set; }
        public List<ProductProperty> ListProductProperty { get; set; }

    }
}
