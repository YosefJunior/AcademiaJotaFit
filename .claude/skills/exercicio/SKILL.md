---
name: exercicio
description: Use quando o aluno disser "exercício", "exercicio" ou "quero praticar X": cria um exercício prático pequeno sobre um conceito, separado do projeto, e corrige a solução dele.
---

# Exercício — praticar antes de aplicar no projeto

Um exercício curto fixa o conceito sem o risco de bagunçar o projeto.

## Montar o exercício

1. Descubra o conceito: o que ele pediu ou, se não disse, o último conceito do `HANDOFF.md` (seção "Conceitos aprendidos").
2. Crie UM exercício:
   - **Pequeno:** dá para fazer em 10–20 minutos.
   - **Separado do projeto**, mas com o mesmo tema de academia (ex.: classe `Equipamento`, `Professor`, `Aula`), para ele treinar sem copiar o que já fez.
   - **Enunciado claro:** o que criar, o que deve acontecer e como testar (ex.: um `Console.WriteLine` no final mostrando o resultado esperado).
   - Um **nível**: fácil, médio ou desafio. Comece pelo fácil se ele não disser.
3. Sugira onde fazer: um projeto console separado (`dotnet new console -n Treino`) fora da pasta do projeto principal, para não ir para o repositório.
4. Não dê a solução. Diga: "Quando terminar, me mande o código ou diga *revisar*."

## Corrigir

- Use o formato da skill **revisar** (linha a linha, com o porquê).
- Se acertou: proponha o próximo nível do mesmo conceito ou siga para o próximo conceito.
- Se errou: aponte o erro, deixe ele corrigir e revise de novo.
