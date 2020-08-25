using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public class ProductSearchRepository : Repository<ProductSearch>, IProductSearchRepository
    {
        private readonly CommsightsContext _context;
        public ProductSearchRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }        
    }
}
