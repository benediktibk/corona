FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
COPY CoronaSpreadViewer/bin/Release/netcoreapp3.1/* App/
COPY appsettings_production.json App/appsettings.json
WORKDIR /App
ENTRYPOINT ["./CoronaSpreadViewer.exe"]