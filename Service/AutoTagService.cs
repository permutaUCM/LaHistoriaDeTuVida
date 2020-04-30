



using System;
// using Amazon.Rekognition;
// using Amazon.Rekognition.Model;


namespace LHDTV.Service
{
    public class AutoTagService : IAutoTagService
    {


        public AutoTagService()
        {


        }

        public void autoTagPhotos(string photo)
        {

            // string photo = "input.jpg";
            //     Amazon.Rekognition.Model.Image image = new Amazon.Rekognition.Model.Image();

            //     try
            //     {
            //         using (FileStream fs = new FileStream(photo, FileMode.Open, FileAccess.Read))
            //         {
            //             byte[] data = null;
            //             data = new byte[fs.Length];
            //             fs.Read(data, 0, (int)fs.Length);
            //             image.Bytes = new MemoryStream(data);
            //         }
            //     }
            //     catch (Exception)
            //     {
            //         Console.WriteLine("Failed to load file " + photo);
            //         return;
            //     }

            //     AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient();
            //     DetectLabelsRequest detectLabelsRequest = new DetectLabelsRequest()
            //     {
            //         Image = image,
            //         MaxLabels = 30,
            //         MinConfidence = 30F
            //     };

            //     try
            //     {
            //         DetectLabelsResponse detectLabelsResponse = rekognitionClient.DetectLabelsAsync(detectLabelsRequest).Result;
            //         Console.WriteLine("Detected labels for " + photo);
            //         foreach(Label label in detectLabelsResponse.Labels)
            //             Console.WriteLine("{0}: {1}", label.Name, label.Confidence);
            //     }
            //     catch (Exception e)
            //     {
            //         Console.WriteLine(e.Message);
            //     }


            // }


        }
    }
}