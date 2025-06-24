# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy toàn bộ solution (vì có nhiều project)
COPY . .

# Restore và publish project chính
WORKDIR /src/ElectricalLearning.Api
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ElectricalLearning.Api.dll"]
