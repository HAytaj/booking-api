using Booking.Application.Abstract;
using Booking.Domain.Concrete;
using System.Collections.Concurrent;

namespace Booking.Infrastructure.InMemory
{
    public class InMemoryHomeRepository : IHomeRepository
    {
        private static readonly ConcurrentDictionary<string, Home> _homes = Seed();
        public Task<IReadOnlyList<Home>> GetAllAsync(CancellationToken ct = default)
            => Task.FromResult<IReadOnlyList<Home>>(_homes.Values.ToList());
        private static ConcurrentDictionary<string, Home> Seed()
        {
            var homes = new ConcurrentDictionary<string, Home>();

            homes.TryAdd("123", new Home(
                id: "123",
                name: "Home 1",
                availableDates:
                [
                    new DateOnly(2025, 7, 15),
                    new DateOnly(2025, 7, 16),
                    new DateOnly(2025, 7, 17)
                ]
            ));

            homes.TryAdd("456", new Home(
                id: "456",
                name: "Home 2",
                availableDates:
                [
                    new DateOnly(2025, 7, 17),
                    new DateOnly(2025, 7, 18),
                    new DateOnly(2025, 7, 19)
                ]
            ));

            homes.TryAdd("789", new Home(
                id: "789",
                name: "Home 3",
                availableDates:
                [
                    new DateOnly(2025, 7, 15),
                    new DateOnly(2025, 7, 16)
                ]
            ));

            return homes;
        }

    }
}
