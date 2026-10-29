# HANDOFF — Academia JotaFit

Última atualização: 29/10/2026

## Onde paramos

Branch `feature/aluno-ef-core`. O `set` do `Cpf` em `Models/Aluno.cs` foi reescrito com a estratégia B e **funciona**: compila e passa no teste de mesa (pontuado aceito; letras, tamanho errado e só formatação recusados). Falta só o acabamento de três detalhes (ver Próximos passos) e o commit.

## O que já foi feito

- Solution `.slnx` e projeto `AcademiaJotaFit.Api` (.NET 10, com controllers); exemplo `WeatherForecast` removido.
- Pastas `Controllers`, `Data`, `DTOs`, `Models`, `Services` criadas.
- Repositório no GitHub com `.gitignore`; commits com e-mail noreply do GitHub.
- Modelo de domínio definido (ver `CLAUDE.md`).
- `CLAUDE.md`, `HANDOFF.md` e skills de estudo em `.claude/skills/`.
- Histórico do Git reescrito para remover dados pessoais de uma versão antiga do `HANDOFF.md`; repositório recriado no GitHub.
- Entidade `Aluno` criada, com validações no `set` de cada propriedade:
  - `NomeCompleto`, `Email`, `Cpf`: não podem ser vazios ou só espaços; espaços das pontas removidos.
  - `Email` guardado em minúsculas.
  - `DataNascimento`: não pode estar no futuro.
  - `Altura` (0–300 cm) e `Peso` (0–600 kg), com limites em constantes.
  - Corrigido bug de recursão infinita no cálculo da `Idade`.
- Métodos de alteração no `Aluno` (PR #2 na `main`):
  - `AtualizarNome` e `AtualizarEmail`: só atribuem à propriedade; a validação do `set` cuida do resto.
  - Validação de altura e peso extraída para `ValidarAltura` e `ValidarPeso` (`private static`); os `set` chamam esses métodos.
  - `AtualizarMedidas(altura, peso)` é "tudo ou nada": valida os dois antes de gravar qualquer um.
- PR #1 da mesma branch levou só um commit de handoff (ficou no histórico; não vale reescrever).
- Instância `MSSQLLocalDB` do LocalDB criada e rodando (SQL Server 17.0 / 2025), com `sqllocaldb start`.
- Pacotes `Microsoft.EntityFrameworkCore.SqlServer` e `.Design` (10.0.14) e ferramenta global `dotnet-ef` (10.0.14) instalados.
- Aviso de vulnerabilidade do `Pkcs` analisado e registrado como decisão (commit "Registra decisão sobre aviso de vulnerabilidade").
- `Data/JotaFitContext.cs`: herda de `DbContext`, tem `DbSet<Aluno> Alunos` e um construtor que recebe `DbContextOptions<JotaFitContext>` e repassa com `: base(options)` (commit "Cria JotaFitContext").
- `appsettings.json`: grupo `ConnectionStrings` com a conexão `JotaFit` (LocalDB, banco `AcademiaJotaFit`, autenticação do Windows) (commit "Configura conexão com o banco no appsettings").
- `Program.cs`: lê a connection string com `GetConnectionString("JotaFit")` e registra o contexto com `AddDbContext` + `UseSqlServer` numa lambda (commit "Registra JotaFitContext no Program.cs").
- Primeira migration gerada, lida e descartada (nada foi aplicado no banco; o banco ainda não existe).
- `set` do `Cpf` na ordem (c) não vazio → (d) remove ponto, traço e espaço numa variável `cpfLimpo` → (e) `foreach` que barra caractere que não é dígito → (b) tamanho diferente de `TamanhoCpf` → (a) grava o `cpfLimpo`. Constante `TamanhoCpf` (`int`) criada; `Trim` removido (o passo (d) já tira os espaços).

## Decisões tomadas

- Benefícios do plano como propriedades do Plano (opção A): simples para o estudo.
- Matrícula guarda data de cancelamento (`DateOnly?`); status é calculado: evita dado duplicado.
- Renovação = nova matrícula: mantém o histórico.
- Ordem de desenvolvimento: Aluno → Plano → Matrícula, uma por vez de ponta a ponta.
- Entidades ricas: todas as propriedades com `private set`; mudanças só por métodos públicos.
- Validação **dentro do `set`** (C# 14, palavra-chave `field`): a regra fica num lugar só e o construtor e os métodos passam por ela automaticamente.
- Espaços das pontas são **limpos** (`Trim`), não barrados: não pune erro de digitação e mantém a busca funcionando.
- E-mail em minúsculas com `ToLowerInvariant`: e-mail não diferencia maiúsculas; a versão Invariant não depende do idioma do computador.
- Nome guardado como digitado: não existe regra automática de maiúsculas que acerte todos os nomes.
- Teto do peso em 600 kg: serve para pegar erro de digitação sem recusar pessoas reais.
- Data Annotations (`[Required]`, `[Range]`) ficam nos **DTOs**, não na entidade: elas só funcionam quando o ASP.NET as lê.
- Aluno: `Id`, `Cpf` e `DataNascimento` não mudam; nome e e-mail com métodos separados; altura e peso juntos em `AtualizarMedidas`.
- Métodos de alteração usam o verbo **`Atualizar`** em todos: consistência, quem usa a classe aprende um verbo só.
- Parâmetros dos métodos na mesma ordem do construtor (altura, peso): `int` vira `double` sem aviso, então inverter a ordem trocaria os valores em silêncio.
- `AtualizarMedidas` atômico (valida tudo, depois grava tudo): evita aluno "pela metade" (altura nova com peso antigo).
- Validações de altura e peso em métodos `private static`: só usam o parâmetro e as constantes, não dependem de dados do aluno.
- A dupla checagem (no `Validar...` e de novo no `set`) é aceita: o `set` protege o construtor e qualquer outro caminho.
- Commits de correção ficam no histórico: mostram a evolução e são prática normal; só se reescreve histórico por motivo real (ex.: dados pessoais).
- Nomes de branch: `feature/` ou `fix/` + entidade + assunto, minúsculo, com hífen e sem acento.
- Mudança só de documentação (handoff) vai junto no PR do código, não num PR próprio.
- Linhas `<Folder Include>` que o Visual Studio põe no `.csproj` para pastas vazias são descartadas (`git restore`).
- Banco: SQL Server/LocalDB. SQLite foi considerado e descartado: não tem `decimal` (não ordena nem compara dinheiro em consultas) e lida mal com acentos na busca.
- Connection string do LocalDB usa autenticação do Windows (sem senha), então pode ficar no `appsettings.json`. Segredos de verdade iriam para User Secrets / variáveis de ambiente.
- Contexto do EF Core: classe `JotaFitContext`, em `Data/`, namespace `AcademiaJotaFit.Api.Data`. Não pode se chamar `DbContext`, porque colidiria com o nome da classe base.
- Por enquanto, um único `DbSet` (o de `Aluno`): `Plano` e `Matricula` ainda não existem, e cada entidade ganha o seu `DbSet` quando for criada.
- A propriedade `DbSet` usa `get` e `set` (convenção): o EF Core preenche a propriedade ao criar o contexto.
- Opções do banco chegam pelo construtor (`DbContextOptions<JotaFitContext>`) e são repassadas com `: base(...)`: quem decide o banco é o `Program.cs`, não a classe.
- Um único `JotaFitContext` para o projeto inteiro: cada entidade nova ganha só uma linha `DbSet` nele. Um banco, um gerente; as entidades se relacionam e são salvas juntas.
- Nada de código comentado nos commits (as linhas `Planos` e `Matriculas` entram quando as entidades existirem).
- Connection string chamada `JotaFit`; leitura numa variável separada antes do `AddDbContext`: cada linha faz uma coisa.
- Migration só é descartada (`migrations remove`) se ainda não foi aplicada nem compartilhada; depois disso, corrige-se com uma migration nova.
- CPF guardado **só com números** (11 caracteres): pontuado e sem pontuação viram o mesmo valor, e o índice único pega a duplicata. A formatação é só para exibição.
- A limpeza do CPF fica no `set` da entidade, não no DTO: a entidade está em todos os caminhos.
- Estratégia B para limpar o CPF: remover só a formatação conhecida (ponto, traço, espaço) e depois exigir só números. A estratégia A (remover tudo que não é número) aceitaria letras digitadas por engano em silêncio.
- Tamanho do CPF numa constante só (ex.: `TamanhoCpf`), porque o mínimo é igual ao máximo; a mensagem fica "exatamente 11 dígitos".
- No banco, o CPF terá índice **único**, além da checagem no Service: é a última linha de defesa contra cadastros simultâneos.
- Ordem do `set` do CPF: limpar (d) antes de conferir dígitos (e) e tamanho (b), senão um CPF pontuado (14 caracteres, com `.` e `-`) seria recusado; (e) antes de (b) para que `"abc"` receba a mensagem que explica o problema real.
- Vírgula **não** é removida do CPF: não faz parte do formato; se aparecer, é erro de digitação e deve ser avisado (estratégia B).
- Sem Regex por enquanto: os métodos de `string` e o `foreach` resolvem, e cada passo fica legível.
- A entidade não escreve no console (`Console.Write`): ela não sabe se existe tela; testes reais virão pelo Postman.
- `using` sugerido pelo Visual Studio que não se reconhece (ex.: `Microsoft.IdentityModel.Tokens`) é sinal de método errado: desconfiar e apagar.
- Aviso de vulnerabilidade do `System.Security.Cryptography.Pkcs` 9.0.11 (transitivo, vem do SqlServer; `GHSA-846f-f764-76mx` e `GHSA-2fmv-rv2c-59x7`) **ignorado por enquanto**: a API só roda local e não recebe certificados nem assinaturas de fora. Rever quando atualizar os pacotes do EF Core.

## Conceitos aprendidos

- Recursão infinita: propriedade ou método que chama a si mesmo → `StackOverflowException`.
- Guard clause: `if` no início que barra valor inválido com `throw`.
- Exceções: `ArgumentException` (valor inválido), `ArgumentOutOfRangeException` (fora da faixa), `ArgumentNullException`.
- Sobrecargas: `ArgumentOutOfRangeException(string)` recebe o **nome do parâmetro**, não a mensagem.
- `nameof(...)`: pega o nome de algo do código como texto.
- No `set`: `value` é o valor que chega; `field` é onde guardar. `value` só existe dentro do `set`.
- Auto-property vs. propriedade com corpo.
- Método estático (`string.IsNullOrWhiteSpace(texto)`) vs. método de instância (`texto.Trim()`); método `static` não usa dados do objeto, só parâmetros e constantes.
- Strings são imutáveis: métodos devolvem um texto novo, que precisa ser guardado.
- Tipos de valor (`int`, `double`, `DateOnly`) nunca são `null`; métodos de `string` não servem para eles.
- Escopo: variável ou parâmetro criado dentro de `{ }` só existe ali; o mesmo valor muda de nome em cada lugar.
- Constantes (`const`) e números mágicos; interpolação de string (`$"...{x}..."`).
- Mensagem de erro deve descrever exatamente o que o código verifica (não "Digite...": a entidade não sabe se existe tela).
- `.gitignore` só afeta arquivos que o Git ainda não acompanha.
- Reescrever histórico muda os IDs dos commits; publicar exige force push e novo clone nos outros computadores.
- `get` lê o valor; `set` grava. `private set` impede gravar de fora da classe, mas métodos da própria classe podem.
- Assinatura de método: visibilidade (`public`/`private`), retorno (`void` = não devolve nada), nome em verbo, parâmetros.
- Parâmetros em camelCase; parâmetro com o mesmo nome de propriedade ou constante a **esconde** dentro do método.
- `return;` em método `void` só serve para sair mais cedo.
- Declarar método (escrever a receita, uma vez, solto na classe) × chamar método (`Nome(valor);`, onde precisar).
- Extrair método: tirar um trecho de código de um lugar e dar um nome a ele para reaproveitar.
- Chamar método é desviar e voltar para a linha seguinte; `throw` interrompe tudo e não volta.
- Exceção é **lançada**, não devolvida.
- Atomicidade ("tudo ou nada"): conferir tudo antes, gravar tudo depois; validação só protege se vier antes.
- Teste de mesa: seguir valores concretos linha a linha para achar o erro.
- Conversão implícita: `int` vira `double` sem aviso.
- Data Annotations são etiquetas: só funcionam quando alguém as lê (ASP.NET, EF Core).
- PR fechado não recebe commits novos; um push depois do merge pede um PR novo.
- Descrição de PR aceita Markdown (lista com `-`, código entre crases).
- `git stash` guarda mudanças numa "gaveta"; `pop` aplica e apaga a cópia, `apply` aplica e mantém.
- `git switch` só leva mudanças não commitadas se o arquivo for igual nas duas branches.
- Configuração em camadas: `appsettings.json` → `appsettings.Development.json` → User Secrets → variáveis de ambiente; a última lida vence.
- LocalDB: instância automática só é criada no primeiro uso; o nome do pipe muda a cada início (usar `(localdb)\MSSQLLocalDB`).
- Pacotes NuGet: o `.csproj` é a lista; o restore baixa sozinho ao compilar. Ferramenta global (`dotnet-ef`) não entra no `.csproj`.
- `PrivateAssets` = pacote só de desenvolvimento, não repassado adiante.
- Dependência transitiva: pacote que vem pela corrente; o NuGet escolhe a menor versão que atende o pedido.
- Visual Studio esconde pastas vazias; botão "Mostrar Todos os Arquivos" (pode estar no "⋯").
- `DbContext` = gerente da conexão (o "arquivo de aço"); entidade = o que é guardado (a "ficha"). O contexto não herda da entidade.
- `DbSet<T>` = uma "gaveta"/tabela; nome no plural. Adicionar ou alterar dados é feito por métodos do `DbSet` (ex.: `Add`), não pelo `set` da propriedade. O `set` só troca a gaveta inteira.
- Herança com repasse: `class Filho : Pai` e `public Filho(x) : base(x)`, em que o construtor do filho entrega o valor ao construtor do pai.
- Tipo genérico: o que vai entre `< >` diz o que tem dentro (`List<string>`, `DbSet<Aluno>`).
- Construtor do EF Core: o EF consegue usar o construtor com parâmetros quando os nomes dos parâmetros batem com os das propriedades (`nomeCompleto` ↔ `NomeCompleto`).
- Montar uma classe: descrever em português antes (é um? tem? recebe para nascer? faz?) e escrever na ordem do arquivo: `using` → `namespace` → classe → constantes → propriedades → construtor → métodos.
- Reconhecer pelo formato: propriedade `public TIPO Nome { get; set; }` (sem parênteses); construtor `public NomeDaClasse(TIPO param)` (sem retorno); método `public RETORNO Verbo(TIPO param)`.
- Parâmetro = tipo + nome; dentro do `base( )` vai o nome do parâmetro (o valor), não o nome de uma classe.
- Método genérico: `Nome<Tipo>()`, na ordem nome → `< >` → `( )`. Os parênteses são o "execute agora" e vêm por último.
- Pasta/namespace (`Data`) organiza arquivos; `builder.Services` é a lista de serviços. Todo registro é `builder.Services.Add...`.
- `Program.cs`: antes do `builder.Build()` monta-se o "cardápio" de serviços; depois, o caminho das requisições.
- Injeção de dependência: quem precisa de um objeto **pede** ao ASP.NET em vez de criar; só é entregue o que foi registrado.
- JSON: pares `"chave": valor`, separados por vírgula (o último sem vírgula); `\` vira `\\`.
- Connection string: pares `Chave=valor;` com `Server`, `Database` e `Trusted_Connection`; `(localdb)` é palavra fixa. O grupo precisa se chamar exatamente `ConnectionStrings`.
- `builder.Configuration.GetConnectionString("Nome")` lê o valor final das camadas de configuração.
- Lambda: mini-método sem nome, `parametro => o que fazer`; é um "bilhete de instruções" que o ASP.NET segue quando precisar.
- Migration = "commit do banco": `migrations add` escreve a receita (não mexe no banco); `database update` aplica. `Up` aplica, `Down` desfaz. Gera três arquivos (receita, `.Designer`, `ModelSnapshot`); o número no nome é data e hora.
- Convenções do EF: propriedade `Id` vira chave primária com `Identity`; propriedade sem `set` (ex.: `Idade`) não vira coluna; `double` → `float`, `DateOnly` → `date`; `string` sem limite → `nvarchar(max)`.
- `nvarchar(max)` não aceita índice único; regra de negócio (CPF único) o EF não adivinha, precisa ser configurada.
- Mensagem de commit no imperativo: completa a frase "este commit vai...".
- Identificador só com dígitos (CPF, telefone, CEP) é `string`, não número: não se faz conta com ele, `int` não comporta 11 dígitos e zeros à esquerda sumiriam.
- `if` só para **conferir** (pode reprovar → `throw`); passo que só **transforma** (ex.: `Replace`) não tem `if`.
- `Replace(procurar, colocar)`: troca um pedaço do texto; com `""` remove. Recebe só 2 argumentos.
- Encadear métodos: chamar o próximo método direto no resultado do anterior (`.Replace(...).Replace(...)`), porque cada um devolve uma `string`.
- Nome de variável é substantivo (o que ela guarda, ex.: `cpfLimpo`); verbo é para método.
- `string` é uma sequência de `char`; `char` é um caractere só, entre aspas simples (`'1'`).
- `foreach (var x in sequencia)`: `x` é a **variável do laço**, recebe um item por volta e só existe dentro das chaves do laço.
- `char.IsDigit(c)`: método estático do tipo `char` que responde se o caractere é de `'0'` a `'9'`.
- Guard clause dentro do `foreach`: o `throw` para tudo no primeiro item inválido; se o laço terminar, todos passaram. Texto vazio = laço com 0 voltas (não reprova nada).
- `texto.Length`: propriedade (sem parênteses) com o número de caracteres. `!=` = "diferente de".
- Sinal de `if` errado: o valor que deveria ser testado não aparece na condição (a resposta nunca muda). Comparar a constante com ela mesma é sempre igual.
- Lado que muda (dado de entrada) à esquerda, lado fixo (constante) à direita da comparação.

## Próximos passos

1. Acabamento do `set` do `Cpf` em `Aluno.cs`: (1) mensagem do `if` de dígitos deve descrever a regra real (depois de tirar a formatação, só números) e sem `$` desnecessário; (2) no `if` do tamanho, `nameof` da **propriedade** (não do `cpfLimpo`), `'CPF'` entre aspas simples e "dígitos" com acento; (3) linha em branco entre o fim do `foreach` e o `if` do tamanho. Compilar e commitar (sugestão: "Valida e limpa o CPF do aluno").
2. Configurar o banco no `JotaFitContext` (lugar novo: `OnModelCreating`): tamanho máximo do CPF, do nome e do e-mail, e índice único no CPF. Commit.
3. Gerar a migration de novo (`dotnet ef migrations add CriaTabelaAlunos`, dentro de `src/AcademiaJotaFit.Api`), ler, aplicar com `dotnet ef database update` e conferir a tabela `Alunos` no SQL Server Object Explorer.
4. DTOs (com Data Annotations), Service, Controller e testes no Postman para o Aluno.

## Dúvidas em aberto

- Validar os dígitos verificadores do CPF (CPF "matematicamente válido"): fica para depois.
- Que tamanho máximo dar ao nome e ao e-mail (decidir no passo 3).
- Busca por nome com e sem acento: ver quando chegar no banco (collation do SQL Server).
- Como o EF Core cria um `Aluno` lendo do banco? A migration não reclamou do construtor; confirmar de vez na primeira leitura do banco (Service/Postman).

## Rotina com dois computadores

- Ao sair: `add` + `commit` + `push`. Ao chegar: `git pull` antes de mexer.
- **Pendente:** no outro computador, apagar a pasta do projeto e fazer `git clone` novo (o histórico foi reescrito; não dar `push` de lá antes disso).
- **Pendente:** depois do clone, conferir com `git config user.email` que o e-mail é o noreply do GitHub antes do primeiro commit.
- **Pendente:** no outro computador, instalar a ferramenta: `dotnet tool install --global dotnet-ef --version 10.0.14` (os pacotes chegam sozinhos pelo restore; a ferramenta não). Conferir também se o LocalDB existe lá (`sqllocaldb info`).
