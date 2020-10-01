using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcSample.Contracts;
using ProtoBuf.Grpc;

namespace GrpcSample.Service.Grpc
{
    public class LazyCounterService : ILazyCounterService
    {
        public async IAsyncEnumerable<CountResult> SlowCountAsync(CountRequest request, CallContext context = default)
        {
            await foreach (var value in SlowCounter(request.LowerBound, request.UpperBound))
            {
                yield return new CountResult {Value = value};
            }
        }

        public IEnumerable<CountResult> FastCount(CountRequest request, CallContext context = default)
        {
            return Enumerable
                .Range(request.LowerBound, request.UpperBound - request.LowerBound + 1)
                .Select(e => new CountResult {Value = e});
        }


        private static async IAsyncEnumerable<int> SlowCounter(int lo, int hi)
        {
            for (var i = lo; i <= hi; i++)
            {
                await Task.Delay(1000);
                yield return i;
            }
        }
    }
}