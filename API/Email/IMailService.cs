using System.Threading.Tasks;

namespace API.Email
{
    public interface IMailService
    {        
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendWelcomeEmailAsync(WelcomeRequest request);
        Task<bool> SendAppoinmentApproval(AppoinmentApprovalMail appoinmentApproval);
    }
}