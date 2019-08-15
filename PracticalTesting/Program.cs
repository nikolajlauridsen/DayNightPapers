using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

using WallpaperLib;

namespace PracticalTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            SunTime sun = new SunTime();
            sun.Latitude = 55.567231;
            sun.Longtitude = 9.754025;
            

            Console.WriteLine("Requesting API");
            string apiResult = sun.setTimes();

           /* 
            string apiResult = @"
    {
      ""results"":
      {
        ""sunrise"":""2015-05-21T05:05:35+00:00"",
        ""sunset"":""2015-05-21T19:22:59+00:00"",
        ""solar_noon"":""2015-05-21T12:14:17+00:00"",
        ""day_length"":51444,
        ""civil_twilight_begin"":""2015-05-21T04:36:17+00:00"",
        ""civil_twilight_end"":""2015-05-21T19:52:17+00:00"",
        ""nautical_twilight_begin"":""2015-05-21T04:00:13+00:00"",
        ""nautical_twilight_end"":""2015-05-21T20:28:21+00:00"",
        ""astronomical_twilight_begin"":""2015-05-21T03:20:49+00:00"",
        ""astronomical_twilight_end"":""2015-05-21T21:07:45+00:00""
      },
       ""status"":""OK""
    }";*/
           Console.WriteLine("API result:\n" + apiResult + "\n\n");
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(apiResult));
            ApiResult data = new ApiResult();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(data.GetType());
            data = serializer.ReadObject(ms) as ApiResult;
            ms.Close();

            // Console.WriteLine($"\nStatus: {data.status}\nSunrise: {data.results.sunrise}\nSunset: {data.results.sunset}\nSolar noon: {data.results.solar_noon}\nDay length: {data.results.day_length}");

            DateTime sunrise = DateTime.ParseExact(data.results.sunrise, "yyyy-MM-ddTHH:mm:ssK", new CultureInfo("UTC"), DateTimeStyles.AssumeUniversal);
            DateTime sunset = DateTime.ParseExact(data.results.sunset, "yyyy-MM-ddTHH:mm:ssK", new CultureInfo("UTC"), DateTimeStyles.AssumeUniversal);

            Console.WriteLine($"Sunrise: {sunrise.ToString()}");
            Console.WriteLine($"Sunset: {sunset.ToString()}");

            Console.ReadKey();
        }
    }
}
