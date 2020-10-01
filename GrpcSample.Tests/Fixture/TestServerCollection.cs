using Xunit;

namespace GrpcSample.Tests.Fixture
{
    [CollectionDefinition(TestCollections.ApiIntegration)]
    public class TestServerCollection : ICollectionFixture<TestServerFixture>
    {
    }
}