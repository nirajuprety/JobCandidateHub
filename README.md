# Candidate Information Management API
## **Overview**

This project is a RESTful API developed using the .NET stack. It provides an endpoint for storing and updating job candidate details using email as a unique identifier. It is designed with scalability in mind, allowing for the storage of large volumes of candidate information in the future. 

**Features**



* **Add or Update Candidate Information**: The API either creates or updates a candidate profile based on the provided email.
* **Unique Identification by Email**: The email field is the unique identifier for candidates.
* **SQL Database**: Candidate information is stored in a SQL database
* **Exception Handling & Logging**: Robust error handling and logging via `ILogger`.
* **Unit Testing**: The application includes unit tests to cover core business logic.


## **API Endpoint**

### POST
```
 /api/candidates
```


The API accepts a JSON payload to either create or update a candidate's information based on the following fields:
```
{
  "firstName": "string",
  "lastName": "string",
  "phoneNumber": "string",
  "email": "user@example.com",
  "fromTime": "string",
  "toTime": "string",
  "linkedInUrl": "string",
  "githubUrl": "string",
  "comment": "string"
}
```

The API determines whether the candidate's information should be updated or created based on the existence of the email in the database.


### **Example Request**

JSON
```
{
  "firstName": "John",
  "lastName": "Doe",
  "phoneNumber": "+1-234-567-8901",
  "email": "john.doe@example.com",
  "fromTime": "09:00 AM",
  "toTime": "05:00 PM",
  "linkedInUrl": "https://www.linkedin.com/in/johndoe",
  "githubUrl": "https://github.com/johndoe",
  "comment": "Interested in software development opportunities."
}
```



### **Example Response**

JSON
```
{
  "data": true,
  "message": "Successfully added job candidate",
  "status": "Ok"
}
```



### **API Logic Overview**
The core logic for managing candidate details is encapsulated in the `CandidateManager` class:

* **AddOrUpdateJobCandidateDetails**: This method handles the process of determining if the candidate's information should be added or updated based on their email.
* **Validation**: The `VaidateModel` method checks that required fields are provided and performs basic validation (e.g., phone number length, time intervals).
* **Candidate Parsing**: The `ParseJobCandidateDetails` method maps the incoming request to an internal `ECandidate` model.
* **Candidate Service Integration**: The logic interacts with an underlying `ICandidateService` to check whether the candidate exists, add new candidates, or update existing profiles.
* **Logging**: The application logs all relevant actions and exceptions for better traceability.


## **Technology Stack**



* **ASP.NET Core 7.0**
* **Entity Framework Core**
* **SQL Database**
* **Dependency Injection**
* **Logging** (using `ILogger`)
* **Unit Testing** (xUnit, Moq)


## **Setup Instructions**

* Clone the Repository \
Open a terminal or command prompt and run the following command to clone the project repository: \
`git clone https://github.com/nirajuprety/JobCandidateHub.git`

* Open the Solution \
Navigate to the cloned repository and open the solution in Visual Studio: \
`cd JobCandidateHub`

* Build the Solution \
In Visual Studio, build the solution to restore the necessary NuGet packages. \
*(You can build the solution by pressing <code>Ctrl+Shift+B</code> or selecting <code>Build</code> > <code>Build Solution</code> from the top menu.)</em>

*    Configure the Database \
Open the `appsettings.json` file and update the connection string to point to your database. \
For example, for PostgreSQL, update the `ConnectionStrings` section like this: \
JSON
```
`"ConnectionStrings": {`

  "DbContext": "Host=localhost;Port=5432;Database=jobcandidatedb;Username=postgres;Password=password"
}

```



* Replace `localhost`, `5432`, `jobcandidatedb`, `postgres`, and `password` with the appropriate values for your environment.

* Add a Migration \
Open the **Package Manager Console** in Visual Studio (`Tools` > `NuGet Package Manager` > `Package Manager Console`) and run the following command to add a migration: \
`add-migration "InitialMigration"`

* Update the Database \
After adding the migration, update the database schema by running the following command in the **Package Manager Console**: \
`update-database`

* Run the Application \
Now, you can run the application using Visual Studio by pressing `F5` or selecting `Debug` > `Start Debugging`.


## **Unit Tests**

Unit tests are available to validate the main logic in the `CandidateManager`. Run the tests using Visual Studio Test Explorer

## **Improvements done**
1. Phone number is validated for 10 character length
2. FromTime should not be greater than to time,
3. Either both from time and to time is required Or both are not required

## **List of Assumptions**
1. Time interval is splitted into FromTime and ToTime that takes string as argument
   Valid Time format in request body => "01:10", "12:23"
   Invalid Time format in request body => "","text", "0112", "01/02"
2. Phone number must be 10 length long

   


## **List of ways for improvement**
1. Caching is not implemented in this version
2. Although logger is implemented it need to be integrated with platform like Grafana for better observability
3. Rate limiting is not implemented
4. Authorization is not implemented
5. More validation can be done for attribute like phone number for uniqueness, length, numeric
