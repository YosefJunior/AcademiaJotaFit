using AcademiaJotaFit.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademiaJotaFit.Api.Data;

public class JotaFitContext : DbContext
{
    public DbSet<Aluno> Alunos { get; set; }

    public JotaFitContext(DbContextOptions<JotaFitContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Aluno>()
            .Property(aluno => aluno.NomeCompleto)
            .HasMaxLength(Aluno.TamanhoMaximoNome);

        modelBuilder.Entity<Aluno>()
            .Property(aluno => aluno.Email)
            .HasMaxLength(Aluno.TamanhoMaximoEmail);

        modelBuilder.Entity<Aluno>()
            .Property(aluno => aluno.Cpf)
            .HasMaxLength(Aluno.TamanhoCpf)
            .IsFixedLength();
    }
}