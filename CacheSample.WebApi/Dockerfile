FROM centos:7 AS base

# Add Microsoft package repository and install ASP.NET Core
RUN rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm \
  && yum install -y aspnetcore-runtime-6.0

# Ensure we listen on any IP Address 
ENV DOTNET_URLS=http://+:3000

WORKDIR /app

# ... remainder of dockerfile as before
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CacheSample.WebApi/CacheSample.WebApi.csproj", "CacheSample.WebApi/"]
RUN dotnet restore "CacheSample.WebApi/CacheSample.WebApi.csproj"
COPY . .
WORKDIR "/src/CacheSample.WebApi"
RUN dotnet build "CacheSample.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CacheSample.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CacheSample.WebApi.dll"]