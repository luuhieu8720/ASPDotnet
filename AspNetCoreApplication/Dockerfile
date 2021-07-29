FROM mcr.microsoft.com/dotnet/sdk:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./AspNetCoreApplication.csproj", "src/"]
RUN dotnet restore "src/AspNetCoreApplication.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "AspNetCoreApplication.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "AspNetCoreApplication.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "AspNetCoreApplication.dll"]