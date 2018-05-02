
FROM microsoft/aspnetcore:2.0.7
LABEL Name=osu-schedule-server Version=1.0.0
ARG source=bin/Debug/netcoreapp2.0/publish/.
WORKDIR /app
COPY $source .
CMD ASPNETCORE_URLS=http://+:$PORT dotnet schedule-server.dll
