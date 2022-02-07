using MimeKit;
using MailKit.Net.Smtp;
using System;
using MailKit;

namespace EmailWebTest.Services
{
    public class EmailSender
    {
        private MimeMessage _message = new MimeMessage();

        private bool _isEmailSent = false;
        private void OnMessageSent(object sender, MessageSentEventArgs e)
        {
            _isEmailSent = true;
        }
        /// <summary>
        /// Get whether or not email is sent
        /// </summary>
        public bool IsEmailSent { get => _isEmailSent; }


        private string _failedmessage;
        /// <summary>
        /// Get the exception message which can be occure by sendind message
        /// </summary>
        public string Failedmessage { get => _failedmessage;}

        /// <summary>
        /// Send email to a list of recipients
        /// </summary>
        /// <param name="subject">Email subject</param>
        /// <param name="body">Email body</param>
        /// <param name="recipients">List of recipients</param>
        public void SendMessage(string subject, string body, string[] recipients)
        {
            _message.From.Add(new MailboxAddress(EmailConfig.USER, EmailConfig.EMAIL));

            foreach (string recipient in recipients)
            {
                _message.To.Add(new MailboxAddress("Dear recipient", recipient));
            }

            _message.Subject = subject;

            _message.Body = new TextPart("Body")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect(EmailConfig.HOST, EmailConfig.PORT, EmailConfig.USESSL);
                client.Authenticate(EmailConfig.EMAIL, EmailConfig.PASSWORD);

                client.MessageSent += OnMessageSent;

                try
                {
                    client.Send(_message);
                }
                catch (Exception ex)
                {
                    _failedmessage = ex.Message;
                }

                client.Disconnect(true);
            };
        }
    }
}
