# HospitalMgmtService

## Clone Repository
```
git status
git init
git clone <URL>
```
## Create Branch 
```
git branch <Branch Name>
```
## Commit code 
```
git add . 
git commit -m "your message"
git push origin <branch_name>
git pull 
```

## initalized project using CLI 
```
dotnet tool install --global dotnet-ef
dotnet ef
dotnet ef database migrations add initdb
dotnet ef database update
dotnet run
```

## Configure Database Fresh
1. Delete the database (hms_db - same db name shold present in connection string) from sql server if its already availalbe
2. Delete the migration files. 
3. In snapshot file delete from model builder. Every tables model builder.
3. Build/Rebuild the code
4. Open Package Manger Console
5. Type add-migration initdb command and run 
6. Type update-database command and run

