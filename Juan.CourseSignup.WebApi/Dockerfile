FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 65507
EXPOSE 44373

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY Juan.CourseSignup.WebApi/Juan.CourseSignup.WebApi.csproj Juan.CourseSignup.WebApi/
RUN dotnet restore Juan.CourseSignup.WebApi/Juan.CourseSignup.WebApi.csproj
COPY . .
WORKDIR /src/Juan.CourseSignup.WebApi
RUN dotnet build Juan.CourseSignup.WebApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Juan.CourseSignup.WebApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Juan.CourseSignup.WebApi.dll"]
