
# feel free to modify this line if your project structure is different to expected
cd HdrBoxReader

$Env:connectionString="Server=localhost; User ID=postgres; Password=guest; Port=7777; Database=hdrboxstorage;"

dotnet build

dotnet run --no-build --project .\HdrBoxReader\HdrBoxReader.csproj


cd ../..