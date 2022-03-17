$header = @{
    "Authorization"="Bearer ";
}

# $response = Invoke-WebRequest -Uri "https://localhost:44390/api/Workouts" -Method "Get" -Headers $header
# $response
$response2 = ""
$response2 = Invoke-RestMethod -Uri "https://localhost:44390/api/exercises" -Method "Get" -Headers $header
$response2