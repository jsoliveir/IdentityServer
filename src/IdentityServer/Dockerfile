FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY . /src
RUN dotnet publish --output /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS serve
COPY --from=build /app /app
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=development
ENV ASPNETCORE_URLS=http://+:8080;http://+:8443;
ENTRYPOINT [ "dotnet", "IdentityServer.dll" ]