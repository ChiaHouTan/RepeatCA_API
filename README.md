# RepeatCA_API

Game Developer → Games (1-to-Many)

Table 1: GameDevelopers

| Column Name     | Data Type | Description                     |
| --------------- | --------- | ------------------------------- |
| `ID`  	 	  | string | Unique ID for each developer    |
| `DeveloperName` | string   | Name of the developer or studio |
| `FoundedYear`   | DateTime      | Year the studio was founded     |
| `Country`       | string   | Country where they're based     |
| `IsIndependent` | bool   | True if the developer is indie  |


Table 2: Games

| Column Name   | Data Type | Description                      |
| ------------- | --------- | -------------------------------- |
| ` ID`         | string    | Unique ID for each game          |
| `Title`       | string    | Title of the game                |
| `Image`       | string    | Image of the game                |
| `ReleaseDate` | DatetIme  | Date the game was released       |
| `Genre`       | GameGenre | Action, RPG, Puzzle, etc.        |
| `Price`       | double    | Price of the game                |
| `Rating`      | double    | Rating of the game(MAX 10)       |
| `DeveloperID` | string    | Foreign key linking to developer |

Enum GameGenre
{
    Action,
    Adventure,
    RPG,
    Puzzle,
    Strategy,
    Simulation,
    Sports,
    Horror,
    Shooter,
    Racing,
    Other
}

Reference

MongoDB Atlas Setup and Connection

MongoDB Atlas Documentation – Getting Started
Step-by-step guide to creating a free MongoDB Atlas cluster and connecting your application.
https://www.mongodb.com/docs/atlas/getting-started/

MongoDB C#/.NET Driver Documentation
Official documentation for the MongoDB C# driver, covering installation, connection, and CRUD operations.
https://www.mongodb.com/docs/drivers/csharp/current/

Get Started with the .NET/C# Driver
Tutorial on creating a .NET application that connects to MongoDB Atlas using the C# driver.
https://www.mongodb.com/docs/drivers/csharp/current/get-started/



Implementing CRUD Operations in .NET

Create a Web API with ASP.NET Core and MongoDB
Microsoft's tutorial on building a web API that performs CRUD operations on a MongoDB database.
https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-9.0

MongoDB CRUD Operations with .NET Core
A practical guide on implementing CRUD operations using MongoDB and .NET Core.
https://medium.com/@jaydeepvpatil225/mongodb-basics-and-crud-operation-using-net-core-7-web-api-884b5b76549a

Create ASP.NET Core CRUD API with MongoDB
Detailed steps to create a CRUD API using ASP.NET Core and MongoDB.
https://www.c-sharpcorner.com/article/create-asp-net-core-crud-api-with-mongodb/



MongoDB CRUD with .NET Core Example Project

A GitHub repository demonstrating CRUD operations with MongoDB and .NET Core.
https://github.com/basharovi/MongoDB-CRUD-with-.Net-Core

Publish ASP.NET Core to Azure App Service
Microsoft Docs – step-by-step guide for publishing from Visual Studio
https://learn.microsoft.com/en-us/visualstudio/deployment/quickstart-deploy-aspnet-web-app?view=vs-2022


