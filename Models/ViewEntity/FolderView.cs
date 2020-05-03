
using System.Collections.Generic;

namespace LHDTV.Models.ViewEntity
{

    public class FolderView
    {

        public ICollection<PhotoView> Photos { get; set; }

        public string Title { get; set; }


    }

}