using System.ComponentModel.DataAnnotations;

namespace LHDTV.Models.Forms
{
    public class UpdateUserForm
    {


        [MaxLength(50, ErrorMessage = "El campo Dni no puede tener más de 50 caracteres.")]
        [StringLength(8, MinimumLength = 1, ErrorMessage = "Debe tener  8 numeros y una letra.")]
        [RegularExpression("^(([A-Z])|\\d)?\\d{8}(\\d|[A-Z])?$")]
        public string Dni { get; set; }
        public string FirstName { get; set; }

        public string LastName1 { get; set; }

        public string LastName2 { get; set; }

        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
        [MaxLength(50, ErrorMessage = "El campo Email no puede tener más de 50 caracteres.")]
        [EmailAddress(ErrorMessage = "Please enter Valid Email ID")]
        public string Email { get; set; }


    }


}