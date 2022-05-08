#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Order/Order.Api/Order.Api.csproj", "Services/Order/Order.Api/"]
COPY ["Services/Order/Order.InfaStructure/Order.Infastructure.csproj", "Services/Order/Order.InfaStructure/"]
COPY ["Services/Order/Order.Domain/Order.Domain.csproj", "Services/Order/Order.Domain/"]
COPY ["Services/Order/Order.Application/Order.Application.csproj", "Services/Order/Order.Application/"]
RUN dotnet restore "Services/Order/Order.Api/Order.Api.csproj"
COPY . .
WORKDIR "/src/Services/Order/Order.Api"
RUN dotnet build "Order.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.Api.dll"]
