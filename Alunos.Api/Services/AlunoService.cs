using Alunos.Api.Context;
using Alunos.Api.Models;
using Alunos.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Alunos.Api.Services
{
    public class AlunoService : IAlunoService
    {
        AppDbContext _context;

        public AlunoService(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByName(string nome)
        {
            IEnumerable<Aluno> alunos;
            if (!string.IsNullOrEmpty(nome))
            {
                alunos = await _context.Alunos.Where(n => n.Nome.Contains(nome)).ToListAsync();
            }
            else
            {
                alunos = await GetAlunos();
            }

            return alunos;
        }

        public async Task<Aluno> GetAlunoById(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            return aluno;
        }

        public async Task CreateAluno(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAluno(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAluno(Aluno aluno)
        {
            _context.Remove(aluno);
            await _context.SaveChangesAsync();
        }
    }
}
