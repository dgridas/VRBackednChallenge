#if you have disabled powerShelS scripts running - just copy script into PowerShell ISE
#then uncomment next string, modify path to actual folder where example was uploaded and press F5
#cd C:\VR-test-example

$Env:hdrPassword="guest"
$Env:hdrDatabase="hdrboxstorage"
$Env:hdrPort="7777"
$Env:hdrSchemaLocation=$("$(Get-Location)\HdrDbSchema\dbHdrBoxSchema.sql")

Write-Host ""
Write-Host "This script will set up a postgres database hosted in a docker container"
read-host "Press ENTER to continue"

docker pull postgres
if (-not $?) { exit 1 }

$portAssign = $Env:hdrPort + ":5432"
$container=$(docker run -e "POSTGRES_PASSWORD=$Env:hdrPassword" -p "$portAssign" -d postgres)
if (-not $?) { exit 1 }

Try {
    Write-Host "Database starting. Setting db schema..."

    pushd HdrDbSchema
    dotnet run
    if (-not $?) { exit 1 }

    Write-Host ""
    Write-Host "The database is ready to use" -ForegroundColor Green
    Write-Host "Conection string: 'Server=localhost; User ID=postgres; Password=$Env:hdrPassword; Port=$Env:hdrPort; Database=$Env:hdrDatabase;'"
    Write-Host "Schema applied to database:"
    cat ./dbHdrBoxSchema.sql
    Write-Host ""
    Write-Host "Press Ctrl^C to stop the database server and exit" -ForegroundColor Green
    docker attach "$container"
} Finally {
    popd
    docker stop "$container"
}

