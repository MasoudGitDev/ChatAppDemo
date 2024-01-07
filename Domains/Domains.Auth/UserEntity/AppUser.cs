using Microsoft.AspNetCore.Identity;
using Shared.ValueObjects;
namespace Domains.Auth.UserEntity;
public class AppUser : IdentityUser<EntityId>{
}
