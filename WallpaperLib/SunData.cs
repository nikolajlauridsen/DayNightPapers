using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace WallpaperLib
{
    [DataContract]
    public class SunData
    {

        [DataMember]
        public string sunrise { get; set; }

        [DataMember]
        public string sunset { get; set; }

        [DataMember]
        public string solar_noon { get; set; }

        [DataMember]
        public int day_length { get; set; }

        [DataMember]
        public string civil_twilight_begin { get; set; }

        [DataMember]
        public string civil_twilight_end { get; set; }

        [DataMember]
        public string nautical_twilight_begin { get; set; }

        [DataMember]
        public string nautical_twilight_end { get; set; }

        [DataMember]
        public string astronomical_twilight_begin { get; set; }

        [DataMember]
        public string astronomical_twilight_end { get; set; }
    }
}
