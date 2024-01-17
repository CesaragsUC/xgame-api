#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#obtem a imagem do dotnet para rodar o projeto
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# expoe as portas 80 e 443 para o container 
EXPOSE 80
EXPOSE 443

# obtem a sdk do dotnet para compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build 
# define o diretorio de trabalho
WORKDIR /src
# copia o arquivo csproj para o diretorio chamado mvc
COPY ["./mvc/mvc.csproj", "mvc/"]
# copia o arquivo csproj para o diretorio chamado Infra
COPY ["./Infra/Infra.csproj", "Infra/"]
# No diretorio mvc, executa o comando dotnet restore para restaurar as dependencias do projeto
RUN dotnet restore "mvc/mvc.csproj"
# copia tudo do diretorio atual para o diretorio mvc
COPY . .
# define o diretorio de trabalho para o diretorio mvc
WORKDIR "mvc"
# executa o comando dotnet build para compilar o projeto
RUN dotnet build "mvc.csproj" -c Release -o /app/build

# agora que o projeto foi compilado, vamos publicar o projeto
FROM build AS publish
# executa o comando dotnet publish para publicar o projeto na pasta publish
RUN dotnet publish "mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
# copia o conteudo da pasta publish para o diretorio atual (app)
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mvc.dll"]