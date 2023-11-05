#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FinCache.API/FinCache.API.csproj", "FinCache.API/"]
RUN dotnet restore "FinCache.API/FinCache.API.csproj"
COPY . .
WORKDIR "/src/FinCache.API"
RUN dotnet build "FinCache.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinCache.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinCache.API.dll"]