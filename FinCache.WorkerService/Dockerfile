#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FinCache.WorkerService/FinCache.WorkerService.csproj", "FinCache.WorkerService/"]
COPY ["FinCache.InMemory/FinCache.InMemory.csproj", "FinCache.InMemory/"]
RUN dotnet restore "FinCache.WorkerService/FinCache.WorkerService.csproj"
COPY . .
WORKDIR "/src/FinCache.WorkerService"
RUN dotnet build "FinCache.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinCache.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinCache.WorkerService.dll"]