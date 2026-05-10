# ChefGame MVP Scaffold

This repository now contains the first-pass gameplay scripts for a Unity playable prototype:

- `GameManager`: round timer, score, win/loss screen
- `PlayerController`: simple character movement
- `CookingStation`: hold-and-release cooking interaction for boil/fry

## Quick setup in Unity

1. Create a Unity 3D project and add this repo as the project folder.
2. Install TextMeshPro (if prompted).
3. Create a scene at `Assets/_Game/Scenes/Level_01.unity`.
4. Add objects:
   - `GameManager` (with `GameManager.cs`)
   - `Player` (Capsule + CharacterController + `PlayerController.cs`, tag `Player`)
   - Two trigger zones with `CookingStation.cs`:
     - one set to `Boil`
     - one set to `Fry`
   - Canvas with TMP labels for order/time/score/result.
5. Link references in inspector.
6. Press Play and hold `E` in a station zone to cook.

## Current loop

- Goal score: 100
- Perfect boil: +45
- Perfect fry: +55
- Round duration: 90 seconds

This is intentionally minimal to get a fast playable baseline.
