FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ENV ASPNETCORE_ENVIRONMENT = "Production"
WORKDIR /app/
COPY Api/IntefellApi_V1/Interfell.API/. ./Interfell.API/
COPY Api/IntefellApi_V1/Business/. ./Business/
COPY Api/IntefellApi_V1/Facade/. ./Facade/
COPY Api/IntefellApi_V1/Utilities/. ./Utilities/
COPY Api/IntefellApi_V1/Entities/. ./Entities/
COPY Api/IntefellApi_V1/Repository/. ./Repository/



#Build for release
WORKDIR /app/
RUN dotnet publish ./Interfell.API -c Release -o /app/publish


FROM base
WORKDIR /app/dist
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Interfell.API.dll"]