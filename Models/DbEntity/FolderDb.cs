
using System.Collections.Generic;


namespace LHDTV.Models.DbEntity
{

    public class FolderDb{

        public int Id {get; set;}

        public UserDb User {get;set;}

        public string Title { get; set; }

        public ICollection<PhotoDb> Photos {get; set;}

        public bool Deleted { get; set; }

    }

}