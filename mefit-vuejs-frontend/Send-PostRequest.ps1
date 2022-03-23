$header = @{
    "Authorization"="Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJPSUFITkRrbFJPbHo2dEZhYTU5OVNLNHdEWGRQb0xZbm9yVDR0VjBGQjBBIn0.eyJleHAiOjE2NDgwMzU4OTUsImlhdCI6MTY0ODAzNTU5NSwiYXV0aF90aW1lIjoxNjQ4MDEyNzA2LCJqdGkiOiI0NGQwNmVhNi01NjNkLTQwZTMtYTdjMi1lNzRiYWM2MDhjYzYiLCJpc3MiOiJodHRwczovL21lZml0LWtleWNsb2FrMS5oZXJva3VhcHAuY29tL2F1dGgvcmVhbG1zL01lRml0X09IIiwiYXVkIjoiYWNjb3VudCIsInN1YiI6IjZmMmQyZDAxLWQ2MDYtNDhlMC04NjQ1LTc5NTZkZjFjMzUwMiIsInR5cCI6IkJlYXJlciIsImF6cCI6Ik1lRml0X1ZBX09IX1BQIiwibm9uY2UiOiI2MWJjMzQ3My03YzkxLTRiNmItODkxYy0xY2MyZjFmYjY4MjIiLCJzZXNzaW9uX3N0YXRlIjoiOGJhMzMwZDktNGFjOC00ZGIyLTkyNzUtZjI2MGRkMjc0YzViIiwiYWNyIjoiMCIsImFsbG93ZWQtb3JpZ2lucyI6WyJodHRwczovL3d3dy5tZWZpdC12YS1wcC1vaC5uZXQvIiwiaHR0cHM6Ly93d3cubWVmaXQtdmEtcHAtb2gubmV0IiwiaHR0cDovL2xvY2FsaG9zdDo0MTczIiwiaHR0cDovL2xvY2FsaG9zdDozMDAwLyIsImh0dHA6Ly9sb2NhbGhvc3Q6MzAwMCIsImh0dHA6Ly9sb2NhbGhvc3Q6NDE3My8iXSwicmVhbG1fYWNjZXNzIjp7InJvbGVzIjpbIm9mZmxpbmVfYWNjZXNzIiwidW1hX2F1dGhvcml6YXRpb24iLCJkZWZhdWx0LXJvbGVzLW1lZml0X29oIl19LCJyZXNvdXJjZV9hY2Nlc3MiOnsiYWNjb3VudCI6eyJyb2xlcyI6WyJtYW5hZ2UtYWNjb3VudCIsIm1hbmFnZS1hY2NvdW50LWxpbmtzIiwidmlldy1wcm9maWxlIl19LCJNZUZpdF9WQV9PSF9QUCI6eyJyb2xlcyI6WyJjb250cmlidXRvciJdfX0sInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwiLCJzaWQiOiI4YmEzMzBkOS00YWM4LTRkYjItOTI3NS1mMjYwZGQyNzRjNWIiLCJlbWFpbF92ZXJpZmllZCI6ZmFsc2UsInJvbGUiOlsiY29udHJpYnV0b3IiXSwibmFtZSI6Ik9saXZlciBIYXVjayIsInByZWZlcnJlZF91c2VybmFtZSI6Im9saXZlciBoYXVjayIsImdpdmVuX25hbWUiOiJPbGl2ZXIiLCJmYW1pbHlfbmFtZSI6IkhhdWNrIiwiZW1haWwiOiJvbGl2ZXIuaGF1Y2tAZGUuZXhwZXJpcy5jb20ifQ.bJpkfXw32R7OnB7wc4bXhbEp9L20aPirFEk8NhDYH03Bv25r7T6AQRZPCDvzyU85AHIvbYSk3VAsLKy35jCAY2QfFPhIWhpXsJI88VMyyQk3M2j3vFNl3jJKQXo1S-_WzcVNzjbdgCckmhY8yE_4trQzJl4obzJCmuZR10H1r-27GYm6_ljGtm1mphnZ7OP_YQKTci10HuRrTx8H1H3m4MS6j9a4dL6uduGhJouQifVbspuLyxR2m3QuCNSijyz_bLVoYjNvKdHgAquJVtaNiQeBYyiclwZtWIaKDnQdl_5tBGkRkchvzJ50noYFveDHPCtY5VWdWDMQyIMt9X9thQ";
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