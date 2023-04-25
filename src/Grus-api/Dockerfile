#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Grus-api/Grus.API.csproj", "Grus-api/"]
COPY ["Grus.Contracts/Grus.Contracts.csproj", "Grus.Contracts/"]
COPY ["Grus.Domain/Grus.Domain.csproj", "Grus.Domain/"]
COPY ["Grus.Infrastructure/Grus.Infrastructure.csproj", "Grus.Infrastructure/"]
COPY ["Grus.Application/Grus.Application.csproj", "Grus.Application/"]
RUN dotnet restore "Grus-api/Grus.API.csproj"
COPY . .
WORKDIR "/src/Grus-api"
RUN dotnet build "Grus.API.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Grus.API.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Grus.API.dll"]