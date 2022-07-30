using Alunos.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alunos.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }
    }
}
