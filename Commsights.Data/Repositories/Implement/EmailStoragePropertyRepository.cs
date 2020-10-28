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
            return _context.EmailStorageProperty.Where(item => item.ParentID == parentID && item.Code.Equals(code)).OrderByDescending(item => item.DateRead).ToList();
        }
        public List<EmailStoragePropertyDataTransfer> GetDataTransferByDatePublishBeginAndDatePublishEndToList(DateTime datePublishBegin, DateTime datePublishEnd)
        {
            List<EmailStoragePropertyDataTransfer> list = new List<EmailStoragePropertyDataTransfer>();
            datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
            datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
            SqlParameter[] parameters =
                   {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_EmailStoragePropertySelectDataTransferByDatePublishBeginAndDatePublishEnd", parameters);
            list = SQLHelper.ToList<EmailStoragePropertyDataTransfer>(dt);
            return list;
        }
    }
}
