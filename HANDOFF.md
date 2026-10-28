# HANDOFF — Academia JotaFit

Última atualização: 28/10/2026

## Onde paramos

Branch `feature/aluno-ef-core`. LocalDB rodando e pacotes do EF Core instalados (commit "Adiciona pacotes do EF Core"). Aviso de pacote vulnerável ignorado de forma consciente (ver Decisões). Próximo passo: criar o `DbContext` em `Data/`.

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

## Próximos passos

1. Responder antes de criar o `DbContext`: quantos `DbSet` agora (e por que não três)? De qual classe herda? Pasta/namespace? Nome da classe?
2. Criar o `DbContext` em `Data/`, configurar a connection string e registrar no `Program.cs`.
3. Rodar a primeira migration e conferir a tabela de alunos no banco.
4. DTOs (com Data Annotations), Service, Controller e testes no Postman para o Aluno.

## Dúvidas em aberto

- CPF com e sem pontuação (`123.456.789-00` × `12345678900`): onde normalizar para a regra "CPF único" funcionar?
- Busca por nome com e sem acento: ver quando chegar no banco (collation do SQL Server).
- Como o EF Core cria um `Aluno` lendo do banco, se a classe só tem construtor com parâmetros e `private set`?

## Rotina com dois computadores

- Ao sair: `add` + `commit` + `push`. Ao chegar: `git pull` antes de mexer.
- **Pendente:** no outro computador, apagar a pasta do projeto e fazer `git clone` novo (o histórico foi reescrito; não dar `push` de lá antes disso).
- **Pendente:** depois do clone, conferir com `git config user.email` que o e-mail é o noreply do GitHub antes do primeiro commit.
- **Pendente:** no outro computador, instalar a ferramenta: `dotnet tool install --global dotnet-ef --version 10.0.14` (os pacotes chegam sozinhos pelo restore; a ferramenta não). Conferir também se o LocalDB existe lá (`sqllocaldb info`).
