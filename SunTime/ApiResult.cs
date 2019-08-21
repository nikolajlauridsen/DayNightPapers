using System.Runtime.Serialization;

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
