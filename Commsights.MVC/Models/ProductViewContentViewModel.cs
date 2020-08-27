using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commsights.MVC.Models
{
    public class ProductViewContentViewModel
    {
        public Product Product { get; set; }
        public List<ProductSearchProperty> ListProductSearchProperty { get; set; }

    }
}
