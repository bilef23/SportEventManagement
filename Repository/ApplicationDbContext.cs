using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportEvents.Domain;

namespace Repository;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Organizer> Organizers { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}