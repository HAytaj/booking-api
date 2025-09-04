using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Concrete
{
    public class Home
    {
        public string Id { get; }
        public string Name { get; }
        public HashSet<DateOnly> AvailableDates { get; }


        public Home(string id, string name, IEnumerable<DateOnly> availableDates)
        {
            Id = id;
            Name = name;
            AvailableDates = [.. availableDates];
        }
    }
}
