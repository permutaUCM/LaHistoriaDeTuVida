

using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using System;
namespace LHDTV.Service
{
    public class AutoTagService : IAutoTagService
    {


        public AutoTagService()
        {


        }

        public ICollection<string> GetLocationData(double lon, double lat)
        {
            var list = new List<string>();

            var client = new System.Net.Http.HttpClient();
            var url = "https://geocode.xyz/" + lon.ToString().Replace(",", ".") + "," + lat.ToString().Replace(",", ".") + "?geoit=json";
            var res = client.GetAsync(url).Result;

            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = res.Content.ReadAsStringAsync().Result;
                JObject contentObj = JObject.Parse(content);

                list.Add(contentObj.Value<string>("city"));
                list.Add(contentObj.Value<string>("country"));
            }

            return list;
        }

        public ICollection<Label> autoTagPhotos(string photo)
        {

            // string photo = "input.jpg";
            Amazon.Rekognition.Model.Image image = new Amazon.Rekognition.Model.Image();

            try
            {
                using (FileStream fs = new FileStream(photo, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = null;
                    data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    image.Bytes = new MemoryStream(data);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load file " + photo);
                return new List<Label>();
            }

            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient();
            DetectLabelsRequest detectLabelsRequest = new DetectLabelsRequest()
            {
                Image = image,
                MaxLabels = 30,
                MinConfidence = 65F
            };

            try
            {
                DetectLabelsResponse detectLabelsResponse = rekognitionClient.DetectLabelsAsync(detectLabelsRequest).Result;

                return detectLabelsResponse.Labels;
                // Console.WriteLine("Detected labels for " + photo);
                // foreach (Label label in detectLabelsResponse.Labels)
                //     Console.WriteLine("{0}: {1}", label.Name, label.Confidence);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Label>();
            }
        }
    }
}
