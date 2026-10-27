namespace AcademiaJotaFit.Api.Models;

public class Aluno
{
    private const int AlturaMaxima = 300;
    private const int AlturaMinima = 0;
    private const double PesoMaximo = 600;
    private const double PesoMinimo = 0;

    public int Id { get; private set; }
    public string NomeCompleto
    {
        get;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("O campo 'Nome' precisa estar preenchido.");
            }
            field = value.Trim();
        }
    }
    public DateOnly DataNascimento
    {
        get;
        private set
        {
            var hoje = DateOnly.FromDateTime(DateTime.Today);

            if (value > hoje)
            {
                throw new ArgumentOutOfRangeException(nameof(DataNascimento), $"Não pode ser maior que a data atual: {hoje}");
            }
            field = value;
        }
    }
    public int Idade
    {
        get
        {
            var hoje = DateOnly.FromDateTime(DateTime.Today);
            var idade = hoje.Year - DataNascimento.Year;

            if (DataNascimento.AddYears(idade) > hoje)
                idade--;
            return idade;
        }
    }
    public string Email
    {
        get;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("O campo 'Email' não pode ser vazio.");
            }
            field = value.ToLowerInvariant().Trim();
        }
    }
    public string Cpf
    {
        get;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("O campo 'CPF' não pode ser vazio.");
            }
            field = value.Trim();
        }
    }
    public int Altura // Altura em centímetros Ex: 175cm
    {
        get;
        private set
        {
            ValidarAltura(value);
            field = value;
        }
    }
    public double Peso // Peso em quilogramas Ex: 80kg
    {
        get;
        private set
        {
            ValidarPeso(value);
            field = value;
        }
    }
    public Aluno(string nomeCompleto, DateOnly dataNascimento, string email, string cpf, int altura, double peso)
    {
        NomeCompleto = nomeCompleto;
        DataNascimento = dataNascimento;
        Email = email;
        Cpf = cpf;
        Altura = altura;
        Peso = peso;
    }

    public void AtualizarNome(string nomeCompleto)
    {
        NomeCompleto = nomeCompleto;
    }

    public void AtualizarEmail(string email)
    {
        Email = email;
    }

    public void AtualizarMedidas(int altura, double peso)
    {
        ValidarAltura(altura);
        ValidarPeso(peso);

        Altura = altura;
        Peso = peso;
    }

    private static void ValidarAltura(int altura)
    {
        if (altura <= AlturaMinima || altura >= AlturaMaxima)
        {
            throw new ArgumentOutOfRangeException(nameof(Altura), $"O campo 'Altura' deve ser superior a {AlturaMinima}cm e inferior a {AlturaMaxima}cm");
        }
    }

    private static void ValidarPeso(double peso)
    {
        if (peso <= PesoMinimo || peso >= PesoMaximo)
        {
            throw new ArgumentOutOfRangeException(nameof(Peso), $"O campo 'Peso' deve ser superior a {PesoMinimo}kg e inferior a {PesoMaximo}kg");
        }
    }
}