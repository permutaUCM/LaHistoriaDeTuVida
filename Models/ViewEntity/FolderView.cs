using System.Collections;
using System.Collections.Generic;

namespace LHDTV.Models.ViewEntity
{

    public class FolderView
    {

        public int Id { get; set; }
        public ICollection<PhotoView> Photos { get; set; }

        public string Title { get; set; }

        public string Path { get; set; }

        public string Transition { get; set; }
        //Default time to next photo inside the folder, in ms
        public int ShowTime { get; set; }
        //Sets if the folder start the transitions automatically or dont
        public bool AutoStart { get; set; }

    }

}