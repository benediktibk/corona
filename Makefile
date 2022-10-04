#!/usr/bin/make -f

COMMONDEPS := Makefile
ENVIRONMENTFILE := ~/Development/infrastructure/build/corona.env

############ general

all: binaries-build tests

clean:
	git clean -xdff
	
run-locally: Corona/CoronaSpreadViewer/bin/Release/netcoreapp6.0/publish/CoronaSpreadViewer.dll
	cd Corona/CoronaSpreadViewer/bin/Release/netcoreapp6.0/publish && env $(shell cat $(ENVIRONMENTFILE) | xargs) dotnet CoronaSpreadViewer.dll

tests: binaries-build
	cd Corona && dotnet test

Corona/CoronaSpreadViewer/bin/Release/netcoreapp6.0/publish/CoronaSpreadViewer.dll: $(COMMONDEPS) $(shell find Corona -type f -not -path "*/bin/*" -not -path "*/obj/*" -name "*")
	cd Corona && dotnet publish --configuration Release

binaries-build:
	cd Corona && dotnet build
	
.PHONY: all clean run-locally tests binaries-build