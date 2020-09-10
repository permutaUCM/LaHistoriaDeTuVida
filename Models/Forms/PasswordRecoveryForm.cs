using System.ComponentModel.DataAnnotations;

namespace LHDTV.Models.Forms
{
    public class PasswordRecoveryForm
    {
        public string NewPassword { get; set; }
        [Required]
        public string Token { get; set; }
    }
}