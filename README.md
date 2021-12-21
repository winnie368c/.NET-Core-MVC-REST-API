# CLI Commands REST API (ASP.NET Core MVC)
## Purpose
To help remember the abundance of CLI commands, this REST API stores command line snippets with a description of their functions and corresponding platforms. 
This project was built to learn how to construct a REST API using .NET Core using C# and the MVC design pattern. 

**The following techniques and technologies were used in the creation of this application:**
* REST API principles
* HTTP methods (GET, POST, DELETE, PUT, PATCH)
* Dependency injection
* Repository pattern
* Entity Framework Core O/RM (DBContext, Migration)
* Data Transfer Objects
* AutoMapper
* SQL Server Express
* SwaggerUI & Postman to Test API Endpoints
* Docker - image, container, and deployment to Docker Hub
* Azure Data Studio

## Overview
### Application Architecture:
![](images/Application%20Architecture.png)
### API CRUD Endpoints:
![](images/API%20End%20points.png)
### Swagger UI:
![](images/SwaggerUI.png)
### Postman Endpoints:
#### [HttpPatch] Updating the value of the /howTo attribute; returns "204 No Content" status code 
![](images/Patch.png)
#### [HttpPost] Creating a new command; returns location URI and "201 Created" status code
![](images/Post.png)



<sub>This project is based on a tutorial by Les Jackson.</sub>
