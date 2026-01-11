# FireworksDX Library

A high-performance fireworks animation library for .NET 9 Windows Forms applications, powered by DirectX hardware acceleration.

## Overview

FireworksDX is a class library that provides realistic fireworks animations using Direct2D rendering with hardware acceleration. It features customizable particle systems, multiple visual effects, and synchronized audio playback.

## Features

### Core Components

- **Hardware-Accelerated Rendering**: Uses SharpDX and Direct2D for optimal GPU performance
- **Particle System Engine**: Realistic physics simulation for rockets, particles, and smoke
- **Audio Synchronization**: NAudio-based sound effects with playback speed adaptation
- **Customizable Visual Effects**: Glow, twinkle, smoke, and trail effects
- **Pre-configured Profiles**: 17 built-in animation styles

### Key Classes

#### `FireworksPanel` (WinForms Control)
The main control that hosts the fireworks animation. Simply drop it into any Windows Forms application.

**Location**: `FireworksDX.WinForms.FireworksPanel`

**Methods**:
- `GetGpuInfo()`: Returns GPU adapter information
- `IsUsingGpu`: Property indicating hardware acceleration status

#### `FireworksEngine`
The core animation engine managing rockets, particles, and physics.

**Location**: `FireworksDX.Engine.FireworksEngine`

**Properties**:
- `Rockets`: List of active rockets
- `Particles`: List of active explosion particles
- `Smoke`: List of smoke particles
- `EnableSound`: Toggle audio playback

**Methods**:
- `Update(int width, int height)`: Updates animation state
- `LaunchImmediateBurst(int width, int height)`: Creates instant explosion effect
- `ReloadSound()`: Reloads audio file (useful for custom sound paths)

#### `FireworksConfig` (Static Configuration)
Global settings for all fireworks behavior.

**Location**: `FireworksDX.Config.FireworksConfig`

**Key Properties**:
- Rocket timing: `MinRocketIntervalFrames`, `MaxRocketIntervalFrames`
- Rocket physics: `RocketMinSpeed`, `RocketMaxSpeed`, `RocketGravity`
- Explosion settings: `MinBursts`, `MaxBursts`, `MinParticles`, `MaxParticles`
- Visual effects: `EnableGlow`, `EnableTwinkle`, `EnableSmoke`
- Audio: `EnableAudio`, `ExplosionSoundPath`

**Built-in Profiles** (17 presets):
1. `LoadDefault()` - Balanced settings
2. `LoadNewYearShow()` - High intensity celebration
3. `LoadJapanHanabi()` - Traditional Japanese style
4. `LoadMegaShow()` - Maximum intensity
5. `LoadGoldenRain()` - Elegant golden particles
6. `LoadCyberNeon()` - Futuristic neon style
7. `LoadCalmFestival()` - Relaxed, sparse
8. `LoadRedDragon()` - Aggressive red theme
9. `LoadWinterFestival()` - Ice-like effects
10. `LoadHalloweenSpirits()` - Orange/purple with dense smoke
11. `LoadSummerBeachParty()` - Vibrant colors, clean
12. `LoadChristmasMagic()` - Red/green/gold theme
13. `LoadChrysanthemum()` - Perfect spherical explosion
14. `LoadPeony()` - Round explosion without long trails
15. `LoadWillow()` - Long falling trails (weeping willow)
16. `LoadPalmTree()` - Central trunk with side branches
17. `LoadStrobeShell()` - Strong intermittent flickering

#### Rendering System
- **`DirectXHost`**: Manages Direct3D device and swap chain
- **`Direct2DRenderer`**: Handles Direct2D drawing operations with effects

#### Audio System
- **`CachedSound`**: Pre-loads audio files into memory for instant playback
- **`CachedSoundSampleProvider`**: Provides variable-speed audio playback with pitch adjustment

## Dependencies

The library requires the following NuGet packages:

```xml
<PackageReference Include="SharpDX" Version="4.2.0" />
<PackageReference Include="SharpDX.Direct2D1" Version="4.2.0" />
<PackageReference Include="SharpDX.Direct3D11" Version="4.2.0" />
<PackageReference Include="SharpDX.DXGI" Version="4.2.0" />
<PackageReference Include="SharpDX.Mathematics" Version="4.2.0" />
<PackageReference Include="NAudio" Version="2.2.1" />
```

## Quick Start

### Basic Usage

```csharp
using FireworksDX.WinForms;

// Add FireworksPanel to your form
var fireworksPanel = new FireworksPanel
{
    Dock = DockStyle.Fill
};
this.Controls.Add(fireworksPanel);
```

### Customizing Settings

```csharp
using FireworksDX.Config;

// Load a preset profile
FireworksConfig.LoadNewYearShow();

// Or customize individual settings
FireworksConfig.MinParticles = 100;
FireworksConfig.MaxParticles = 200;
FireworksConfig.EnableGlow = true;
FireworksConfig.EnableTwinkle = true;
FireworksConfig.EnableSmoke = false;
```

### Custom Audio

```csharp
using FireworksDX.Config;

// Set custom explosion sound
FireworksConfig.ExplosionSoundPath = @"C:\Sounds\custom_explosion.wav";

// Access engine and reload sound
var engine = GetEngineInstance(); // see reflection example below
engine?.ReloadSound();
```

### Accessing the Engine (Reflection)

The `FireworksPanel` has a private `engine` field. If you need direct access:

```csharp
using System.Reflection;

var panelType = fireworksPanel.GetType();
var engineField = panelType.GetField("engine", BindingFlags.NonPublic | BindingFlags.Instance);
var engine = engineField?.GetValue(fireworksPanel) as FireworksEngine;

if (engine != null)
{
    // Launch immediate burst effect
    engine.LaunchImmediateBurst(fireworksPanel.Width, fireworksPanel.Height);
    
    // Toggle audio
    engine.EnableSound = false;
}
```

### GPU Information

```csharp
// Check if hardware acceleration is active
bool isHardwareAccelerated = fireworksPanel.IsUsingGpu;

// Get detailed GPU information
string gpuInfo = fireworksPanel.GetGpuInfo();
MessageBox.Show(gpuInfo, "DirectX Information");
```

## Audio System

The library includes a built-in audio file (`Audio\explosion.wav`) that is automatically copied to the output directory. The audio system features:

- **Cached Playback**: Audio is pre-loaded into memory for instant playback
- **Speed Adaptation**: Playback speed adjusts based on rocket flight time for perfect synchronization
- **Mixing Support**: Multiple sounds can play simultaneously without clipping

### Audio Path Resolution

1. If `FireworksConfig.ExplosionSoundPath` is set, uses that path
2. Otherwise, looks for `Audio\explosion.wav` relative to the DLL location

## Performance Considerations

- **Hardware Acceleration**: The library automatically detects and uses GPU acceleration when available
- **Fallback Rendering**: Falls back to software rendering if DirectX initialization fails
- **Particle Limits**: Adjust `MinParticles`/`MaxParticles` to balance visual quality and performance
- **Frame Rate**: Targets 60 FPS (~16ms per frame)

## Project Structure

```
FireworksDX/
├── Audio/
│   ├── CachedSound.cs                 # Audio file caching
│   ├── CachedSoundSampleProvider.cs   # Variable-speed playback
│   └── explosion.wav                  # Default sound effect
├── Config/
│   └── FireworksConfig.cs             # Global settings and profiles
├── Engine/
│   ├── FireworksEngine.cs             # Main animation engine
│   ├── Rocket.cs                      # Rocket entity
│   ├── Particle.cs                    # Explosion particle
│   ├── SmokeParticle.cs               # Smoke effect particle
│   └── Vec2.cs                        # 2D vector math
├── Rendering/
│   ├── DirectXHost.cs                 # DirectX device management
│   └── Direct2DRenderer.cs            # Direct2D rendering
└── WinForms/
    └── FireworksPanel.cs              # Windows Forms control
```

## Target Framework

- **.NET 9.0** (Windows)
- Requires Windows Forms support

## License

LICENSED UNDER THE MIT LICENSE. SEE LICENSE FILE FOR DETAILS.

## Author

G.L. aka Firefox_1998


## Repository

[Add repository URL here]
