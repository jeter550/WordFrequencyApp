# Agent: Verify Word Frequency App

## Descrição
Verifica que mudanças no código funcionam corretamente na aplicação rodando.

## Contexto do Projeto
- Frontend: Angular 19 em http://localhost:4200
- Backend: .NET 8 API em http://localhost:5000
- Database: SQL Server em localhost:1433
- Orquestração: Docker Compose

## Responsabilidades
1. ✅ Verificar que `docker compose up --build` completa sem erros
2. ✅ Testar frontend carregando em http://localhost:4200
3. ✅ Testar API respondendo em http://localhost:5000/swagger
4. ✅ Testar endpoint POST /api/analysis com texto de exemplo
5. ✅ Verificar que resultados aparecem em ordem decrescente de frequência
6. ✅ Testar validação (texto vazio deve retornar erro 400)
7. ✅ Testar toggle entre Table/Chart views no frontend

## Checklist de Testes
- [ ] Todos os containers (api, frontend, sqlserver) rodando e healthy
- [ ] Frontend carrega sem erros JavaScript
- [ ] API retorna análise correta para exemplo: "hello world hello"
  - Esperado: hello(2), world(1) em ordem decrescente
- [ ] Validação funciona para entrada vazia
- [ ] Swagger documentação acessível
- [ ] Nenhum erro nos logs: `docker compose logs`

## Relatório Esperado
Deve informar:
- Status de cada container
- Resposta HTTP de endpoints críticos
- Exemplos de requisições bem-sucedidas
- Qualquer erro encontrado com detalhes

## Ativar
Use quando: Após fazer mudanças no código (validações, análise, UI, API)
Comando: `invoke skill verify-app`
