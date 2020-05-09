
using System.Collections;
using System.Collections.Generic;


namespace LHDTV.Models.DbEntity
{

    public class FolderDb
    {

        public int Id { get; set; }

        public PhotoDb DefaultPhoto { get; set; }

        public UserDb User { get; set; }

        public string Title { get; set; }

        public ICollection<PhotoDb> Photos { get; set; }

        public ICollection<FileTags> PhotosTags { get; set; }

        //Default animation for the photos inside the folder
        public string Transition { get; set; }

        //Default time to next photo inside the folder, in ms
        public int ShowTime { get; set; }
        //Sets if the folder start the transitions automatically or dont
        public bool AutoStart { get; set; }

        //public ICollection<TagDb> Tags {get;set;}

        // Borrado logico o fisico??
        public bool Deleted { get; set; }

    }

}