FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SaadPortfolio/SaadPortfolio.csproj", "SaadPortfolio/"]
RUN dotnet restore "SaadPortfolio/SaadPortfolio.csproj"
COPY . .
WORKDIR "/src/SaadPortfolio"
RUN dotnet build "SaadPortfolio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SaadPortfolio.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "SaadPortfolio.dll"]
