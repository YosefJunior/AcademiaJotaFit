namespace AcademiaJotaFit.Api.Models;

public class Aluno
{
    public int Id { get; private set; }
    public string NomeCompleto { get; set; }
    public DateOnly DataNascimento { get; private set; }
    public int Idade
    {
        get
        {
            var hoje = DateOnly.FromDateTime(DateTime.Today);
            var idade = hoje.Year - DataNascimento.Year;

            if (DataNascimento.AddYears(Idade) > hoje)
                idade--;
            return idade;
        }
    }
    public string Email { get; set; }
    public string Cpf { get; private set; }
    public int Altura { get; set; } // Altura em centímetros Ex: 175cm
    public double Peso { get; set; } // Peso em quilogramas Ex: 80kg

    public Aluno(string nomeCompleto, DateOnly dataNascimento, string email, string cpf, int altura, double peso)
    {
        NomeCompleto = nomeCompleto;
        DataNascimento = dataNascimento;
        Email = email;
        Cpf = cpf;
        Altura = altura;
        Peso = peso;
    }
}
