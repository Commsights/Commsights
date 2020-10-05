using Commsights.Data.DataTransferObject;
using Commsights.Data.Enum;
using Commsights.Data.Helpers;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commsights.Data.Repositories
{
    public class EmailStorageRepository : Repository<EmailStorage>, IEmailStorageRepository
    {
        private readonly CommsightsContext _context;     
        public EmailStorageRepository(CommsightsContext context) : base(context)
        {
            _context = context;
       
        }     
    }
}
