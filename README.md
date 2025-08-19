
# CookBook API

This is a personal project developed to practice and enhance my skills in .NET Core. The CookBook API serves as a backend solution for a recipe application, incorporating several modern features and a clean architectural approach.

## Features Implemented

* **AI-Powered Recipe Generation**: Leverages the OpenAI API to dynamically generate new recipes based on user inputs or predefined models.
* **Azure Blob Storage for Images**: All recipe images are securely stored and managed using Azure Blob Storage, ensuring a scalable and robust solution for media content.
* **Google Authentication**: Users can sign up and log in securely and conveniently using their Google accounts, powered by OAuth 2.0.
* **Password Recovery**: A secure password recovery system that sends a reset link to the user's registered email address.

## Project Architecture

The project is organized following the principles of Clean Architecture, separating concerns into distinct layers. This approach makes the application more maintainable, scalable, and testable.

* **Domain**: This layer contains the core business logic and entities of the application, such as the `Recipe` and `User` models. It has no dependencies on other layers.
* **Application**: This layer orchestrates the business logic by handling application use cases. It contains services and interfaces that are implemented by the Infrastructure layer.
* **Infrastructure**: This layer handles external concerns, such as database access, third-party API integrations (OpenAI, Azure Blob Storage), and email services. It implements the interfaces defined in the Application layer.
* **API**: This is the presentation layer, an ASP.NET Core Web API that exposes the application's features through RESTful endpoints. It depends on the Application layer to execute user requests.

## Technologies Used

* .NET Core
* ASP.NET Core Web API
* Entity Framework Core
* OpenAI API
* Azure Blob Storage
* Google Identity Services (OAuth 2.0)
* SMTP for Email Services