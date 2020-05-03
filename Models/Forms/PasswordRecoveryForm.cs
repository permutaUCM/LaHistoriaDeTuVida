using System.ComponentModel.DataAnnotations;

namespace LHDTV.Models.Forms
{
    public class PasswordRecoveryForm
    {
        [Required]
        public string Nick { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        //TODO: Create new validation with regExp for passwords
        public string NewPassword { get; set; }
        [Required]
        public string Token { get; set; }
    }
}