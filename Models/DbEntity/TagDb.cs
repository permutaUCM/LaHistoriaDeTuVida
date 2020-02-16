using System.Collections.Generic;
namespace LHDTV.Models.DbEntity
{
    public class TagDb
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public ICollection<TagPropDb> Properties { get; set; }
    }
}