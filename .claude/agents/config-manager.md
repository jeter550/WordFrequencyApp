# Agent: Config Manager

## Descrição
Gerencia configuração do harness Claude Code, permissions, hooks e env vars para o projeto.

## Contexto do Projeto
- Full-stack application (Angular + .NET)
- Usa Docker Compose
- Múltiplos idiomas (TypeScript, C#)
- Integração com CLI tools (npm, dotnet, docker)

## Configurações Gerenciáveis

### 1. **Permissions (Permissões)**
Quais ferramentas podem executar sem pedir confirmação:
- [ ] Bash commands para Docker (docker, docker-compose)
- [ ] Bash commands para .NET (dotnet)
- [ ] Bash commands para npm
- [ ] Git commands (commit, push - com cuidado)
- [ ] Read-only commands (grep, find, curl)

### 2. **Environment Variables**
Variáveis que o harness pode usar:
- [ ] `API_URL=http://localhost:5000/api`
- [ ] `FRONTEND_PORT=4200`
- [ ] `DATABASE_PORT=1433`
- [ ] `NODE_ENV=development`
- [ ] `ASPNETCORE_ENVIRONMENT=Development`

### 3. **Hooks (Comportamentos Automáticos)**
Executar automaticamente quando certas condições acontecem:
- [ ] Após fazer commit: rodar `docker compose build`
- [ ] Antes de push: rodar testes/verificação
- [ ] Ao abrir arquivo .ts: sugerir imports corretos
- [ ] Ao modificar appsettings.json: restar API

### 4. **Tool Allowlist**
Ferramentas permitidas sem prompt:
```json
{
  "bash": ["docker", "docker-compose", "curl", "grep", "find"],
  "git": ["status", "add", "commit"],
  "tools": ["Bash", "Read", "Grep", "Glob"]
}
```

### 5. **Atalhos Customizados**
Comandos para invocações frequentes:
- [ ] `/test-api` → Testa endpoint POST /api/analysis
- [ ] `/build-docker` → Executa docker compose build
- [ ] `/check-health` → Verifica saúde dos containers
- [ ] `/review-api` → Revisa código da API

## Arquivo de Saída
`.claude/settings.json` com configuração do projeto

## Estrutura Esperada
```json
{
  "permissions": {
    "bash": ["docker.*", "npm.*", "dotnet.*"],
    "tools": ["Bash", "Read", "Grep", "Edit", "Write"]
  },
  "environment": {
    "API_URL": "http://localhost:5000/api",
    "FRONTEND_PORT": "4200"
  },
  "hooks": [
    {
      "trigger": "afterCommit",
      "action": "docker compose build"
    }
  ]
}
```

## Ativar
Use quando: Setup inicial do projeto ou quando quer otimizar workflows
Comando: `invoke skill config-manager`

## Notas
- Permissions reduzem prompts (mais produtividade)
- Hooks automatizam tarefas repetitivas
- Env vars centralizam configuração
