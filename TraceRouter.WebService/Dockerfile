# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de solução e restaura as dependências
COPY ["TraceRouter.WebService/TraceRouter.WebService.csproj", "TraceRouter.WebService/"]
COPY ["TraceRouter.Core/TraceRouter.Core.csproj", "TraceRouter.Core/"]
COPY ["TraceRouter.Infrastructure/TraceRouter.Infrastructure.csproj", "TraceRouter.Infrastructure/"]
RUN dotnet restore "TraceRouter.WebService/TraceRouter.WebService.csproj"

# Copia o restante dos arquivos e compila
COPY . .
WORKDIR "/src/TraceRouter.WebService"
RUN dotnet publish -c Release -o /app --no-restore

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 5000
EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5000;https://+:5001
ENTRYPOINT ["dotnet", "TraceRouter.WebService.dll"]
