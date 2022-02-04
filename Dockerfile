#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Spotifalso.API/Spotifalso.API.csproj", "Spotifalso.API/"]
COPY ["Spotifalso.Aplication/Spotifalso.Aplication.csproj", "Spotifalso.Aplication/"]
COPY ["Spotifalso.Core/Spotifalso.Core.csproj", "Spotifalso.Core/"]
COPY ["Spotifalso.Infrastructure/Spotifalso.Infrastructure.csproj", "Spotifalso.Infrastructure/"]
RUN dotnet restore "Spotifalso.API/Spotifalso.API.csproj"
COPY . .
WORKDIR "/src/Spotifalso.API"
RUN dotnet build "Spotifalso.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Spotifalso.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Spotifalso.API.dll"]