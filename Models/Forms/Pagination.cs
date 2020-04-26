using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace LHDTV.Models.Forms
{
    public class Pagination
    {
        public int Page { get; set; }
        public int TamPag{get;set;}
        public int Order {get;set;}

        public int Filters {get;set;}

    }
}