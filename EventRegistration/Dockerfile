FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY ./bin/Release/net8.0/publish ./

ENV ASPNETCORE_ENVIRONMENT=Docker

EXPOSE 8020

ENTRYPOINT ["dotnet", "EventRegistration.dll"]