using System.Collections.Generic;
using LHDTV.Models.DbEntity;


namespace LHDTV.Fakes
{


    public class Fakes
    {
        public List<PhotoDb> photos { get; set; } = new List<PhotoDb>();

        public List<FolderDb> folders { get; set; } = new List<FolderDb>();

        public List<UserDb> users { get; set; } = new List<UserDb>();

    }
}