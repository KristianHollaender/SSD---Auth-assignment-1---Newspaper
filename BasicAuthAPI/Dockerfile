FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BasicAuthAPI/BasicAuthAPI.csproj", "BasicAuthAPI/"]
RUN dotnet restore "BasicAuthAPI/BasicAuthAPI.csproj"
COPY . .
WORKDIR "/src/BasicAuthAPI"
RUN dotnet build "BasicAuthAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BasicAuthAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BasicAuthAPI.dll"]
