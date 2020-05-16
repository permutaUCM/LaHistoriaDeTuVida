using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;


namespace LHDTV.Models.Forms
{
    public class AddPhotoForm
    {
        [MaxLength(25, ErrorMessage = "El campo Title no puede tener más de 25 caracteres.")]
        public string Title { get; set; }

        [MaxLength(50, ErrorMessage = "El campo Title no puede tener más de 50 caracteres.")]
        public string Caption { get; set; }

        public List<TagForm> Tags { get; set; }

        public int FolderId { get; set; }

        [Required(ErrorMessage = "El campo File es obligatorio.")]
        public IFormFile File { get; set; }
    }



}