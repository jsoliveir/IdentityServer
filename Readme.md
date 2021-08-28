# Get a new token

```powershell
curl -X POST 'https://localhost:5001/connect/token' `
    --data 'client_id=service.client&client_secret=secret&grant_type=client_credentials&scope=weather.read' 
```  

# Request the api


```powershell
curl -X GET "https://localhost:5002/WeatherForecast" `
  -H  "Authorization: Bearer <access_token>"
```