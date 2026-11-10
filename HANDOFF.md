# HANDOFF — Academia JotaFit

Última atualização: 10/11/2026

## Onde paramos

Branch `feature/aluno-ef-core`, tudo commitado e enviado. `CadastroAlunoDto` concluído; o CPF agora aceita só dígitos de 0 a 9 (DTO e entidade) e a entidade recusa CPF com todos os dígitos iguais. Próxima etapa: decidir a idade mínima e máxima do aluno.

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
  - `Altura` (65–300 cm) e `Peso` (27–600 kg), com limites em constantes (mínimos eram 0 até 08/11).
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
- Acabamento das mensagens do CPF (texto da regra real, `nameof(Cpf)`, aspas simples e acento) (commit "Ajusta mensagens de validação do CPF").
- Constantes de tamanho no topo do `Aluno`: `TamanhoCpf` (11), `TamanhoMaximoNome` (100), `TamanhoMaximoEmail` (254) e `TamanhoMinimoNome` (2), todas `public`.
- `set` do `NomeCompleto` e do `Email`: o texto limpo (`Trim`, e no e-mail também `ToLowerInvariant`) vai para uma variável, o tamanho é conferido com `ArgumentOutOfRangeException` e uma mensagem que usa as constantes, e a variável é gravada. O nome tem mínimo e máximo; o e-mail, só máximo (commit "Conclui limites de tamanho do nome e do e-mail").
- `JotaFitContext` sobrescreve o `OnModelCreating` (chamando o `base`): `NomeCompleto` e `Email` com `HasMaxLength` e a constante; `Cpf` com `HasMaxLength` + `IsFixedLength` (commit "Configura o tamanho das colunas da entidade Aluno").
- Quarto bloco no `OnModelCreating`: `HasIndex` no `Cpf` + `IsUnique` (commit "Cria índice único para CPF da entidade Aluno").
- Migration `CriaTabelaAlunos` gerada e lida: `Cpf` como `nchar(11)`, `NomeCompleto` `nvarchar(100)`, `Email` `nvarchar(254)`, índice `IX_Alunos_Cpf` com `unique: true`; `Down` faz `DropTable`. Aplicada com `dotnet ef database update`.
- Tabela conferida no Pesquisador de Objetos do SQL Server (Visual Studio).
- Computador de casa: projeto clonado e banco criado com `dotnet ef database update`. `Cpf` como `nchar(11)` e índice `IX_Alunos_Cpf` (`UNIQUE NONCLUSTERED`) conferidos.
- DTO de cadastro do Aluno definido no papel (campos e regras de formato).
- `DTOs/CadastroAlunoDto.cs` criado com Data Annotations e mensagens em `NomeCompleto`, `DataNascimento`, `Email`, `Cpf` e `Altura` (commit "Inicia DTO de cadastro do Aluno").
- Constantes de limite do `Aluno` (altura, peso e tamanho mínimo do nome) passaram a `public`, para o DTO usá-las.
- `Peso` no DTO (`double?`, `Range` de mínimo a máximo com as constantes) e mensagens padronizadas: minúscula no meio da frase, "e-mail" igual ao da entidade, "CPF" sempre maiúsculo (commit "Conclui DTO de cadastro do Aluno").
- CPF restrito a dígitos de 0 a 9: regex `[0-9]` no DTO e `char.IsAsciiDigit` na entidade (commit "Restringe a validação do CPF a dígitos entre 0 e 9").
- Passo (f) no `set` do `Cpf`, depois do tamanho e antes de gravar: recusa CPF com os 11 dígitos iguais, usando uma variável `bool` `digitosIguais` e `break` (commit "Certifica que os dígitos do CPF não são todos iguais").

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
- Sem Regex na entidade por enquanto: os métodos de `string` e o `foreach` resolvem, e cada passo fica legível. No DTO, o atributo `[RegularExpression]` confere que o CPF só tem dígitos.
- A entidade não escreve no console (`Console.Write`): ela não sabe se existe tela; testes reais virão pelo Postman.
- `using` sugerido pelo Visual Studio que não se reconhece (ex.: `Microsoft.IdentityModel.Tokens`) é sinal de método errado: desconfiar e apagar.
- Tamanho máximo: nome **100** (nomes brasileiros longos passam de 40; o teto pega erro, não recusa pessoa real) e e-mail **254** (limite da especificação RFC 5321).
- O limite fica nos **dois lugares** com o mesmo valor: entidade (`if` no `set`, mensagem clara) e banco (`OnModelCreating`). Se o banco for menor que a entidade, o erro só aparece ao gravar, com mensagem técnica.
- Constantes de limite do `Aluno` são `public`: o `JotaFitContext` e o DTO as leem (ex.: `Aluno.TamanhoCpf`), e o limite continua definido num lugar só.
- O tamanho é conferido **depois** do `Trim`: o que vai para o banco é o texto limpo.
- Duplicata de CPF verificada no Service **e** barrada pelo índice único: o Service dá a mensagem clara (409); o índice garante nos cadastros simultâneos. Só o banco daria erro 500 com mensagem técnica.
- Configuração do banco direto no `OnModelCreating` do `JotaFitContext`, não em arquivos separados (`IEntityTypeConfiguration` + `ApplyConfigurationsFromAssembly`): só existe uma entidade, e a forma separada usa interface, que ainda não foi estudada. Separar (refatorar) quando o método crescer com Plano e Matrícula.
- No lambda da configuração, o parâmetro se chama `aluno` nas três linhas: ele é sempre um aluno, e a propriedade é o que vem depois do ponto.
- CPF com tamanho **fixo** no banco (`IsFixedLength`, vira `nchar(11)`): sempre tem exatamente 11 dígitos.
- Commit "Conclui" só quando houve um "Inicia" antes. Título do commit com até uns 50 caracteres, no formato verbo + o quê + de quem.
- De agora em diante, o aluno escreve as próprias mensagens de commit; o Claude só comenta.
- Índice é configuração da **entidade** (`Entity<Aluno>().HasIndex(...)`), não da coluna: fica num bloco separado do `Property` do CPF, que mantém o tamanho.
- Mensagem de commit em português corrido: sem palavra-chave SQL em maiúsculo (ex.: "UNIQUE") e sem dizer o óbvio ("no banco").
- DTO de cadastro do Aluno **não** recebe `Id` (gerado pelo banco, `Identity`) nem `Idade` (calculada a partir da data de nascimento).
- Regras de formato do DTO de cadastro: `NomeCompleto` obrigatório, de 2 a 100 caracteres; `Email` obrigatório, até 254, com `[EmailAddress]`; `DataNascimento` obrigatória; `Cpf` obrigatório, exatamente 11 dígitos; `Altura` obrigatória, de 65 a 300 cm; `Peso` obrigatório, de 27 a 600 kg. Os limites são os mesmos da entidade.
- CPF entra na API **sem máscara** (só os 11 dígitos): o contrato fica padronizado e formatar para exibição é papel do front.
- Formato de e-mail com o atributo pronto `[EmailAddress]`, sem regra inventada: regras caseiras (ex.: "2 letras antes do @") recusam e-mails válidos como `a@b.com`.
- "Data de nascimento não pode estar no futuro" fica na **entidade**, não no DTO: precisa valer em todo caminho (API, job, importação), e um job que cria `new Aluno(...)` não passa pelo DTO.
- Mínimos de altura (65 cm) e peso (27 kg) baseados em dados reais, em vez de 0: zero não é medida de uma pessoa.
- Aviso de vulnerabilidade do `System.Security.Cryptography.Pkcs` 9.0.11 (transitivo, vem do SqlServer; `GHSA-846f-f764-76mx` e `GHSA-2fmv-rv2c-59x7`) **ignorado por enquanto**: a API só roda local e não recebe certificados nem assinaturas de fora. Rever quando atualizar os pacotes do EF Core.
- Campos de tipo de valor obrigatórios no DTO (`DataNascimento`, `Altura`, `Peso`) são anuláveis: sem o `?`, campo ausente receberia o valor padrão, e o `[Required]` não conseguiria reprovar.
- O DTO espelha a entidade: mesmo tipo (`Peso` é `double`, não `decimal`, porque `decimal` é para dinheiro), mesmas constantes, mesma ordem de mínimo e máximo.
- CPF aceita só os dígitos ASCII de 0 a 9 nas duas camadas: `\d` e `char.IsDigit` aceitam dígitos de qualquer alfabeto (ex.: arábico), e o CPF só existe com 0 a 9.
- CPF com os 11 dígitos iguais é recusado **na entidade** (regra de validade, vale em todo caminho; regex não é a ferramenta certa para "todos iguais"). Fica depois do tamanho, porque aí as verificações anteriores já garantem 11 dígitos de 0 a 9 (pré-condição), e cada erro recebe a mensagem do problema real.
- O tamanho de nome e e-mail é medido antes do `Trim` no DTO e depois na entidade (`" A "` passa no DTO e falha na entidade → erro 500). Não se remenda no DTO: resolve-se no item 3 do roteiro (exceção → 400).
- Uma regra por commit: cada mudança fica fácil de revisar e de desfazer.

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
- `char.IsDigit(c)`: responde se o caractere é um dígito de **qualquer** alfabeto (inclui `'٣'` arábico). `char.IsAsciiDigit(c)`: só de `'0'` a `'9'`.
- Guard clause dentro do `foreach`: o `throw` para tudo no primeiro item inválido; se o laço terminar, todos passaram. Texto vazio = laço com 0 voltas (não reprova nada).
- `texto.Length`: propriedade (sem parênteses) com o número de caracteres. `!=` = "diferente de".
- Sinal de `if` errado: o valor que deveria ser testado não aparece na condição (a resposta nunca muda). Comparar a constante com ela mesma é sempre igual.
- Lado que muda (dado de entrada) à esquerda, lado fixo (constante) à direita da comparação.
- Ordem das validações: limpar antes do tamanho (a formatação soma caracteres); conferir dígitos antes do tamanho (por causa da **mensagem**, não do resultado: o valor é recusado nas duas ordens).
- `private` = só dentro da classe enxerga; outra classe (ex.: `JotaFitContext`) precisa de `public`.
- `const` é automaticamente `static`: pertence à classe, não a cada objeto; acessa-se como `Tipo.Membro` (`Aluno.TamanhoCpf`, igual a `string.IsNullOrWhiteSpace`).
- Erro de banco não tratado vira **500** (parece defeito do servidor); erro de regra tratado no Service vira resposta clara (ex.: **409 Conflict**).
- Commit com várias linhas: título, linha em branco, corpo. No Git Bash, crase dentro de aspas duplas é executada como comando; usar `git commit` sem `-m` (abre o editor).
- Branch `feature/` pode receber trabalho pela metade; `git stash` fica só no computador local, não viaja pelo GitHub.
- Herança: a classe filha (`JotaFitContext : DbContext`) recebe tudo o que a classe pai tem.
- `virtual` (o pai permite trocar) e `override` (o filho troca o comportamento): base do polimorfismo.
- `base.Metodo(...)` chama explicitamente o método do pai. Com `override`, o código do pai continua existindo, mas só roda se for chamado. Esquecer o `base` numa classe pai que configura coisas (ex.: `IdentityDbContext`) faz configurações sumirem sem erro.
- `OnConfiguring` (recebe `DbContextOptionsBuilder`) diz **como conectar**; `OnModelCreating` (recebe `ModelBuilder`) diz **como são as tabelas**. O tipo do parâmetro mostra a diferença.
- Fluent API: encadear entidade → propriedade → regra (`Entity<T>()` → `Property(...)` → `HasMaxLength(...)`).
- No lambda `aluno => aluno.Cpf`, o parâmetro representa um aluno qualquer, e o nome é livre. O lambda serve para apontar **qual** propriedade configurar.
- `HasMaxLength` = tamanho variável até o limite (`nvarchar(n)`); com `IsFixedLength`, é sempre aquele tamanho (`nchar(n)`).
- E-mail com 254: a RFC 5321 limita o path a 256 caracteres, contando os sinais `< >`, e 256 − 2 = 254. A parte local (antes do `@`) vai até 64.
- Condição de corrida (*race condition*): duas requisições checam ao mesmo tempo, as duas veem "não existe" e as duas gravam. Só uma restrição no banco (índice único) barra isso. É a mesma ideia da idempotência contra pedido duplicado.
- Conferir o que o autocomplete escolhe antes de aceitar (nome e tipo do parâmetro).
- `HasIndex(lambda)` cria índice; `IsUnique()` o torna único. Sem `unique`, o índice só acelera a busca e aceita repetido. Nome padrão do EF: `IX_Tabela_Coluna`.
- CPF é **chave candidata**: identifica sozinho, mas não é a chave primária; garante-se com restrição `UNIQUE` / índice único.
- Migration é um processo: muda o modelo → `migrations add` (compara com o snapshot) → **ler a receita** → `database update` → commit. A tabela `__EFMigrationsHistory` registra o que já foi aplicado.
- `migrations remove` só se a migration não foi aplicada nem compartilhada; senão, corrige-se com migration nova (igual a commit publicado no Git).
- Em produção, gera-se script com `dotnet ef migrations script` para revisão, em vez de `database update` direto.
- DDL (`CREATE TABLE`, `ALTER`, `CREATE INDEX`) define estrutura; DML (`INSERT`, `SELECT`, `UPDATE`, `DELETE`) mexe nos dados.
- Pesquisa boa é pelo **conceito** ou nome do método ("EF Core HasIndex"), não pelo problema inteiro ("CPF único EF Core"), que devolve a solução pronta.
- SSH: par de chaves (pública no GitHub, privada só no computador); cada computador tem o seu. `Permission denied (publickey)` = chave daquele computador não cadastrada.
- Migration é código e viaja pelo Git; o **banco** não. Cada computador tem o próprio LocalDB, vazio até rodar `dotnet ef database update`. É isso que torna o banco reproduzível em qualquer ambiente.
- `dotnet ef migrations ...` mexe nos **arquivos** de migration; `dotnet ef database ...` mexe no **banco**.
- Índice **clustered** define a ordem física da tabela (só um por tabela, normalmente a PK); **nonclustered** é uma estrutura separada que aponta para as linhas (pode haver vários).
- `nchar(n)` = tamanho fixo (CPF); `nvarchar(n)` = tamanho variável até `n` (nome, e-mail).
- Defesa em camadas: o DTO filtra o formato do que entra pela API (mensagem amigável); a entidade garante as regras do domínio em qualquer caminho; o banco garante unicidade até em requisições simultâneas.
- 500 = erro interno (exceção não tratada); 501 = *Not Implemented* (outra coisa).
- Git só enxerga o que está **salvo no disco**: edição não salva no editor não aparece no `git status`.
- `[Required]` só reprova `null`. Um `int` ausente no JSON fica `0`, que não é `null`: passa pelo `[Required]` e cai no `[Range]`, com a mensagem errada.
- `int?` (tipo anulável) **aceita** `null`: é isso que permite ao `[Required]` detectar que o campo não veio.
- Regex: `\d` = dígito de qualquer alfabeto; `[0-9]` = intervalo, só de 0 a 9. Colchetes definem os caracteres aceitos (`[a-z]`).
- `decimal` não vira `double` sozinho (precisa de cast); `int` vira `double` sem aviso.
- `[Range(min, max)]`: a ordem importa. Com o mínimo maior que o máximo, o atributo lança exceção ao validar (erro 500).
- Placeholders: no `StringLength`, `{1}` = máximo e `{2}` = mínimo; no `Range`, `{1}` = mínimo e `{2}` = máximo.
- Compilar não é funcionar: teste de mesa com um caso válido e um inválido antes de pedir revisão.
- `!` antes do valor = negação (inverte `true`/`false`); ler como "se NÃO...". `x!` depois do nome é outro operador (perdoa-nulo).
- Índice de `string`: `texto[0]` é o primeiro caractere; a contagem começa em 0.
- `=` sozinho = "recebe" (atribuição). `var` só na criação; depois, só nome, `=` e valor.
- Variável `bool` como "memória" do laço: declarada **fora** (escopo e para não reiniciar a cada volta), começa com a suposição ("iguais até que se prove o contrário") e o laço só a derruba.
- Decisão sobre "todos" ou "nenhum" só depois de percorrer tudo; "existe algum" pode parar no primeiro achado (`break`). Por isso um `else` dentro do laço não serve para "todos iguais".
- `break` sai do laço; depois dele o código segue na linha após o `}` do `foreach`.
- Pré-condição: o que as verificações anteriores já garantem quando um passo roda.
- Nome de `bool`: afirmação que leia bem no `if` (`if (digitosIguais)`).
- Título de commit diz **o que mudou** e precisa de leitura literal ("dígitos 0 e 9" ≠ "de 0 a 9").

## Próximos passos

1. Decidir a idade mínima e máxima do aluno (hoje só se barra data no futuro: `0001-01-01` e "ontem" passam). Se houver regra, vai na entidade, com constantes.
2. Decidir se os `Replace` de ponto, traço e espaço continuam no `set` do `Cpf`, agora que a API só aceita CPF sem máscara.
3. Service do Aluno (cadastrar com checagem de CPF duplicado → 409), Controller e testes no Postman.
4. Tratamento de erros (exceção → 400), que resolve também o tamanho medido antes × depois do `Trim`.
5. Abrir PR da `feature/aluno-ef-core` para a `main` quando o Aluno funcionar de ponta a ponta.

## Dúvidas em aberto

- Validar os dígitos verificadores do CPF (CPF "matematicamente válido"): fica para depois. A parte "todos os dígitos iguais" já foi feita.
- `[EmailAddress]` é fraco (aceita `a@b`): aceitável; confirmação real de e-mail é por link.
- Busca por nome com e sem acento: ver quando chegar no banco (collation do SQL Server).
- Como o EF Core cria um `Aluno` lendo do banco? A migration não reclamou do construtor; confirmar de vez na primeira leitura do banco (Service/Postman).

## Rotina com dois computadores

- Ao sair: `add` + `commit` + `push`. Ao chegar: `git pull` antes de mexer.
- Clone e banco no computador de casa resolvidos em 08/11.
- **Pendente:** conferir com `git config user.email` que o e-mail é o noreply do GitHub antes do primeiro commit.
- **Pendente:** no outro computador, instalar a ferramenta: `dotnet tool install --global dotnet-ef --version 10.0.14` (os pacotes chegam sozinhos pelo restore; a ferramenta não). Conferir também se o LocalDB existe lá (`sqllocaldb info`).
