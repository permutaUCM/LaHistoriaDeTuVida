
using System.Collections.Generic;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace LHDTV.Service
{

    public interface IAutoTagService
    {

        ICollection<Label> autoTagPhotos(string photo);

        ICollection<string> GetLocationData(double lon, double lat);
    }
}