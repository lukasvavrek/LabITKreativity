$global:rootPath = (Get-Item $PSScriptRoot).Parent.FullName

function Docker-Up($stack) {
    pushd "$rootPath\tools\docker\$stack"

    try
    {
        docker-compose --project-name $stack up -d
    }
    finally
    {
        popd
    }
}

function Docker-Down($stack) {
    pushd "$rootPath\tools\docker\$stack"

    try
    {
        docker-compose --project-name $stack down
    }
    finally
    {
        popd
    }
}

if ($args[0] -eq "docker" -and $args[1] -eq "up") {
    Docker-Up($args[2])
}

if ($args[0] -eq "docker" -and $args[1] -eq "down") {
    Docker-Down($args[2])
}

