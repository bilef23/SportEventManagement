using SportEvents.Domain;
using SportEvents.Enum;

namespace Repository.Seed;

public class DbSeeder : IDbSeeder
{
    private readonly ApplicationDbContext _dbContext;

    public DbSeeder(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedTestData()
    {
        await SeedOrganizerData();
        await SeedEventData();
        await SeedTeamData();
        await SeedParticipantData();
        await SeedRegistrationData();
        await SeedTicketData();
       
    }

    private async Task SeedEventData()
    {
        if (!_dbContext.Events.Any())
        {
            var event1 = new Event
            {
                Id = Guid.NewGuid(),
                Name = "Marathon 2024",
                Description = "Annual city marathon event",
                Location = "Central Park",
                StartDate = DateTime.Now.AddMonths(1).ToUniversalTime(),
                EndDate = DateTime.Now.AddMonths(1).AddHours(5).ToUniversalTime(),
                EventType = EventType.Tournament,
                Status = "Scheduled",
                OrganizerId = _dbContext.Organizers.FirstOrDefault().Id
            };

            var event2 = new Event
            {
                Id = Guid.NewGuid(),
                Name = "Soccer Tournament",
                Description = "Local soccer teams competing",
                Location = "Soccer Field",
                StartDate = DateTime.Now.AddMonths(2).ToUniversalTime(),
                EndDate = DateTime.Now.AddMonths(2).AddDays(1).ToUniversalTime(),
                EventType = EventType.Tournament,
                Status = "Scheduled",
                OrganizerId = _dbContext.Organizers.FirstOrDefault().Id
            };
            await _dbContext.Events.AddAsync(event1);
            await _dbContext.Events.AddAsync(event2);
            await _dbContext.SaveChangesAsync();
        }
    }
    private async Task SeedOrganizerData()
    {
        if (!_dbContext.Organizers.Any())
        {
            var organizer1 = new Organizer
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                ContactEmail = "john@example.com",
                ContactPhone = "123-456-7890",
                Address = "123 Main St"
            };

            var organizer2 = new Organizer
            {
                Id = Guid.NewGuid(),
                Name = "Jane Smith",
                ContactEmail = "jane@example.com",
                ContactPhone = "098-765-4321",
                Address = "456 Elm St"
            };
            await _dbContext.Organizers.AddAsync(organizer1);
            await _dbContext.Organizers.AddAsync(organizer2);
            await _dbContext.SaveChangesAsync();
        }
    }
    private async Task SeedParticipantData()
    {
        if (!_dbContext.Participants.Any())
        {
            var participant1 = new Participant
            {
                Id = Guid.NewGuid(),
                FirstName = "Alice",
                LastName = "Johnson",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(1990, 5, 21).ToUniversalTime(),
                Email = "alice@example.com",
                PhoneNumber = "111-222-3333",
                TeamId = _dbContext.Teams.FirstOrDefault().Id
            };

            var participant2 = new Participant
            {
                Id = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Williams",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1988, 10, 12).ToUniversalTime(),
                Email = "bob@example.com",
                PhoneNumber = "444-555-6666",
                TeamId = _dbContext.Teams.FirstOrDefault().Id
            };
            await _dbContext.Participants.AddAsync(participant1);
            await _dbContext.Participants.AddAsync(participant2);
            await _dbContext.SaveChangesAsync();
        }
    }
    private async Task SeedRegistrationData()
    {
        if (!_dbContext.Registrations.Any())
        {
            var registration1 = new Registration
            {
                Id = Guid.NewGuid(),
                RegistrationDate = DateTime.Now.AddDays(100).ToUniversalTime(),
                Status = RegistrationStatus.Confirmed,
                EventId = _dbContext.Events.FirstOrDefault().Id,
                ParticipantId = _dbContext.Participants.FirstOrDefault().Id
            };

            var registration2 = new Registration
            {
                Id = Guid.NewGuid(),
                RegistrationDate = DateTime.Now.AddDays(-5).ToUniversalTime(),
                Status = RegistrationStatus.Confirmed,
                EventId = _dbContext.Events.FirstOrDefault().Id,
                ParticipantId = _dbContext.Participants.FirstOrDefault().Id
            };
            await _dbContext.Registrations.AddAsync(registration1);
            await _dbContext.Registrations.AddAsync(registration2);
            await _dbContext.SaveChangesAsync();
        }
    }
    private async Task SeedTeamData()
    {
        if (!_dbContext.Teams.Any())
        {
            var team1 = new Team
            {
                Id = Guid.NewGuid(),
                Name = "City Runners",
                CoachName = "Coach Carter",
                ContactEmail = "coach@example.com",
                ContactPhone = "222-333-4444",
                City = "New York"
            };

            var team2 = new Team
            {
                Id = Guid.NewGuid(),
                Name = "Soccer Stars",
                CoachName = "Coach Smith",
                ContactEmail = "smith@example.com",
                ContactPhone = "555-666-7777",
                City = "Boston"
            };
            await _dbContext.Teams.AddAsync(team1);
            await _dbContext.Teams.AddAsync(team2);
            await _dbContext.SaveChangesAsync();
        }
    }
    private async Task SeedTicketData()
    {
        if (!_dbContext.Tickets.Any())
        {
            var ticket1 = new Ticket
            {
                Id = Guid.NewGuid(),
                Price = 50.00m,
                SeatNumber = "A1",
                PurchaseDate = DateTime.Now.AddDays(-1).ToUniversalTime(),
                BuyerName = "Emily",
                BuyerEmail = "emily@example.com",
                EventId = _dbContext.Events.FirstOrDefault().Id
            };

            var ticket2 = new Ticket
            {
                Id = Guid.NewGuid(),
                Price = 30.00m,
                SeatNumber = "B2",
                PurchaseDate = DateTime.Now.ToUniversalTime(),
                BuyerName = "James",
                BuyerEmail = "james@example.com",
                EventId = _dbContext.Events.FirstOrDefault().Id
            };
            await _dbContext.Tickets.AddAsync(ticket1);
            await _dbContext.Tickets.AddAsync(ticket2);
            await _dbContext.SaveChangesAsync();
        }
    }
}