# HANDOFF — Academia JotaFit

Última atualização: 26/10/2026

## Onde paramos

Branch `feature/aluno-metodos-alteracao` criada a partir da `main`, ainda sem alterações. Próximo passo: escrever o método `AlterarNome` no `Aluno` (`public void`, depois do construtor) e pedir **revisar**.

## O que já foi feito

- Solution `.slnx` e projeto `AcademiaJotaFit.Api` (.NET 10, com controllers); exemplo `WeatherForecast` removido.
- Pastas `Controllers`, `Data`, `DTOs`, `Models`, `Services` criadas.
- Repositório no GitHub com `.gitignore`; commits com e-mail noreply do GitHub.
- Modelo de domínio definido (ver `CLAUDE.md`).
- `CLAUDE.md`, `HANDOFF.md` e skills de estudo em `.claude/skills/`.
- Entidade `Aluno` criada (PR #1 na `main`).
- Histórico do Git reescrito para remover dados pessoais de uma versão antiga do `HANDOFF.md`; mensagens dos commits padronizadas; repositório recriado no GitHub e `main` enviada.
- Corrigido bug de recursão infinita no cálculo da `Idade` (usava a propriedade `Idade` em vez da variável local).
- Validações no `set` de cada propriedade do `Aluno`:
  - `NomeCompleto`, `Email`, `Cpf`: não podem ser vazios ou só espaços; espaços das pontas removidos.
  - `Email` guardado em minúsculas.
  - `DataNascimento`: não pode estar no futuro.
  - `Altura` (0–300 cm) e `Peso` (0–600 kg), com limites em constantes.

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
- Métodos de alteração só atribuem à propriedade: a validação já está no `set`, então não se repete o `if`.
- Commits de correção ficam no histórico: mostram a evolução e são prática normal; só se reescreve histórico por motivo real (ex.: dados pessoais).
- Nomes de branch: `feature/` ou `fix/` + entidade + assunto, minúsculo, com hífen e sem acento.

## Conceitos aprendidos

- Recursão infinita: propriedade que chama a si mesma → `StackOverflowException`.
- Guard clause: `if` no início que barra valor inválido com `throw`.
- Exceções: `ArgumentException` (valor inválido), `ArgumentOutOfRangeException` (fora da faixa), `ArgumentNullException`.
- Sobrecargas: `ArgumentOutOfRangeException(string)` recebe o **nome do parâmetro**, não a mensagem.
- `nameof(...)`: pega o nome de algo do código como texto.
- No `set`: `value` é o valor que chega; `field` é onde guardar.
- Auto-property vs. propriedade com corpo.
- Método estático (`string.IsNullOrWhiteSpace(texto)`) vs. método de instância (`texto.Trim()`).
- Strings são imutáveis: métodos devolvem um texto novo, que precisa ser guardado.
- Tipos de valor (`int`, `double`, `DateOnly`) nunca são `null`; métodos de `string` não servem para eles.
- Escopo: variável criada dentro de `{ }` só existe ali.
- Constantes (`const`) e números mágicos; interpolação de string (`$"...{x}..."`).
- Mensagem de erro deve descrever exatamente o que o código verifica.
- `.gitignore` só afeta arquivos que o Git ainda não acompanha.
- Reescrever histórico muda os IDs dos commits; publicar exige force push e novo clone nos outros computadores.
- `get` lê o valor; `set` grava. `private set` impede gravar de fora da classe, mas métodos da própria classe podem.
- Assinatura de método: visibilidade (`public`), retorno (`void` = não devolve nada), nome em verbo no imperativo.
- Data Annotations são etiquetas: só funcionam quando alguém as lê (ASP.NET, EF Core).

## Próximos passos

1. Escrever `AlterarNome` no `Aluno` e pedir **revisar**.
2. Escrever `AlterarEmail` e `AtualizarMedidas` no mesmo modelo; **revisar**, commit e push da branch.
3. Abrir PR de `feature/aluno-metodos-alteracao` para a `main`.
4. Instalar o EF Core, criar o `DbContext`, configurar a connection string e rodar a primeira migration.
5. DTOs (com Data Annotations), Service, Controller e testes no Postman para o Aluno.

## Dúvidas em aberto

- CPF com e sem pontuação (`123.456.789-00` × `12345678900`): onde normalizar para a regra "CPF único" funcionar?
- Busca por nome com e sem acento: ver quando chegar no banco (collation do SQL Server).

## Rotina com dois computadores

- Ao sair: `add` + `commit` + `push`. Ao chegar: `git pull` antes de mexer.
- **Pendente:** no outro computador, apagar a pasta do projeto e fazer `git clone` novo (o histórico foi reescrito; não dar `push` de lá antes disso).
