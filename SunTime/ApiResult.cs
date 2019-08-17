using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SunData
{
    [DataContract]
    public class ApiResult
    {
        [DataMember]
        public SunTimeData results;
        [DataMember]
        public string status;
    }
}
