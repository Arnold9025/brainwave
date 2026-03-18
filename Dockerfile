# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
WORKDIR /app

# Copy all .csproj files and restore dependencies
COPY src/*/*.csproj ./src/
RUN for file in ./src/*.csproj; do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done
COPY BrainWave.slnx ./
RUN dotnet restore

# Copy all source files
COPY . .
WORKDIR /app/src/BrainWave.Api
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Environment variables
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "BrainWave.Api.dll"]
