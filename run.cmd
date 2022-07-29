docker build -t aspnet-core-net-6-api-cache -f ./CacheSample.WebApi/Dockerfile .
docker create --name aspnet-core-net-6-api-cache-core aspnet-core-net-6-api-cache
docker start aspnet-core-net-6-api-cache-core
docker run --rm -p 3000:3000 aspnet-core-net-6-api-cache
