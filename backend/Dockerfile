# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder

WORKDIR /src

COPY ["WordFrequency.API/WordFrequency.API.csproj", "WordFrequency.API/"]
COPY ["WordFrequency.Application/WordFrequency.Application.csproj", "WordFrequency.Application/"]
COPY ["WordFrequency.Domain/WordFrequency.Domain.csproj", "WordFrequency.Domain/"]
COPY ["WordFrequency.Infrastructure/WordFrequency.Infrastructure.csproj", "WordFrequency.Infrastructure/"]

RUN dotnet restore "WordFrequency.API/WordFrequency.API.csproj"

COPY . .

RUN dotnet build "WordFrequency.API/WordFrequency.API.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM builder AS publish

RUN dotnet publish "WordFrequency.API/WordFrequency.API.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=publish /app/publish .

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

EXPOSE 5000

ENTRYPOINT ["dotnet", "WordFrequency.API.dll"]
