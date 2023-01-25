FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Transactions", "Transactions/"]
COPY ["src/Shared", "Shared/"]
RUN dotnet restore "Transactions/Transactions.App/Transactions.App.csproj"
RUN dotnet build "Transactions/Transactions.App/Transactions.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Transactions/Transactions.App/Transactions.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Transactions.App.dll"]