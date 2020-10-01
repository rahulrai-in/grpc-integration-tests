using System.Runtime.Serialization;

namespace GrpcSample.Contracts
{
    [DataContract]
    public class CountResult
    {
        [DataMember(Order = 1)]
        public int Value { get; set; }
    }
}