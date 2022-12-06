using Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity.Entities
{
    /// <summary>
    /// Роль в системе
    /// </summary>
    public class ApplicationRole : IdentityRole, IEntityWithId<string>
    {
        
    }
}
