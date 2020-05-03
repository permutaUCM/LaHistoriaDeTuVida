using System.ComponentModel.DataAnnotations;

namespace LHDTV.Models.Forms
{
    public class AuthenticateForm
    {
        [Required]
        public string NickName { get; set; }
        [Required]
        //TODO: Create new validation with regExp for passwords
        public string Password { get; set; }
  
    }
}