# NKWalks.API - Project 1
 
## Overview
  This project demonstrates the implementation of an ASP.NET Core Web API with a domain model, including CRUD operations using asynchronous programming, the repository pattern, and AutoMapper.

## Features
  - CRUD Operations: Implemented using asynchronous programming for better performance.
  - Repository Pattern: Ensures a clean separation of concerns and easier unit testing.
  - AutoMapper: Simplifies object-object mapping.
  - Model Validation: Ensures data integrity and consistency.
  - Filtering, Sorting, and Pagination: Enhances data retrieval capabilities.
  - Authentication and Authorization: Secured using JWT tokens.
  - Image Upload: Supports uploading images.
  - Logging: Added comprehensive logging for better monitoring and debugging.

## Endpoints
  - GET /api/items: Retrieves a list of items with filtering, sorting, and pagination.
  - GET /api/items/{id}: Retrieves a specific item by ID.
  - POST /api/items: Creates a new item.
  - PUT /api/items/{id}: Updates an existing item.
  - DELETE /api/items/{id}: Deletes an item.

## Authentication
  Obtain a JWT token by authenticating with your credentials.
  Include the token in the Authorization header of your requests.
  
## Image Upload
  Use the /api/items/upload endpoint to upload images.
  
## Logging
  Logs are generated for all operations and can be found in the logs directory.
  
## Contributing
  Contributions are welcome! Please submit a pull request or open an issue to discuss any changes.
  
## License
  This project is licensed under the MIT License.


# NKWalk.UI -- Project 2

## Overview
This project demonstrates how to version multiple APIs and consume them using CRUD (Create, Read, Update, Delete) operations.

## API Versioning
We have implemented versioning for our APIs to ensure backward compatibility and to allow for incremental updates. The versioning is done through the URL path.

### Example:
- `v1/endpoint`
- `v2/endpoint`

## CRUD Operations
The following CRUD operations are supported:
  - Create 
  - Update
  - Delete
    
## Contributing
We welcome contributions! Please read our contributing guidelines for more details.

## License
This project is licensed under the MIT License - see the LICENSE file for details.





