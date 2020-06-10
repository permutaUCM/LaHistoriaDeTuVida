
using System.Collections.Generic;
using System;
using System.IO;
using Amazon.Rekognition.Model;


namespace LHDTV.Service
{

    public interface IAutoTagService
    {

        List<Label> autoTagPhotos(Stream fileStream);

        ICollection<string> GetLocationData(double lon, double lat);
    }
}