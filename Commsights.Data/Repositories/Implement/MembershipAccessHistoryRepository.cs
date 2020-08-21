using Commsights.Data.Helpers;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commsights.Data.Repositories
{
    public class MembershipAccessHistoryRepository : Repository<MembershipAccessHistory>, IMembershipAccessHistoryRepository
    {
        private readonly CommsightsContext _context;

        public MembershipAccessHistoryRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }

    }
}
