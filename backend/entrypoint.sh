#!/bin/bash
set -e

echo "Waiting for SQL Server to be ready..."
for i in {1..50}; do
  if dotnet tool run dotnet-ef -- database update -s WordFrequency.API -p WordFrequency.Infrastructure 2>/dev/null; then
    echo "Database migration completed successfully"
    break
  fi
  echo "Attempt $i: SQL Server not ready yet, waiting..."
  sleep 3
done

echo "Starting application..."
exec dotnet WordFrequency.API.dll
