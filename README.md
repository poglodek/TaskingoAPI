# Taskingo API
 Web API with SignalR in C# - ASP .NET 
<hr>

# Required: <br />
- Runtime .NET 5.0
<hr>
# Packages: <br />
- <h3><b><a href="https://github.com/dotnet/efcore">Entity Framework Core</a></b></h3> Used for operating on database
- <h3><b><a href="https://github.com/aspnet/Security/tree/master/src/Microsoft.AspNetCore.Authentication">AspNetCore Authentication</a></b></h3> Used for authorization and authentication - using JWT Token with expire 15 days (you can change this in appsettings.json).
- <h3><b><a href="https://github.com/AutoMapper/AutoMapper">AutoMapper</a></b></h3> Used for object mapping
- <h3><b><a href="https://github.com/NLog">Nlog</a></b></h3> Used for logging
- <h3><b><a href="https://github.com/FluentValidation/FluentValidation">FluentValidation</a></b></h3> Used to validate submitted objects
- <h3><b><a href="https://github.com/swagger-api">Swagger</a></b></h3> Used to show api call for frontend developers
- <h3><b><a href="https://www.newtonsoft.com/json">Newtonsoft</a></b></h3> for Json Convert
<hr>

# SignalR <br />
I used SignalR to real-time chat 

# Swagger example
<img src="https://user-images.githubusercontent.com/83174704/134965990-2bc8e3a7-da10-429a-b7ea-8c8bf76dde6d.png" />
<img src="https://user-images.githubusercontent.com/83174704/134966047-aa408758-0391-4663-b704-4bfd52baca1f.png" />
<br /><br/ >
<a target="_blank" href="https://github.com/poglodek/TaskingoAPI/files/7238521/swagger.pdf">All API Call in PDF</a>
<hr />

# Database

I used SQL Server from  <a href="https://www.microsoft.com/pl-pl/sql-server/sql-server-downloads" >microsoft</a>. It's work with EF Core very good. <br>

SQL database In <a href= "https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15">SQL Server Managment Studio </a><br /><br />
<img src="https://user-images.githubusercontent.com/83174704/134966176-29ebac6a-99d4-4691-8b8d-054608fae8da.png" />

# TODO
- Message Encrypt 
- SignalR New Task Notifications
