FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY ["FXFinder.API/FXFinder.API.csproj", "FXFinder.API/"]
COPY ["FXFinder.Core/FXFinder.Core.csproj", "FXFinder.Core/"]

RUN dotnet restore "FXFinder.API/FXFinder.API.csproj"
COPY . .

WORKDIR "/src/FXFinder.API"
RUN dotnet build "FXFinder.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FXFinder.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FXFinder.API.dll"]
