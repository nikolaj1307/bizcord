### Getting Started

**Information:** This Project can be used with an ```API gateway```. The project for the gateway can be found here: https://github.com/nikolaj1307/api-gateway

## 1. Start the infrastructure

Start the Docker containers
```bash
docker compose up -d
```

## 2. Setup databases

1. Make sure you are connected to ```channel-db``` & ```notification-db```.

Connect using these credentials:

**channel-db**
```
POSTGRES_USER=admin
POSTGRES_PASSWORD=admin
POSTGRES_DB=channel
Port=5432
```

For ```channel-db``` use the file ```db-schema.sql```, which is located under ```ChannelService/Infrastructure/db-schema.sql```.

1. Copy the query
2. Open a new query console for ```channel-db```
3. Paste & run the query in order to create tables


**notification-db**
```
POSTGRES_USER=admin
POSTGRES_PASSWORD=admin
POSTGRES_DB=notification
Port=5433
```

For ```notification-db``` use the file ```db-schema.sql```, which is located under ```NotificationService/Infrastructure/db-schema.sql```.

1. Copy the query
2. Open a new query console for ```notification-db```
3. Paste & run the query in order to create tables


## 3. Access the services

| Component | URL | Description |
| --- | --- | --- |
| ChannelService | http://localhost:5001 | Channel management |
| NotificationService | http://localhost:5002 | Notifications |
| Vault | http://localhost:8200 | Secrets management |
| Kibana | http://localhost:5601 | UI for logging |


### HashiCorp Vault
HashiCorp Vault is included for centralized secrets management.

**Important:** Make sure to setup the vault.

Start Vault from the ```Vault``` directory:

```bash
docker compose config --quiet
docker compose up -d
docker compose logs vault
```

This setup is for local development.

*** Initialization & unsealing *** 
1. Open http://localhost:8200
2. Initialize Vault with Key Shares = 3 and Key Threshold = 2
3. Save the root token and unseal keys securely
4. Enter two different unseal keys to unseal Vault
5. Now sign in with the root token.

Initialize Vault only once. After a restart, unseal it with the same keys. 

The named Docker volume keeps your data; ```docker compose down -v``` deletes it.

**Creating a secret**

1. Open a shell in the Vault container and enter your root token at the login prompt:
```bash
docker compose exec vault sh
vault login
```

2. Enable the KV v2 secrets engine, store an example API key and create a policy that only allows reading that secret:
```bash
vault secrets enable -path=secret kv-v2
vault kv put secret/bizcord ApiKey=this-is-an-example-api-key-only-for-fun
printf 'path "secret/data/bizcord" { capabilities = ["read"] }' | vault policy write bizcord-read -
vault token create -policy=bizcord-read -no-default-policy -ttl=1h
exit
```

3. Copy the application token returned by ```vault token create```

4. Go to the ```API gateway``` project and export the application token:
```bash
export VAULT_ADDR=http://localhost:8200
read -s VAULT_TOKEN
export VAULT_TOKEN
dotnet run --project VaultRead
```

After ```read -s VAULT_TOKEN```, paste the application token and press Enter. The token is not displayed.

You should see ```Vault secret loaded successfully``` (value not displayed).

5. You should now be able to use the ```API gateway```. Make sure to follow the README for the ```API gateway``` project, in order to ensure correct setup.

### Logging 
The project uses a centralized logging infrastructure consisting of:

1. Fluentd – Collects logs from application containers.
2. Elasticsearch – Stores and indexes collected logs.
3. Kibana – Provides an interface for searching and visualizing logs.

**Important:** Make sure to setup logs. 

1. Go to http://localhost:5601/app/discover#/
2. Create data view
3. Specify ```fluentd-*``` to ```Index pattern``` and click ```Save data view to Kibana```


### Documentation

Additional architectural documentation is available in the /docs directory:

- Domain Boundaries

- Saga Pattern


