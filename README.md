# ASP.NET Core (.NET 6) Web API with cache

To run this project from Docker

```sh
$> docker build -t aspnet-core-net-6-api-cache -f ./CacheSample.WebApi/Dockerfile .
$> docker create --name aspnet-core-net-6-api-cache-core aspnet-core-net-6-api-cache
$> docker start aspnet-core-net-6-api-cache-core
$> docker run --rm -p 3000:3000 aspnet-core-net-6-api-cache
```

Solution sample to (port is already allocated)
```
Bind for 0.0.0.0:3000 failed: port is already allocated.

$> docker ps

CONTAINER ID   IMAGE                         COMMAND                  CREATED          STATUS          PORTS                    NAMES
69e65c95dfaf   aspnet-core-net-6-api-cache   "dotnet CacheSample.…"   35 seconds ago   Up 29 seconds                            aspnet-core-net-6-api-cache-core

$> docker stop 69e65c95dfaf

$> docker ps 

CONTAINER ID   IMAGE     COMMAND   CREATED   STATUS    PORTS     NAMES

$> docker run --rm -p 3000:3000 aspnet-core-net-6-api-cache
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://[::]:3000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /app/
```

#### Using Memory
![image](https://user-images.githubusercontent.com/22874642/177662412-5d5e270c-09bf-4024-9a21-4df086c20e8b.png)

#### Using Redis
![image](https://user-images.githubusercontent.com/22874642/177662439-8d442cab-f901-4bac-91b9-b97533cc52db.png)

You can add your custom DB Provider (MongoDB, SQL Server, PostgreSQL, etc) and adapt it as necessary

Go to the file "appsettings.json" (aspnet-core-net-6-api-cache/CacheSample.WebApi/appsettings.json) and replace your redis connection string

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Redis": {
    "ConnectionString": "your_host:your_port,password=your_password"
  }
}
```

## Screenshots:
![image](https://user-images.githubusercontent.com/22874642/177671596-38090778-d838-47ab-b077-1f5117640efe.png)

![image](https://user-images.githubusercontent.com/22874642/177671712-501aca4b-b54c-4a22-8050-61c9ffe182de.png)

## Result:

![image](https://user-images.githubusercontent.com/22874642/177673086-e14a003b-7a99-47f0-bb29-4ff493f81617.png)



