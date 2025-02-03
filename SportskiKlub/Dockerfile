FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SportskiKlub.csproj", "."]
RUN dotnet restore "./SportskiKlub.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SportskiKlub.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "SportskiKlub.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportskiKlub.dll"]
ENV ASPNETCORE_URLS=http://*:80