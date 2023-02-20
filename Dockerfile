FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
COPY . /src
WORKDIR /src
RUN ls
RUN dotnet build "ECoursesLogger.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECoursesLogger.sln" -c Release -o /app/publish

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECoursesLogger.dll"]

