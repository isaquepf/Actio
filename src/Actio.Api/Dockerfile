FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /dotnetapp

# Copiar csproj e restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Build da aplicacao
COPY . ./
RUN dotnet publish -c Release -o ./bin/Docker

# Build da imagem
FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /dotnetapp
COPY ./bin/Docker .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
ENTRYPOINT ["dotnet", "Actio.Api.dll", "--environment=docker"]





FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /dotnetapp
COPY ./bin/Docker .
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIRONMENT docker
#RUN dotnet publish -c Release -o ./bin/Docker
ENTRYPOINT dotnet Actio.Api.dll