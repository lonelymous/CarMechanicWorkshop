# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file and restore
COPY ../CarMechanicWorkshop.sln ./

# Copy all project files (for restore caching)
COPY ../CarMechanicWorkshop.Shared/CarMechanicWorkshop.Shared.csproj ./CarMechanicWorkshop.Shared/
COPY ./CarMechanicWorkshop.API/CarMechanicWorkshop.API.csproj ./CarMechanicWorkshop.API/
# (Optional: add other project csproj if needed, e.g. .Client, .Tests)

RUN dotnet restore CarMechanicWorkshop.API/CarMechanicWorkshop.API.csproj

# Copy everything else (source code)
COPY .. .

# Build and publish API
WORKDIR /src/CarMechanicWorkshop.API
RUN dotnet publish -c Release -o /app/publish

# Final runtime stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CarMechanicWorkshop.API.dll"]
