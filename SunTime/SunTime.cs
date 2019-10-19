using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using SunData.Properties;

namespace SunData
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

        private ApiResult _apiResult = null;

        public SunTimeData SunTimeData => getApiResult().results;
        public string ApiStatus => getApiResult().status;

        public DateTime Sunrise => SunTimeData.Sunrise;
        public DateTime Sunset => SunTimeData.Sunset;

        private string getJsonString()
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
            builder.Append($"&date={DateTime.Now.ToString("yyyy-MM-dd")}");
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
        
        private ApiResult getApiResult()
        {
            // Fetch API data for current day if it has not been fetched yet
            // or the time hast passed sunset and it's no longer the same date
            if (_apiResult == null || (DateTime.Now > _apiResult.results.Sunset && DateTime.Now.Date != _apiResult.results.Sunset.Date))
            {
                string jsonString = getJsonString();
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                ApiResult data = new ApiResult();
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(data.GetType());
                data = serializer.ReadObject(ms) as ApiResult;
                ms.Close();
                _apiResult = data;
                return data;
            }
            else
            {
                return _apiResult;
            }
        }

    }
}
