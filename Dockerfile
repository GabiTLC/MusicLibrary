FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MusicLibrary.csproj", "./"]
RUN dotnet restore "MusicLibrary.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "MusicLibrary.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicLibrary.csproj" -c Release -o /app/publish  
# /p:UseAppHost=false 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MusicLibrary.dll"]