# CarMechanicWorkshop



dotnet ef migrations add InitialCreate \
    --project src/Infrastructure \
    --startup-project src/Api \
    --output-dir Persistence/Migrations
