using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Commsights.Service.Mail
{
    public class MailService : IMailService
    {
        public void Send(Mail mail)
        {
            var client = Initialization(mail);
            if (client != null)
            {
                var message = WriteMessage(mail);
                client.Send(message);
            }
        }

        public SmtpClient Initialization(Mail mail)
        {
            if (mail != null)
            {
                SmtpClient client = new SmtpClient()
                {
                    EnableSsl = mail.IsUsingSSL,
                    Host = mail.STMPServer,
                    Port = mail.SMTPPort,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mail.Username, mail.Password),
                };
                if (mail.STMPServer.Contains("mail.commsightsvn.com") == true)
                {
                    client.EnableSsl = false;
                }
                return client;
            }
            return null;
        }

        public MailMessage WriteMessage(Mail mail)
        {
            if (mail != null)
            {
                MailMessage message = new MailMessage()
                {
                    IsBodyHtml = mail.IsMailBodyHtml,
                    Subject = mail.Subject,
                    Body = mail.Content,
                    Priority = MailPriority.High,
                    BodyEncoding = Encoding.GetEncoding("UTF-8"),
                    From = new MailAddress(mail.FromMail, mail.Display),
                };
                if (!string.IsNullOrEmpty(mail.AttachmentFiles))
                {
                    foreach (string attachmentFile in mail.AttachmentFiles.Split(';'))
                    {
                        if (!string.IsNullOrEmpty(attachmentFile))
                        {
                            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(mail.AttachmentFiles);
                            message.Attachments.Add(attachment);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(mail.ToMail))
                {
                    mail.ToMail = mail.ToMail.Replace(@";", @",");
                    message.To.Add(mail.ToMail);
                }
                if (!string.IsNullOrEmpty(mail.CCMail))
                {
                    message.CC.Add(mail.CCMail);
                }
                if (!string.IsNullOrEmpty(mail.BCCMail))
                {
                    message.Bcc.Add(mail.BCCMail);
                }
                return message;
            }
            return null;
        }
    }
}
