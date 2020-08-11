# Conta Service

Microservice .Net Core 2.2 in Docker Container simulating Bank Account operations.

## Get started

To call the Conta Corrente API operations, you must be authenticated. The service has an authorization API that provides access to requests.

### Calling the APIs using Swagger

Upon launch, the service will open the Swagger UI page. Select the "Authorize" button, then open a modal with the authorization fields.
Fill in the "username" field with the user "sample@mail.com" and the password field with "accountservice", the remaining fields must be blank. If all goes well, Swagger will be authorized to make requests for the API.

### Caling the APIs using REST Client

To obtain authorization, make the request using the POST method for 
"[host]/api/v1/authorization". The request body must contain the following parameters:

```json
{
    "username": "sample@mail.com",
    "password": "accountservice"
}
```
After the request, the response should look like the response below:


```json

{
  "token_type": "Bearer",
  "expires_in": 3600,
  "ext_expires_in": 0,
  "access_token":"eyJhbGciOiJSUzI1NiIsImtpZCI6ImFmMDg2ZmE4Y2Q5NDFlMDY3ZTc3NzNkYmIwNDcxMjAxMTBlMDA1NGEiLCJ0eXAiOiJKV1QifQeyJpc3MiOiJodHRwczovL3NlY3VyZXRva2V...",
  "id_token":"eyJhbGciOiJSUzI1NiIsImtpZCI6ImFmMDg2ZmE4Y2Q5NDFlMDY3ZTc3NzNkYmIwNDcxMjAxMTBlMDA1NGEiLCJ0eXAiOiJKV1QifQeyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdv..."
}

```

To make requests add the following header:

```
Authorization: Bearer [access_token] 

