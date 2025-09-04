namespace Booking.Application.Contracts
{
    public class AvailableHomesDtos
    {
        public sealed record HomeAvailabilityResult(string HomeId, string HomeName, List<string> AvailableSlots);
        public sealed record AvailableHomesResponse(string Status, IReadOnlyList<HomeAvailabilityResult> Homes);
    }
}
