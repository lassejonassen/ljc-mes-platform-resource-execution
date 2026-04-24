# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/ResourceExecution.WebAPI/ResourceExecution.WebAPI.csproj", "src/ResourceExecution.WebAPI/"]
COPY ["src/ResourceExecution.Application/ResourceExecution.Application.csproj", "src/ResourceExecution.Application/"]
COPY ["src/ResourceExecution.Contracts/ResourceExecution.Contracts.csproj", "src/ResourceExecution.Contracts/"]
COPY ["src/ResourceExecution.SharedKernel/ResourceExecution.SharedKernel.csproj", "src/ResourceExecution.SharedKernel/"]
COPY ["src/ResourceExecution.Domain/ResourceExecution.Domain.csproj", "src/ResourceExecution.Domain/"]
COPY ["src/ResourceExecution.Infrastructure/ResourceExecution.Infrastructure.csproj", "src/ResourceExecution.Infrastructure/"]
COPY ["src/ResourceExecution.WebAPI.Contracts/ResourceExecution.WebAPI.Contracts.csproj", "src/ResourceExecution.WebAPI.Contracts/"]
RUN dotnet restore "./src/ResourceExecution.WebAPI/ResourceExecution.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/ResourceExecution.WebAPI"
RUN dotnet build "./ResourceExecution.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ResourceExecution.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ResourceExecution.WebAPI.dll"]