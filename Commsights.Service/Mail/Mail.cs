using Commsights.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commsights.Service.Mail
{
    public class Mail
    {
        //private protected Mail() { }

        //private Mail _instance { get; set; }

        //public Mail GetInstance()
        //{
        //    if (this._instance != null)
        //        return this._instance;
        //    return new Mail();
        //}

        public string FromMail { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Display { get; set; }

        public string ToMail { get; set; }
        public string CCMail { get; set; }
        public string BCCMail { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public string AttachmentFiles { get; set; }

        public string STMPServer { get; set; }

        public int SMTPPort { get; set; }

        public bool IsUsingSSL { get; set; }

        public bool IsMailBodyHtml { get; set; }

        public void Initialization()
        {
            if (string.IsNullOrEmpty(this.STMPServer))
            {
                this.STMPServer = AppGlobal.SMTPServer;
            }
            if (string.IsNullOrEmpty(this.FromMail))
            {
                this.FromMail = AppGlobal.MasterEmailUser;
            }
            if (string.IsNullOrEmpty(this.Username))
            {
                this.Username = AppGlobal.MasterEmailUser;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                this.Password = AppGlobal.MasterEmailPassword;
            }
            if (string.IsNullOrEmpty(this.Display))
            {
                this.Display = AppGlobal.MasterEmailDisplay;
            }
            if (string.IsNullOrEmpty(this.Subject))
            {
                this.Subject = AppGlobal.MasterEmailSubject;
            }
            if (this.SMTPPort == 0)
            {
                this.SMTPPort = AppGlobal.SMTPPort;
            }
            this.IsMailBodyHtml = true;
            this.IsUsingSSL = true;
        }
    }
}
