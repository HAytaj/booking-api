using Booking.Application.Abstract;
using Booking.Application.Contracts;
using Booking.Domain.Utilities;
using System.Globalization;
using static Booking.Application.Contracts.AvailableHomesDtos;

namespace Booking.Application.Concrete
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IHomeRepository _repo;
        public AvailabilityService(IHomeRepository repo) => _repo = repo;
        public async Task<IReadOnlyList<HomeAvailabilityResult>> FindAvailableHomesAsync(
            DateOnly startDate, 
            DateOnly endDate, 
            CancellationToken ct)
        {
            if (endDate < startDate)
                throw new ArgumentException("EndDate must be greater than StartDate!");

            if (endDate.DayNumber - startDate.DayNumber > 366)
                throw new ArgumentException("Date range is too large (max 366 days)!");

            var requestedRange = DateRange.Enumerate(startDate, endDate).ToArray();
            var homes = await _repo.GetAllAsync(ct);


            var results = homes
            .AsParallel()
            .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
            .WithCancellation(ct)
            .Where(h => requestedRange.All(d => h.AvailableDates.Contains(d)))
            .Select(h => new HomeAvailabilityResult(
            h.Id,
            h.Name,
            requestedRange.Select(d => d.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)).ToList()))
            .ToList();


            return results;
        }
    }
}
