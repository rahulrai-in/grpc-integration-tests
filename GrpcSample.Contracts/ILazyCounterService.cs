using System.Collections.Generic;
using System.ServiceModel;
using ProtoBuf.Grpc;

namespace GrpcSample.Contracts
{
    [ServiceContract(Name = "GrpcSample.LazyCounter")]
    public interface ILazyCounterService
    {
        [OperationContract]
        IAsyncEnumerable<CountResult> SlowCountAsync(CountRequest request, CallContext context = default);

        [OperationContract]
        IEnumerable<CountResult> FastCount(CountRequest request, CallContext context = default);
    }
}