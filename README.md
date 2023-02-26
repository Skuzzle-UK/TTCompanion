# Tabletop Companion
The purpose of Tabletop Companion is to provide a resource for tabletop gamers.

Initially this will start with an API followed by a Blood Bowl companion app for creating team rosters, tracking team stats through the lifetime of the team and setting up leagues/championships/friendlies while tracking the game results. Players will be able to see their teams standings and stats compared to other public teams. Private leagues will be managable via team invite and public leagues will exist.

## Rules
Anybody can contribute. Code will be reviewed via pull request before it makes it to master. Unit tests must be created in an associated xUnit tests project and submissions must pass all tests.
Contributors should aim for clean coding but guidance will be given if a pull request needs work before merging.
Documentation is also key to a successful project so if you make code changes that might need documenting then please update readme's and wiki.
If you are not a coder then feel free to update wiki and do what you can.
All projects are C# .NET 6 (Core) where possible

## Projects
### TTCompanion.API
An ASP.NET 6 (Core) Web API for tabletop game data such as the many games published by GW.

### TTCompanion.API.Tests
.NET 6 (Core) xUnit test project for testing TTCompanion.API
