using Booking.Application.Abstract;
using Booking.Domain;
using Booking.Domain.Concrete;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Collections.Concurrent;

namespace Booking.Tests;

public sealed class AvailableHomesEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AvailableHomesEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.Single(s => s.ServiceType == typeof(IHomeRepository));
                services.Remove(descriptor);
                services.AddSingleton<IHomeRepository>(_ => new TestRepo());
            });
        });
    }

    [Fact]
    public async Task ReturnsOnlyFullyMatchingHomes()
    {
        var client = _factory.CreateClient();
        var res = await client.GetFromJsonAsync<AvailableHomesResponse>(
            "/api/available-homes?startDate=2025-07-15&endDate=2025-07-16");

        Assert.NotNull(res);
        Assert.Equal("OK", res!.Status);
        Assert.Equal(1, res.Homes.Count);
        var home = res.Homes[0];
        Assert.Equal("A", home.HomeId);
        Assert.Equal(new List<string> { "2025-07-15", "2025-07-16" }, home.AvailableSlots);
    }

    // ---------- Test repository using ConcurrentDictionary ----------
    private sealed class TestRepo : IHomeRepository
    {
        public Task<IReadOnlyList<Home>> GetAllAsync(CancellationToken ct = default)
        {
            var homes = new List<Home>
            {
                new Home("A", "Home A", new ConcurrentDictionary<DateOnly, byte>(
                    new[]
                    {
                        new KeyValuePair<DateOnly, byte>(new DateOnly(2025,07,15), 0),
                        new KeyValuePair<DateOnly, byte>(new DateOnly(2025,07,16), 0)
                    }).Keys),
                new Home("B", "Home B", new ConcurrentDictionary<DateOnly, byte>(
                    new[]
                    {
                        new KeyValuePair<DateOnly, byte>(new DateOnly(2025,07,15), 0),
                        new KeyValuePair<DateOnly, byte>(new DateOnly(2025,07,17), 0)
                    }).Keys)
            };
            return Task.FromResult<IReadOnlyList<Home>>(homes);
        }
    }

    private sealed record AvailableHomesResponse(string Status, List<HomeAvailabilityResult> Homes);
    private sealed record HomeAvailabilityResult(string HomeId, string HomeName, List<string> AvailableSlots);
}
