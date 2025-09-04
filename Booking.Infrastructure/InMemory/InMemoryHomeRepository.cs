using Booking.Application.Abstract;
using Booking.Domain.Concrete;

namespace Booking.Infrastructure.InMemory
{
    public class InMemoryHomeRepository : IHomeRepository
    {
        private static readonly IReadOnlyList<Home> _homes = Seed();
        public Task<IReadOnlyList<Home>> GetAllAsync(CancellationToken ct = default)
                                => Task.FromResult(_homes);
        private static IReadOnlyList<Home> Seed()
        {
            return new List<Home>
             {
                new Home(
                    id: "123",
                    name: "Home 1",
                    availableDates:
                    [
                        new DateOnly(2025,07,15), new DateOnly(2025,07,16), new DateOnly(2025,07,17)
                    ]
                ),
                new Home(
                        id: "456",
                        name: "Home 2",
                        availableDates:
                        [
                            new DateOnly(2025,07,17), new DateOnly(2025,07,18), new DateOnly(2025,07,19)
                        ]
                ),
                new Home(
                        id: "789",
                        name: "Home 3",
                        availableDates:
                        [
                            new DateOnly(2025,07,15), new DateOnly(2025,07,16)
                        ]
                )
            };


        }
    }
}
