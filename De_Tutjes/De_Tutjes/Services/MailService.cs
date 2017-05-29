using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace De_Tutjes.Services
{
    public class EmailTemplate
    {
        public string subject { get; set; }
        public string content { get; set; }

        public EmailTemplate() { }
    }

    public class CreateNewAccount
    {
        public string firstname { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public CreateNewAccount() { }
    }

    public class MailService
    {

        public string receiver { get; set; }
        public string sender { get; set; }
        public string subject { get; set; }
        public string content { get; set; }

        public CreateNewAccount cna { get; set; }

        public string host { get; set; }

        public MailService()
        {

            this.host = "localhost";
            this.sender = "noreply@detutjes.be";

#if DEBUG
            this.host = "relay.proximus.be";
#endif

        }

        public void SendMail(object o)
        {
            switch (o.GetType().Name)
            {
                case "CreateNewAccount":
                    cna = (CreateNewAccount)o;
                    this.receiver = cna.email;
                    this.subject = NewAccount().subject;
                    this.content = NewAccount().content;
                    break;
            }

            MailMessage mail = new MailMessage(sender, receiver);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = host;

            mail.Subject = subject;
            mail.Body = content;

            mail.IsBodyHtml = true;

            client.Send(mail);
        }

        public EmailTemplate NewAccount()
        {
            EmailTemplate et = new EmailTemplate();
            et.subject = "Je account op detutjes.be";
            et.content = 
                "<h2>Welkom " + cna.firstname + "</h2>" +
                "<p>Er werd zonet een nieuw account aangemaakt op jouw naam bij kinderdagverblijf De Tutjes.<br>"+
                "Hieronder vind je jouw inloggegevens terug:<br>"+
                "<table><tr><td>Emailadres:</td><td>"+cna.email+"</td></tr>"+
                "<tr><td>Wachtwoord:</td><td>"+cna.password+"</td></tr></table><br>"+
                "Vriendelijke groeten<br>"+
                "De Tutjes";

            return et;
        }

    }
}