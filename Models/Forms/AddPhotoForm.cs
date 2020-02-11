using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace LHDTV.Models.Forms
{
    public class AddPhotoForm
    {
        [Required(ErrorMessage = "El campo Tittle es obligatorio."), MaxLength(50, ErrorMessage = "El campo Tittle no puede tener más de 50 caracteres.")]
        public string Tittle { get; set; }

        public List<string> Tags { get; set; }

        [Required(ErrorMessage = "El campo File es obligatorio.")]
        public string File { get; set; }
    }

}