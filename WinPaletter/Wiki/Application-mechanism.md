# WinPaletter: Take Full Control of Windows Customization

WinPaletter is a powerful tool that lets you customize Windows beyond the standard Settings app. Whether you want to tweak colors, fonts, sounds, or cursors, WinPaletter gives you complete control in a clean, user-friendly interface.

---

## What WinPaletter Does

Windows already allows you to personalize certain elements—like accent colors, desktop backgrounds, and sound schemes. WinPaletter expands on this by unlocking hidden settings and providing extra features to make your PC truly yours.

### Key Features
- **Full Color Control**: Modify system colors, including hidden or classic Windows colors not available in standard settings.
- **UI Customization**: Adjust window metrics, spacing, and fonts to your liking.
- **Complete Personalization**: Manage wallpapers, system sounds, and mouse cursors all in one place.
- **Theme Management**: Save your customizations as theme files and reload them anytime.
- **WinPaletter Store**: Browse, upload, and apply themes created by others directly from the app.

---

## How WinPaletter Works (For Developers and Curious Users)

Under the hood, WinPaletter uses organized data classes to manage your Windows customization settings. Each class represents a specific part of the Windows appearance, making it easy to read, modify, and apply changes safely.

### Key Data Structures

| Class Name                | Purpose                                |
|---------------------------|----------------------------------------|
| `Color`                   | Stores main system colors (like accent and background colors) |
| `ClassicColors`           | Manages legacy Windows colors from older versions |
| `MetricsFonts`            | Controls window sizes, spacing, and fonts |
| `Sounds`                  | Handles custom sound schemes for system events |
| `Cursors`                 | Stores mouse pointer customizations |
| `Wallpaper`               | Manages wallpaper path and display settings |

### How It All Fits Together

Here’s a simplified view of how WinPaletter applies your customizations:

```plaintext
+------------------------+
|   You make changes in  |
|     WinPaletter UI     |
+----------+-------------+
           |
           v
+------------------------+
|   WinPaletter stores   |
|   your settings in     |
|   memory               |
|  (Colors, Fonts, etc.) |
+----------+-------------+
           |
           v
+------------------------+
|   Settings are written |
|   to the Windows       |
|   Registry and applied |
+------------------------+
```

## Why This Approach?
- 💾 Safe: All changes go through structured logic instead of directly editing the registry.
- ♻️ Reversible: You can export and re-import your settings.
- 🌐 Sharable: You can download themes made by others or upload your own to the Store.
- 💡 Advanced Control: Access settings not easily reachable in Windows Settings or Control Panel.

---

## Get Started
Whether you're a casual user looking to refresh your desktop or a developer curious about Windows customization, WinPaletter makes it easy and fun to personalize your PC.