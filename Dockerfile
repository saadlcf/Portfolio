# Utiliser une image officielle .NET SDK pour la compilation
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

# Copier le fichier projet et restaurer les dépendances
COPY ["SaadPortfolio/SaadPortfolio.csproj", "SaadPortfolio/"]
RUN dotnet restore "SaadPortfolio/SaadPortfolio.csproj"

# Copier tous les fichiers et compiler l'application
COPY . .
WORKDIR "/src/SaadPortfolio"
RUN dotnet build "SaadPortfolio.csproj" -c Release -o /app/build

# Publier l'application
FROM build AS publish
RUN dotnet publish "SaadPortfolio.csproj" -c Release -o /app/publish

# Utiliser l'image d'exécution ASP.NET pour exécuter l'application
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Exposer le port 80
EXPOSE 80

# Commande d'entrée
ENTRYPOINT ["dotnet", "SaadPortfolio.dll"]
