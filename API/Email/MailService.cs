using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Constants;
using API.Data;
using API.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace API.Email
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly DataContext _context;
        public MailService(IOptions<MailSettings> mailSettings, DataContext context)
        {
            _context = context;
            _mailSettings = mailSettings.Value;
        }

        public async Task<bool> SendResetPasswordLink(string userEmail, string resetLink)
        {
            try
            {
                string MailText = _context.EmailTemplates.FirstOrDefault(c => c.Id == (int)EmailTemplates.ResetPassword).Template;
                MailText = MailText.Replace("[confirmationLink]", resetLink);

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(userEmail));
                email.Subject = "Resetare Parola EASY-MED";
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception ex)
            {
                var error = new Error{
                    Message = ex.Message,
                    InnerMessage = ex.InnerException.Message,
                    StackTrace = ex.InnerException.StackTrace,
                };
                _context.Errors.Add(error);
                await _context.SaveChangesAsync();
                return false;
            }
        }

        public async Task<bool> SendConfirmationMail(string userEmail, string confirmationLink)
        {
            try
            {
                string MailText = _context.EmailTemplates.FirstOrDefault(c => c.Id == (int)EmailTemplates.ConfirmAccount).Template;
                MailText = MailText.Replace("[confirmationLink]", confirmationLink);

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(userEmail));
                email.Subject = "Confirmare Email EASY-MED";
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SendAppoinmentApproval(AppoinmentApprovalMail appoinmentApproval)
        {
            try
            {
                string MailText = _context.EmailTemplates.FirstOrDefault(c => c.Id == (int)EmailTemplates.AppoinmentConfirmation).Template;
                MailText = MailText.Replace("[appoinmentDate]", appoinmentApproval.AppoinmentDate)
                .Replace("[firstName]", appoinmentApproval.DoctorFirstName)
                .Replace("[secondName]", appoinmentApproval.DoctorSecondName);

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(appoinmentApproval.ToEmail));
                email.Subject = "Confirmare Programare EASY-MED";
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendWelcomeEmailAsync(WelcomeRequest request)
        {
            string MailText = _context.EmailTemplates.FirstOrDefault(c => c.Id == (int)EmailTemplates.Welcome).Template;
            MailText = MailText.Replace("[username]", request.UserName).Replace("[email]", request.ToEmail);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Welcome {request.UserName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}