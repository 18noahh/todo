# Basis-Image für die Laufzeitumgebung
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Build-Image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Kopiere die gesamte Solution-Datei und die Projektdateien ins Image
COPY ["ToDo-App M324.sln", "."]
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["ToDo-App M324/ToDo-App M324.csproj", "ToDo-App M324/"]
COPY ["TestProject1/TestProject1.csproj", "TestProject1/"]

# Restore für alle Projekte
RUN dotnet restore "ToDo-App M324.sln"

# Kopiere den gesamten Code ins Build-Image
COPY . .

# Baue die Solution (damit werden alle Projekte gebaut)
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

WORKDIR "/src/ToDo-App M324"
RUN dotnet build "ToDo-App M324.csproj" -c Release -o /app/build

# Publish-Phase
FROM build AS publish
WORKDIR "/src/WebApi"
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

WORKDIR "/src/ToDo-App M324"
RUN dotnet publish "ToDo-App M324.csproj" -c Release -o /app/publish

# Finales Image mit der laufenden Anwendung
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]

