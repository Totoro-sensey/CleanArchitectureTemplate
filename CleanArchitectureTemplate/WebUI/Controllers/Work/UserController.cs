using Application.Work.Users.Commands.CreateUser;
using CleanArchitectureTemplate.Controllers.Common;
using Microsoft.AspNetCore.Mvc;
using CreateDto = Application.Work.Users.Commands.CreateUser.UserDto;

namespace CleanArchitectureTemplate.Controllers.Work;

public class UserController : ApiMediatorController
{
    /// <summary>
    /// Создание пользователя
    /// </summary>
    /// <param name="dto">Объект передачи данных для создания </param>
    /// <returns>Идентификатор новой записи</returns>
    [HttpPost]
    public async Task<long> Create([FromBody] CreateDto dto)
        => await Mediator.Send(new CreateUserCommand { Dto = dto });
}