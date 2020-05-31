using System.ComponentModel.DataAnnotations;

namespace LHDTV.Models.Forms
{
    public class AddPhotoToFolderForm
    
    {

        public int FolderId { get; set; }
        public System.Collections.Generic.List<int> PhotosIds { get; set; }
    }
}