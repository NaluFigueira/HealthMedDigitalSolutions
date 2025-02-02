FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0.303 AS build
WORKDIR /src
COPY ["./PosTech.Hackathon.Appointments.Api/PosTech.Hackathon.Appointments.Api.csproj", "./PosTech.Hackathon.Appointments.Api/"]
COPY ["./PosTech.Hackathon.Appointments.Application/PosTech.Hackathon.Appointments.Application.csproj", "./PosTech.Hackathon.Appointments.Application/"]
COPY ["./PosTech.Hackathon.Appointments.Domain/PosTech.Hackathon.Appointments.Domain.csproj", "./PosTech.Hackathon.Appointments.Domain/"]
COPY ["./PosTech.Hackathon.Appointments.Infra/PosTech.Hackathon.Appointments.Infra.csproj", "./PosTech.Hackathon.Appointments.Infra/"]
RUN dotnet restore "./PosTech.Hackathon.Appointments.Api/PosTech.Hackathon.Appointments.Api.csproj"
COPY . .
RUN dotnet build "./PosTech.Hackathon.Appointments.Api/PosTech.Hackathon.Appointments.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/sdk:8.0.303 AS migration
WORKDIR /src
COPY . .
RUN dotnet restore "./PosTech.Hackathon.Appointments.Infra/PosTech.Hackathon.Appointments.Infra.csproj"
COPY . .
WORKDIR "/src/PosTech.Hackathon.Appointments.Infra"
RUN dotnet build "PosTech.Hackathon.Appointments.Infra.csproj" -c Release -o /app/migration

FROM build AS publish
RUN dotnet publish "./PosTech.Hackathon.Appointments.Api/PosTech.Hackathon.Appointments.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /migration
COPY --from=migration /app/migration .
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PosTech.Hackathon.Appointments.Api.dll"]