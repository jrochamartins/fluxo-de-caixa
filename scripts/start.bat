dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\fluxodecaixa.api.pfx -p 3346669e-5d67-4312-8001-d47eac475851
dotnet dev-certs https --trust

docker-compose up -d

set /p DUMMY=Hit ENTER to continue...
docker-compose down