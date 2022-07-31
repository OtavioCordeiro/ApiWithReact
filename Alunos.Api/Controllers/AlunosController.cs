using Alunos.Api.Models;
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

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> Get()
        {
            var alunos = await _alunoService.GetAlunos();

            return Ok(alunos);
        }

        [HttpGet("AlunosPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetByName(string name)
        {
            var alunos = await _alunoService.GetAlunosByName(name);
            if (alunos is null || alunos.Count() == 0)
                return NotFound();

            return Ok(alunos);
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public async Task<ActionResult<Aluno>> GetById(int id)
        {
            var aluno = await _alunoService.GetAlunoById(id);
            if (aluno is null)
                return NotFound();

            return Ok(aluno);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            await _alunoService.CreateAluno(aluno);
            return CreatedAtRoute(nameof(GetById), new { id = aluno.Id }, aluno);
        }

        [HttpPut]
        public async Task<ActionResult> Edit(int id, Aluno aluno)
        {
            if (aluno.Id == id)
            {
                await _alunoService.UpdateAluno(aluno);
                return NoContent();
            }
            else
            {
                return BadRequest("Dados inconsistentes");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var aluno = await _alunoService.GetAlunoById(id);
            if (aluno != null)
            {
                await _alunoService.DeleteAluno(aluno);
                return Ok($"Aluno de id={id} foi excluido com sucesso");
            }
            else
            {
                return NotFound($"Aluno com id ={id} não encontrado");
            }
        }
    }
}
