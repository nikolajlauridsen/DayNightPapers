using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WallpaperLib
{
    [DataContract]
    public class ApiResult
    {
        [DataMember]
        public SunData results;
        [DataMember]
        public string status;
    }
}
