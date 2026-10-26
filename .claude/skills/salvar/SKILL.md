---
name: salvar
description: Use quando o aluno disser "salvar", "handoff" (registrar onde parou) ou "retomar"/"onde paramos" (continuar o estudo): mantém o HANDOFF.md do projeto atualizado.
---

# Salvar — registrar e retomar o estudo

O `HANDOFF.md` fica na RAIZ do projeto, ao lado do `CLAUDE.md`. É a ponte entre uma sessão de estudo e a próxima.

## "salvar" / "handoff"

1. Leia o `HANDOFF.md` atual e o `CLAUDE.md`.
2. Confira o estado REAL da pasta do projeto (arquivos criados ou alterados). Se não puder rodar comandos, peça a saída de `git status` e `git log --oneline -5`.
3. Reescreva o `HANDOFF.md` com:
   - **Última atualização:** data de hoje.
   - **Onde paramos:** uma ou duas frases concretas (arquivo, classe, etapa).
   - **O que já foi feito:** lista acumulada; mantenha os itens antigos e condense se ficar longa.
   - **Decisões tomadas:** cada decisão com o motivo em uma linha.
   - **Conceitos aprendidos:** uma linha por conceito que apareceu (base para o **quiz**).
   - **Próximos passos:** 3–5 passos em ordem; o primeiro bem específico.
   - **Dúvidas em aberto.**
4. Se o modelo de domínio ou as convenções mudaram, atualize também o `CLAUDE.md`.
5. Grave no disco e confirme em uma ou duas frases.
6. Sugira o commit (o aluno roda): `git add HANDOFF.md CLAUDE.md` e `git commit -m "Atualiza handoff"`.

## "retomar" / "onde paramos"

1. Leia `CLAUDE.md` e `HANDOFF.md`.
2. Confira se a pasta bate com o handoff.
3. Responda curto: onde parou, o próximo passo e uma pergunta rápida sobre o último conceito aprendido, para aquecer.

## Regras

- Fatos concretos, não intenções vagas ("Classe Aluno criada, falta revisar").
- Nada de código no handoff.
