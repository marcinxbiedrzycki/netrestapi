using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;

namespace NetRestApiTests;

public class InitContainerTest
{
    public async Task<MsSqlTestcontainer> InitContainerTestt()
    {
        var testcontainersBuilder = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(new MsSqlTestcontainerConfiguration
            {
                Password = "yourStrong(!)Password123"
            });

        return testcontainersBuilder.Build();
    }
}