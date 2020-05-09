using System.Collections.Generic;
using LHDTV.Models.ViewEntity;

namespace LHDTV.Models
{
    //Return metadata when a single folder is requested
    public class FolderMetadata
    {
        public List<PhotoTransitionView> Transitions { get; set; }

        public List<PhotoTagsTypesView> Types { get; set; }
    }
}