#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
RUN dotnet dev-certs https
WORKDIR /src
COPY ["Services/CatalogService/Catalog.API.UI/Catalog.API.csproj", "Services/CatalogService/Catalog.API.UI/"]
COPY ["Services/CatalogService/Catalog.Application/Catalog.Application.csproj", "Services/CatalogService/Catalog.Application/"]
COPY ["Services/CatalogService/Catalog.Core/Catalog.Core.csproj", "Services/CatalogService/Catalog.Core/"]
COPY ["Services/CatalogService/Catalog.Infrastrcture/Catalog.Infrastrcture.csproj", "Services/CatalogService/Catalog.Infrastrcture/"]
RUN dotnet restore "./Services/CatalogService/Catalog.API.UI/./Catalog.API.csproj"
COPY . .
WORKDIR "/src/Services/CatalogService/Catalog.API.UI"
RUN dotnet build "./Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]