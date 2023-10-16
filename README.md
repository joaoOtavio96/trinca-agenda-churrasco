
# Agenda Churrasco
API para gerenciar churrascos da empresa.
### Dependencias
```.NET 6```
```Docker```
```Docker compose```

### Usando a aplicação 
Etapas para usar a aplicação:

- Subir aplicação no Docker.
```bash
docker compose up -d --build
```
- Rodar migrations.
```bash
cd src/Trinca.AgendaChurrasco.Data
dotnet ef --startup-project ../Trinca.AgendaChurrasco.Api/ database update
```
### Testes
```bash
dotnet test Trinca.AgendaChurrasco.sln
```
