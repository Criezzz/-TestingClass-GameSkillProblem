# Game Demo - Typing Battle

## Note
- The whole purpose of this repo is to create test cases using different taught testing methods on class.
- Cast() is the target method.

## Requirements
- .NET 8.0 SDK

## Build & Run

```bash
# Build solution (2 projects: main + test)
dotnet build "[TestingClass]GameSkillProblem.sln"

# Run demo
dotnet run --project GameSkillProblem.csproj
```

## Run Tests

```bash
# Run all tests
dotnet test "[TestingClass]GameSkillProblem.sln"

# Run tests with verbose output
dotnet test "[TestingClass]GameSkillProblem.sln" --verbosity normal
```

## Game Rules

- Select skill: 1 (Fireball - 30 damage, 25 mana) or 2 (Heal - 0 damage, 20 mana) (ignore this)
- Type the sentence shown to cast skill
- Wrong character: -5 mana penalty
- Time limit: 7 seconds/turn
- Lose if: no mana or timeout while enemy alive
