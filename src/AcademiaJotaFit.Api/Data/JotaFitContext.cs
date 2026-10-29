using AcademiaJotaFit.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademiaJotaFit.Api.Data;

public class JotaFitContext : DbContext
{
    public DbSet<Aluno> Alunos { get; set; }

    public JotaFitContext(DbContextOptions<JotaFitContext> options) : base(options) { }
}