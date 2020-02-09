using System.ComponentModel.DataAnnotations;


namespace LHDTV.Models.Forms
{
    public class PhotoForm
    {
        [Required(ErrorMessage="La id es obligatoria")]
        public string id { get; set; }
    }
}