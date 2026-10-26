# HANDOFF — Academia JotaFit

Última atualização: 26/10/2026

## Onde paramos

Recomeçando a partir do commit inicial. Próximo passo: refazer a limpeza do WeatherForecast, recriar as pastas e escrever a classe **`Aluno`** já com as correções da primeira revisão.

## Correções da primeira revisão da classe Aluno (aplicar ao refazer)

- Classe **`public`** (sem modificador ela é `internal` e vai dar erro de acessibilidade inconsistente nos controllers).
- **Sem `id` no construtor**: o banco gera o Id.
- Parâmetros em **camelCase** (`nomeCompleto`, `dataNascimento`).
- Propriedade **`Cpf`** em vez de `CPF` (siglas de 3+ letras só com a inicial maiúscula).
- Para pensar: encapsulamento. Quais propriedades não deveriam ter `set` público?

## Git em dois computadores

- Regra: ao sair de um computador, `add` + `commit` + `push`; ao chegar no outro, `git pull` antes de mexer.

## O que já foi feito

- Solution `.slnx` e projeto `AcademiaJotaFit.Api` criados por linha de comando (.NET 10, com controllers).
- `.gitignore` criado; repositório publicado no GitHub (`main`).
- Erro GH007 (e-mail privado) resolvido com o e-mail noreply do GitHub.
- Exemplo `WeatherForecast` removido.
- Pastas criadas: `Data`, `DTOs`, `Models`, `Services` (o Visual Studio adicionou `<Folder Include>` no `.csproj` para mantê-las visíveis).
- Git Bash configurado como terminal padrão do Visual Studio.
- Sugestões de IA do Visual Studio em modo **Manual** (Text Editor → Inline Suggestions → Invocation); "C# suggestions" e "Inline diff views" do IntelliCode desmarcados.
- Modelo de domínio definido (ver `CLAUDE.md`).
- `CLAUDE.md`, `HANDOFF.md` e skills de estudo em `.claude/skills/` criados (explicar, dica, revisar, exercicio, quiz, salvar).

## Decisões tomadas

- Benefícios do plano como propriedades do próprio Plano (opção A).
- Matrícula guarda data de cancelamento (`DateOnly?`); status é calculado.
- Renovação = nova matrícula.
- Ordem de desenvolvimento: Aluno → Plano → Matrícula, uma por vez de ponta a ponta.
- Entidades ricas (estilo DDD): todas as propriedades com `private set`; mudanças só por métodos que validam (guard clauses).
- Aluno: `Id`, `Cpf` e `DataNascimento` não mudam (sem método); nome e e-mail com métodos **separados** (mudam em momentos diferentes); altura e peso juntos em `AtualizarMedidas` (medidos na mesma avaliação).

## Próximos passos

1. Corrigir o autor do commit inicial e refazer a limpeza do projeto.
2. Escrever a classe `Aluno` com as correções acima e revisar (comando **revisar**).
3. Commit: "Cria entidade Aluno".
4. Instalar o EF Core, criar o `DbContext`, configurar a connection string e rodar a primeira migration.
5. Seguir com DTOs, Service, Controller e testes no Postman para Aluno.

## Dúvidas em aberto

- Nenhuma no momento.
