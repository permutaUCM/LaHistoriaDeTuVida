using System.ComponentModel.DataAnnotations;


namespace LHDTV.Models.Forms
{
    public class FolderForm
    {
        [Required(ErrorMessage="La id es obligatoria")]
        public int id { get; set; }
    }
}