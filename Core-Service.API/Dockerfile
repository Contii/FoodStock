# use the official .NET SDK image 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# set the working directory in the container to /app
WORKDIR /app
RUN chmod -R +x /app


# copy the project files to the working directory ("*.sln" means move all files with the .sln extension while "." means the root folder inside the container)
COPY Core-Service.API.sln .
# Model/*.csproj means move all files with the .csproj extension in the Models folder inside the container
COPY Models/Models.csproj ./Models/
COPY Persistence/Persistence.csproj ./Persistence/
COPY Core-Service.API/Core-Service.API.csproj ./Core-Service.API/
# restore all NuGet packages, including the project file
RUN ls -l /app
RUN dotnet restore Core-Service.API.sln


# copy the remaining files to the working directory of the container
COPY Models/. ./Models/
COPY Persistence/. ./Persistence/
COPY Core-Service.API/. ./Core-Service.API/
# build and publish the application using the Release configuration
WORKDIR /app/Core-Service.API
RUN dotnet publish -c Release -o out


# set up the runtime environment using the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# set the working directory in the container
WORKDIR /app
# set the ASPNETCORE_URLS environment variable to use port 8050
ENV ASPNETCORE_URLS=http://+:8050
# copy the published application to the container
COPY --from=build /app/Core-Service.API/out ./

# set the default command to run when the container starts
ENTRYPOINT ["dotnet", "Core-Service.API.dll"]