using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GrpcSample.Contracts;
using GrpcSample.Tests.Fixture;
using ProtoBuf.Grpc.Client;
using Shouldly;
using Xunit;
using CallContext = ProtoBuf.Grpc.CallContext;

namespace GrpcSample.Tests
{
    [Collection(TestCollections.ApiIntegration)]
    public class LazyCounterServiceShould
    {
        public LazyCounterServiceShould(TestServerFixture testServerFixture)
        {
            var channel = testServerFixture.GrpcChannel;
            _clientService = channel.CreateGrpcService<ILazyCounterService>();
        }

        private readonly ILazyCounterService _clientService;

        [Fact]
        public void FastCountFromLowToHigh()
        {
            // arrange
            var request = new CountRequest {LowerBound = 1, UpperBound = 10};

            // act
            var result = _clientService.FastCount(request, CallContext.Default);

            // assert
            var resultList = result.ToList();
            resultList.ShouldNotBeNull();
            resultList.Count().ShouldBe(10);
            resultList.First().Value.ShouldBe(1);
            resultList.Last().Value.ShouldBe(10);
        }

        [Fact]
        public async Task SlowCountFromLowToHighAsync()
        {
            // arrange
            var counter = 1;
            var timer = new Stopwatch();
            var request = new CountRequest {LowerBound = 1, UpperBound = 5};

            // act
            timer.Start();
            var result = _clientService.SlowCountAsync(request, CallContext.Default);

            // assert
            await foreach (var value in result)
            {
                value.Value.ShouldBe(counter++);
            }

            timer.Stop();
            counter.ShouldBe(6);
            timer.Elapsed.ShouldBeGreaterThan(TimeSpan.FromSeconds(5));
        }
    }
}