
Function DeleteEnvVar($key) {
    Write-Output "Deleting $key"
    DeleteEnvVarByTarget $key "Process"
    DeleteEnvVarByTarget $key "Machine"
    DeleteEnvVarByTarget $key "User"
}

Function DeleteEnvVarByTarget($key, $target) {
    [System.Environment]::SetEnvironmentVariable($key,$null, [System.EnvironmentVariableTarget]$target)
}

Function GetEnvVar($key) {
    $value = GetEnvVarByTarget $key "Process"
    Write-Output "$key Process $value"
    $value = GetEnvVarByTarget $key "Machine"
    Write-Output "$key Machine $value"
    $value = GetEnvVarByTarget $key "User"
    Write-Output "$key User $value"
}

Function GetEnvVarByTarget($key, $target) {
    [System.Environment]::GetEnvironmentVariable($key, [System.EnvironmentVariableTarget]$target)
}

foreach($line in Get-Content .\variables_list.txt) {
    DeleteEnvVar($line)
}