# ------------------------------
# Makefile for CarMechanicWorkshop Solution
# ------------------------------

# Projects
API=src/Api
INFRA=src/Infrastructure
APP=src/Application
DOMAIN=src/Domain
CLIENT=src/Client
SHARED=src/Shared

# Tests
UNIT_TESTS=tests/UnitTests
INTEGRATION_TESTS=tests/IntegrationTests

# Docker
DOCKER_IMAGE_NAME=carmechanicworkshop
DOCKER_COMPOSE_FILE=docker-compose.yml

# ------------------------------
# EF Core Commands
# ------------------------------
migration:
ifndef name
	@echo "❌ ERROR: You must specify a migration name."
	@echo "Usage: make migration name=FixModel"
	@exit 1
endif
	dotnet ef migrations add $(name) --project $(INFRA) --startup-project $(API)

update:
	dotnet ef database update --project $(INFRA) --startup-project $(API)

# ------------------------------
# Build & Run
# ------------------------------
build:
	dotnet clean
	dotnet build

rebuild: build update

run:
	dotnet run --project $(API)

# ------------------------------
# Tests
# ------------------------------
test-unit:
	dotnet test $(UNIT_TESTS) --no-build --verbosity normal

test-integration:
	dotnet test $(INTEGRATION_TESTS) --no-build --verbosity normal

test: test-unit test-integration

# ------------------------------
# Docker Commands
# ------------------------------
docker-build:
	docker build -t $(DOCKER_IMAGE_NAME) .

docker-run:
	docker run --rm -p 5000:5000 -p 5001:5001 $(DOCKER_IMAGE_NAME)

docker-compose-up:
	docker-compose -f $(DOCKER_COMPOSE_FILE) up -d

docker-compose-down:
	docker-compose -f $(DOCKER_COMPOSE_FILE) down

# ------------------------------
# Helpers
# ------------------------------
help:
	@echo "Available targets:"
	@echo "  migration name=<Name>    Add a new EF migration"
	@echo "  update                   Apply EF migrations to the database"
	@echo "  build                    Clean and build all projects"
	@echo "  rebuild                  Build + update database"
	@echo "  run                      Run API project"
	@echo "  test-unit                Run unit tests"
	@echo "  test-integration         Run integration tests"
	@echo "  test                     Run all tests"
	@echo "  docker-build             Build Docker image"
	@echo "  docker-run               Run Docker image"
	@echo "  docker-compose-up        Start docker-compose services"
	@echo "  docker-compose-down      Stop docker-compose services"
