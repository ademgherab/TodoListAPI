FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY src/Todolist.Api/Todolist.Api.csproj src/Todolist.Api/
RUN dotnet restore src/Todolist.Api/Todolist.Api.csproj

COPY . .
RUN dotnet publish src/Todolist.Api/Todolist.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Todolist.Api.dll"]