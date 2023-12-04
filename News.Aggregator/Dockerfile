#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["News.Aggregator/News.Aggregator.csproj", "News.Aggregator/"]
RUN dotnet restore "News.Aggregator/News.Aggregator.csproj"
COPY . .
WORKDIR "/src/News.Aggregator"
RUN dotnet build "News.Aggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "News.Aggregator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "News.Aggregator.dll"]