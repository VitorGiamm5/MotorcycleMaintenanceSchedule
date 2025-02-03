FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /source

COPY ../MotorcycleMaintenanceSchedule.sln .
COPY ../src/MotorcycleMaintenanceSchedule.Api/MotorcycleMaintenanceSchedule.Api.csproj src/MotorcycleMaintenanceSchedule.Api/

RUN dotnet restore src/MotorcycleMaintenanceSchedule.Api/MotorcycleMaintenanceSchedule.Api.csproj

COPY ../src/ src/

RUN dotnet publish src/MotorcycleMaintenanceSchedule.Api/MotorcycleMaintenanceSchedule.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5274

ENTRYPOINT ["dotnet", "MotorcycleMaintenanceSchedule.Api.dll"]
