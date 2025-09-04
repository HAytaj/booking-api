using Booking.Domain.Concrete;

namespace Booking.Application.Abstract
{
    public interface IHomeRepository
    {
        Task<IReadOnlyList<Home>> GetAllAsync(CancellationToken ct = default);
    }
}
