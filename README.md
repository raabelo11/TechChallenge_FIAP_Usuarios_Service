# Usuários Service - Tech Challenge FIAP

## Descrição
Microsserviço responsável pelo gerenciamento de usuários:
- Cadastro de novos usuários
- Login e autenticação
- Atualização e gerenciamento de perfis

Implementado em .NET 8 com Entity Framework Core e banco SQL Server.

## Funcionalidades
- Registro de usuários
- Autenticação JWT
- Atualização de perfil
- Event sourcing para rastrear mudanças

## Tecnologias
- .NET 8
- Entity Framework Core
- SQL Server
- JWT Authentication
- Serilog para logs
- Docker (opcional)

## Estrutura
- `Usuarios.Service.Domain` → Entidades e lógica de negócio
- `Usuarios.Service.Infrastructure` → Repositórios e contexto EF Core
- `Usuarios.Service.Application` → Casos de uso
- `Usuarios.ApiService` → API REST

## Configuração
Configurações principais no `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=UsuariosDB;Trusted_Connection=True;"
  },
  "Jwt": {
    "Key": "SUA_CHAVE_SECRETA",
    "Issuer": "UsuariosService",
    "Audience": "UsuariosService"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```
## 🏗️ Arquitetura do fluxo do Kubernetes
```
                    +-----------------+
                    |     Usuários     |
                    |  (Clientes/Front)|
                    +--------+--------+
                             |
                             v
                    +-----------------+
                    | LoadBalancer AWS|
                    +--------+--------+
                             |
        ------------------------------------------------
        |                      |                      |
        v                      v                      v
    AWS- EKS                AWS- EKS                AWS- EKS
+---------------+       +---------------+       +---------------+
| Usuarios.svc  |       | Jogos.svc     |       | Pagamentos.svc|
| (Deployment)  |       | (Deployment)  |       | (Deployment)  |
| 1 - 3 Pods    |       | 1 - 3 Pods    |       | 1 - 3 Pods    |
+-------+-------+       +-------+-------+       +-------+-------+
        |                       |                       |
        v                       v                       v
  +-----------+           +-----------+           +-----------+
  |  Pod(s)   |           |  Pod(s)   |           |  Pod(s)   |
  | Usuarios  |           | Jogos     |           | Pagamentos|
  +-----------+           +-----------+           +-----------+
        |                       |                       |
        v                       v                       v
   Banco de Dados          Banco de Dados          Banco de Dados
   Independente            Independente            Independente

```
