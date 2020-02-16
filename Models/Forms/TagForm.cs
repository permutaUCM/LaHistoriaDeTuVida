using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace LHDTV.Models.Forms
{
    public class TagForm
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public List<KeyValuePair<string, string>> Properties { get; set; }
    }
}