#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["SE_DevOps_Assignment_Project/SE_DevOps_Assignment_Project.csproj", "./SE_DevOps_Assignment_Project/"]
COPY ["SE_DevOps_DataLayer/SE_DevOps_DataLayer.csproj", "./SE_DevOps_DataLayer/"]
RUN dotnet restore "./SE_DevOps_Assignment_Project/SE_DevOps_Assignment_Project.csproj"

COPY ["SE_DevOps_Assignment_Project/", "./SE_DevOps_Assignment_Project/"]
COPY ["SE_DevOps_DataLayer/", "./SE_DevOps_DataLayer/"]
WORKDIR "/src/SE_DevOps_Assignment_Project"
RUN dotnet build "SE_DevOps_Assignment_Project.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "SE_DevOps_Assignment_Project.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SE_DevOps_Assignment_Project.dll"]
