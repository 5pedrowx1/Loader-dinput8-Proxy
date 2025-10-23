# GTA V MOD LOADER v2.1.0
**by 5pedrowx1**

---

> Interprocess Mod Loader and dinput8.dll proxy for GTA V — developer-friendly IPC API, GUI manager, performance monitor, and hot-reload support.

---

## Table of Contents

1. [For Users](#for-users)  
   1.1 [Features](#features)  
   1.2 [Requirements](#requirements)  
   1.3 [Installation](#installation)  
   1.4 [How to Use](#how-to-use)  
   1.5 [Configuration](#configuration)  
   1.6 [Troubleshooting](#troubleshooting)

2. [For Developers](#for-developers)  
   2.1 [Architecture](#architecture)  
   2.2 [Building](#building)  
   2.3 [IPC API](#ipc-api)  
   2.4 [Mod Integration](#mod-integration)  
   2.5 [Performance Monitoring](#performance-monitoring)  
   2.6 [Debugging](#debugging)  
   2.7 [Contributing](#contributing)

---

## For Users

### Features
- **Automatic Loading** — Detects and loads mods from `mods/` and `scripts/` folders.  
- **Graphical Interface** — Visual Mod Manager (`GTAVModManager.exe`).  
- **Performance Monitor** — Track CPU, memory usage, and per‑mod performance.  
- **Hot Reload** — Reload mods without restarting the game.  
- **Logging System** — Detailed console and full history logs.  
- **ScriptHook Support** — Compatible with ScriptHookV and ScriptHookVDotNet.  
- **IPC Server** — Interprocess communication using Named Pipes.  
- **Smart Detection** — Automatically scans loaded modules.

### Requirements
- **Operating System:** Windows 10/11 (64‑bit)  
- **GTA V:** Latest version of the game  
- **Visual C++ Redistributable:** 2015–2022 x64  
  - Download: `https://aka.ms/vs/17/release/vc_redist.x64.exe`  
- **.NET Framework:** 4.8 or later (required for GUI)

### Installation
1. Download the latest release from the repository releases page.  
2. Extract all files into your GTA V root directory:

```
Grand Theft Auto V/
├── dinput8.dll          ← Main Mod Loader proxy
├── dinput8_config.ini   ← Configuration file
├── GTAVModManager.exe   ← Graphical Interface (optional)
├── mods/                ← Place your mods here (.dll, .asi)
├── scripts/             ← ScriptHookVDotNet scripts (.dll, .cs)
└── logs/                ← Automatic logs
```

3. (Optional) Install ScriptHook:  
   - ScriptHookV: http://www.dev-c.com/gtav/scripthookv/  
   - ScriptHookVDotNet: https://github.com/scripthookvdotnet/scripthookvdotnet

4. Launch GTA V normally — the Mod Loader will start automatically.

### How to Use

#### Method 1 — Graphical Interface (Recommended)
1. Launch GTA V (Mod Loader starts automatically).  
2. `GTAVModManager.exe` will open.  
3. Use the GUI to:
   - View loaded mods  
   - Reload individual mods  
   - Monitor performance  
   - View real‑time logs

#### Method 2 — Manual
1. Place mods in:
   - `mods/` — for `.dll` and `.asi` mods  
   - `scripts/` — for .NET scripts

2. Start GTA V.  
3. Open the console (ALT+TAB) to view logs, e.g.:

```
[2025-01-15 10:30:45] [SUCCESS] Loaded: MyMod.dll (1024 KB)
[2025-01-15 10:30:46] [INFO] Mods loaded: 5
```

### Configuration
Edit `dinput8_config.ini`:

```ini
[UI]
Enable=1                      # 1 = Enable GUI
AutoLaunch=1                  # 1 = Auto-launch GUI with the game
ExecutableName=GTAVModManager.exe

[AutoLoad]
AutoLoadModsFolder=1          # 1 = Load mods from mods/
AutoLoadScriptsFolder=1       # 1 = Load scripts from scripts/

[ScriptHook]
LoadScriptHookV=1             # 1 = Load ScriptHookV.dll
LoadScriptHookVDotNet=0       # 1 = Load ScriptHookVDotNet.asi

[Logging]
MaxLogEntries=500             # Max logs in memory
VerboseLogging=0              # 1 = Enable debug-level logs

[Performance]
EnableMonitor=1               # 1 = Enable performance monitor
MonitorIntervalMS=5000        # Interval (ms)
```

### Troubleshooting

**Problem:** Game doesn't start  
**Solutions:**
- Ensure the file name is exactly `dinput8.dll`.  
- Install Visual C++ Redistributable (x64).  
- Temporarily move `dinput8.dll` out of the GTA folder to test.

**Problem:** Mods aren't loading  
**Solutions:**
- Verify files are in `mods/` or `scripts/`.  
- Check console/logs for error messages.  
- Ensure `AutoLoadModsFolder=1` in the config.

**Problem:** GUI not opening  
**Solutions:**
- Ensure `Enable=1` and `AutoLaunch=1`.  
- Confirm .NET Framework 4.8+ is installed.  
- Manually run `GTAVModManager.exe`.

**Problem:** Performance issues  
**Solutions:**
- Disable `EnableMonitor` in the config.  
- Disable `VerboseLogging`.  
- Investigate slow mods with the performance monitor.

---

## For Developers

### Architecture

```
┌─────────────────────────────────────┐
│         dinput8.dll (Proxy)         │
├─────────────────────────────────────┤
│  ConfigManager (INI Configuration)  │
│  ModManager  (Load/Unload/Reload)   │
│  PerformanceMonitor (per-mod stats) │
│  IPCServer (Named Pipe: GTAVModLoader)
│  Logger (Console + File + History)  │
└─────────────────────────────────────┘
```

### Building

**Requirements**
- Visual Studio 2022  
- Windows SDK 10.0  
- C++20 (recommended)

**Build steps**

```bash
# Clone the repository
git clone https://github.com/5pedrowx1/Loader-dinput8-Proxy.git
cd gtav-mod-loader

# Open solution
start "GTA V Loader.sln"

# Or build from command line
msbuild "GTA V Loader.sln" /p:Configuration=Release /p:Platform=x64
```

**Project structure**

```
GTA V Loader/
├── Core.h / Core.cpp
├── ConfigManager.h / ConfigManager.cpp
├── ModManager.h / ModManager.cpp
├── PerformanceMonitor.h / PerformanceMonitor.cpp
├── IPCServer.h / IPCServer.cpp
├── Logger.h / Logger.cpp
├── dinput8.cpp
├── dinput8.def
└── pch.h
```

### IPC API

The loader exposes a Named Pipe API at: `\\.\pipe\GTAVModLoader`

**C++ example**
```cpp
HANDLE hPipe = CreateFileW(
    L"\\\\.\\pipe\\GTAVModLoader",
    GENERIC_READ | GENERIC_WRITE,
    0, NULL, OPEN_EXISTING, 0, NULL
);
if (hPipe == INVALID_HANDLE_VALUE) {
    // handle error
}
```

**C# example**
```csharp
using (var client = new NamedPipeClientStream(".", "GTAVModLoader", PipeDirection.InOut))
{
    client.Connect(5000); // timeout in ms
    // use client
}
```

**IPC Message structure**
```cpp
#pragma pack(push, 1)
struct IPCMessage {
    char command[32];   // e.g. "GET_MODS", "LOAD_MOD"
    char data[512];     // optional payload (path, mod id, etc.)
};
#pragma pack(pop)
```

**Available commands & responses (summary)**

- `GET_MODS`  
  **Request:** `IPCMessage` with `command="GET_MODS"`  
  **Response:** JSON example:
  ```json
  {
    "mods": [
      {
        "id": "mod_a1b2c3d4",
        "name": "MyMod.dll",
        "path": "C:\\GTA\\mods\\MyMod.dll",
        "loaded": true,
        "type": "mod",
        "size": 1048576,
        "call_count": 1250,
        "avg_execution_us": 45,
        "load_time": "2025-01-15 10:30:45"
      }
    ],
    "count": 1
  }
  ```

- `LOAD_MOD`  
  **Request:** `command="LOAD_MOD"` + `data="C:\\path\\to\\MyMod.dll"`  
  **Response:** `"SUCCESS"` or `"FAILED"`

- `UNLOAD_MOD`  
  **Request:** `command="UNLOAD_MOD"` + `data="mod_a1b2c3d4"`

- `RELOAD_MOD`  
  **Request:** `command="RELOAD_MOD"` + `data="mod_a1b2c3d4"`

- `GET_STATUS`  
  **Response (JSON):**
  ```json
  {
    "version": "2.1.0",
    "uptime_seconds": 3600,
    "mods_loaded": 5,
    "server_running": true,
    "performance_monitor_active": true,
    "game_detected": true
  }
  ```

- `GET_LOGS`  
  **Response (JSON):**
  ```json
  {
    "logs": [
      {
        "timestamp": "2025-01-15 10:30:45",
        "level": "INFO",
        "message": "Mod loaded successfully"
      }
    ],
    "count": 100
  }
  ```

- `GET_PERFORMANCE`  
  **Response (JSON):**
  ```json
  {
    "memory_mb": 256,
    "peak_memory_mb": 512,
    "cpu_percent": 5.43,
    "thread_count": 12,
    "handle_count": 456,
    "total_mods": 5,
    "uptime_seconds": 3600,
    "slowest_mod": "HeavyMod.dll",
    "slowest_mod_time_us": 1500
  }
  ```

- Other commands: `SCAN_FOLDER`, `RELOAD_ALL`, `RESCAN_LOADED`, `CLEAR_LOGS`

### Mod Integration

**Optional exported entry points**
```cpp
extern "C" __declspec(dllexport) void Initialize() {
    // initialization code
}
extern "C" __declspec(dllexport) void Cleanup() {
    // cleanup code
}
```

**Alternative names supported**
- `Init()`, `Startup()`, `OnLoad()`  
- `Shutdown()`, `OnUnload()`, `Dispose()`

**C# client usage example (async helpers assumed)**
```csharp
var client = new ModLoaderClient();
var modsJson = client.GetMods();
// parse JSON and enumerate mods...
```

### Performance Monitoring

Automatic metrics:
- Per‑mod call count and average execution time  
- Memory (current & peak)  
- CPU usage  
- Thread and handle counts  
- Uptime

**Scoped timer example**
```cpp
#include "PerformanceMonitor.h"

void MyModFunction() {
    PerformanceMonitor::ScopedTimer timer("my_mod_id");
    // your code...
}
```

### Debugging

**Logging macros**
```cpp
#include "Logger.h"

LOG_DEBUG("Debug message");
LOG_INFO("Info message");
LOG_SUCCESS("Success message");
LOG_WARNING("Warning message");
LOG_ERROR("Error message");
```

**Temporarily disable features**
```cpp
Globals::g_Config.autoLoadMods = false;
Globals::g_Config.autoLoadScripts = false;
Globals::g_Config.loadScriptHookV = false;
Globals::g_Config.loadScriptHookVDotNet = false;
```

### Contributing

1. Fork the repository  
2. Create a feature branch: `git checkout -b feature/MyFeature`  
3. Commit your changes: `git commit -m "Add MyFeature"`  
4. Push: `git push origin feature/MyFeature`  
5. Open a Pull Request

**Guidelines**
- Use modern C++ (C++20) when appropriate  
- Follow existing code style  
- Add useful logging and tests  
- Test with multiple mods  
- Document significant changes

---

## Author & Acknowledgments

**Author:** `5pedrowx1`

**Acknowledgments:**
- GTA V Modding Community  
- ScriptHookV by Alexander Blade — http://www.dev-c.com/gtav/scripthookv/  
- ScriptHookVDotNet — https://github.com/scripthookvdotnet/scripthookvdotnet

---

If this project helped you, please consider **starring** the repo on GitHub:  
`https://github.com/5pedrowx1/Loader-dinput8-Proxy`

---
