# Event Registration

A tiny app showing an example of how to create ASP .NET core application using MVC pattern.

## Use case

1. There are two kinds of users 
   - An event creator who has to login 
   - An event participant, who can register for the event 
2. The event creator  
   - Can create an event 
   - Can see all registrations for an event he created
   - Can draft events(such events are not visible to participants)
   - Can delete events
   - Can modify events
3. The event participant 
   - Can see all events which are not in draft state
   - Choose one event and fill the registration form for it 

## Build and Run
- Debug mode
```
dotnet run
```

- Release mode
```
dotnet publish -c Release -r <required-arch> --self-contained

./bin/Release/net8.0/osx-arm64/publish/EventRegistration
```

- Docker
```
docker-compose up
```


## Access
The app is available under localhost:8020