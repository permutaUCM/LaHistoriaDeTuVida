using System.Threading.Tasks;


namespace LHDTV.Service{
    public interface IMailService{
        Task SendEmail(
            string fromDisplayName,
            string fromEmailAddress,
            string toName,
            string toEmailAddres,
            string subject,
            string message);
    }
}