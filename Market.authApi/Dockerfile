FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Market.authApi/Market.authApi.csproj", "Market.authApi/"]
RUN dotnet restore "Market.authApi/Market.authApi.csproj"
COPY . .
WORKDIR "/src/Market.authApi"
RUN dotnet build "Market.authApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Market.authApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Market.authApi.dll"]
