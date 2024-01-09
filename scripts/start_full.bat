@echo off
dotnet dev-certs https --clean
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\fluxodecaixa.api.pfx -p 3346669e-5d67-4312-8001-d47eac475851
dotnet dev-certs https --trust

docker-compose -f docker-compose_full.yaml up -d

start https://localhost:5001/swagger

set /p DUMMY=Hit ENTER to finish containers and exit...
docker-compose down