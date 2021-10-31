# dotnet-mailgun

How to add a NuGet source:

```sh
$ dotnet nuget add source https://nuget.pkg.github.com/USERNAME/index.json --name github --username "USERNAME" --password "PASSWORD"
```

How to pack and push a NuGet package:

```sh
$ dotnet pack --configuration Release
$ dotnet nuget push .\src\Mailgun.Client\bin\Release\Mailgun.Client.VERSION.nupkg --source github
```
