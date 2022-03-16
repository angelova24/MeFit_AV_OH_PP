FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["MeFit.API/MeFit.API.csproj", "MeFit.API/"]
RUN dotnet restore "MeFit.API/MeFit.API.csproj"
COPY . .
WORKDIR "/src/MeFit.API"
RUN dotnet build "MeFit.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeFit.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeFit.API.dll"]
