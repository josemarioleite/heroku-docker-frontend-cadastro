FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["desafio-pontotel-backend.csproj", "./"]
RUN dotnet restore "desafio-pontotel-backend.csproj"
COPY . .
RUN dotnet build "desafio-pontotel-backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "desafio-pontotel-backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# ENTRYPOINT ["dotnet", "desafio-pontotel-backend.dll"]
RUN useradd -m myapppontotel
USER myapppontotel
CMD ASPNETCORE_URLS="http://*:$PORT" dotnet desafio-pontotel-backend.dll