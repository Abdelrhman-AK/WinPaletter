# Changelog

---

### 1.0.6.9

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Beta`   |  27 Jan 2023 |

> **What's New?**

# New Features:
### 1. Windows Metrics & Fonts: 
- Now you can change Windows Metrics and fonts (e.g., Titlebar Height, Titlebar Font, Window Border Width, Icons Spacing & Fonts, .... etc.)
- There are some limitations when you apply (not all items) that require logoff and logon
- `Known Issue (You may not face it)`: In Windows 11 22H2, Titlebar Font may not be changed from Segoe UI even if the font is changed correctly in registry and even if you restart your Windows.

### 2. Languages: 
- Languages become stable (not experimental), and its mechanism has been changed, becoming dependent on JSON files not the old file (*.wplng)
- This makes it easier to modify by a text\code editor like VSCode or by WinPaletter Language Developing Tools (In Settings > Language)
- You can load language quickly (there is no delay) and without restarting WinPaletter (Except for returning to English)
- You can use WinPaletter Language Developing Tools to create, modify or update language json files (The Tools were separated before in WinPaletter Language Translator.exe, now you won’t use it you will use WinPaletter itself).
- So, it is necessary for language creators\contributors to use the latest version of WinPaletter and update their translations\languages (As it is not complete compared to earlier versions)
- Crashes that occurred during startup while loading language files are mostly fixed.
- `Known Issue`: People who are using old language files (*.wplng) and updated WinPaletter, an error will occur during loading the application, ignore this error, download newer language files (*.json), go to settings and load the new file and save.

### 3. Logs:
Extended Details will be shown during applying the theme (including time info and errors), you can change the preferences in Settings > Theme Logging.

### 4. Preview Switch
 In main page, you can navigate between all supported versions of Windows, making it easier to design one theme file for all supported Windows editions. 

### 5. Theme File Blocks:
Now the theme file that is saved by WinPaletter (.wpth) has 2 blocks for Windows 11/10 (before it was one block applied to both Windows 11/10), making it easier to design a theme presented in one file for both versions of Windows (To avoid conflict between colors applied between these Windows Versions) (Themes File of WinPaletter earlier than 1.0.6.9 can be loaded/saved too)

---

# Redesigns:
- Application’s UI improved a lot, especially the light mode becomes better, with the ability to change WinPaletter’s Colors and Backgrounds (Theming WinPaletter itself). Also, legacy dialogs like message boxes are modernized.
- Themes Previews in all forms are redesigned to match Windows Metrics and fonts (becoming more realistic compared to previous versions of WinPaletter)
- Drag and Drop preview are redesigned to have a bigger size
- Windows Terminal forms are redesigned to have tabs instead of the condensed ones.

---

# Multiple Improvements and Bugs Fixes:
1. Added the ability to uninstall from Control Panel
2. Loading Preferences of Windows Colors/Options from registry mechanism improved a lot, and WinPaletter will continue applying theme even if there is an error (exception error) happened (i.e., the error part will be skipped)
3. When you switch Dark\Light Mode in theme, there was a great time delay during theme applying. It is now fixed and not causing any delay in theme applying (Become quicker than before)
4. Cursors: Main Arrow is improved to match the exact outline of default Windows
5. Quick apply (found in Win32UI, Terminals, ...) mechanism changed to a better one (becoming quicker than before)
6. Confirmation Dialog that appear on closing WinPaletter will remember the preferences you choose, with the ability to control if this dialog appears or not (You can change the preferences also in Settings)
7. Newton.JSON component updated to 13.0.2 (Used in phrasing JSON files)
8. Right-Click Menu on color now will have more colors varieties & the extended menu will have Windows Terminals Colors (Previously they were not included)
9. Application Startup Speed Optimized
10. Windows 7: when choosing a color with basic mode applied, there was a crash. Now it is fixed. 
11. Windows 7: not switching basic/aero theme correctly. (Now you should logoff and logon to apply this effect correctly)
12. Improvements in Updates, especially the situation of Windows 7 that can’t connect to GitHub Repository (Fixed)
13. Fixed Windows Terminal (Stable\Preview) error after modification by WinPaletter
14. Mechanism of network detection changed, the old one was slower.

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.6.3`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.6.3...v1.0.6.9) |
| Previous Beta   | `1.0.5.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.5.9...v1.0.6.9) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.6.9) |

---

### 1.0.6.3

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 14 Nov 2022  |

> **What's New?**

- Fixing minor bugs in translation. `If you're using the Russian Language, please re-download it (The old translation file contained minor issues too)`

- Now Previewing Bug\Exception Error Details changed to a better one, there was a bug preventing the app from stating up and also not showing the details of error. Now it is mostly fixed and the details will appear. These issues were faced in [#55](https://github.com/Abdelrhman-AK/WinPaletter/issues/55) [#45](https://github.com/Abdelrhman-AK/WinPaletter/issues/45)

- Minor UI Design improvements, especially for the Light Mode.

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.6.2`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.6.2...v1.0.6.3) |
| Previous Beta   | `1.0.5.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.5.9...v1.0.6.3) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.6.3) |

---

### 1.0.6.2

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 13 Nov 2022  |

> **What's New?**

- Now Previewing Bug\Exception Error Details changed to a better one, there was a bug preventing the app from stating up and also not showing the details of error. Now it is mostly fixed and the details will appear. These issues were faced in [#55](https://github.com/Abdelrhman-AK/WinPaletter/issues/55) [#45](https://github.com/Abdelrhman-AK/WinPaletter/issues/45)
- Minor UI Design improvements, especially for the Light Mode.

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.6.1`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.6.1...v1.0.6.2) |
| Previous Beta   | `1.0.5.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.5.9...v1.0.6.2) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.6.2) |

---

### 1.0.6.1

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 20 Oct 2022  |

> **What's New?**

- Fixed bug: Applying accent on titlebar was not working on Windows 11 22H2 for some users
- Application Language: improved and fixed minor bugs
- Minor UI fixes and improvements

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.6.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.6.0...v1.0.6.1) |
| Previous Beta   | `1.0.5.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.5.9...v1.0.6.1) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.6.1) |

---

### 1.0.6.0

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 16 Oct 2022  |

> **What's New?**

1) **New Feature (Stable) (1): Customize Windows Consoles\Terminals:**
   
   - Including Command Prompt, PowerShell, Windows Terminal and External Terminals
   - You can modify Colors, schemes of foreground and background, fonts, cursor color and other tweaks.
   - Multiple bugs and issues fixed from the previous beta `1.0.5.9`; [@OrthodoxWindows](https://github.com/OrthodoxWindows) [#48](https://github.com/Abdelrhman-AK/WinPaletter/issues/48) [#47](https://github.com/Abdelrhman-AK/WinPaletter/issues/47)
   - It is better to have a look at documentation [WinPaletter/Terminal.md at master · Abdelrhman-AK/WinPaletter · GitHub](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Documentations/Terminal.md)

2) **New Feature (2): Windows 10 and 11 scheme re-constructed, with ability to apply accents on taskbar alone or taskbar, start menu and action center:**
   
   - Added ability to apply accents on taskbar alone or taskbar, start menu and action center: [@OrthodoxWindows](https://github.com/OrthodoxWindows) [#46](https://github.com/Abdelrhman-AK/WinPaletter/issues/46)
   - Windows 10\11 Colors ID\Descriptions changed to better ones instead of old\misleading ones
   - A new 9th color added, it was found in registry accent array, but without known usage
   - Reordering colors after changing toggles removed, the colors stay in the same order of accent array in registry. Each accent color has a number beside it, it is its order in registry's array
   - If you find a color "Undefined", that doesn't mean that it is not usable, you can use it but its effect on system is Unknown\Undefined

3) **New Feature (3): Right-Click on theme file now has "Edit" and "Apply"; [@OrthodoxWindows](https://github.com/OrthodoxWindows) 's suggestion [#32](https://github.com/Abdelrhman-AK/WinPaletter/issues/32)**

4) **Multiple improvements:**
   
   - Language Translation now depends on GitHub only `(Not Telegram; Deleted as it isn't necessary)`
   - You can resize the main form as colors descriptions may be partially invisible (Some languages)
   - Fixing bug in translation causing the application not opening completely
   - Application Language: improved and fixed minor bugs
   - Minor UI fixes and improvements
   - Minor Improvement in Application Start Speed (You may not notice it)

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.5.2`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.5.2...v1.0.6.0) |
| Previous Beta   | `1.0.5.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.5.9...v1.0.6.0) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.6.0) |

---

### 1.0.5.9

> **Info:**

| Branch | Release Date |
|:------:|:------------:|
| `Beta` | 07 Oct 2022  |

> **What's New?**

1) New Feature (Beta): Edit Terminals (Command Prompt, PowerShell & Windows Terminal):
   
   - You can modify background and foreground colors and popup colors, with fonts and other tweaks.
   - Please read its documentation before starting editing terminals: [WinPaletter/Terminal.md at master · Abdelrhman-AK/WinPaletter · GitHub](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Documentations/Terminal.md)

2) Minor improvements in UI and Application Loading Speed

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.5.2`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.5.2...v1.0.5.9) |
| Previous Beta   | `1.0.4.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.4.0...v1.0.5.9) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.5.9) |

---

### 1.0.5.2

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 13 Sep 2022  |

> **What's New?**

- Windows 10,11 LogonUI colors (PersonalColors_Background, PersonalColors_Accent and Background) are removed from the program, as when they are applied correctly to registry, several critical bugs occurs to Windows (These Colors are Specific for Windows 8.1)
- Fixing flickering while right clicking on colors in Windows 10 (Design Bug Fixed)
- Fixing non consistent size for items after reloading Windows Default Palette (Design Bug Fixed)
- Win32UI minor improvement: [@Anixx](https://github.com/Anixx) [#43](https://github.com/Abdelrhman-AK/WinPaletter/issues/43)

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.5.1`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.5.1...v1.0.5.2) |
| Previous Beta   | `1.0.4.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.4.0...v1.0.5.2) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.5.2) |

---

### 1.0.5.1

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 11 Sep 2022  |

> **What's New?**

1) Right Click Menu for Colors:
   
   - Right click on an item of a color palette, then this menu will appear.
   - It will help you copy, cut, paste, choose variants and restore default colors.
   - You can see all colors used in your palette in the sub-menu.

2) Nerd Color Info for Color Palette:
   
   - For those who love advanced information, now you can see the info of your selected colors.
   - Available Formats are HEX, RGB, HSL and Decimal
   - Know that if the color info starts with "D" and font is underlined, the color is the same as the default one.
   - You can edit its settings or disable it from Settings > Miscellaneous

3) Fixing Critical Bug in Color Palette loading for users used WinPaletter 1.0.5.0 for the first time (not for old users)
   
   - Reported by [@TF2-Gaming](https://github.com/TF2-Gaming) and suggestion to help by [@Yuriy-RU](https://github.com/Yuriy-RU) [#42](https://github.com/Abdelrhman-AK/WinPaletter/issues/42)

4) Win32UI:
   
   - New: You can save classic theme of Win32UI in *.theme format to used it in any OS (Starting by Windows 95), Open Win32UI Form for more info
   - Fixing bugs in the Windows Classic (Win32UI) previewer, thanks to [@Anixx](https://github.com/Anixx) [#37](https://github.com/Abdelrhman-AK/WinPaletter/issues/37) [#36](https://github.com/Abdelrhman-AK/WinPaletter/issues/36) [#35](https://github.com/Abdelrhman-AK/WinPaletter/issues/35)

5) Windows 8.1:
   
   - Aero Lite Theme in Windows 8.1 can't be remembered if applied or not, now Fixed](https://github.com/Abdelrhman-AK/WinPaletter/issues/43)

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.5.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.5.0...v1.0.5.1) |
| Previous Beta   | `1.0.4.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.4.0...v1.0.5.1) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.5.1) |

---

### 1.0.5.0

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 06 Sep 2022  |

> **What's New?**

1) New Huge Feature: Extended Support to Windows 8.1 and Windows 7
   
   - This requires at least .NET Framework 4.7, or better 4.8, then reboot your Windows (Necessary) [Download it from here, choose "Runtime"](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)
   - Now you can modify Windows 8.1 and Windows 7 colors in an advanced way.
   - Windows 7: You can change main color and color of blur (Aero) and their amount, and also intensity of both blur and aero glass reflection.
   - Windows 8.1: You can change background colors and accent colors in start menu and logonui backgrounds freely, and also start background scheme and Windows Desktop apps color (Titlebar\Taskbar)
   - You can change the theme into (Aero\Opaque\Basic) according to your OS
   - LogonUI: You can modify both Windows 7's and 8.1s LogonUI\Lockscreen by Custom Image or colors, and also you can apply custom effects like acrylic/blur and so on ...

2) New Feature Debut: Command Line Support, idea by [@OrthodoxWindows](https://github.com/OrthodoxWindows) [#32](https://github.com/Abdelrhman-AK/WinPaletter/issues/32)
   
   - Usage: `WinPaletter /apply:ThemeFile`
   - Example 1: WinPaletter /apply:MyTheme.wpth
   - Example 2: WinPaletter /apply:"D:\My Files\My Favourite Theme.wpth" `Notice the quotes here, use quotes when file path\name has spaces`

3) Fixing Bugs:
   
   - Win32UI: Multiple bugs fixes, thanks to [@OrthodoxWindows](https://github.com/OrthodoxWindows) and [@Anixx](https://github.com/Anixx) [#24](https://github.com/Abdelrhman-AK/WinPaletter/issues/24) [#25](https://github.com/Abdelrhman-AK/WinPaletter/issues/25) [#26](https://github.com/Abdelrhman-AK/WinPaletter/issues/26) [#27](https://github.com/Abdelrhman-AK/WinPaletter/issues/27) [#30](https://github.com/Abdelrhman-AK/WinPaletter/issues/30)
   - Fixing a bug in Color Picker: Screen Pixel Picker opens the wrong form after leaving the mouse
   - Fixing bugs in Cursor Colorizer (It was broken in 32-bit OS, now fixed)
   - Adjusted Settings Saving speed (It was slow)
   - Drag and drop now support showing Win32UI with Windows Modern Elements

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.4.1`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.4.1...v1.0.5.0) |
| Previous Beta   | `1.0.4.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.4.0...v1.0.5.0) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.5.0) |

---

### 1.0.4.1

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 13 Aug 2022  |

> **What's New?**

1) New Feature `Stable`: Colorize Cursors as you need, even the animated ones!
   
   - You can colorize cursor background, outline and the animated circles.
   - The colors can be solid, or gradient, or with noise texture effect.
   - **Note:** To apply cursors, you should enable the toggle in its form, and activate Automatic Apply Custom Cursors from Settings > Theme applying behavior

2) Win32UI: It has now "Quick Apply" to see instant effects, instead of loading to current palette and applying

3) Win32UI: Fixing bugs in the previewer, thanks to both [@Anixx](https://github.com/Anixx) and [@OrthodoxWindows](https://github.com/OrthodoxWindows) Contributions': [#20](https://github.com/Abdelrhman-AK/WinPaletter/issues/20) [#19](https://github.com/Abdelrhman-AK/WinPaletter/issues/19)

4) Win32UI: Now you can apply "Flat menus" and "Gradient Titlebar" without logging off and on, thanks to [@OrthodoxWindows](https://github.com/OrthodoxWindows) [#22](https://github.com/Abdelrhman-AK/WinPaletter/issues/22)

5) Retro Themes Presets, thanks to [@Anixx](https://github.com/Anixx) 's idea [#16](https://github.com/Abdelrhman-AK/WinPaletter/issues/16) [#20](https://github.com/Abdelrhman-AK/WinPaletter/issues/20)
   
   - Themes rights go to Microsoft, they are from Windows 3.1, Windows 98 and Windows Classic (2000/XP)
   - The presets are found in Windows Classic (Win32UI) Editor and Color Picker

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.3.1`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.3.1...v1.0.4.1) |
| Previous Beta   | `1.0.4.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.4.0...v1.0.4.1) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.4.1) |

---

### 1.0.4.0

> **Info:**

| Branch | Release Date |
|:------:|:------------:|
| `Beta` | 10 Aug 2022  |

> **What's New?**

1) New Feature `beta`: Colorize Cursors as you need, even the animated ones!
   
   - You can colorize cursor background, outline and the animated circles.
   - The colors can be solid, or gradient, or with noise texture effect.
   - **Note:** To apply cursors, you should enable the toggle in its form, and activate Automatic Apply Custom Cursors from Settings > Theme applying behavior

2) Fixing bugs in the Windows Classic (Win32UI) previewer, thanks to both [@OrthodoxWindows](https://github.com/OrthodoxWindows) and [@Anixx](https://github.com/Anixx) Contributions' : [#15](https://github.com/Abdelrhman-AK/WinPaletter/issues/15) [#18](https://github.com/Abdelrhman-AK/WinPaletter/issues/18)

3) Retro Themes Presets, thanks to [@Anixx](https://github.com/Anixx) 's idea [#16](https://github.com/Abdelrhman-AK/WinPaletter/issues/16)
   
   - Themes rights go to Microsoft, they are from Windows 3.1, Windows 98 and Windows Classic (2000/XP)
   - The presets are found in Windows Classic (Win32UI) Editor and Color Picker

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.3.1`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.3.1...v1.0.4.0) |
| Previous Beta   | `1.0.2.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.2.0...v1.0.4.0) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.4.0) |

---

### 1.0.3.1

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 25 Jul 2022  |

> **What's New?**

- Rescue Box Removed (due to unhandled exceptions errors, not useful anymore after fixing explorer restart mechanism)
- Now you can apply Win32UI colors immediately without restarting explorer, without logging off or without restarting windows - Thanks to [@OrthodoxWindows](https://github.com/OrthodoxWindows) 's contribution [#13](https://github.com/Abdelrhman-AK/WinPaletter/issues/13)
- Fixing Color Picker not showing chosen color
- UI Improvements

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.3.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.3.0...v1.0.3.1) |
| Previous Beta   | `1.0.2.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.2.0...v1.0.3.1) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.3.1) |

---

### 1.0.3.0

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 22 Jul 2022  |

> **What's New?**

1) Merge Nostalgia (Windows 9x) with UWP/Win3UI! By using new features of Color Picker and Win32UI:
   
   - `NEW` Color Picker can now extract colors of Windows Classic themes (*.theme), and so you can use it in your modern Windows as you want.
   - `NEW` Win32UI has a live preview, so you can see every change you make in Windows Classic elements, and you can apply an old theme (*.theme) to Windows 11/10 `Only some elements like window background and classic apps run by OTVDM are customizable`.

2) `Stable` Rescue Box; to fix errors of Windows crashes after Explorer Restart, especially for those who are on Windows 11 22621.

3) Fixes and tweaks: Explorer restart mechanism changed to a better one, UI improvements and startup slight increase in speed.

4) `EXPERIMENTAL` Languages: You can translate now the app, but it still needs other contributions:  [TranslationContribution.md](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/TranslationContribution.md)

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.1.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.1.0...v1.0.3.0) |
| Previous Beta   | `1.0.2.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.2.0...v1.0.3.0) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.3.0) |

---

### 1.0.2.0

> **Info:**

| Branch | Release Date |
|:------:|:------------:|
| `Beta` | 17 Jun 2022  |

> **What's New?**

1) Fixing Restarting Explorer issues by:
   
   - New Explorer Restarting Mechanism **(Code Changed)**
   - `Beta` Introducing Rescue Box (Appears when you apply a theme, **helps** you with the problem of **not launching** explorer after restarting or its **blinking** and repairs **crashed Windows Apps** **(by SFC, DISM)**) `You can turn it off from settings`

2) Tweaks:
   
   - Tweaks for Theme File and Exported Settings File Extensions registration
   - `Beta` New Settings Layout instead of the old condensed one
   - Changelog Form Tweaks

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.1.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.1.0...v1.0.2.0) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.3.0) |

---

### 1.0.1.0

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 10 Jun 2022  |

> **What's New?**

- Now you can use WinPaletter on Windows 11 22621, but with precautions:
  1. Disable Automatic Restarting Explorer From WinPaletter Settings
  2. If you want to see instant effects of applied palette, restart explorer yourself
  3. If you faced crashing: Open Command Prompt as Administrator and type:  
     `sfc /scannow`  
     `dism /Online /Cleanup-Image /RestoreHealth`  
     Then close Command Prompt and restart you Windows
  - You might face this in Windows 10, if so, run app as Administrator only or do the same previous steps.
  - The problem was that killing explorer of Win11-22621 automatically crashes it and crashes also Windows Apps (WinUI3\UWP)
  - I'll rewrite a better code for restarting explorer

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.1.0) |

---

### 1.0.0.0

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 21 May 2022  |

Initial Release
