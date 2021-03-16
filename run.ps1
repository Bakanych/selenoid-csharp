#!/usr/bin/env pwsh
docker pull selenoid/chrome
docker-compose up --build --force-recreate --abort-on-container-exit --remove-orphans -t 1 --exit-code-from tests
docker-compose down