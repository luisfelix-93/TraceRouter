# TraceRouter

Aplica��o ASP.NET Core para execu��o de traceroute com notifica��es em tempo real via SignalR.

## Descri��o

O TraceRouter � um servi�o web que permite executar opera��es de traceroute em hosts remotos, fornecendo atualiza��es em tempo real sobre cada salto (hop) da rota, al�m de notificar a conclus�o ou eventuais erros do processo. A comunica��o em tempo real � feita utilizando SignalR.

## Funcionalidades

- Execu��o de traceroute utilizando ICMP (Ping).
- Notifica��es em tempo real para clientes conectados via SignalR.
- API RESTful para iniciar e monitorar opera��es de traceroute.
- Suporte a CORS para integra��o com frontends diversos.
- Documenta��o autom�tica via Swagger.

## Tecnologias Utilizadas

- .NET 8 / C# 12
- ASP.NET Core Web API
- SignalR
- Swagger (Swashbuckle)
- Inje��o de depend�ncia

## Como Executar:
### A. Via CLI:
1. **Clone o reposi�rio:**
`$ git clone https://github.com/luisfelix-93/TraceRouter`
2. **Restaure os pacotes do projeto e compile:**
```bash 
	$ dotnet restore
	$ dotnet build
```
3. ** Execute a aplica��o:**
```bash
	$ cd TraceRouter.Client
	$ dotnet run <host>
```

### Via API:
1. **Clone o reposi�rio:**
`$ git clone https://github.com/luisfelix-93/TraceRouter`
2. **Restaure os pacotes do projeto e compile:**
```bash 
	$ dotnet restore
	$ dotnet build
```
3. ** Execute a aplica��o:**
```bash
	dotnet run --project TraceRouter.WebService
```
4. **Acesse a API:**
   - Navegue at� `http://localhost:5000/swagger` para acessar a documenta��o Swagger.
   - Utilize o endpoint `/api/traceroute` para iniciar uma nova opera��o de traceroute.


## Endpoints principais 
- `POST /api/traceroute/start`  
  Inicia uma nova opera��o de traceroute.  
  Corpo esperado:
```json
{
	"host": "example.com",
	"connectionId": "api-session"
}
```

- **Hub SignalR:**  
  Conecte-se ao hub `/tracerouteHub` para receber notifica��es em tempo real dos eventos:
  - `ReceiveHopUpdate`
  - `TraceComplete`
  - `TraceError`


## Estrutura do Projeto

- `TraceRouter.Core` � Dom�nio e interfaces.
- `TraceRouter.Infrastructure` � Implementa��es de notificadores e runners.
- `TraceRouter.WebService` � API, Hubs e Controllers.
- `TraceRouter.Client` � Cliente de console para testes e execu��o de traceroute.

## Observa��es

- O projeto est� configurado para aceitar requisi��es de qualquer origem (__CORS__).
- Para ambientes de desenvolvimento, o Swagger est� habilitado por padr�o.

## Licen�a

Este projeto est� sob a licen�a MIT.
