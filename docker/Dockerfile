FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY . ./

RUN dotnet restore EnglishHub.Api
RUN dotnet publish -c Release -o out EnglishHub.Api

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
CMD dotnet EnglishHub.Api.dll