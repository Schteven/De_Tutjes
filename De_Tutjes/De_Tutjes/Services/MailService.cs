using De_Tutjes.Models;
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

    public class DiaryOverviewMail
    {
        public string email { get; set; }
        public ICollection<DiaryToddlerUpdate> dtu { get; set; }
        public string firstname { get; set; }

        public DiaryOverviewMail() { }
    }

    public class MailService
    {

        public string receiver { get; set; }
        public string sender { get; set; }
        public string subject { get; set; }
        public string content { get; set; }

        public CreateNewAccount cna { get; set; }
        public DiaryOverviewMail dom { get; set; }

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
                    this.subject = NewAccountTemplate().subject;
                    this.content = NewAccountTemplate().content;
                    break;
                case "DiaryOverviewMail":
                    dom = (DiaryOverviewMail)o;
                    this.receiver = dom.email;
                    this.subject = DiaryOverviewTemplate().subject;
                    this.content = DiaryOverviewTemplate().content;
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

        public EmailTemplate NewAccountTemplate()
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

        public EmailTemplate DiaryOverviewTemplate()
        {
            String updatelines = @"<table style=""font - size:16px; ""><tr><th> " + dom.firstname + " :</th><th></th></tr> ";

            foreach (DiaryToddlerUpdate d in dom.dtu)
            {
                updatelines += @"<tr style = ""border-bottom:1px solid grey;""><td> " + d.Timestamp.Hour + ":" + d.Timestamp.Minute + "</td><td>";

                switch (d.UpdateType)
                {
                    case 1:
                        updatelines += @"<span>Is aangekomen bij De Tutjes</span><br />";                                                                                                                                        ;

                        break;
                    case 2:
                        updatelines += @"<span>Gaat slapen</span><br />";                                                                                                                                       ;

                        break;
                    case 3:
                        updatelines += @"<span>Is wakker geworder</span><br />";                                                                                                                                         ;

                        break;
                    case 4:
                        updatelines += @"<span>Is aan het eten.</span><br />";                                                                                                                                         ;

                        break;
                    case 5:
                        updatelines += @"<span>Heeft een nieuwe pamper gekregen</span><br />";                                                                                                                                         ;

                        break;
                    case 6:
                        updatelines += @"<span>Is naar huis</span><br />";

                        break;
                    case 7:
                            
                        break;
                    default:
                        break;
                }
                if(d.Comment != null && d.Comment != "")
                {
                    updatelines += @"<span class=""glyphicon glyphicon-comment"" style=""display: inline-block; width:20px;""></span><span><i>" + d.Comment + "</i></span>";
                }
                updatelines += "</td></tr>";
            }
            updatelines += "</table>";

            EmailTemplate et = new EmailTemplate();
            et.subject = "Dagboek overzicht van " + dom.firstname + " op " + DateTime.Now.ToString("dddd/MM");
            et.content =
                "<h2>Beste ouder,</h2>" +
                "<p><span>Vandaag is </span>" + dom.firstname + "<span> opgevangen door De Tutjes.</span><br />" +
                "<span>Hieronder vind je een overzicht van het dagboek van vandaag, </span>" + DateTime.Now.Day + " " + DateTime.Now.Month + " " + DateTime.Now.Year + "<span>.</span></p>" +
                updatelines;

            return et;
        }

    }
}