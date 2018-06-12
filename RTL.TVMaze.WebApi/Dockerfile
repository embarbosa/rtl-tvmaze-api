FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY RTL.TVMaze.WebApi/RTL.TVMaze.WebApi.csproj RTL.TVMaze.WebApi/
RUN dotnet restore RTL.TVMaze.WebApi/RTL.TVMaze.WebApi.csproj
COPY . .
WORKDIR /src/RTL.TVMaze.WebApi
RUN dotnet build RTL.TVMaze.WebApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RTL.TVMaze.WebApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RTL.TVMaze.WebApi.dll"]
