

namespace LHDTV.Models.Forms
{
    public class UpdateFolderForm
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Transition { get; set; }
        public bool AutoStart { get; set; }
        public int TransitionTime { get; set; }
    }
}