using Models;
using System.Net;
using System.Net.Mail;
namespace Services
{
    public class EmailService
    {
        public EmailService() { }
        

        public async Task<ServiceResponse<string>> SendEmail(string email, string subject, string content)
        {
            var oService = new ServiceResponse<string>();

            int port = 587;
            string smtpServer = "smtp.gmail.com";  
            string fromEmail = "jazzytchannel065@gmail.com";
            string password = "hexhfxtpyohshyte";

            try
            {
                using (var client = new SmtpClient(smtpServer, port))
                {
                    client.Credentials = new NetworkCredential(fromEmail, password);
                    client.EnableSsl = true;

                    var mail = new MailMessage(fromEmail, email)
                    {
                        Subject = subject,
                        Body = content,
                        IsBodyHtml = true
                    };

                    await client.SendMailAsync(mail);
                    
                }

                return new ServiceResponse<string>(email, "Email sent successfully");

            }
            catch (Exception ex) { oService.Message = ex.Message; }
            return oService;
        }

    }
}
