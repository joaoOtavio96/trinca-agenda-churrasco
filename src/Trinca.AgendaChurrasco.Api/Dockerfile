FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Trinca.AgendaChurrasco.Api/Trinca.AgendaChurrasco.Api.csproj", "src/Trinca.AgendaChurrasco.Api/"]
COPY ["src/Trinca.AgendaChurrasco.Data/Trinca.AgendaChurrasco.Data.csproj", "src/Trinca.AgendaChurrasco.Data/"]
COPY ["src/Trinca.AgendaChurrasco.Domain/Trinca.AgendaChurrasco.Domain.csproj", "src/Trinca.AgendaChurrasco.Domain/"]
RUN dotnet restore "src/Trinca.AgendaChurrasco.Api/Trinca.AgendaChurrasco.Api.csproj"
COPY . .
WORKDIR "/src/src/Trinca.AgendaChurrasco.Api"
RUN dotnet build "Trinca.AgendaChurrasco.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Trinca.AgendaChurrasco.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Trinca.AgendaChurrasco.Api.dll"]
