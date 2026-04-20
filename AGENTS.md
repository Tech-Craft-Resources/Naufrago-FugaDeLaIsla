# AGENTS.md for unity-naufrago

## Project Overview
- Unity 2D platformer game: "Naufrago Fuga de la Isla" (Shipwreck Escape from the Island).
- Template: com.unity.template.2d@7.0.4 (requires Unity 2021+).
- Key scenes: MainMenu, Level01, GameOver, WinScreen (Assets/Scenes/).
- ProjectSettings default scene: Assets/Scenes/SampleScene.unity (does not exist; start from MainMenu).

## Key Packages
- Input System (1.14.2): Uses New Input for controls.
- Cinemachine (2.10.7): Camera management.
- TextMeshPro (3.0.7): UI text.
- Ads (4.4.2), Analytics (3.8.1), Purchasing (4.11.0): Monetization features.

## Scripts Structure
- Player: Movement, Jump, Health, NewInput.
- Enemy: PatrolEnemy, JumpEnemy.
- Managers: GameManager, UIManager, ScoreManager, AudioManager.
- Collectibles: Collectible.
- Mechanics: DeadZone, WinTrigger, Checkpoint.

## Build Targets
- Android: Min SDK 22, target not specified.
- iOS: Target OS 12.0.
- macOS: Target 10.13.0.
- Standalone: Default resolution 1920x1080.

## Operational Notes
- No CI workflows; builds via Unity Editor or command line.
- No tests configured beyond Unity Test Framework package.
- Uses Unity Collaborate for version control (collab-proxy package).</content>
<parameter name="filePath">/Users/daniv/dev/USA/unity-naufrago/AGENTS.md