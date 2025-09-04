# Booking API (In-Memory, .NET 8)

This project is a simple booking API where users can check which homes are available for a given date range.  
Data is stored in-memory (no database), and the focus is on clean architecture, filtering logic, and performance.

---

## How to run

```bash
cd Booking.Api
dotnet run


## Example request (once running):

GET http://localhost:5xxx/api/available-homes?startDate=2025-07-15&endDate=2025-07-16



##How to test

dotnet test

This runs integration tests that start the API in memory and verify the filtering logic.



##️ Architecture

Domain → entities (Home etc.) and date helper logic (no dependencies).

Application → interfaces, DTOs, and the AvailabilityService that does the filtering.

Infrastructure → in-memory repository with some seeded data.

API → controllers + dependency injection setup. Controllers don’t contain business logic.




##️ Filtering logic

Read startDate and endDate (YYYY-MM-DD).

Build a complete date range between them.

A home is included only if it has all dates in that range.

The response includes the matching homes with the available slots.


##Performance choices

In-memory data store (fast, lightweight).

HashSet<DateOnly> used for O(1) lookups.

Parallelized filtering (AsParallel) with cancellation support.

Guard clauses for large ranges.




## Possible extensions

Replace the seed data with dynamic loaders.

Add more tests (bad input, empty results, long ranges).

Propagate CancellationToken across layers.


![.NET CI](https://github.com/<YourUsername>/<YourRepoName>/actions/workflows/ci.yml/badge.svg)
