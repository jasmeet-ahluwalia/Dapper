# Dapper Example with Generic Database Helper

This repository contains a C# example demonstrating the usage of Dapper with a generic database helper class for CRUD operations. The example uses SQL Server as the database provider.

## Prerequisites

Before running the example, ensure you have the following installed:

- .NET Core SDK
- SQL Server (or SQL Server Express)

## Getting Started

Follow these steps to run the example:

1. Clone this repository to your local machine.
2. Open the solution in your preferred IDE (e.g., Visual Studio, Visual Studio Code).
3. Update the connection string in the `Program.cs` file with your SQL Server connection string.
4. Build and run the application.

## Project Structure

- `GenericDatabaseHelper.cs`: Contains the generic database helper class that provides CRUD operations for any entity type.
- `User.cs`: Defines the `User` class representing a user entity.
- `Program.cs`: Main program file demonstrating the usage of the generic database helper with the `User` entity.
- `README.md`: This file providing an overview of the project and instructions for running the example.

## Usage

The `GenericDatabaseHelper` class provides the following methods:

- `CreateTable`: Creates a table for the specified entity type if it does not already exist.
- `Insert`: Inserts a new entity into the database.
- `GetAll`: Retrieves all entities of the specified type from the database.
- `GetById`: Retrieves an entity by its ID from the database.
- `Update`: Updates an existing entity in the database.
- `Delete`: Deletes an entity by its ID from the database.

To use the generic database helper with a specific entity type, instantiate the `GenericDatabaseHelper<T>` class with the appropriate entity type, and call the desired methods.
