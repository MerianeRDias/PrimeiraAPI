#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MinhaPrimeiraApi/MinhaPrimeiraApi.csproj", "MinhaPrimeiraApi/"]
RUN dotnet restore "MinhaPrimeiraApi/MinhaPrimeiraApi.csproj"
COPY . .
WORKDIR "/src/MinhaPrimeiraApi"
RUN dotnet build "MinhaPrimeiraApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MinhaPrimeiraApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinhaPrimeiraApi.dll"]