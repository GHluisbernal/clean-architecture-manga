#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["accounts-api/src/WebApi/WebApi.csproj", "accounts-api/src/WebApi/"]
COPY ["accounts-api/src/Application/Application.csproj", "accounts-api/src/Application/"]
COPY ["accounts-api/src/Domain.Accounts/Domain.csproj", "accounts-api/src/Domain.Accounts/"]
COPY ["accounts-api/src/Infrastructure/Infrastructure.csproj", "accounts-api/src/Infrastructure/"]
RUN dotnet restore "accounts-api/src/WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/accounts-api/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]