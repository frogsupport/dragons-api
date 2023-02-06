# Here there be dragons

A sample ASP.NET CORE project

## Setup

You will need to have you AWS credentials configured to have access to the database we are using. Make sure to run `aws configure` from the commande
line to get signed in.

## Database

This project uses Postgresql, as well as Dapper for its data access. The database is hosted on AWS.

### How to use

To connect to your database (if you want to use your own) you need the connection string. Add an appsettings.json file inside dragons.api. Here's an example:

```json
{
  "ConnectionStrings": {
    "DragonConnection": "Host=****;Username=****;Password=****;Database=****"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

If you want to connect to my database then you will need to work with me LOL

### Users Repository

A repository to hold users. Email must be unique, and will uniquely identify that user.

### Dragons Repository

A repository to hold dragons

Select query example

```sql
SELECT * FROM dragons;
```

Insert query example

```sql
INSERT INTO dragons(dragon_id, name, type, size)
VALUES ('92151c49-e34b-46c9-a412-d80827746b86', 'albus', 'gold', 'mid')
RETURNING *;
```

Update

```sql
Update dragons SET name='Wizard', type='Gold', size='Massive' WHERE dragon_id='92151c49-e34b-46c9-a412-d80827746b86';
```

<b>Note that naming columns with uppercase letters in postgres is a huge pain!!</b>

## Build and Deploy

Navigete to the proper project folder in powershell.

To build the framework-dependent release build<br>
`dotnet publish -r linux-x64 --self-contained false`

To run the compiled project<br>
`dotnet <project_name>.dll`

## Fluent Validation

Automatic fluent validation is currently configured for this application, and if a validator is registered for a type that validator will run before
the controller is called. If one of the validation rules fails, then a bad request will be returned automatically with the results of the 
errors in a dictionary format.

## Reference Documentation

[RID Graph](https://learn.microsoft.com/en-us/dotnet/core/rid-catalog#rid-graph)<br>
[Framework dependent deployment](https://learn.microsoft.com/en-us/dotnet/core/deploying/#framework-dependent-deployments-fdd)<br>
[Host linux server on nginx](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-6.0&tabs=linux-ubuntu)<br>
[Proxy servers and load balancers](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/proxy-load-balancer?view=aspnetcore-6.0)<br>
[Transfer files to EC2 locally](https://docs.aws.amazon.com/managedservices/latest/appguide/qs-file-transfer.html)<br>

[Linking db example](https://www.syncfusion.com/blogs/post/creating-an-asp-net-core-crud-web-api-with-dapper-and-postgresql.aspx)<br>
[Another db example](https://github.com/SyncfusionExamples/ASP.NET-Core-Web-API-with-Dapper-and-PostgreSQL)<br>
[Dapper](https://www.learndapper.com/)<br>

[ASP.NET CORE 6.0 Basic Auth tutorial](https://www.youtube.com/watch?v=pY9Rcc3gsAA&ab_channel=NihiraTechiees)<br>
[Microsoft authentication documentation](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/limitingidentitybyscheme?view=aspnetcore-6.0)<br>
[Role based authorization example](https://github.com/cornflourblue/dotnet-6-role-based-authorization-api)<br>

[Microsoft integration test doc](https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0)<br>

[Run .NET 6 on EC2](https://nodogmablog.bryanhogan.net/2021/12/how-to-run-net-6-kestrel-and-web-api-on-an-aws-ec2-windows-instance/)<br>
[AWS CDK tutorial](https://docs.aws.amazon.com/cdk/v2/guide/hello_world.html)<br>
[AWS CDK module tutorial](https://aws.amazon.com/getting-started/guides/deploy-webapp-ec2/module-one/)<br>
[Create a code pipeline](https://docs.aws.amazon.com/codepipeline/latest/userguide/pipelines-create.html)<br>
