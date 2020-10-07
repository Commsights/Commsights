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
    public class EmailStoragePropertyRepository : Repository<EmailStorageProperty>, IEmailStoragePropertyRepository
    {
        private readonly CommsightsContext _context;
        public EmailStoragePropertyRepository(CommsightsContext context) : base(context)
        {
            _context = context;
        }
        public List<EmailStorageProperty> GetParentIDAndCodeToList(int parentID, string code)
        {
            return _context.EmailStorageProperty.Where(item => item.ParentID == parentID && item.Code.Equals(code)).OrderBy(item => item.ID).ToList();
        }
    }
}
