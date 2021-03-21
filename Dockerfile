FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
EXPOSE 5000/tcp
COPY Corona/CoronaSpreadViewer/bin/Release/netcoreapp5.0/* App/
COPY build/appsettings.json App/appsettings.json
WORKDIR /App
ENTRYPOINT ["dotnet", "CoronaSpreadViewer.dll"]
