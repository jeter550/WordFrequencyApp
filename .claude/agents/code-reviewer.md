# Agent: Code Reviewer

## Descrição
Revisa mudanças no código para bugs, performance e simplificações antes de commit.

## Contexto do Projeto
- Arquitetura: Clean Architecture (Domain, Application, Infrastructure, API)
- Backend: .NET 8 com Entity Framework Core
- Frontend: Angular 19 standalone components
- Validação: Value Objects (TextInput) com Result pattern

## Áreas de Foco
1. **Camadas da Arquitetura**
   - [ ] Domain: Entidades e ValueObjects livres de dependências externas
   - [ ] Application: Use Cases e DTOs sem lógica de infraestrutura
   - [ ] Infrastructure: Implementações de repositórios, serviços externos
   - [ ] API: Controllers enxutos, delegam para Application

2. **Validação e Segurança**
   - [ ] TextInput valida tamanho máximo (2048 chars)
   - [ ] TextInput valida texto não-vazio
   - [ ] Entrada de usuário sanitizada antes de HTML parsing
   - [ ] Requisições HTTP para URLs têm timeout

3. **Simplificações Possíveis**
   - [ ] Redução de abstrações desnecessárias
   - [ ] Remoção de código duplicado
   - [ ] Consolidação de métodos similares

4. **Performance**
   - [ ] Queries EF Core sem N+1 problems
   - [ ] Análise de frequência não faz loops desnecessários
   - [ ] Frontend não faz re-renders excessivos

## Padrões Esperados
- Value Objects retornam Result<T> com erro descritivo
- Repositórios usam async/await
- Controllers retornam ProblemDetails em caso de erro
- DTOs separam tipos de entrada/saída

## Relatório Esperado
Deve listar:
- Violações de arquitetura encontradas
- Bugs potenciais
- Oportunidades de simplificação
- Sugestões de performance

## Ativar
Use quando: Antes de fazer commit de mudanças significativas
Comando: `invoke skill code-reviewer`
