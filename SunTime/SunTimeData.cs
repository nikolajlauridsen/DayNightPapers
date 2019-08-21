using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace SunData
{
    [DataContract]
    public class SunTimeData
    {

        //Sunrise
        [DataMember]
        private string sunrise { get; set; }

        public DateTime Sunrise => parseTime(sunrise);

        // Sunset
        [DataMember]
        private string sunset { get; set; }

        public DateTime Sunset => parseTime(sunset);

        // Solar Noon
        [DataMember]
        private string solar_noon { get; set; }

        public DateTime SolarNoon => parseTime(solar_noon);

        // Day length
        [DataMember]
        private int day_length { get; set; }

        public int DayLength => day_length;

        // Civil twilight begin
        [DataMember]
        private string civil_twilight_begin { get; set; }

        public DateTime CivilTwilightBegin => parseTime(civil_twilight_begin);

        // Civil twilight end
        [DataMember]
        private string civil_twilight_end { get; set; }

        public DateTime CivilTwilightEnd => parseTime(civil_twilight_end);

        // Nautical twilight begin
        [DataMember]
        private string nautical_twilight_begin { get; set; }

        public DateTime NauticalTwilightBegin => parseTime(nautical_twilight_begin);

        // Nautical twilight end
        [DataMember]
        private string nautical_twilight_end { get; set; }

        public DateTime NauticalTwilightEnd => parseTime(nautical_twilight_end);
        
        // Astronomical twilight begin
        [DataMember]
        private string astronomical_twilight_begin { get; set; }

        public DateTime AstronomicalTwilightBegin => parseTime(astronomical_twilight_begin);

        // Astronomical twilight end
        [DataMember]
        private string astronomical_twilight_end { get; set; }

        public DateTime AstronomicalTwilightEnd => parseTime(astronomical_twilight_end);

        private DateTime parseTime(string timeString)
        {
            // Thank you to .net for handling the nasty nasty time stuff <3
            return DateTime.ParseExact(timeString, "yyyy-MM-ddTHH:mm:ssK", new CultureInfo("UTC"), DateTimeStyles.AssumeUniversal);
        }
    }
}
