#!/usr/bin/env bash

docker pull selenoid/chrome
docker pull selenoid/firefox:100.0

docker compose up --build -d --remove-orphans -t 1

a=1
until [ -z "$a" ]; do
    a=$(docker compose ps --services --filter status=running | grep "test-")
done

docker-compose down