# Word Frequency App

Uma aplicação de análise de frequência de palavras desenvolvida com **.NET 8** (Backend) e **Angular 22** (Frontend).

## 📋 Requisitos

- **Docker**: 20.10+
- **Docker Compose**: 1.29+
- **Git**: para clonar o repositório

## 🚀 Quick Start

### 1. Clone o repositório

```bash
git clone <repository-url>
cd WordFrequencyApp
```

### 2. Inicie os serviços com Docker Compose

```bash
docker compose up --build
```

Isso irá:
- Iniciar o **SQL Server** na porta `1433`
- Iniciar a **API .NET** na porta `5000`
- Iniciar o **Frontend Angular** na porta `4200`

### 3. Acesse a aplicação

- **Frontend**: http://localhost:4200
- **API Swagger**: http://localhost:5000/swagger
- **SQL Server**: localhost:1433 (SA / P@ssw0rd123)

## 📁 Estrutura do Projeto

```
WordFrequencyApp/
├── backend/
│   ├── Dockerfile
│   ├── .dockerignore
│   ├── WordFrequency.sln
│   ├── WordFrequency.API/
│   ├── WordFrequency.Application/
│   ├── WordFrequency.Domain/
│   └── WordFrequency.Infrastructure/
├── frontend/
│   └── word-frequency-ui/
│       ├── Dockerfile
│       ├── nginx.conf
│       ├── .dockerignore
│       ├── src/
│       ├── package.json
│       └── angular.json
├── docker-compose.yml
├── docker-compose.override.yml
├── .env.example
├── .gitignore
└── README.md
```

## 🛠️ Desenvolvimento Local

### Sem Docker (Desenvolvimento Nativo)

#### Backend (.NET 8)

```bash
cd backend

# Restore packages
dotnet restore

# Run migrations
dotnet ef database update -p WordFrequency.Infrastructure -s WordFrequency.API

# Start API
dotnet run --project WordFrequency.API
```

A API estará disponível em: http://localhost:5000

#### Frontend (Angular 22)

```bash
cd frontend/word-frequency-ui

# Install dependencies
npm install

# Start dev server
ng serve
```

O Frontend estará disponível em: http://localhost:4200

### Com Docker

```bash
# Build e start
docker compose up --build

# View logs
docker compose logs -f

# Stop
docker compose down

# Stop e remover volumes
docker compose down -v
```

## 📚 API Endpoints

### Análise de Frequência de Palavras

```http
POST /api/analysis
Content-Type: application/json

{
  "text": "your text here (max 2048 characters)"
}
```

**Response:**
```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "totalWords": 42,
  "uniqueWords": 15,
  "results": [
    {
      "word": "the",
      "count": 8,
      "rank": 1
    },
    {
      "word": "and",
      "count": 6,
      "rank": 2
    }
  ]
}
```

### Buscar Análise Anterior

```http
GET /api/analysis/{id}
```

## 🐳 Docker Services

### SQL Server

- **Container**: `wfa-sqlserver`
- **Porta**: `1433`
- **Usuário**: `sa`
- **Senha**: `P@ssw0rd123`
- **Database**: `WordFrequencyDb`
- **Imagem**: `mcr.microsoft.com/mssql/server:2022-latest`

### API

- **Container**: `wfa-api`
- **Porta**: `5000`
- **Health Check**: `/health`
- **Imagem Base**: `mcr.microsoft.com/dotnet/aspnet:8.0`

### Frontend

- **Container**: `wfa-frontend`
- **Porta**: `4200` → `80` (Nginx)
- **Health Check**: `http://localhost`
- **Imagem Base**: `nginx:alpine`

## 🔧 Configurações

### Variáveis de Ambiente

Crie um arquivo `.env` baseado em `.env.example`:

```bash
cp .env.example .env
```

### Connection String

Para usar um SQL Server externo, modifique `docker-compose.yml`:

```yaml
environment:
  ConnectionStrings__DefaultConnection: "Server=your-server;Initial Catalog=WordFrequencyDb;User Id=sa;Password=your-password;"
```

## ✅ Health Checks

Os serviços possuem health checks configurados:

```bash
docker compose ps

# Status esperado:
# wfa-sqlserver  healthy
# wfa-api        healthy
# wfa-frontend   healthy
```

## 📊 Monitorar Logs

```bash
# Todos os serviços
docker compose logs -f

# Específico
docker compose logs -f api
docker compose logs -f frontend
docker compose logs -f sqlserver
```

## 🧹 Limpeza

```bash
# Stop containers
docker compose down

# Stop e remover volumes
docker compose down -v

# Remove images
docker image rm wordfrequencyapp-api wordfrequencyapp-frontend
```

## 🐛 Troubleshooting

### Porta já em uso

```bash
# Verificar serviços em porta
netstat -ano | findstr :5000

# Encerrar processo
taskkill /PID <PID> /F
```

### SQL Server não conecta

```bash
# Aguardar serviço ficar healthy
docker compose up sqlserver

# Testar conexão
sqlcmd -S localhost -U sa -P P@ssw0rd123
```

### Frontend não carrega

```bash
# Verificar logs do Nginx
docker compose logs frontend

# Rebuld
docker compose up --build frontend
```

## 📖 Documentação

- [Plano de Desenvolvimento](./CLAUDE.md)
- [Especificação do Projeto](./Word-Frequency-App.md)

## 📝 Fases de Desenvolvimento

- ✅ **Fase 1**: Infraestrutura Base (Docker, Compose, Health Checks)
- ⏳ **Fase 2**: Backend (Clean Architecture + API)
- ⏳ **Fase 3**: Frontend (Angular + UI)
- ⏳ **Fase 4**: Bonus (Gráficos)
- ⏳ **Fase 5**: Bonus (Análise de URLs)

## 📜 License

MIT
