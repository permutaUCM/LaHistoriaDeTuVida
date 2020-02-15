using System.ComponentModel.DataAnnotations;


namespace LHDTV.Models.Forms
{
    public class PhotoForm
    {
        [Required(ErrorMessage="La id es obligatoria")]
        public int id { get; set; }
    }
}