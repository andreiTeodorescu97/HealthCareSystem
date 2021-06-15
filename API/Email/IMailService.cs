using System.Threading.Tasks;

namespace API.Email
{
    public interface IMailService
    {        
        Task<bool> SendConfirmationMail(string userEmail, string confirmationLink);
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendWelcomeEmailAsync(WelcomeRequest request);
        Task<bool> SendAppoinmentApproval(AppoinmentApprovalMail appoinmentApproval);
        Task<bool> SendResetPasswordLink(string userEmail, string resetLink);
    }
}