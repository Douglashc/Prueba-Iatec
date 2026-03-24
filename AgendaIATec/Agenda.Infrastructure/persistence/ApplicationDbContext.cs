using System.Text;
using Microsoft.EntityFrameworkCore;
using Agenda.Domain.Entities;

namespace Agenda.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Invitation> Invitations { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>().Property(e => e.Name).IsRequired().HasMaxLength(100);

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin", Email = "douglash.dcz@gmail.com", Password = Convert.ToBase64String(Encoding.UTF8.GetBytes("admin123")), Alias = "Admin" },
            new User { Id = 2, Username = "juan", Email = "juan@gmail.com", Password = Convert.ToBase64String(Encoding.UTF8.GetBytes("juan123")), Alias = "John" },
            new User { Id = 3, Username = "maria", Email = "maria@gmail.com", Password = Convert.ToBase64String(Encoding.UTF8.GetBytes("maria123")), Alias = "Maria" },
            new User { Id = 4, Username = "carlos", Email = "carlos@gmail.com", Password = Convert.ToBase64String(Encoding.UTF8.GetBytes("carlos123")), Alias = "Carlos" }
        );

        modelBuilder.Entity<Event>().HasData(


            // EN CURSO 
            new Event { Id = 1, Name = "Sprint Review", Description = "Revisión del sprint actual", StartDate = DateTime.Now.AddHours(-2), EndDate = DateTime.Now.AddHours(2), Location = "Oficina", Type = EventType.Exclusive, CreatorId = 1 },
            new Event { Id = 2, Name = "Reunión técnica", Description = "Discusión de arquitectura", StartDate = DateTime.Now.AddHours(-1), EndDate = DateTime.Now.AddHours(3), Location = "Zoom", Type = EventType.Shared, CreatorId = 1 },

            // FUTUROS 
            new Event { Id = 3, Name = "Partido Bolivia vs Surinam", Description = "Repechaje mundialista", StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(1).AddHours(2), Location = "Estadio Hernando Siles", Type = EventType.Shared, CreatorId = 1 }, // compartido
            new Event { Id = 4, Name = "Demo al cliente", Description = "Presentación del sistema", StartDate = DateTime.Now.AddDays(2), EndDate = DateTime.Now.AddDays(2).AddHours(1), Location = "Oficina", Type = EventType.Exclusive, CreatorId = 1 },
            new Event { Id = 5, Name = "Entrenamiento personal", Description = "Rutina gym", StartDate = DateTime.Now.AddDays(3), EndDate = DateTime.Now.AddDays(3).AddHours(2), Location = "Gym", Type = EventType.Exclusive, CreatorId = 1 },
            new Event { Id = 6, Name = "Cena con amigos", Description = "Salida social", StartDate = DateTime.Now.AddDays(4), EndDate = DateTime.Now.AddDays(4).AddHours(3), Location = "Restaurante", Type = EventType.Shared, CreatorId = 1 }, // compartido

            // PASADOS
            new Event { Id = 7, Name = "Curso de Angular", Description = "Capacitación frontend", StartDate = DateTime.Now.AddDays(-5), EndDate = DateTime.Now.AddDays(-5).AddHours(3), Location = "Centro TI", Type = EventType.Exclusive, CreatorId = 1 },
            new Event { Id = 8, Name = "Partido Bolivia vs Perú", Description = "Amistoso internacional", StartDate = DateTime.Now.AddDays(-3), EndDate = DateTime.Now.AddDays(-3).AddHours(2), Location = "Estadio", Type = EventType.Shared, CreatorId = 1 }, // compartido
            new Event { Id = 9, Name = "Deploy producción", Description = "Liberación versión 1.0", StartDate = DateTime.Now.AddDays(-2), EndDate = DateTime.Now.AddDays(-2).AddHours(1), Location = "Servidor", Type = EventType.Exclusive, CreatorId = 1 },



            // EN CURSO 
            new Event { Id = 10, Name = "Daily Scrum", Description = "Reunión diaria", StartDate = DateTime.Now.AddHours(-1), EndDate = DateTime.Now.AddHours(1), Location = "Teams", Type = EventType.Exclusive, CreatorId = 2 },
            new Event { Id = 11, Name = "Debug sesión", Description = "Resolución de bugs", StartDate = DateTime.Now.AddHours(-2), EndDate = DateTime.Now.AddHours(2), Location = "Oficina", Type = EventType.Shared, CreatorId = 2 },

            // FUTUROS 
            new Event { Id = 12, Name = "Planificación sprint", Description = "Organización tareas", StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(1).AddHours(2), Location = "Zoom", Type = EventType.Exclusive, CreatorId = 2 },
            new Event { Id = 13, Name = "Partido fútbol", Description = "Partido entre amigos", StartDate = DateTime.Now.AddDays(2), EndDate = DateTime.Now.AddDays(2).AddHours(2), Location = "Cancha", Type = EventType.Shared, CreatorId = 2 },
            new Event { Id = 14, Name = "Cena familiar", Description = "Reunión familiar", StartDate = DateTime.Now.AddDays(3), EndDate = DateTime.Now.AddDays(3).AddHours(3), Location = "Casa", Type = EventType.Exclusive, CreatorId = 2 },
            new Event { Id = 15, Name = "Revisión código", Description = "Code review", StartDate = DateTime.Now.AddDays(4), EndDate = DateTime.Now.AddDays(4).AddHours(1), Location = "GitHub", Type = EventType.Shared, CreatorId = 2 },

            // PASADOS
            new Event { Id = 16, Name = "Deploy hotfix", Description = "Corrección urgente", StartDate = DateTime.Now.AddDays(-4), EndDate = DateTime.Now.AddDays(-4).AddHours(1), Location = "Servidor", Type = EventType.Exclusive, CreatorId = 2 },
            new Event { Id = 17, Name = "Reunión cliente", Description = "Feedback sistema", StartDate = DateTime.Now.AddDays(-3), EndDate = DateTime.Now.AddDays(-3).AddHours(2), Location = "Oficina", Type = EventType.Shared, CreatorId = 2 },
            new Event { Id = 18, Name = "Testing QA", Description = "Pruebas del sistema", StartDate = DateTime.Now.AddDays(-2), EndDate = DateTime.Now.AddDays(-2).AddHours(2), Location = "QA Lab", Type = EventType.Exclusive, CreatorId = 2 }
        );

        modelBuilder.Entity<Invitation>().HasData(

            // USER 1 → USER 2
            new Invitation { Id = 1, EventId = 2, ReceiverId = 2, Status = InvitationStatus.Accepted },
            new Invitation { Id = 2, EventId = 3, ReceiverId = 2, Status = InvitationStatus.Pending },
            new Invitation { Id = 3, EventId = 6, ReceiverId = 2, Status = InvitationStatus.Accepted },
            new Invitation { Id = 4, EventId = 8, ReceiverId = 2, Status = InvitationStatus.Rejected },

            // USER 2 → USER 1
            new Invitation { Id = 5, EventId = 11, ReceiverId = 1, Status = InvitationStatus.Accepted },
            new Invitation { Id = 6, EventId = 13, ReceiverId = 1, Status = InvitationStatus.Pending },
            new Invitation { Id = 7, EventId = 15, ReceiverId = 1, Status = InvitationStatus.Accepted },
            new Invitation { Id = 8, EventId = 17, ReceiverId = 1, Status = InvitationStatus.Rejected }
        );
    }
}