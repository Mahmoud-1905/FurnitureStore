# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS builder

WORKDIR /src

COPY FurnitureStore.csproj .

RUN dotnet restore "FurnitureStore.csproj"

COPY . .

RUN dotnet build "FurnitureStore.csproj" -c Release -o /app/build

RUN dotnet publish "FurnitureStore.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine

WORKDIR /app

COPY --from=builder /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "FurnitureStore.dll"]
