# backend/Neurona.Health.Api/Dockerfile

# 1) Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Zkopíruj csproj (kvůli cache)
COPY ["Neurona.Health.Api/Neurona.Health.Api.csproj", "Neurona.Health.Api/"]

# Restore NuGet balíčků
RUN dotnet restore "Neurona.Health.Api/Neurona.Health.Api.csproj"

# Zkopíruj zbytek backend řešení (jen adresář Neurona.Health.Api)
COPY Neurona.Health.Api/. Neurona.Health.Api/

# Publish do /app/publish
RUN dotnet publish "Neurona.Health.Api/Neurona.Health.Api.csproj" -c Release -o /app/publish

# 2) Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Neurona.Health.Api.dll"]
