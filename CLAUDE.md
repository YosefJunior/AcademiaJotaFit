# CLAUDE.md — Academia JotaFit

Contexto do projeto para o Claude. Leia este arquivo e o `HANDOFF.md` no início de cada sessão.

## Sobre o projeto

- **O quê:** API REST de gestão de academia, criada como projeto de **estudo**.
- **Objetivo:** aprender orientação a objetos, Git/GitHub, Postman, CRUD e EF Core.
- **Stack:** C# / .NET 10, ASP.NET Core Web API com Controllers, EF Core + SQL Server (a instalar).
- **IDE:** Visual Studio, com Git Bash como terminal padrão.
- **Repositório:** https://github.com/YosefJunior/AcademiaJotaFit (branch `main`).

## Como o Claude deve ajudar

- **Não entregar código pronto.** O aluno escreve o código. O Claude explica o passo a passo, o conceito de cada coisa e o **porquê** das decisões.
- Pode mostrar trechos mínimos de sintaxe para ilustrar um conceito, mas não a solução da tarefa.
- Explicar em português, com linguagem simples.
- As sugestões automáticas de IA do Visual Studio estão em modo **Manual** de propósito. Não sugerir religá-las.

## Comandos do aluno (skills em `.claude/skills/`)

| Comando | O que o Claude faz |
|---|---|
| **explicar X** | Explica um conceito: o que é, analogia, por que existe, onde aparece no projeto, erro comum e uma pergunta para conferir. |
| **dica** / **travei** | Dicas em 4 níveis, uma por vez (pergunta-guia → conceito → sintaxe genérica → passo a passo). |
| **revisar** | Revisa o código **linha a linha**: o que está certo, o que está errado e por quê, sem entregar o código corrigido. |
| **exercicio** | Cria um exercício pequeno, separado do projeto, sobre um conceito, e corrige a solução. |
| **quiz** | 5 perguntas rápidas, uma por vez, sobre o que já foi estudado. |
| **salvar** / **retomar** | Atualiza o `HANDOFF.md` com onde parou / lê o `HANDOFF.md` para continuar. |

## Estrutura

```
AcademiaJotaFit/
├── .claude/skills/   skills de estudo (explicar, dica, revisar, exercicio, quiz, salvar)
├── CLAUDE.md
├── HANDOFF.md
├── AcademiaJotaFit.slnx
└── src/AcademiaJotaFit.Api/
    ├── Controllers/   endpoints HTTP
    ├── Data/          DbContext do EF Core
    ├── DTOs/          o que entra e sai da API
    ├── Models/        entidades do domínio
    ├── Services/      regras de negócio
    └── Program.cs     registro de serviços + pipeline
```

**Fluxo das camadas:** Controller → Service → Data → Models.

**Método de trabalho:** uma entidade por vez, de ponta a ponta (Model → Data → DTO → Service → Controller → Postman → commit). Ordem: **Aluno → Plano → Matrícula**.

## Modelo de domínio (decidido)

**Aluno**
- Id (`int`), nome completo, data de nascimento (`DateOnly`), CPF (`string`, único), altura, peso, e-mail.
- Idade é **calculada** a partir da data de nascimento, não guardada.

**Plano**
- Id, nome (Bronze, Prata, Ouro), valor (`decimal`), duração em meses (`int`).
- Benefícios como propriedades (opção A): permite aulas coletivas (`bool`), permite luta (`bool`), doses de pré-treino por dia (`int`), doses de whey por dia (`int`).
- Bronze: só musculação. Prata: + 1 dose de pré-treino/dia. Ouro: + whey, pré-treino, aulas coletivas e luta.

**Matrícula**
- Id, aluno, plano, data de início, data de fim, valor pago (`decimal`), data de cancelamento (`DateOnly?`).
- Status **calculado**: Cancelada (tem data de cancelamento), Vencida (hoje > data de fim), Ativa (caso contrário).

**Regras**
- CPF não pode se repetir.
- Aluno não pode ter duas matrículas ativas ao mesmo tempo.
- Renovação gera uma **nova** matrícula (mantém histórico).
- Regras que consultam o banco ficam no **Service**.
- **Entidades ricas (estilo DDD):** a entidade valida os próprios dados (invariantes) no construtor e nos métodos de alteração, e lança exceção se forem inválidos. Validação de formato da entrada fica nos DTOs.

**Para o futuro:** entidade Avaliação Física (histórico de altura/peso), controle das doses retiradas por dia, Benefício como entidade separada.

## Convenções

- Dinheiro sempre em `decimal`.
- CPF como `string`.
- Valores que podem ser derivados (idade, status) são calculados, não guardados.
- Mensagens de commit em português, no imperativo (ex.: "Cria entidade Aluno").
- Git: e-mail **noreply** do GitHub configurado no `user.email`.
