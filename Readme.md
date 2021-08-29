# Discovery Endpoints & Confiurations

```powershell
http://localhost:8080/identity/.well-known/openid-configuration
```  

# APIs Authorization  (client credentials)

### Get a new Token
```powershell
$token = (curl -X POST 'http://localhost:8080/identity/connect/token' `
   --data 'client_id=service&client_secret=secret&grant_type=client_credentials&scope=weather.read' | ConvertFrom-Json
).access_token
```  

### Request the api


```powershell
curl -X GET "http://localhost:8080/api/WeatherForecast" `
  -H  "Authorization: Bearer <access_token>"
```

# OIDC (Resource Owner Password)

### Get a new Token

```powershell
curl -X POST 'http://localhost:8080/identity/connect/token' `
    --data 'client_id=customer&grant_type=password&username=jsoliveira&password=1234&scope=openid profile'
```

### Get user profile info

```powershell
 curl -X POST 'http://localhost:8080/identity/connect/userinfo' -H "Authorization: Bearer <acess_token>"
```

result:

 ```json 
 {"username":"jsoliveira","email":"jose.oliveira@domain.com","sub":"1234"}
 ```
