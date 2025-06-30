# TraceRouter

Aplicação ASP.NET Core para execução de traceroute com notificações em tempo real via SignalR.

## Descrição

O TraceRouter é um serviço web que permite executar operações de traceroute em hosts remotos, fornecendo atualizações em tempo real sobre cada salto (hop) da rota, além de notificar a conclusão ou eventuais erros do processo. A comunicação em tempo real é feita utilizando SignalR.

## Funcionalidades

- Execução de traceroute utilizando ICMP (Ping).
- Notificações em tempo real para clientes conectados via SignalR.
- API RESTful para iniciar e monitorar operações de traceroute.
- Suporte a CORS para integração com frontends diversos.
- Documentação automática via Swagger.

## Tecnologias Utilizadas

- .NET 8 / C# 12
- ASP.NET Core Web API
- SignalR
- Swagger (Swashbuckle)
- Injeção de dependência

## Como Executar:
### A. Via CLI:
1. **Clone o reposiório:**
`$ git clone https://github.com/luisfelix-93/TraceRouter`
2. **Restaure os pacotes do projeto e compile:**
```bash 
	$ dotnet restore
	$ dotnet build
```
3. ** Execute a aplicação:**
```bash
	$ cd TraceRouter.Client
	$ dotnet run <host>
```

### Via API:
1. **Clone o reposiório:**
`$ git clone https://github.com/luisfelix-93/TraceRouter`
2. **Restaure os pacotes do projeto e compile:**
```bash 
	$ dotnet restore
	$ dotnet build
```
3. ** Execute a aplicação:**
```bash
	dotnet run --project TraceRouter.WebService
```
4. **Acesse a API:**
   - Navegue até `http://localhost:5000/swagger` para acessar a documentação Swagger.
   - Utilize o endpoint `/api/traceroute` para iniciar uma nova operação de traceroute.


## Endpoints principais 
- `POST /api/traceroute/start`  
  Inicia uma nova operação de traceroute.  
  Corpo esperado:
```json
{
	"host": "example.com",
	"connectionId": "api-session"
}
```

- **Hub SignalR:**  
  Conecte-se ao hub `/tracerouteHub` para receber notificações em tempo real dos eventos:
  - `ReceiveHopUpdate`
  - `TraceComplete`
  - `TraceError`


## Estrutura do Projeto

- `TraceRouter.Core` — Domínio e interfaces.
- `TraceRouter.Infrastructure` — Implementações de notificadores e runners.
- `TraceRouter.WebService` — API, Hubs e Controllers.
- `TraceRouter.Client` — Cliente de console para testes e execução de traceroute.

## Observações

- O projeto está configurado para aceitar requisições de qualquer origem (__CORS__).
- Para ambientes de desenvolvimento, o Swagger está habilitado por padrão.

## Licença

Este projeto está sob a licença MIT.
