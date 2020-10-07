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
        public List<EmailStorageDataTransfer> GetDataTransferByDatePublishBeginAndDatePublishEndToList(DateTime datePublishBegin, DateTime datePublishEnd)
        {
            List<EmailStorageDataTransfer> list = new List<EmailStorageDataTransfer>();
            datePublishBegin = new DateTime(datePublishBegin.Year, datePublishBegin.Month, datePublishBegin.Day, 0, 0, 0);
            datePublishEnd = new DateTime(datePublishEnd.Year, datePublishEnd.Month, datePublishEnd.Day, 23, 59, 59);
            SqlParameter[] parameters =
                   {
                    new SqlParameter("@DatePublishBegin",datePublishBegin),
                    new SqlParameter("@DatePublishEnd",datePublishEnd),
                    };
            DataTable dt = SQLHelper.Fill(AppGlobal.ConectionString, "sp_EmailStorageSelectDataTransferByDatePublishBeginAndDatePublishEnd", parameters);
            list = SQLHelper.ToList<EmailStorageDataTransfer>(dt);
            return list;
        }
    }
}
