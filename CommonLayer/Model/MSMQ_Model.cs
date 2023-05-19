using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MSMQ_Model
    {
        MessageQueue message = new MessageQueue();
        private string receiverEmailId;
        private string receiverName;

        public void sendMessage(string token, string emailId, string name)
        {
            receiverEmailId = emailId;
            receiverName = name;
            message.Path = @".\private$\token";

            try
            {
                if (!(MessageQueue.Exists(message.Path)))
                {
                    MessageQueue.Create(message.Path);
                }
                message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                message.ReceiveCompleted += Message_ReceiveCompleted;
                message.Send(token);
                message.BeginReceive();
                message.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        public void sendData2Queue(string token)
        {
            message.Path = @".\private$\token";
            if (!(MessageQueue.Exists(message.Path)))
            {
                MessageQueue.Create(message.Path);
            }

            message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            message.ReceiveCompleted += Message_ReceiveCompleted;
            message.Send(token);
            message.BeginReceive();
            message.Close();
        }

        public void Message_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = message.EndReceive(e.AsyncResult);
            string token = msg.Body.ToString();
            MailMessage mailMessage = new MailMessage();
            string Subject = "Fundoo Notes Reset Token";
            string Body = token;
            var SMTP = new SmtpClient("Smtp.gmail.com")
            { 
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("shravanthi27041996@gmail.com", "pjkebdccuiyggikn"),
                EnableSsl = true

            };

            mailMessage.From = new MailAddress("shravanthi27041996@gmail.com");
            mailMessage.To.Add(new MailAddress(receiverEmailId));
            string mailBody = msg.Body.ToString();
            SMTP.Send("shravanthi27041996@gmail.com", "shravanthi27041996@gmail.com", Subject, Body);
            message.BeginReceive();
        }
    }
}
