using FurryFriends.Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace FurryFriends.Domain;

public class Person : IGenericEntity<int>
{
    [Key] public int Id { get; set; }
    public int PersonTypeId { get; set; }
    public int PersonRoleId { get; set; }
    public int ContactId { get; set; }
	public string IdentityCard {  get; set; }
    public string Name { get; set; }
	public string Surname { get; set; }
	public DateTime Birthday { get; set; }
	public string Gender { get; set; }
    public bool Status { get; set; }

	public virtual PersonType PersonType { get; set; }
	public virtual PersonRole PersonRole { get; set; }
    public virtual Contact Contact { get; set; }

    public ICollection<User> Users { get; set; } = new HashSet<User>();
    public ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new HashSet<MedicalRecord>();
    public virtual ICollection<Adoption> Adoptions { get; set; } = new HashSet<Adoption>();
}