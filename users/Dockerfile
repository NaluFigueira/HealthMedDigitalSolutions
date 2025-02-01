FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0.303 AS build
WORKDIR /src
COPY ["./PosTech.Hackathon.Users.Api/PosTech.Hackathon.Users.Api.csproj", "./PosTech.Hackathon.Users.Api/"]
COPY ["./PosTech.Hackathon.Users.Application/PosTech.Hackathon.Users.Application.csproj", "./PosTech.Hackathon.Users.Application/"]
COPY ["./PosTech.Hackathon.Users.Domain/PosTech.Hackathon.Users.Domain.csproj", "./PosTech.Hackathon.Users.Domain/"]
COPY ["./PosTech.Hackathon.Users.Infra/PosTech.Hackathon.Users.Infra.csproj", "./PosTech.Hackathon.Users.Infra/"]
RUN dotnet restore "./PosTech.Hackathon.Users.Api/PosTech.Hackathon.Users.Api.csproj"
COPY . .
RUN dotnet build "./PosTech.Hackathon.Users.Api/PosTech.Hackathon.Users.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/sdk:8.0.303 AS migration
WORKDIR /src
COPY . .
RUN dotnet restore "./PosTech.Hackathon.Users.Infra/PosTech.Hackathon.Users.Infra.csproj"
COPY . .
WORKDIR "/src/PosTech.Hackathon.Users.Infra"
RUN dotnet build "PosTech.Hackathon.Users.Infra.csproj" -c Release -o /app/migration

FROM build AS publish
RUN dotnet publish "./PosTech.Hackathon.Users.Api/PosTech.Hackathon.Users.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /migration
COPY --from=migration /app/migration .
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PosTech.Hackathon.Users.Api.dll"]