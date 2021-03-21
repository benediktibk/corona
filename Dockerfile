FROM mcr.microsoft.com/dotnet/aspnet:5.0
EXPOSE 5000/tcp
COPY Corona/CoronaSpreadViewer/bin/Release/netcoreapp5.0/* App/
WORKDIR /App
ENTRYPOINT ["dotnet", "CoronaSpreadViewer.dll"]
