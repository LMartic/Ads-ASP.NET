using Ads.Application.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Ads.API.Extensions
{
    public class SmtpEmailSender : IEmailSender
    {
        private string _host;
        private int _port;
        private string _from;
        private string _password;

        public SmtpEmailSender(string host, int port, string from, string password)
        {
            this._host = host;
            this._port = port;
            this._from = from;
            this._password = password;
        }

        public string ToEmail { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }

        public void Send()
        {
            var smtp = new SmtpClient
            {
                Host = _host,
                Port = _port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_from, _password)
            };

            using (var message = new MailMessage(_from, ToEmail)
            {
                Subject = Subject,
                Body = Body
            })
            {
                smtp.Send(message);
            }
        }
    }
}