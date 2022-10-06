using Cmms.Core.Domain;
using Cmms.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cmms.Core.Application
{
  
    public class MailSender
    {
        private readonly string  _apiKey;
     
        public MailSender(string apiKey)
        {

            _apiKey= apiKey;

        }

        public string GetInviteTemplate()
        {
            string siteContent = string.Empty;

            // The url you want to grab
            string url = "https://propupstorage.blob.core.windows.net/root/InviteUserTemplateMail.html";

            // Here we're creating our request, we haven't actually sent the request to the site yet...
            // we're simply building our HTTP request to shoot off to google...
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())  
            using (Stream responseStream = response.GetResponseStream())              
            using (StreamReader streamReader = new StreamReader(responseStream))       
            {
                siteContent = streamReader.ReadToEnd(); 
            }

            return siteContent;

        }

        public async Task SendMail(string to,String fullName,string subject,string plainTextContent,string htmlContent)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("propup_au@outlook.com", "Propup");
            var toMail = new EmailAddress(to,fullName);
            
            var msg = MailHelper.CreateSingleEmail(from, toMail, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }



        public async Task SendInviteMail(InvitedUser inviteUser)
        {
            string htmlContext = GetInviteTemplate();
            htmlContext = htmlContext.Replace("@FullName", inviteUser.FirstName + " " + inviteUser.SurName);
            htmlContext = htmlContext.Replace("@InviteUrl", "https://propupstorage.z8.web.core.windows.net/#/user/acceptinvite/"+inviteUser.Id.ToString());
            await SendMail(inviteUser.Email, inviteUser.FirstName + " " + inviteUser.SurName, "Invite from Propup", "Please Accept your Invite", htmlContext);


        }
    }
}
