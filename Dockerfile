#FROM microsoft/dotnet AS base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80/tcp
EXPOSE 443/tcp

#docker pull mcr.microsoft.com/windows/nanoserver


#FROM mcr.microsoft.com/windows/servercore:ltsc2022 AS build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install -g dotnet-ef --version 6.0.6

WORKDIR /src
COPY [ "NetRestApi/NetRestApi.csproj", "NetRestApi/" ]
RUN dotnet restore "NetRestApi/NetRestApi.csproj"
COPY . .
RUN dotnet build "NetRestApi/NetRestApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetRestApi/NetRestApi.csproj" -c Release -o /app/publish
WORKDIR /src/NetRestApi
#RUN dotnet ef database update


FROM build AS tests
ENTRYPOINT [ "dotnet" ]
CMD [ "test", "NetRestApiTests" ]
RUN dotnet restore "NetRestApiTests/NetRestApiTests.csproj"
     #RUN dotnet test -c Release --results-directory /src/logs --logger "trx;LogFileName=errorLog.trx" NetRestApiTests/NetRestApiTests.csproj  


FROM base
ENTRYPOINT [ "dotnet" ]
CMD [ "NetRestApi.dll" ]
COPY --from=publish /app/publish .