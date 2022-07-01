FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ["NetRestApi/NetRestApi.csproj", "NetRestApi/"]
RUN dotnet restore "NetRestApi/NetRestApi.csproj"
COPY . .
WORKDIR "/src/NetRestApi"
RUN dotnet build "NetRestApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetRestApi.csproj" -c Release -o /app/publish

COPY ["NetRestApiTests/NetRestApiTests.csproj", "NetRestApiTests/"]
RUN dotnet restore "NetRestApiTests/NetRestApiTests.csproj"
COPY . .
WORKDIR "/src/NetRestApiTests"
RUN dotnet vtest ./NetRestApiTests

FROM base AS final
WORKDIR /migration
COPY --from=migration /app/migration .


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetRestApi.dll"]

