$header = @{
    "Authorization"="Bearer ";
    "Content-Type"="application/json"
    "Accept"="application/json, text/plain, */*"
}
# $body = @{
#     "startDate"="2022-01-02" #2nd jan
#     "endDate"="2022-01-03"  #3rd Jan
# }
$body = @{
    "startDate"="2022-03-24T00:00:00.000Z" #2nd jan
    "endDate"="2022-01-03"  #3rd Jan
}

# $response = Invoke-WebRequest -Uri "https://localhost:44390/api/Workouts" -Method "Get" -Headers $header
# $response
$response2 = ""
# $response2 = Invoke-RestMethod -Uri "https://localhost:44390/api/exercises" -Method "Get" -Headers $header
# $response2 = Invoke-RestMethod -Uri "https://localhost:5001/api/user" -Method "Get" -Headers $header
# $response2 = Invoke-WebRequest -Uri "https://localhost:5001/api/user/5" -Method "Get" -Headers $header
# $response2 = Invoke-WebRequest -Uri "https://localhost:5001/api/exercises" -Method "Get" -Headers $header

$response2 = Invoke-WebRequest -Uri "https://mefitapi-va-pp-oh.azurewebsites.net/api/goal/" -Method "Post" -Headers $header -Body ($body | ConvertTo-Json)

$response2