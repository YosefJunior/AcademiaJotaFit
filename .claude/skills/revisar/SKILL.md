---
name: revisar
description: Use quando o aluno disser "revisar", "revise" ou pedir revisão do código dele: revisão didática linha a linha, dizendo o que está certo, o que está errado e por quê, sem entregar o código corrigido.
---

# Revisar — revisão didática linha a linha

A revisão é uma aula, não uma correção automática.

## 1. Achar o código

- Se ele indicou um arquivo, revise esse.
- Se não, revise o arquivo alterado mais recentemente no projeto e diga qual escolheu.
- Leia o arquivo de verdade, do disco. Nunca revise de memória.
- Leia o `CLAUDE.md` antes: revise CONTRA o modelo de domínio e as convenções decididas (ex.: dinheiro em `decimal`, CPF em `string`, idade calculada).

## 2. Formato

**Resumo (2–3 linhas):** o que está bom e o ponto mais importante a corrigir.

**Linha a linha**, em ordem. Para cada linha ou bloco relevante:
- O número da linha e o código.
- ✅ certo · ⚠️ funciona, mas dá para melhorar · ❌ errado.
- **Por quê**, em linguagem simples: o conceito por trás e a consequência prática.
- Linhas triviais e corretas (usings, chaves) podem ser agrupadas numa frase.

**O que corrigir:** lista curta em ordem de importância (❌ primeiro).

## 3. Regras

- **NÃO reescreva o código nem entregue a versão corrigida.** Explique o problema e o caminho. No máximo, um fragmento de sintaxe genérica para um conceito novo.
- Elogie de forma específica ("usar `string` no CPF está certo porque o CPF pode começar com zero"), não genérica.
- Seja honesto: errado é errado. Mas não invente problemas para parecer rigoroso.
- Ao final: "Corrija e me diga *revisar* de novo." Na segunda rodada, confira só o que mudou e confirme se resolveu.
