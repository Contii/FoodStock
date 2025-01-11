# Stage 1: Build the Blazor WebAssembly app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
RUN chmod -R +x /app

# Copy the project files and restore dependencies
COPY Core-Service.UI.sln .
COPY Models/Models.csproj ./Models/
COPY Core-Service.UI/Core-Service.UI.csproj ./Core-Service.UI/
RUN ls -l /app
RUN dotnet restore Core-Service.UI.sln

# Copy the remaining files and build the app
COPY . .
WORKDIR /app/Core-Service.UI
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Serve the app with Nginx
FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build /app/publish/wwwroot .

# Copy the Nginx configuration file
COPY Core-Service.UI/nginx.conf /etc/nginx/nginx.conf
COPY Core-Service.UI/myapp.conf /etc/nginx/conf.d/

# Expose port 80
EXPOSE 80

# Start Nginx
CMD ["nginx", "-g", "daemon off;"]