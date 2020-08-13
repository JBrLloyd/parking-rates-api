# Parking Rate API

## Getting started

Using Visual Studio, you can start the WebUI project.
Using dotnet cli in Powershell, `dotnet run src/WebUI/WebUI.csproj`
*Note*, data will be seeded to an in memory SQL dB.

## Known Bugs

**SwaggerUI does not Support executing HTTP/1.1 GET method with JSON body***

## ToDos

[ ] Add health check for dB (only external resource)
[ ] Add Integration Tests for dB (only external resource)
[ ] Rework the data structures (they're messy and rushed) 😢
[ ] Add more exception behaviours
[ ] Add OAuth Authentication and Claim-based Authorization (don't want to spend money on fancy Idps)
[ ] Git Actions Build and CI Deploy

## Logic Assumptions

1. If parking is over 2 days the rate is max per day (Even if the exits at 1 AM)
2. The things that should be returned is the Rate Name, Cost and Total

## Decision

- Using and extending Jason Taylor's Clean Architecture template
- Open Swagger immediately rooted
- Using GET Method with a JSON request payload because we're retrieving information
- Keeping a flat data structure and no relationships so this service can use a NoSQL dB
