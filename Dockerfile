FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS app
WORKDIR /app
COPY . .
RUN dotnet restore

FROM app AS build
WORKDIR /app/Repo.Core.WebApi
RUN dotnet build --no-restore

FROM build AS publish
WORKDIR /app/Repo.Core.WebApi
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS runtime
WORKDIR /app
COPY --from=publish /app/Repo.Core.WebApi/out ./
EXPOSE 80

ENTRYPOINT ["dotnet", "Repo.Core.WebApi.dll"]