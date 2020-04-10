using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace LHDTV.Models.Forms
{
    public class TagFormUpdate
    {
        public int TagId { get; set; }
        public int PhotoId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
        public string Extra3 { get; set; }
    }
}