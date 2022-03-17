#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MeFit.API/MeFit.API.csproj", "MeFit.API/"]
COPY ["MeFit.DAL/MeFit.DAL.csproj", "MeFit.DAL/"]
RUN dotnet restore "MeFit.API/MeFit.API.csproj"
COPY . .
WORKDIR "/src/MeFit.API"
RUN dotnet build "MeFit.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeFit.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeFit.API.dll"]