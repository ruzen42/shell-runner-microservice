FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base 
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ShellRunner.csproj", "./"]
RUN dotnet restore "ShellRunner.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "./ShellRunner.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ShellRunner.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:AppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShellRunner.dll"]
