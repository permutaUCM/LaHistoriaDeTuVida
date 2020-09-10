using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;


namespace LHDTV.Models.Forms
{
    public class AddPhotoTagForm
    {
        [Required]
        [MaxLength(15)]
        public string Title { get; set; }
        [MaxLength(15)]
        public string Extra1 { get; set; }

        [MaxLength(15)]
        public string Extra2 { get; set; }
        
        [MaxLength(15)]
        public string Extra3 { get; set; }
        
        public string Icon { get; set; }
    }
}