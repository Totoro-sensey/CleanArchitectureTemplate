using Application.Mappings;
using Domain.Entities;

namespace Application.Work.Users.Commands.CreateUser;

public class UserDto : IMapTo<Office>
{
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
}
