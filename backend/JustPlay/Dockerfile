FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY . .
RUN curl -sL https://deb.nodesource.com/setup_12.x |  bash -
RUN apt-get install -y nodejs
RUN dotnet restore "./JustPlay/JustPlay.csproj"
RUN dotnet publish "./JustPlay/JustPlay.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR	/app
EXPOSE 8080
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "JustPlay.dll"]