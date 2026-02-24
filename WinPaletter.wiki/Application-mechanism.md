## What WinPaletter Does

Windows lets you customize many elements of your interface—like system colors, wallpapers, sounds, and cursors. WinPaletter takes that further by giving you full control over these elements, with a clean UI and extra tools.

### Key Features
- Edit system colors (including hidden or classic ones).
- Customize UI metrics and fonts.
- Manage wallpapers, system sounds, and cursors.
- Save and load themes as files.
- Browse and apply themes online from the built-in **WinPaletter Store**.

---

## How WinPaletter Works (Behind the Scenes)

WinPaletter creates **in-memory structures (variables)** that represent your Windows customization settings. Each structure is responsible for a specific part of the Windows appearance.

For example:

| Structure Name             | Purpose                                |
|---------------------------|----------------------------------------|
| `ColorStructure`          | Holds main system colors               |
| `ClassicColorsStructure`  | Stores legacy/classic Windows colors   |
| `MetricsFontsStructure`   | Controls window sizes, spacing, fonts  |
| `SoundsStructure`         | Custom sound scheme                    |
| `CursorsStructure`        | Custom mouse pointers                  |
| `WallpaperStructure`      | Wallpaper path and mode                |

### Diagram: WinPaletter Customization Flow

```plaintext
+------------------------+
|   User interacts with  |
|     WinPaletter UI     |
+----------+-------------+
           |
           v
+-----------------------+
| Structures are read from the Windows Registry and system settings, and stored in the app’s memory |
     |-- [ ColorsStructure ]
     |      |-- AccentColor
     |      |-- BackgroundColor
     |      |-- etc...
     |
     |-- [ FontsStructure ]
     |      |-- CaptionFont
     |      |-- MessageFont
     |      |-- etc...
     |
     |-- [ WallpaperStructure ]
     |      |-- Path
     |      |-- Style
     |      |-- etc...
+----------+------------+
           |
           v
+------------------------+
|    Windows Registry    |
| (Settings are applied) |
+------------------------+
```

This diagram shows the process:
- You change something in the app.
- WinPaletter updates its internal memory structures.
- It writes the changes directly to the Windows Registry.

Why This Approach?
- 💾 Safe: All changes go through structured logic instead of directly editing the registry.
- ♻️ Reversible: You can export and re-import your settings.
- 🌐 Sharable: You can download themes made by others or upload your own to the Store.
- 💡 Advanced Control: Access settings not easily reachable in Windows Settings or Control Panel.