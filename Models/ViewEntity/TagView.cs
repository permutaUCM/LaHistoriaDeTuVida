using System;

using System.Collections.Generic;
namespace LHDTV.Models.ViewEntity
{
    public class TagView
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public ICollection<TagPropView> Properties { get; set; }
    }
}