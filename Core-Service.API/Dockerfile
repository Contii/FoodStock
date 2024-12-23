# use the official .NET SDK image 
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# set the working directory in the container to /app
WORKDIR /app


# copy the project files to the working directory ("*.sln" means move all files with the .sln extension while "." means the root folder inside the container)
COPY *.sln .
# Model/*.csproj means move all files with the .csproj extension in the Models folder inside the container
COPY Models/*.csproj ./Models/
COPY Persistence/*.csproj ./Persistence/
COPY WebAPI/*.csproj ./WebAPI/
# restore all NuGet packages, including the project file
RUN dotnet restore


# copy the remaining files to the working directory of the container
COPY Models/. ./Models/
COPY Persistence/. ./Persistence/
COPY WebAPI/. ./WebAPI/
# build and publish the application using the Release configuration
WORKDIR /app/WebAPI
RUN dotnet publish -c Release -o out


# set up the runtime environment using the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# set the working directory in the container
WORKDIR /app
# set the ASPNETCORE_URLS environment variable to use port 80
ENV ASPNETCORE_URLS=http://+:80
# copy the published application to the container
COPY --from=build /app/WebAPI/out ./
# set the default command to run when the container starts
ENTRYPOINT ["dotnet", "WebAPI.dll"]