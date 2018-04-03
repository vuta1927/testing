FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY Demo.sln ./
COPY WebApi/WebApi.csproj WebApi/
COPY Demo/Demo.csproj Demo/
COPY Demo.AspNetCore/Demo.AspNetCore.csproj Demo.AspNetCore/
COPY Demo.Common/Demo.Common.csproj Demo.Common/
COPY Demo.Notification.Firebase/Demo.Notification.Firebase.csproj Demo.Notification.Firebase/
COPY Demo.Storage.EntityFrameworkCore/Demo.Storage.EntityFrameworkCore.csproj Demo.Storage.EntityFrameworkCore/
COPY Demo.BackgroundJobs.Hangfire/Demo.BackgroundJobs.Hangfire.csproj Demo.BackgroundJobs.Hangfire/
COPY Demo.IdentityServer4/Demo.IdentityServer4.csproj Demo.IdentityServer4/
COPY Demo.Validation.DataAnnotations/Demo.Validation.DataAnnotations.csproj Demo.Validation.DataAnnotations/
COPY Demo.IdentityServer4.EntityFrameworkCore/Demo.IdentityServer4.EntityFrameworkCore.csproj Demo.IdentityServer4.EntityFrameworkCore/
RUN dotnet restore -nowarn:msb3202,nu1503
COPY . .
WORKDIR /src/WebApi
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApi.dll"]
