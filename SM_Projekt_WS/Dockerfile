#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SM_Projekt_WS/SM_Projekt_WS.csproj", "SM_Projekt_WS/"]
RUN dotnet restore "SM_Projekt_WS/SM_Projekt_WS.csproj"
COPY . .
WORKDIR "/src/SM_Projekt_WS"
RUN dotnet build "SM_Projekt_WS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SM_Projekt_WS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SM_Projekt_WS.dll"]