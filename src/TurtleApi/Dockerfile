FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TurtleApi.csproj", "TurtleApi/"]
RUN dotnet restore "TurtleApi/TurtleApi.csproj"

WORKDIR "/src/TurtleApi"
COPY . .
RUN dotnet build TurtleApi.csproj -c Release -o /app/build

COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM build AS publish
RUN dotnet publish "TurtleApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TurtleApi.dll"]