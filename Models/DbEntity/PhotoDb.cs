using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LHDTV.Models.DbEntity
{
    public class PhotoDb
    {


        public string Id { get; set; }
        public UserDb User { get; set; }
        public ICollection<TagDb> Tag { get; set; }

        [MaxLength(25)]
        public string Tittle { get; set; }

        public DateTime UploadDate { get; set; }
        public DateTime RealDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public decimal Size { get; set; }
        public bool Deleted { get; set; }
        [MaxLength(100)]
        public string Url { get; set; }
    }
}
