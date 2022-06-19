#!/usr/bin/env pwsh

docker pull selenoid/chrome
docker pull selenoid/firefox:100.0

docker-compose up -d --remove-orphans -t 1

Do
{
    $testInProgress = (docker-compose ps --services --filter status=running) -like "test-*"
}
while ($testInProgress)

docker-compose down