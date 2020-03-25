using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly string apiKey = Keys.Sendgrid_Key;
        public EmailService()
        {
        }

        public async Task<bool> EmailAsync(Owner owner,string htmlContent)
        {
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(Keys.System_Email, "Milwaukee POS");

            //TODO pass in htmlContent
            var subject = "Shopping List";
            var to = new EmailAddress(owner.EmailAddress, owner.Name);
            var plainTextContent = "Here's your shopping list!";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return response.StatusCode == System.Net.HttpStatusCode.Accepted ? true : false;
        }
    }
}
