using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Service.Mail
{
    public interface IMailService
    {
        public void Send(Mail mail);
    }
}
