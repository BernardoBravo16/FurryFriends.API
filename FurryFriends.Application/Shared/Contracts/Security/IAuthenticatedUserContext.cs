using FurryFriends.Domain.Enum;

namespace FurryFriends.Application.Shared.Contracts.Security;

public interface IAuthenticatedUserContext
{
    public int AuthUserId { get; set; }
    public string AuthUsername { get; set; }
    public string AuthEmail { get; set; }
    public RoleEnum AuthRole { get; set; }
    public int AuthPersonId { get; set; }
    public PersonTypeEnum AuthPersonType { get; set; }
}