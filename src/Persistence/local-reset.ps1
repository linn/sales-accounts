$env:DATABASE_HOST='localhost'
$env:DATABASE_NAME='sales-accounts'

# to enable priviledges on your local machine only allow sales accounts login role to Create database and Superuser
$env:DATABASE_USER_ID='sales-accounts'
$env:DATABASE_PASSWORD='sales-accounts'

dotnet ef database drop -f
dotnet ef database update
