# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los archivos de proyecto y restaurar dependencias
COPY *.sln ./
COPY MindFactory.News.*/*.csproj ./
RUN for f in MindFactory.News.*/*.csproj; do dotnet restore "$f"; done

# Copiar el resto del código
COPY . .

# Publicar la app
WORKDIR /src/MindFactory.News.Api
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto por defecto
EXPOSE 80

# Comando por defecto
ENTRYPOINT ["dotnet", "MindFactory.News.Api.dll"]