# RandomUserProject

Verson used
.net core 3.1
.EntityFrameworkCore 3.1.3

Sql Server is the used Database

Instructions to run the project:

1. replace the connection string for the database from appsettings.json
2. run database migration.
3. use the command dotnet run
4. Useful url
    Get All User - api/user/get-user-list
    Get All User With Matching Condition- api/user/get-user-list?limit=20&skip=0&search=avi
    Get User By user Id - api/user/{userId}
    Create User - api/user/
-------------------------------------------------------------------------------------------------------
                  body:
                  {
                      "title": "Ms",
                      "firstName": "Stephaniy",
                      "lastName": "Morrison",
                      "email": "stephanie.morrison@example.com",
                      "phoneNumber": "(707)-597-5955",
                      "dateOfBirth": "1960-08-26T07:53:09.619Z",
                      "imageDetail": {
                          "original": "https://randomeuser.me/api/portraits/women/41.jpg",
                          "thumbnail": "https://randomuser.me/api/portraits/thumb/women/41.jpg",
                         }
                   }
--------------------------------------------------------------------------------------------------------
    Update User - api/user/update/{userId}
    Delete User - api/user/{userId}
    Use RandomUser endpoint to populate database -api/user/create-random-user
    

