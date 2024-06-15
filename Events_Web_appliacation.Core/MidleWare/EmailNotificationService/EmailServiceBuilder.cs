using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Events_Web_appliacation.Core.MidleWare.EmailNotificationService
{
    public class EmailServiceBuilder
    {
        internal readonly SmtpClient smtp = new SmtpClient("smtp.gmail.com", 465);
        internal MailAddress _fromMailAddress;
        internal List<MailAddress> _toMailAddress;
        internal MailContent _mailContent;

        public EmailServiceBuilder()
        {
            _toMailAddress = new List<MailAddress>();
        }

        public EmailServiceBuilder SetMailSender(string senderEmail, string senderName = "")
        {
            if (string.IsNullOrEmpty(senderEmail)) throw new ArgumentException(nameof(senderEmail));
            _fromMailAddress = string.IsNullOrEmpty(senderName) ? new MailAddress(senderEmail) : new MailAddress(senderEmail, senderName);
            return this;
        }

        public EmailServiceBuilder SetMailRecievers(string[] recieverEmail)
        {
            if (recieverEmail == null) throw new ArgumentException(nameof(recieverEmail));
            foreach(string resiever in recieverEmail)
                _toMailAddress.Add(new MailAddress(resiever));
            return this;
        }

        public EmailServiceBuilder SetMailContent(string theme, string content)
        {
            if (string.IsNullOrEmpty(content)) throw new ArgumentException(nameof(content));
            _mailContent = new MailContent();
            _mailContent.Theme = theme;
            _mailContent.Content = content;
            return this;
        }

        public EmailServiceBuilder SetCreditials(string email, string password)
        {
            smtp.Credentials = new NetworkCredential(email, password);
            smtp.EnableSsl = true;
            return this;
        }

        public EmailService Build() { return new EmailService(this); }

        public class EmailService
        {
            private EmailServiceBuilder _builder;
            internal EmailService(EmailServiceBuilder builder) 
            {
                _builder = builder;
            }
            public async Task SendAsync()
            {
                await Task.Run(() =>
                {
                    foreach (MailAddress toMail in _builder._toMailAddress)
                        _builder.smtp.Send(new MailMessage(_builder._fromMailAddress, toMail)
                        {
                            Subject = _builder._mailContent.Theme,
                            Body = _builder._mailContent.Content,
                        });
                });
            }

        }

        internal class MailContent
        {
            public string Theme { get; set; }
            public string Content { get; set; }
        }
    }
}
