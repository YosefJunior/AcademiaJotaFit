using AcademiaJotaFit.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace AcademiaJotaFit.Api.DTOs;

public class CadastroAlunoDto
{
    [Required(ErrorMessage = "Nome completo é obrigatório.")]
    [StringLength(Aluno.TamanhoMaximoNome, MinimumLength = Aluno.TamanhoMinimoNome, ErrorMessage = "O nome deve ter entre {2} e {1} caracteres.")]
    public string? NomeCompleto { get; set; }

    [Required(ErrorMessage = "A Data de nascimento é obrigatória.")]
    public DateOnly? DataNascimento { get; set; }

    [Required(ErrorMessage = "O Email é obrigatório.")]
    [StringLength(Aluno.TamanhoMaximoEmail, ErrorMessage = "O Endereço de Email deve ter até {1} caracteres.")]
    [EmailAddress(ErrorMessage = "Endereço de Email inválido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [StringLength(Aluno.TamanhoCpf, MinimumLength = Aluno.TamanhoCpf, ErrorMessage = "O CPF deve ter exatamente {1} dígitos.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "O CPF deve conter apenas números.")]
    public string? Cpf { get; set; }

    [Required(ErrorMessage = "A Altura é obrigatória.")]
    [Range(Aluno.AlturaMinima, Aluno.AlturaMaxima, ErrorMessage = "A Altura deve estar entre {1} e {2} cm.")]
    public int? Altura { get; set; }
}