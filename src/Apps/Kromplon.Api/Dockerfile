FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

COPY . .
RUN dotnet tool restore
ENV PATH="/root/.dotnet/tools:${PATH}"

# Tutaj już jesteś w katalogu z .csproj
WORKDIR /src
RUN dotnet restore -nowarn:msb3202,nu1503
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app

FROM base AS final
RUN apk update && apk upgrade
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "Kromplon.Api.dll"]
