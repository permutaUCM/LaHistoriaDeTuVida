using System.ComponentModel.DataAnnotations;

namespace LHDTV.Models.Forms
{
    public class RequestPasswordRecoveryForm
    {
        [Required]
        public string Nick { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}