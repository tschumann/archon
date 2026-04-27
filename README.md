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
```
curl -v "http://localhost:57257/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v1/?gameid=70"
```
```
curl -v "http://localhost:57257/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v1/?gameid=220"
```
```
curl -v "http://localhost:57257/ISteamUserStats/GetNumberOfCurrentPlayers/v1/?appid=220"
```
```
curl -v "http://localhost:57257/ISteamWebAPIUtil/GetServerInfo/v0001/"
```
```
curl -v "http://localhost:57257/ISteamWebAPIUtil/GetSupportedAPIList/v0001/"
```