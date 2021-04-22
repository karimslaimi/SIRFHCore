﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace SIRHCoreWeb.EmailService
{
    public class EmailSender : IEmailSender
    {

        /// <summary>
        /// The SMTP server.
        /// </summary>
        private readonly string host;

        /// <summary>
        /// The port that the SMTP server listens on.
        /// </summary>
        private readonly int port;

        /// <summary>
        /// The flag either enables or disables SSL.
        /// </summary>
        private readonly bool enableSsl;

        /// <summary>
        /// The user name for the SMTP server. Use "" if not required.
        /// </summary>
        private readonly string userName;

        /// <summary>
        /// The password for the SMTP server. Use "" if not required.
        /// </summary>
        private readonly string password;

        /// <summary>
        /// The email address that the email should come from.
        /// </summary>
        private readonly string senderEmail;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        public EmailSender(string host, int port, bool enableSSL, string userName, string password, string senderEmail)
        {
            this.host = host;
            this.port = port;
            this.enableSsl = enableSSL;
            this.userName = userName;
            this.password = password;
            this.senderEmail = senderEmail;
        }

        /// <summary>
        /// This method sends emails asynchronously.
        /// </summary>
        /// <param name="email">
        /// The email address the message will be sent to.
        /// </param>
        /// <param name="subject">
        /// The subject of the message.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient(this.host, this.port)
            {
                Credentials = new NetworkCredential(this.userName, this.password),
                EnableSsl = this.enableSsl
            };

            return client.SendMailAsync(new MailMessage(this.senderEmail, email, subject, message) { IsBodyHtml = true });
        }
        
    }
}