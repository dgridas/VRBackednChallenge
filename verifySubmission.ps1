
# feel free to modify this line if your project structure is different to expected
cd VismaChallenge/Employee

$Env:connectionString="Server=localhost; User ID=postgres; Password=guest; Port=7777; Database=analyticsproject;"

dotnet build

dotnet run --no-build --project .\set-employee\set-employee.csproj --employeeId 1 --employeeName John --employeeSalary 333
dotnet run --no-build --project .\set-employee\set-employee.csproj --employeeId 2 --employeeName Mary --employeeSalary 444

dotnet run --no-build --project .\get-employee\get-employee.csproj --employeeId 1
dotnet run --no-build --project .\get-employee\get-employee.csproj --employeeId 2

$past = (Get-Date).ToUniversalTime()

Start-Sleep -s 2

dotnet run --no-build --project .\set-employee\set-employee.csproj --employeeId 1 --employeeName John --employeeSalary 555

dotnet run --no-build  --project .\get-employee\get-employee.csproj --employeeId 1 
dotnet run --no-build  --project .\get-employee\get-employee.csproj --employeeId 1 --simulatedTimeUtc "$($past.ToString("yyyy-MM-ddTHH:mm:ssZ"))"

cd ../..