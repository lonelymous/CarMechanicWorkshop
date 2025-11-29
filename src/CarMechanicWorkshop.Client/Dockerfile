# BUILD STAGE
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project files for all dependencies (restore cache)
COPY ./CarMechanicWorkshop.Domain/CarMechanicWorkshop.Domain.csproj ./CarMechanicWorkshop.Domain/
COPY ./CarMechanicWorkshop.Application/CarMechanicWorkshop.Application.csproj ./CarMechanicWorkshop.Application/
COPY ./CarMechanicWorkshop.Infrastructure/CarMechanicWorkshop.Infrastructure.csproj ./CarMechanicWorkshop.Infrastructure/
COPY ./CarMechanicWorkshop.Shared/CarMechanicWorkshop.Shared.csproj ./CarMechanicWorkshop.Shared/
COPY ./CarMechanicWorkshop.Client/CarMechanicWorkshop.Client.csproj ./CarMechanicWorkshop.Client/

# Restore client project
RUN dotnet restore ./CarMechanicWorkshop.Client/CarMechanicWorkshop.Client.csproj

# Copy source code (actual files)
COPY ./CarMechanicWorkshop.Domain/ ./CarMechanicWorkshop.Domain/
COPY ./CarMechanicWorkshop.Application/ ./CarMechanicWorkshop.Application/
COPY ./CarMechanicWorkshop.Infrastructure/ ./CarMechanicWorkshop.Infrastructure/
COPY ./CarMechanicWorkshop.Shared/ ./CarMechanicWorkshop.Shared/
COPY ./CarMechanicWorkshop.Client/ ./CarMechanicWorkshop.Client/


# Publish
WORKDIR /src/CarMechanicWorkshop.Client
RUN dotnet publish -c Release -o /app/publish

# RUNTIME (Nginx)
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Clear default nginx page
RUN rm -rf ./*

# Copy published Blazor WebAssembly app
COPY --from=build /app/publish/wwwroot .

# Expose ports
EXPOSE 80
EXPOSE 443

CMD ["nginx", "-g", "daemon off;"]
