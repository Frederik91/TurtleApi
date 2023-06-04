FROM microsoft/aspnetcore-build AS builder
WORKDIR /source

COPY TurtleApi.csproj .
RUN dotnet restore
RUN dotnet build TurtleApi.csproj -c Release -o /app/build

COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TurtleApi.dll"]