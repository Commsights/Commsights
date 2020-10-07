using Commsights.Data.DataTransferObject;
using Commsights.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Data.Repositories
{
    public interface IEmailStoragePropertyRepository : IRepository<EmailStorageProperty>
    {
        public List<EmailStorageProperty> GetParentIDAndCodeToList(int parentID, string code);
        public List<EmailStoragePropertyDataTransfer> GetDataTransferByDatePublishBeginAndDatePublishEndToList(DateTime datePublishBegin, DateTime datePublishEnd);
    }
}
