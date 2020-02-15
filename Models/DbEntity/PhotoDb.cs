using System.Collections;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LHDTV.Models.DbEntity
{
    public class PhotoDb
    {


        public int Id { get; set; }
        public UserDb User { get; set; }
        public ICollection<TagDb> Tag { get; set; }
        [MaxLength(25)]
        public string Tittle { get; set; }
        [MaxLength(50)]
        public string caption { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime RealDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public decimal Size { get; set; }
        public bool Deleted { get; set; }
        public string Url { get; set; }

    }
}
