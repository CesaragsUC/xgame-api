#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./api/simple-api.csproj", "api/"]
COPY ["./Domain/Domain.csproj", "Domain/"]
COPY ["./Infra/Infra.csproj", "Infra/"]
RUN dotnet restore "api/simple-api.csproj"
COPY . .
WORKDIR "api"
RUN dotnet build "simple-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "simple-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "simple-api.dll"]