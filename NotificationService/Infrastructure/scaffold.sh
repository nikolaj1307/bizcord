#!/bin/bash
#the below will read the the CONN_STR from .env file - you may change this to your connection string
set -a
source .env
set +a

dotnet ef dbcontext scaffold "Server=localhost;Port=5433;Database=notification;User Id=admin;Password=admin;"  Npgsql.EntityFrameworkCore.PostgreSQL  --project ../ --output-dir Models   --context-dir ./Data   --context NotificationContext --no-onconfiguring  --namespace Models --context-namespace  Infrastructure --force 