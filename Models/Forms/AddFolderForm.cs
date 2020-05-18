using System.ComponentModel.DataAnnotations;

namespace LHDTV.Models.Forms
{
    public class AddFolderForm
    {
        [MaxLength(25, ErrorMessage = "El campo Title no puede tener más de 25 caracteres.")]
        public string Title { get; set; }

        public string Transition { get; set; }
        public bool AutoStart { get; set; }
        public int TransitionTime { get; set; }

    }


}