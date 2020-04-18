using System.ComponentModel.DataAnnotations;



namespace LHDTV.Models.Forms
{
    public class UpdateUserForm
    {
        
        public string Dni { get; set; }
        public string FirstName { get; set; }


        public string LastName1 { get; set; }

     
        public string LastName2 { get; set; }

    //    [MaxLength(50, ErrorMessage = "El campo Dni no puede tener más de 50 caracteres.")]
    //    public string Dni { get; set; }

    
        public string NewPassword { get; set; }

        public string OldPassword { get; set; }

        public string Email { get; set; }
    //    [MaxLength(50, ErrorMessage = "El campo Nickname no puede tener más de 50 caracteres.")]
    //    public string Nickname { get; set; }
        
    //    [MaxLength(50, ErrorMessage = "El campo Email no puede tener más de 50 caracteres.")]
    //    public string Email { get; set; }


    }


}