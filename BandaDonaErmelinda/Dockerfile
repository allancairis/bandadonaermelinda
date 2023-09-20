FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BandaDonaErmelinda/BandaDonaErmelinda.csproj", "BandaDonaErmelinda/"]
RUN dotnet restore "BandaDonaErmelinda/BandaDonaErmelinda.csproj"
COPY . .
WORKDIR "/src/BandaDonaErmelinda"
RUN dotnet build "BandaDonaErmelinda.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BandaDonaErmelinda.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BandaDonaErmelinda.dll"]
