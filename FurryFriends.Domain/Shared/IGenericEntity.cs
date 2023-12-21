namespace FurryFriends.Domain.Shared;

public interface IGenericEntity<T>
{
    public T Id { get; set; }
}