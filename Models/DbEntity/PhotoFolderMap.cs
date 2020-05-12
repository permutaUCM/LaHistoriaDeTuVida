
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace LHDTV.Models.DbEntity
{
    public class PhotoFolderMap
    {
        public int PhotoId { get; set; }

        public PhotoDb Photo { get; set; }

        public int FolderId { get; set; }

        public FolderDb Folder { get; set; }
    }
}