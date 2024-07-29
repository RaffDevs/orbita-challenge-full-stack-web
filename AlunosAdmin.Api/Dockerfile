FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
ARG MIGRATE
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AlunosAdmin.Api/AlunosAdmin.Api.csproj", "AlunosAdmin.Api/"]
RUN dotnet restore "AlunosAdmin.Api/AlunosAdmin.Api.csproj"
COPY . .
WORKDIR "/src/AlunosAdmin.Api"
RUN dotnet build "AlunosAdmin.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AlunosAdmin.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV RUN_MIGRATE=${MIGRATE}
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlunosAdmin.Api.dll"]
