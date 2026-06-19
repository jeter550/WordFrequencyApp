# Agent: Security Auditor

## Descrição
Audita segurança de mudanças, especialmente entradas de usuário e requisições HTTP.

## Contexto do Projeto
- Processa entrada de texto do usuário (até 2048 caracteres)
- Faz requisições HTTP para URLs externas (funcionalidade de URL analysis)
- Faz HTML parsing de conteúdo externo
- API exposta publicamente

## Vulnerabilidades Críticas a Revisar

### 1. **Injection Attacks**
- [ ] TextInput valida e sanitiza entrada antes de processamento
- [ ] HTML parser remove scripts/styles perigosos
- [ ] Nenhum SQL injection possível (usando EF Core parametrizado)
- [ ] Nenhuma command injection em operações de arquivo/sistema

### 2. **XSS (Cross-Site Scripting)**
- [ ] HTML parsing remove `<script>` tags
- [ ] HTML parsing remove `on*` event handlers
- [ ] Frontend não faz innerHTML com dados de usuário
- [ ] Angular sanitiza dados em templates

### 3. **SSRF (Server-Side Request Forgery)**
- [ ] URL fetcher valida URL antes de fazer request
- [ ] URL fetcher não permite localhost/127.0.0.1
- [ ] URL fetcher tem timeout (evita DoS)
- [ ] URL fetcher valida protocolo (apenas HTTP/HTTPS)

### 4. **DoS (Denial of Service)**
- [ ] Limite de tamanho de texto (2048 chars) é enforçado
- [ ] Timeout em requisições HTTP externas
- [ ] Análise de frequência não tem loops infinitos
- [ ] Rate limiting considerado para API

### 5. **Data Exposure**
- [ ] Erros não expõem stack traces em produção
- [ ] Logs não contêm dados sensíveis de usuário
- [ ] Respostas de erro são genéricas (sem detalhes internos)

### 6. **CORS**
- [ ] CORS está configurado corretamente (não permite `*`)
- [ ] Frontend pode acessar API (verificar origin)

## Checklist de Segurança
- [ ] Entrada de texto validada (tamanho, caracteres)
- [ ] URLs validadas antes de fetch
- [ ] HTML sanitizado antes de parse
- [ ] Nenhuma credencial em logs/respostas
- [ ] Timeouts configurados em operações externas
- [ ] Erros tratados sem expor detalhes

## Relatório Esperado
Deve listar:
- Vulnerabilidades críticas encontradas
- Vulnerabilidades de média/baixa severidade
- Recomendações de mitigação
- Código vulnerável específico com linha

## Ativar
Use quando: Antes de adicionar features que processam entrada externa (URLs, etc)
Comando: `invoke skill security-auditor`
