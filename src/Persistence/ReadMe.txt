Open bash and go to persistence folder
cd /c/projects/sales-accounts/src/Persistence

Make Migration
dotnet ef migrations add first-thing -o Migrations

export DATABASE_HOST='localhost'
export DATABASE_NAME='discounting'
export DATABASE_USER_ID='discounting'
export DATABASE_PASSWORD='discounting'

(or in Powershell $env:DATABASE_HOST = 'localhost' etc...)

Apply Migration
dotnet ef database update first-thing