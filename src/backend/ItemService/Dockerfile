FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["ItemService.csproj", "./"]
RUN dotnet restore "ItemService.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "ItemService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ItemService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ItemService.dll"]
