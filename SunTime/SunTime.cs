using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using SunData.Properties;

namespace SunData
{
    public class SunTime : SunTimePublisher
    {
        private List<SunTimeSubscriber> subscribers = new List<SunTimeSubscriber>();
        private static string apiUrl = @"https://api.sunrise-sunset.org/json";
        
        /// <summary>
        /// Get or set the lattitude used to fetch the data from the API
        /// </summary>
        public double Latitude
        {
            get { return Settings.Default.Latitude; }
            set
            {
                Settings.Default.Latitude = value;
                Settings.Default.Save();
                // Setting _apiResult to null will cause it to 
                // be updated next time any API data is requested.
                _apiResult = null;
            }
        }

        /// <summary>
        /// Get or set the longtitude used to fetch the data from the API
        /// </summary>
        public double Longtitude
        {
            get { return Settings.Default.Longtitude; }
            set
            {
                Settings.Default.Longtitude = value;
                Settings.Default.Save();
                _apiResult = null;
            }
        }

        private ApiResult _apiResult = null;
        
        /// <summary>
        /// Get all sun data from API for the currently set location
        /// </summary>
        public SunTimeData SunTimeData => getApiResult().results;

        /// <summary>
        /// Get status of the last API request
        /// </summary>
        public string ApiStatus => getApiResult().status;

        /// <summary>
        /// Get sunrise date and time for currently set location
        /// </summary>
        public DateTime Sunrise => SunTimeData.Sunrise;

        /// <summary>
        /// Get sunset date and time for currently set location
        /// </summary>
        public DateTime Sunset => SunTimeData.Sunset;

        /// <summary>
        /// Get JSON string from api for currently set location
        /// </summary>
        /// <returns></returns>
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
        
        /// <summary>
        /// Get API Result, automatically fetches new data if current data is outdated
        /// </summary>
        /// <returns>ApiResult object containing API data</returns>
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
                SunTimeChanged();
                return data;
            }
            else
            {
                return _apiResult;
            }
        }

        /// <summary>
        /// Register a subscriber to be notified when sun data changes
        /// </summary>
        /// <param name="subscriber">Subscriber to be notified</param>
        public void RegisterSubscriber(SunTimeSubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        /// <summary>
        /// Notify subscribers of changed sun data
        /// </summary>
        public void SunTimeChanged()
        {
            foreach(SunTimeSubscriber sub in subscribers)
            {
                sub.SunTimeChanged(SunTimeData);
            }
        }
    }
}
