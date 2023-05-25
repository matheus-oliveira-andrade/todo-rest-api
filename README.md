### Todo REST API using clean architecture principles

#### Run in cli

    dotnet test

<br>

    dotnet run --project src/Todo.Api


#### Run with Docker

    docker build -t todoapp:latest .

<br>

    docker run --name todo-app -d -p 5000:80 todoapp:latest
