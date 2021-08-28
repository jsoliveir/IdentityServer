# Discovery Endpoints & Confiurations

```powershell
https://localhost:5001/.well-known/openid-configuration
```  

# APIs Authorization  (client credentials)

### Get a new Token
```powershell
curl -X POST 'https://localhost:5001/connect/token' `
    --data 'client_id=service&client_secret=secret&grant_type=client_credentials&scope=weather.read' 
```  

### Request the api


```powershell
curl -X GET "https://localhost:5002/WeatherForecast" `
  -H  "Authorization: Bearer <access_token>"
```

# OIDC (Resource Owner Password)

### Get a new Token

```powershell
curl -X POST 'https://localhost:5001/connect/token' `
    --data 'client_id=customer&grant_type=password&username=jsoliveira&password=1234&scope=openid profile'
```

### Get user profile info

```powershell
 curl -X POST 'https://localhost:5001/connect/userinfo' -H "Authorization: Bearer <acess_token>"
```

result:

 ```json 
 {"username":"jsoliveira","email":"jose.oliveira@domain.com","sub":"1234"}
 ```
