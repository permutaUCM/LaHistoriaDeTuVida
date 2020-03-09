
using System.Collections;
using System.Collections.Generic;


namespace LHDTV.Models.DbEntity
{

    public class FolderDb{

        public int Id {get; set;}

        public PhotoDb DefaultPhoto {get;set;}

        public UserDb User {get;set;}

        public string Title { get; set; }

        public ICollection<PhotoDb> Photos {get; set;}

        //Convertir en un mapa clave valor , todictionary

        public ICollection<string> PhotosTags {get ; set;}

        //public ICollection<TagDb> Tags {get;set;}

        // Borrado logico o fisico??
        public bool Deleted { get; set; }

    }

}