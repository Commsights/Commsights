using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IEmailStorageRepository : IRepository<EmailStorage>
    {
        public List<EmailStorageDataTransfer> GetDataTransferByDatePublishBeginAndDatePublishEndToList(DateTime datePublishBegin, DateTime datePublishEnd);
    }
}
