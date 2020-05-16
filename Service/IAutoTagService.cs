
using System.Collections.Generic;

namespace LHDTV.Service
{

    public interface IAutoTagService
    {

        void autoTagPhotos(string photo);

        ICollection<string> GetLocationData(double lon, double lat);
    }
}