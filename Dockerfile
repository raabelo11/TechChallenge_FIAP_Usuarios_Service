FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Usuarios.ApiService/Usuarios.ApiService.csproj", "Usuarios.ApiService/"]
COPY ["Usuarios.Service.Application/Usuarios.Service.Application.csproj", "Usuarios.Service.Application/"]
COPY ["Usuarios.Service.Domain/Usuarios.Service.Domain.csproj", "Usuarios.Domain/"]
COPY ["Usuarios.Service.Infrastructure/Usuarios.Service.Infrastructure.csproj", "Usuarios.Service.Infrastructure/"]
RUN dotnet restore "./Usuarios.ApiService/Usuarios.ApiService.csproj"
COPY . .
WORKDIR "/src/Usuarios.ApiService"
RUN dotnet build "./Usuarios.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Usuarios.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Usuarios.ApiService.dll"]
