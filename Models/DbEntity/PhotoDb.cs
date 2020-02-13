using System;

namespace LHDTV.Models.DbEntity
{
    public class PhotoDb
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public DateTime UploadDate { get; set; }

        public bool Deleted{ get; set; }

        public string Title { get; set;}


    }
}