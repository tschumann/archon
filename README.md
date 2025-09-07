Archon
======


Tests
-----

Powershell:
```
powershell ./test.ps1
```


Testing Archon
--------------

```
powershell ./run-archon.ps1
```
```
dotnet run --project Archon.TestClient
```


Testing Ludum
-------------

```
powershell ./run-ludum.ps1
```
```
dotnet run --project Ludum.TestClient
```


Testing Vapour
--------------

```
powershell ./run-vapour.ps1
```

Return an auth error:
```
curl -v "http://localhost:57257/IGameServersService/GetServerList/v1/"
```

Return a response:
```
curl -v "http://localhost:57257/IGameServersService/GetServerList/v1/?key=1"
```
```
curl -v "http://localhost:57257/IGameServersService/GetServerList/v1/?key=1&filter=appid\70"
```
