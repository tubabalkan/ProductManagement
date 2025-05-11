# 1. Build katmanı
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Yalnızca API ve bağlı katman projelerini kopyala
COPY ProductManagement.API/ProductManagement.API.csproj ProductManagement.API/
COPY ProductManagement.Application/ProductManagement.Application.csproj ProductManagement.Application/
COPY ProductManagement.Domain/ProductManagement.Domain.csproj ProductManagement.Domain/
COPY ProductManagement.Infrastructure/ProductManagement.Infrastructure.csproj ProductManagement.Infrastructure/

# 👉 Sadece API projesini restore ediyoruz (.sln değil!)
RUN dotnet restore ProductManagement.API/ProductManagement.API.csproj

# Tüm dosyaları kopyala
COPY . .

# Publish işlemi
WORKDIR /src/ProductManagement.API
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime katmanı
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProductManagement.API.dll"]
