using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Booking.Application.Contracts.AvailableHomesDtos;

namespace Booking.Application.Abstract
{
    public interface IAvailabilityService
    {
        Task<IReadOnlyList<HomeAvailabilityResult>> FindAvailableHomesAsync(
            DateOnly startDate, 
            DateOnly endDate, 
            CancellationToken ct);
    }
}
