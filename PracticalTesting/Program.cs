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

           Console.WriteLine("API result:\n" + apiResult + "\n\n");
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(apiResult));
            ApiResult data = new ApiResult();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(data.GetType());
            data = serializer.ReadObject(ms) as ApiResult;
            ms.Close();

            Console.WriteLine($"Status: {data.status}\nSunrise: {data.results.Sunrise}\nSunset: {data.results.Sunset}\nSolar noon: {data.results.SolarNoon}\n" +
                              $"Day length: {data.results.DayLength}\nCivil twilight: {data.results.CivilTwilightBegin} --> {data.results.CivilTwilightEnd}\n" +
                              $"Nautical twilight: {data.results.NauticalTwilightBegin} --> {data.results.NauticalTwilightEnd}\n " +
                              $"Astronomical twilight: {data.results.AstronomicalTwilightBegin} --> {data.results.AstronomicalTwilightEnd}");

            Console.ReadKey();
        }
    }
}
