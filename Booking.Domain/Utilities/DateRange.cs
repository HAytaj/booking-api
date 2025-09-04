using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Utilities
{
    public static class DateRange
    {
        public static IEnumerable<DateOnly> Enumerate(DateOnly start, DateOnly end)
        {
            if (end < start) yield break;
            for (var d = start; d <= end; d = d.AddDays(1))
                yield return d;
        }
    }
}
