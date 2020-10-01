using System.Runtime.Serialization;

namespace GrpcSample.Contracts
{
    [DataContract]
    public class CountRequest
    {
        [DataMember(Order = 1)]
        public int LowerBound { get; set; }

        [DataMember(Order = 2)]
        public int UpperBound { get; set; }
    }
}