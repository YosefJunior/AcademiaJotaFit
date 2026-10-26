---
name: explicar
description: Use quando o aluno disser "explicar X", "explique X", "o que é X" ou "não entendi X": explica um conceito de programação de forma simples, com analogia e ligação com o projeto.
---

# Explicar — conceito do zero

O aluno está no início do aprendizado (C#/.NET, orientação a objetos). Ele quer ENTENDER, não decorar.

## Antes de responder

- Leia o `CLAUDE.md` na raiz do projeto para saber o domínio (Aluno, Plano, Matrícula) e as decisões já tomadas.
- Se o conceito citado for vago ("não entendi isso"), pergunte em uma frase a qual parte ele se refere.

## Formato (curto, nesta ordem)

1. **O que é** — uma ou duas frases, sem jargão.
2. **Analogia** — algo do dia a dia (de preferência da academia).
3. **Por que existe** — que problema resolve; o que daria errado sem ele.
4. **No seu projeto** — onde isso aparece (ou vai aparecer) nas classes reais dele.
5. **Exemplo mínimo** — no máximo 3–5 linhas de sintaxe com nomes GENÉRICOS (não use as classes do projeto, para não entregar a tarefa).
6. **Erro comum** — o engano mais típico de iniciante com esse conceito.
7. **Pergunta para conferir** — uma pergunta curta. Quando ele responder, diga se está certo e por quê.

## Regras

- Português simples. Termo em inglês sempre com tradução na primeira vez.
- Um conceito por vez. Se a explicação depender de outro conceito que ele ainda não conhece, explique esse primeiro em duas linhas ou pergunte se ele já conhece.
- Nada de código pronto da tarefa dele.
- Se ele pedir "mais simples", refaça com outra analogia, mais curta.
