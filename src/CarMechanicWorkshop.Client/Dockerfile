# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file and restore
COPY ../CarMechanicWorkshop.sln ./

# Copy all project files (for restore caching)
COPY ../CarMechanicWorkshop.Shared/CarMechanicWorkshop.Shared.csproj ./CarMechanicWorkshop.Shared/
COPY ./CarMechanicWorkshop.Client/CarMechanicWorkshop.Client.csproj ./CarMechanicWorkshop.Client/

RUN dotnet restore CarMechanicWorkshop.Client/CarMechanicWorkshop.Client.csproj

COPY . .

WORKDIR /src/CarMechanicWorkshop.Client

# Argument for backend URL (build-time)
ARG BACKEND_API=http://localhost:8080
ENV BACKEND_API=${BACKEND_API}

# 
RUN sed -i "s|%BACKEND_API%|${BACKEND_API}|g" wwwroot/index.html

RUN dotnet publish -c Release -o /app/publish

# Runtime stage — use lightweight web server
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=build /app/publish/wwwroot .

EXPOSE 80
