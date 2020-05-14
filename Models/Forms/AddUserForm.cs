using System.ComponentModel.DataAnnotations;



namespace LHDTV.Models.Forms
{
    public class AddUserForm
    {
        [MaxLength(25, ErrorMessage = "El campo Title no puede tener más de 25 caracteres.")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "El campo LastName1 no puede tener más de 50 caracteres.")]
        public string LastName1 { get; set; }

        [MaxLength(50, ErrorMessage = "El campo LastName2 no puede tener más de 50 caracteres.")]
        public string LastName2 { get; set; }

        [MaxLength(50, ErrorMessage = "El campo Dni no puede tener más de 50 caracteres.")]
        [StringLength(8, MinimumLength = 1, ErrorMessage = "Debe tener  8 numeros y una letra.")]
        [RegularExpression("^(([A-Z])|\\d)?\\d{8}(\\d|[A-Z])?$")]
        public string Dni { get; set; }

        [MaxLength(50, ErrorMessage = "El campo Password no puede tener más de 50 caracteres.")]
        public string Password { get; set; }

        [MaxLength(50, ErrorMessage = "El campo Nickname no puede tener más de 50 caracteres.")]
        public string Nickname { get; set; }
        
        [MaxLength(50, ErrorMessage = "El campo Email no puede tener más de 50 caracteres.")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Please enter Valid Email ID")]
        public string Email { get; set; }


    }


}