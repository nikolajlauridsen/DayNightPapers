using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

using WallpaperLib;
using SunData;

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
            Console.WriteLine($"\nStatus: {sun.ApiStatus}");

            SunTimeData data = sun.SunTimeData;

            Console.WriteLine($"Sunrise: {sun.Sunrise}\nSunset: {sun.Sunset}\nSolar noon: {data.SolarNoon}\n" +
                              $"Day length: {data.DayLength}\nCivil twilight: {data.CivilTwilightBegin} --> {data.CivilTwilightEnd}\n" +
                              $"Nautical twilight: {data.NauticalTwilightBegin} --> {data.NauticalTwilightEnd}\n" +
                              $"Astronomical twilight: {data.AstronomicalTwilightBegin} --> {data.AstronomicalTwilightEnd}");

            Console.ReadKey();
        }
    }
}
