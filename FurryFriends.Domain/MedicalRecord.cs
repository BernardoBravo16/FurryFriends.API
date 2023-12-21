using FurryFriends.Domain.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurryFriends.Domain;

public class MedicalRecord : IGenericEntity<int>
{
    [Key] public int Id { get; set; }
    public int AnimalId { get; set; }
	public int VeterinaryId { get; set; }
	public DateTime Treatment_Date { get; set; }
	public string Diagnosis { get; set; }
	public string Treatment { get; set; }

	public virtual Animal Animal { get; set; }
	public virtual Person Person { get; set; }
}