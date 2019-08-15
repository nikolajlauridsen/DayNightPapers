using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Requesting API");
            Console.WriteLine(sun.setTimes());
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
