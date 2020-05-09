using System;
using System.Collections.Generic;

namespace LHDTV.Models.ViewEntity
{
    public class PhotoView
    {
        public int Id { get; set; }
        public ICollection<TagView> Tag { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public DateTime RealDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public decimal Size { get; set; }
        public string Url { get; set; }
    }
}