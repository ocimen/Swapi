# Swapi


### 1. Application Run

#### **Using Dotnet**

1) Open your prompt in <b>src/Swapi</b> folder and execute this code below:

    ```sh
    dotnet run
    ```

2) After the application run you can access swagger from  <http://localhost:5000/swagger>

#### **Using Docker**

1) Make sure that your Docker client is running locally
2) Open your prompt in Solution folder and execute this code below:

    ```sh
    docker build -t swapi .
    docker run -p 5000:5000 swapi
    ```
3) After the application run you can access swagger from  <http://localhost:5000/swagger>


- - - -

### 2. Using REST API

#### LOGIN AND GET TOKEN

##### **Login**

You need to post a request to Auth endpoint with username and password
http://localhost:5000/api/Auth

The username and password information are stored in <b>appsettings.json</b> under <b>SwapApi</b> section.


```sh
"SwapApi": {
    "BaseUrl": "http://swapi.dev/api/",
    "SwapiClient": "Swapi",
    "UserName": "string",
    "Password":  "string"
  }
````

##### **Token**

If the request was succesful you can get Bearer token which will be validate for next 1 hour.

```sh
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJzdHJpbmciLCJqdGkiOiJjODk3OWNkMy00MWMxLTRhOGEtYjFkNS01MjZiNjdjNDk5MWUiLCJpYXQiOiIwMy8yMy8yMDIxIDEzOjUxOjEzIiwibmJmIjoxNjE2NTA3NDczLCJleHAiOjE2MTY1MTEwNzMsImlzcyI6ImxvY2FsaG9zdCIsImF1ZCI6IlRlc3QgVXNlciJ9.oNXJRNKDLbPkD361Q7CdLdQ9j5ie197Zg76fM_7YmSo",
  "type": "Bearer",
  "expiresIn": 60
}
````

##### **Endpoints**

> Auth http://localhost:5000/api/Auth


For belowed endpoints you have to be authentication token which you can get from Auth endpoint.

> People Detail http://localhost:5000/api/People/{id}
> People Search http://localhost:5000/api/People?name={name}&page={pageNumber}

> Planet Detail http://localhost:5000/api/Planets/{id}
> Planet Search http://localhost:5000/api/Planets?name={name}&page={pageNumber}

You can also see and test all the endpoints on swagger  <http://localhost:5000/swagger>


### 3. Using gRPC API

You can run <b> Swapi.Grpc.Server</b>
 project under <b>/grpc/Swapi.Grpc.Server</b> folder with <b>dotnet run </b> command.

 This app will be run on http://localhost:5100

 After the server app was started you can test it via <b>Swapi.Grpc.Console.Client</b>
 You can run this project same way with the Server app.

 On Client project there 4 example request which are
 * People Detail
 * Search People
 * Planet Detail
 * Search Planet
## About Project

**Packages:**

* AutoMapper
* Fluent Validation
* Moq
* Serilog
* xUnit
* Newtonsoft.Json
* Microsoft Native Injector
* Swashbuckle (Swagger)
* Others .NetCore Packages

- - - -

**TODO (BackEnd):**

* Add unit tests for controllers

- - - -
