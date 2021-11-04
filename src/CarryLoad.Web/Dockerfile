#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/CarryLoad.Web/CarryLoad.Web.csproj", "src/CarryLoad.Web/"]
COPY ["src/CarryLoad.Application/CarryLoad.Application.csproj", "src/CarryLoad.Application/"]
COPY ["src/DataLayer/CarryLoad.Repository/CarryLoad.Repository.csproj", "src/DataLayer/CarryLoad.Repository/"]
COPY ["src/DataLayer/CarryLoad.EFContext/CarryLoad.EFContext.csproj", "src/DataLayer/CarryLoad.EFContext/"]
COPY ["src/DataLayer/CarryLoad.Models/CarryLoad.Models.csproj", "src/DataLayer/CarryLoad.Models/"]
RUN dotnet restore "src/CarryLoad.Web/CarryLoad.Web.csproj"
COPY . .
WORKDIR "/src/src/CarryLoad.Web"
RUN dotnet build "CarryLoad.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarryLoad.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarryLoad.Web.dll"]
