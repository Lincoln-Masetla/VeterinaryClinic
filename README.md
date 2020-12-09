# Veterinary Clinic

I have used the following: DynamoDBAWS, LamdaAPI, Gateway, Angular 11

## Installation

### Front End

```bash
npm install
ng serve --open 
```

### Backend

FOR LOCAL Database instance
- Install local dynamo [DynamoDB](https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DynamoDBLocal.html)
- Once everything installed successfully. With postman do the following request
- ```
   POST
   https://localhost:44345/api/tables/create
   ```
FOR REMOTE Database instance 
    -   Please ask me for 
``` 
        "AccessKey": "from me", 
        "SecretKey": "from me"
```

OR you can create your own Remote Dynamodb
    - Create a table Owners with Primary Key of OwnerId
    - Create a table Pets with Primary Key of PetId and Sort key of OwnerId 



