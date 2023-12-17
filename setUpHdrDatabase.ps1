cd C:\Work\Dima\dev\VRGroup-test-task-backend\VR-backend-challenge
$Env:hdrPassword="hdruser"
$Env:hdrDatabase="HdrBoxStorage"
$Env:hdrPort="8888"
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

#C:\Work\Dima\dev\VRGroup-test-task-backend\VR-backend-challenge\HdrDbSchema
#C:\Work\Dima\dev\VRGroup-test-task-backend\VR-backend-challenge\HdrDbSchema