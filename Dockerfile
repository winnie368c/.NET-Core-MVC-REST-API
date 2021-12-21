# # Get copy of .NET Core SDK Image from Microsoft and create working directory within container 
# FROM mcr.microsoft.com/dotnet/core/sdk:3.1.201 AS build-env
# WORKDIR /app

# # Copy .csproj file into working container and restore any dependencies
# COPY *.csproj ./
# RUN dotnet restore

# # Copy the project files into working directory, and build, placing app build dll in folder (out)
# COPY . ./
# RUN dotnet publish -c Release -o out

# # Generate runtime image
# FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.201
# WORKDIR /app
# EXPOSE 80
# COPY --from=build-env /app/out .
# ENTRYPOINT ["dotnet", "Commander.dll"]

# Get copy of .NET Core SDK Image from Microsoft and create working directory within container 
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy .csproj file into working container and restore any dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the project files into working directory 
# and build, placing app build dll in folder (out)
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Commander.dll"]