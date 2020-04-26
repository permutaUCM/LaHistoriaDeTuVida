using System.ComponentModel.DataAnnotations;



namespace LHDTV.Models.Forms
{
    public class UpdateUserForm
    {
        
        public string Dni { get; set; }
        public string FirstName { get; set; }

        public string LastName1 { get; set; }

        public string LastName2 { get; set; }

        public string NewPassword { get; set; }

        public string OldPassword { get; set; }

        public string Email { get; set; }


    }


}