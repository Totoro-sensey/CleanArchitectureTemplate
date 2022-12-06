using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.Controllers.Common
{
    [Route("/[controller]/[action]")]
    public abstract class ApiMediatorController : MediatorController
    {
    }
}