using AcademiaJotaFit.Api.Data;
using AcademiaJotaFit.Api.DTOs;
using AcademiaJotaFit.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademiaJotaFit.Api.Services;

public class AlunoService
{
    private readonly JotaFitContext _context;

    public AlunoService(JotaFitContext context)
    {
        _context = context;
    }

    public async Task<Aluno> CadastrarAsync(CadastroAlunoDto dto)
    {
        var existe = await _context.Alunos.AnyAsync(a => a.Cpf == dto.Cpf);
        if (existe) throw new InvalidOperationException("Já existe cadastro para o CPF informado.");

        var aluno = new Aluno(dto.NomeCompleto, dto.DataNascimento.Value, dto.Email, dto.Cpf, dto.Altura.Value, dto.Peso.Value);
        _context.Alunos.Add(aluno);

        await _context.SaveChangesAsync();
        return aluno;
    }
}
