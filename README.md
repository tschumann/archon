Archon
======


Tests
-----

Powershell:
```
powershell ./test.ps1
```


Testing
-------

```
powershell ./run-vapour.ps1
```

Return an auth error:
```
curl -v http://localhost:57257/IGameServersService/GetServerList/v1/
```

Return a response:
```
curl -v http://localhost:57257/IGameServersService/GetServerList/v1/?key=1
```
