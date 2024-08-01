FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
ARG MIGRATE
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StudentAdmin/StudentAdmin.Api/StudentAdmin.Api.csproj", "StudentAdmin.Api/"]
COPY ["StudentAdmin/StudentAdmin.CrossCutting/StudentAdmin.CrossCutting.csproj", "StudentAdmin.CrossCutting/"]
COPY ["StudentAdmin/StudentAdmin.Application/StudentAdmin.Application.csproj", "StudentAdmin.Application/"]
COPY ["StudentAdmin/StudentAdmin.Infrastructure/StudentAdmin.Infrastructure.csproj", "StudentAdmin.Infrastructure/"]
COPY ["StudentAdmin/StudentAdmin.Core/StudentAdmin.Core.csproj", "StudentAdmin.Core/"]
RUN dotnet restore "StudentAdmin.Api/StudentAdmin.Api.csproj"
COPY . .
WORKDIR "/src/StudentAdmin/StudentAdmin.Api"
RUN dotnet build "StudentAdmin.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "StudentAdmin.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV RUN_MIGRATE=${MIGRATE}
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["sh", "-c", "sleep 3 && dotnet StudentAdmin.Api.dll"]