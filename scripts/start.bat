@echo off

docker-compose up -d

set /p DUMMY=Hit ENTER to finish containers and exit...
docker-compose down