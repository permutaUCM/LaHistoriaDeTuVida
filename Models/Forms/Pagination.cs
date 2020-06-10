
using System.Collections.Generic;
namespace LHDTV.Models.Forms
{
    public class Pagination
    {
        public int NumPag { get; set; }
        public int TamPag { get; set; }
        public string OrderField { get; set; }

        public string OrderDir { get; set; }

        public IDictionary<string, string> Filter { get; set; }
    }
}