# RUNTIME IMAGE
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

# BUILD IMAGE
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy .csproj files (for restore caching)
COPY ./CarMechanicWorkshop.Domain/CarMechanicWorkshop.Domain.csproj ./CarMechanicWorkshop.Domain/
COPY ./CarMechanicWorkshop.Application/CarMechanicWorkshop.Application.csproj ./CarMechanicWorkshop.Application/
COPY ./CarMechanicWorkshop.Infrastructure/CarMechanicWorkshop.Infrastructure.csproj ./CarMechanicWorkshop.Infrastructure/
COPY ./CarMechanicWorkshop.Shared/CarMechanicWorkshop.Shared.csproj ./CarMechanicWorkshop.Shared/
COPY ./CarMechanicWorkshop.Api/CarMechanicWorkshop.Api.csproj ./CarMechanicWorkshop.Api/

# Restore API project
RUN dotnet restore ./CarMechanicWorkshop.Api/CarMechanicWorkshop.Api.csproj

# Copy full source
COPY ./CarMechanicWorkshop.Domain/ ./CarMechanicWorkshop.Domain/
COPY ./CarMechanicWorkshop.Application/ ./CarMechanicWorkshop.Application/
COPY ./CarMechanicWorkshop.Infrastructure/ ./CarMechanicWorkshop.Infrastructure/
COPY ./CarMechanicWorkshop.Shared/ ./CarMechanicWorkshop.Shared/
COPY ./CarMechanicWorkshop.Api/ ./CarMechanicWorkshop.Api/

# Build & publish
WORKDIR /src/CarMechanicWorkshop.Api
RUN dotnet publish -c Release -o /app/publish

# FINAL IMAGE
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose ports
EXPOSE 8080
EXPOSE 8443

ENTRYPOINT ["dotnet", "CarMechanicWorkshop.Api.dll"]
