using System;
using System.IO;

using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


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

        public List<Label> autoTagPhotos(Stream photoStream)
        {

            Amazon.Rekognition.Model.Image image = new Amazon.Rekognition.Model.Image();

            try
            {
                
                    byte[] data = null;
                    data = new byte[photoStream.Length];
                    photoStream.Read(data, 0, (int)photoStream.Length);
                    image.Bytes = new MemoryStream(data);
                
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load file");
                return new List<Label>();
            }

            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient();
            DetectLabelsRequest detectLabelsRequest = new DetectLabelsRequest()
            {
                Image = image,
                MaxLabels = 40,
                MinConfidence = 30F
            };

            try
            {
                DetectLabelsResponse detectLabelsResponse = rekognitionClient.DetectLabelsAsync(detectLabelsRequest).Result;
                
                
            var res = JsonConvert.SerializeObject(detectLabelsResponse.Labels);
                return detectLabelsResponse.Labels;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Label>();
            }
        }
    }
}
