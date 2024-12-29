# Base .NET SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar arquivos do projeto e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar todo o código e compilar o projeto
COPY . ./
RUN dotnet publish -c Release -o out

# Runtime do .NET para execução
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/out .

# Comando para iniciar o produtor
ENTRYPOINT ["dotnet", "PedidoProducer.dll"]
