using FurryFriends.Domain;
using Microsoft.EntityFrameworkCore;

namespace FurryFriends.Persistence;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    #region DbSet Properties
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<PersonType> PersonTypes { get; set; }
    public virtual DbSet<PersonRole> PersonRoles { get; set; }
    public virtual DbSet<Person> Persons { get; set; }
    public virtual DbSet<Contact> Contacts { get; set; }
    public virtual DbSet<Appointment> Appointments { get; set; }
    public virtual DbSet<AppointmentReason> AppointmentReasons { get; set; }
    public virtual DbSet<AppointmentStatus> AppointmentStatutes { get; set; }
    public virtual DbSet<Shelter> Shelters { get; set; }
    public virtual DbSet<Breed> Breeds { get; set; }
    public virtual DbSet<AnimalType> AnimalTypes { get; set; }
    public virtual DbSet<Animal> Animals { get; set; }
    public virtual DbSet<Location> Locations { get; set; }
    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }
    public virtual DbSet<Adoption> Adoptions { get; set; }
    #endregion

    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    #region Entities configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MedicalRecord>(entity =>
        entity.HasOne(x => x.Person)
                    .WithMany(x => x.MedicalRecords)
                    .HasForeignKey(x => x.VeterinaryId));
    }
    #endregion
}