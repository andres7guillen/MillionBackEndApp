# MillionBackEndApp
Appliaction created using .Net 8, 
1. if you want to run and test the project, you should put your connection string into app settings development json at "DbConnection" inside "ConnectionStrings" object, once done open the package manager console pointing to the MillionApp.Data and with the MillionApp.Api as Start project, then run the command: update-database, this command will run all the current migrations and should create the database.
2. the connection string created on step 1 you shoul put on the class: ApplicationDbContextFactory
