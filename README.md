# ToDoApplication

Installation instructions: clone it build it and run it (make sure you select multiple startup project and API - ToDoApi and WebApp - ToDoApplication selected)

My application (ToDoApplication project) using API to Create Read Update Delete (CRUD). So API (ToDoApi project) should be running too with my Wep Application.

1. I have razor page (ASP.NET core Web App) project which is one page project have referance to API project and use it for CRUD.
In Home page, we can add new task, edit tasks and delete tasks.
I showed id for just test purpose. In future we have to update the project because it is In Momory app, when we close project our memory erase too.

2. I have MemoryStorage (Class Library project) which I have my method for CRUD operation and validation methods.
Additionally I have message class which I defined my status messages for my application.
If we want more validation we can update our Validation class (ValidateToDoTask method)

3. I have ToDoApi (ASP.NET Core Web Api) project (have referance to MemoryStorage project) where I have ToDoController using my Reposity class methods (from my MemoryStorage project).
I have CRUD methods whcih just call corresponding methods from Reposity class. 

4. I have ToDoTest project which is xUnit test project where I have valid Unit tests for repository class which placed in MemoryStorage project. 
So Test project have referance to Memory Storage project

