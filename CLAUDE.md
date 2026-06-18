# Word Frequency App - Documentação do Projeto

## Visão Geral
Este é um projeto **Tier 1 (Beginner)** que implementa um analisador de frequência de palavras. A aplicação conta a frequência de palavras em um bloco de texto e exibe uma tabela ordenada por frequência em ordem decrescente.

## Objetivo
Calcular e exibir a frequência de palavras em um texto usando técnicas que têm aplicação em algoritmos de busca, ordenação e análise semântica.

## Requisitos Funcionais

### User Stories Essenciais
- [ ] Exibir caixa de entrada de texto, botão 'Traduzir'/'Analisar' e tabela de frequência
- [ ] Caixa de entrada suporta até 2048 caracteres de texto grande (copiar e colar)
- [ ] Botão para analisar frequência de palavras do texto inserido
- [ ] Mostrar mensagem de erro se a caixa de entrada estiver vazia
- [ ] Tabela preenchida com palavras únicas e contagem de ocorrências
- [ ] Tabela ordenada em sequência decrescente por frequência

### Bonus Features
- [ ] Representação gráfica (gráfico de bolhas, coluna ou outro)
- [ ] Permitir entrada de URL de página web para análise (em vez de texto manual)

## Referências Úteis
- [Bag of Words Model (Wikipedia)](https://en.wikipedia.org/wiki/Bag-of-words_model)
- [Semantic Analysis (Wikipedia)](https://en.wikipedia.org/wiki/Sentiment_analysis)

## Exemplos de Inspiração
- [Word Frequency Counter](https://codepen.io/maxotar/pen/aLrwJM)
- [Bubble Chart](https://codepen.io/Quendoline/pen/pjELpM)
- [Svelte Word Frequency by Gabriele Corti](https://codepen.io/borntofrappe/pen/QWWWqQM)

## Notas Técnicas
- Máximo de 2048 caracteres de entrada
- Necessário validação de entrada vazia
- Ordenação em ordem decrescente por frequência