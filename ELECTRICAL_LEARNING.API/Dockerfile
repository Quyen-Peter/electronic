# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .

# CHẠY đúng file csproj bằng đường dẫn tường minh
RUN dotnet restore "ELECTRICAL_LEARNING.API/ElectricalLearning.Api.csproj"
RUN dotnet publish "ELECTRICAL_LEARNING.API/ElectricalLearning.Api.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ElectricalLearning.Api.dll"]
