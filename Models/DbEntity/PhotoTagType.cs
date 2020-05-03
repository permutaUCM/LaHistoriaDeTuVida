

namespace LHDTV.Models.DbEntity
{

    public class PhotoTagsTypes
    {
        //Tag name ("Restaurant", "Museum", etc..)
        public string Name { get; set; }
        //Extra info for a tag
        public Extra Extra1 { get; set; }
        public Extra Extra2 { get; set; }
        public Extra Extra3 { get; set; }

    }

    public class Extra
    {
        //Name for the extra info
        public string Name { get; set; }
        //Data type of the extra info ("select", "string", "ratio", etc..)
        public string type { get; set; }
        //Extra useful param, this value depends on the type:
        //Select: Selectionable items split by comma
        //Ratio: int value 1 - 5
        //String: null
        public string extras { get; set; }
    }
}