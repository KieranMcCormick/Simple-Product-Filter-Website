#!/bin/bash

echo "Starting backend..."
cd backend
dotnet run &

sleep 3
echo "Opening frontend..."
open ../frontend/index.html

wait