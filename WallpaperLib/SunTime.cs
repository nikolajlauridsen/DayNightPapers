using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WallpaperLib.Properties;

namespace WallpaperLib
{
    public class SunTime
    {
        private static string apiUrl = @"https://api.sunrise-sunset.org/json";
        public double Latitude
        {
            get { return Settings.Default.Latitude; }
            set
            {
                Settings.Default.Latitude = value;
                Settings.Default.Save();
            }
        }

        public double Longtitude
        {
            get { return Settings.Default.Longtitude; }
            set
            {
                Settings.Default.Longtitude = value;
                Settings.Default.Save();
            }
        }

        private DateTime? sunrise = null;
        public DateTime Sunrise { get; set; }
        private DateTime? sunset = null;
        public DateTime Sunset { get; set; }

        public string setTimes()
        {
            // Not optimal TODO: Revise
            StringBuilder builder = new StringBuilder(apiUrl);
            // Add latitude
            builder.Append("?lat=");
            builder.Append(Latitude);
            // Add longtitude
            builder.Append("&lng=");
            builder.Append(Longtitude);
            // Add date
            builder.Append("&date=today");
            // Add formatted
            builder.Append("&formatted=0");
            

            WebRequest req = WebRequest.Create(builder.ToString());
            WebResponse res = req.GetResponse();

            string content = "";
            using (Stream datastream = res.GetResponseStream())
            {
                StreamReader reader = new StreamReader(datastream);
                content = reader.ReadToEnd();
            }

            return content;
        }
    }
}
