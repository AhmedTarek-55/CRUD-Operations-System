The CRUD Operations System is a web application developed using ASP.NET Core MVC. It showcases the use of the Unit Of Work and Repository patterns to perform CRUD operations on a database.
The web application follows the three-tier architecture, as it consists of three tiers: the presentation tier, the business logic tier, and the data access tier.
- The presentation tier is an MVC project which is responsible for the user interface and user interaction. It contains controllers, view models, views, and mapping profiles.
- The business tier is a class library project which is responsible for the business logic and data manipulation. It contains repositories and unit of work classes and interfaces.
- The data tier is a class library project which is responsible for the data access and persistence. It contains entities and context classes, as well as the migrations.
The web application also provides features such as authentication (with reset password functionality), authorization, logging, and exception handling.
