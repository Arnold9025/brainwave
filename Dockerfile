# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

# Copy the solution file
COPY BrainWave.slnx ./

# Copy all .csproj files preserving directory structure for caching
COPY src/BrainWave.Api/BrainWave.Api.csproj src/BrainWave.Api/
COPY src/BrainWave.Application/BrainWave.Application.csproj src/BrainWave.Application/
COPY src/BrainWave.Domain/BrainWave.Domain.csproj src/BrainWave.Domain/
COPY src/BrainWave.Infrastructure/BrainWave.Infrastructure.csproj src/BrainWave.Infrastructure/
COPY tests/BrainWave.Api.IntegrationTests/BrainWave.Api.IntegrationTests.csproj tests/BrainWave.Api.IntegrationTests/
COPY tests/BrainWave.Application.UnitTests/BrainWave.Application.UnitTests.csproj tests/BrainWave.Application.UnitTests/

# Restore dependencies
RUN dotnet restore

# Copy all remaining source files
COPY . .

# Build and Publish
WORKDIR /app/src/BrainWave.Api
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime
RUN apk add --no-cache krb5-libs icu-libs
WORKDIR /app
COPY --from=build /app/publish .

# Environment variables
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "BrainWave.Api.dll"]
