# Changelog

---

### 1.0.8.4

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 08 Dec 2023  |

# The same as 1.0.8.3, except for some bugs fixes:

### New bugs fixes:
1- Fix issue of opening WinPaletter main form after opening a wpth file with settings of not opening application is checked #202 
2- Fix bug in translator GUI text search #203 
3- Fix increased memory usage on rendering cursors
4- Fix exception errors happening on applying cursors
5- Fix error on applying Windows 8.1 lock screen background and Windows 7 LogonUI background

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.8.3`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.3...v1.0.8.4) |
| Previous Beta   | `1.0.8.2`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.2...v1.0.8.4) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.8.4) |

---

### 1.0.8.3

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 01 Dec 2023  |


> **Warning**
> If you are using a language file, please re-download it from [here](https://github.com/Abdelrhman-AK/WinPaletter/tree/master/Languages)

> **Warning**
> Please run `SFC /scanfile=%windir%\System32\imageres.dll` in Command Prompt as administrator and restart your Windows if you used a previous beta version to fix imageres.dll corruption made by modifying Windows startup sound. Don't use previous beta versions `1.0.7.x`.

# These are all new features since last stable release `1.0.8.0`:
# If you have read this before, read feature NO. 3 (Cursors new features), improvements and bugs fixes (to save your time)

### 1. WinPaletter is written now in C# instead of Visual Basic .NET
- In 1.0.8.3, there is extensive optimization in code after coding language shift and bugs fixes.
- It is expected to face bugs due to programming language shift. Please report problems you may face in Issues.

### 2. Multiple users
- It is possible now to control multiple users in WinPaletter.
- This feature helped WinPaletter to reduce UAC dialogs appearance and fixed issue of not applying and getting theme data from a different account #185
- Due to unknown reason, when you apply certain theme items to another user, you may notice that current user profile that opened WinPaletter is affected by the theme until logoff and log back on. After doing so, you will find that the selected user's profile is correctly themed, while the user who opened WinPaletter retains their previous state and is unaffected by the theme.
- So, the best practice method is to open target user then open WinPaletter inside this profile, not from a different one.

### 3. Cursors new features
- Cursors studio layout has been changed to tabs, making it easier to understand it instead of the old condensed layout
- Busy and app waiting cursors can be rendered in high size when high DPI is set
- Now you can select external cursors files instead of rendering them
- New style for busy and app waiting cursors; fluid. And dot scheme is changed into modern
- Fix scaling preview bug, and also in WinPaletter Store.

### 4. Sounds service #184
- Task Scheduler method is removed, being replaced by a service made by WinPaletter.
- This service listens to system events and play sounds according to the received event, and this method is relatively better than Task Scheduler method.
- This service can handle Windows shutdown, logoff, logon, account lock and unlock, and charger connection and disconnection sounds.

### 5. Drag and drop colors items - read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Color-picker-control#3-drag-and-drop) to learn more.

You can drag a color into another color to make it easier and quicker to change colors. You can also swap between colors, you can learn it from wiki.

https://github.com/Abdelrhman-AK/WinPaletter/assets/59510211/9d082727-720b-473c-8827-80c6f2f8c8d9

### 6. Palette extraction and distribution

- If you are confused with colors for your Windows colors, WinPaletter now can generate a palette from one color or from an image.

- Read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Palette-generator) to learn more about this feature

### 7. Applying theme thread
- Applying WinPaletter theme is now in a separate thread, to fix issue of Windows and WinPaletter freezing #180 #155 #102
- New Explorer restart method (all to avoid crashing all open applications): When you apply a theme, firstly it will kill Explorer, waits for theme applying finish and finally reopen Explorer. Formerly, the method was both killing and opening after finishing theme apply.

### 8. Colors history

Click on a color item and navigate to history tab, you will see all color used for current open item.

- Read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Color-picker-control#4-previous-colors-like-undo-or-colors-history) to learn more about this feature

### 9. GUI language editor

- It will help you create, modify and update languages JSON files by showing mini-forms that you can edit so that you can see all text items in real time

- Read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Language-creation) to learn more about this feature

### 10. New download sources for WinPaletter

- Including WinGet and Chocolatey #168

- Read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Get-WinPaletter) to know how to download from these sources

### 11. Theme log levels

- There is a new level called "Advanced details" that shows you all registry modifications and actions done to your system during theme applying

- Read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Theme-log-verbose-level) to learn more about this feature

### 12. Windows Effects new feature

- Animate controls and elements inside window

***

# Improvements:

- Minor updates in WinPaletter UI, as a preparation for a new major WinPaletter UI update (new look)

- In classic colors: Plus! 98 CD themes are included as schemes

- Rescue tools are returned, to help you fix your Windows and manage current Windows session if you are stuck in an error

- Classic colors preview speed improvements

- UI improvements #186 and others.

- This is in code, but nothing will be visible to you: make WinPaletter can handle Windows 12. WinPaletter will manage it as if it is Windows 11 until Windows 12 stable is released and WinPaletter is optimized and tested in it.

- Cyotek color picker component is updated into 2.0.0-beta.7

- Sounds
  - Added Unlock and Charger connected sounds #170
  - Fix empty icons after patching imageres.dll
  - Fix "Sound API only supports playing PCM wave files" on opening a WAV file
  - Added [alert dialog](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Advanced-options-to-patch-PE-files) on changing PE files resources, for example imageres.dll (for startup sound)
 
***

# Bugs fixes:
- Fix user exception error that prevents users from opening WinPaletter #198 
- Fix issues in Application themer
- Fix bug of applying application theme on closing WinPaletter Store with a theme open
- Fix error of WinEffects: GETCLIENTANIMATION get and set in Windows XP
- Fix preview not synced in classic colors after selecting a color
- Bug report fix: Windows 10 OS info if Windows 11 is running
- Alt+Tab opacity preview fix for Windows 10
- Sounds: Fix bug of repeated pathing imageres.dll alert with sounds are the same
- ExplorerPatcher error fix #181
- GUI translator bug fix: Form exception error #183
- Fix color picker not working in Windows XP
- Fix Preview of Windows 11/10 not syncing color with color picker
- Fix metrics preview of Windows XP to 8.1 (inner padding)
- Bug report fix: Windows 10 OS info if Windows 11 is running
- Fix issue of disappearing taskbar and start in preview
- Fix issue of conflict between custom DPI and metrics and fonts #179 #178
- Fix small icons fonts and other fonts on applying metrics and fonts with DPI higher than 125 and with delaying metrics and fonts effects option in settings is enabled 
- Fix bug of opening an old theme file causes null or non-found values for the newly loaded theme
- Fix WinPaletter theme load issue #173

***

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.8.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.0...v1.0.8.3) |
| Previous Beta   | `1.0.8.2`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.2...v1.0.8.3) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.8.3) |

---

### 1.0.8.2

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Beta`   | 13 Nov 2023  |

> **Warning**
> If you are using a language file, please re-download it from [here](https://github.com/Abdelrhman-AK/WinPaletter/tree/master/Languages)

> **Warning**
> Please run `SFC /scanfile=%windir%\System32\imageres.dll` in Command Prompt as administrator and restart your Windows if you used a previous beta version to fix imageres.dll corruption made by modifying Windows startup sound. Don't use previous beta versions `1.0.7.x`.

# New Features:

### 1. WinPaletter is written now in C# instead of Visual Basic .NET
- In 1.0.8.2, there is extensive optimization in code after coding language shift and bugs fixes.
- It is expected to face bugs due to programming language shift. Please report problems you may face in Issues.

### 2. Multiple users
- It is possible now to control multiple users in WinPaletter.
- This feature helped WinPaletter to reduce UAC dialogs appearance and fixed issue of not applying and getting theme data from a different account #185
- Due to unknown reason, when you apply certain theme items to another user, you may notice that current user profile that opened WinPaletter is affected by the theme until logoff and log back on. After doing so, you will find that the selected user's profile is correctly themed, while the user who opened WinPaletter retains their previous state and is unaffected by the theme.
- So, the best practice method is to open target user then open WinPaletter inside this profile, not from a different one.

### 3. Sounds service #184
- Task Scheduler method is removed, being replaced by a service made by WinPaletter.
- This service listens to system events and play sounds according to the received event, and this method is relatively better than Task Scheduler method.
- This service can handle Windows shutdown, logoff, logon, account lock and unlock, and charger connection and disconnection sounds.

### 4. Drag and drop colors items - read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Color-picker-control#3-drag-and-drop) to learn more.

You can drag a color into another color to make it easier and quicker to change colors. You can also swap between colors, you can learn it from wiki.

https://github.com/Abdelrhman-AK/WinPaletter/assets/59510211/9d082727-720b-473c-8827-80c6f2f8c8d9

### 5. Palette extraction and distribution

- If you are confused with colors for your Windows colors, WinPaletter now can generate a palette from one color or from an image.

- Read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Palette-generator) to learn more about this feature

### 6. Applying theme thread
- Applying WinPaletter theme is now in a separate thread, to fix issue of Windows and WinPaletter freezing #180 #155 #102
- New Explorer restart method (all to avoid crashing all open applications): When you apply a theme, firstly it will kill Explorer, waits for theme applying finish and finally reopen Explorer. Formerly, the method was both killing and opening after finishing theme apply.

### 7. Colors history

Click on a color item and navigate to history tab, you will see all color used for current open item.

- Read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Color-picker-control#4-previous-colors-like-undo-or-colors-history) to learn more about this feature

### 8. GUI language editor

- It will help you create, modify and update languages JSON files by showing mini-forms that you can edit so that you can see all text items in real time

- Read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Language-creation) to learn more about this feature

### 9. New download sources for WinPaletter

- Including WinGet and Chocolatey #168

- Read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Get-WinPaletter) to know how to download from these sources

### 10. Theme log levels

- There is a new level called "Advanced details" that shows you all registry modifications and actions done to your system during theme applying

- Read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Theme-log-verbose-level) to learn more about this feature

### 11. Windows Effects new feature

- Animate controls and elements inside window

***

# Improvements:

- Rescue tools are returned, to help you fix your Windows and manage current Windows session if you are stuck in an error

- Classic colors preview speed improvements

- UI improvements #186 and others.

- This is in code, but nothing will be visible to you: make WinPaletter can handle Windows 12. WinPaletter will manage it as if it is Windows 11 until Windows 12 stable is released and WinPaletter is optimized and tested in it.

- Cyotek color picker component is updated into 2.0.0-beta.7

- Sounds
  - Added Unlock and Charger connected sounds #170
  - Fix empty icons after patching imageres.dll
  - Fix "Sound API only supports playing PCM wave files" on opening a WAV file
  - Added [alert dialog](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Advanced-options-to-patch-PE-files) on changing PE files resources, for example imageres.dll (for startup sound)
 
***

# Bugs fixes:

- Sounds: Fix bug of repeated pathing imageres.dll alert with sounds are the same
- ExplorerPatcher error fix #181
- GUI translator bug fix: Form exception error #183
- Fix color picker not working in Windows XP
- Fix Preview of Windows 11/10 not syncing color with color picker
- Fix metrics preview of Windows XP to 8.1 (inner padding)
- Bug report fix: Windows 10 OS info if Windows 11 is running
- Fix issue of disappearing taskbar and start in preview
- Fix issue of conflict between custom DPI and metrics and fonts #179 #178
- Fix small icons fonts and other fonts on applying metrics and fonts with DPI higher than 125 and with delaying metrics and fonts effects option in settings is enabled 
- Fix bug of opening an old theme file causes null or non-found values for the newly loaded theme
- Fix WinPaletter theme load issue #173

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.8.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.0...v1.0.8.2) |
| Previous Beta   | `1.0.8.1`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.1...v1.0.8.2) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.8.2) |

---

### 1.0.8.1

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Beta`   | 05 Oct 2023  |

> **Warning**
> If you are using a language file, please re-download it from [here](https://github.com/Abdelrhman-AK/WinPaletter/tree/master/Languages)

> **Warning**
> Please run `SFC /scanfile=%windir%\System32\imageres.dll` in Command Prompt as administrator and restart your Windows if you used a previous beta version to fix imageres.dll corruption made by modifying Windows startup sound. Don't use previous beta versions `1.0.7.x`.

# New Features:

### 1. WinPaletter is written now in C# instead of Visual Basic .NET
It is expected to face bugs due to programming language shift. Please report problems you may face in Issues.

### 2. Drag and drop colors items - read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Color-picker-control#3-drag-and-drop) to learn more.

You can drag a color into another color to make it easier and quicker to change colors. You can also swap between colors, you can learn it from wiki.

### 3. Palette extraction and distribution

- If you are confused with colors for your Windows colors, WinPaletter now can generate a palette from one color or from an image.

- Read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Palette-generator) to learn more about this feature

### 4. Colors history

Click on a color item and navigate to history tab, you will see all color used for current open item.

- Read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Color-picker-control#4-previous-colors-like-undo-or-colors-history) to learn more about this feature

### 5. GUI language editor

- It will help you create, modify and update languages JSON files by showing mini-forms that you can edit so that you can see all text items in real time

- Read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Language-creation) to learn more about this feature

### 6. New download sources for WinPaletter

- Including WinGet and Chocolatey #168

### 7. Theme log levels

- There is a new level called "Advanced details" that shows you all registry modifications and actions done to your system during theme applying

- Read [this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Theme-log-verbose-level) to learn more about this feature

### 8. Windows Effects new feature

- Animate controls and elements inside window

***

# Improvements:

- Sounds
  - Added Unlock and Charger connected sounds #170
  - Improvement in Task Scheduler deflection method #170
  - Fix empty icons after patching imageres.dll
  - Fix "Sound API only supports playing PCM wave files" on opening a WAV file
  - Added [alert dialog](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Advanced-options-to-patch-PE-files) on changing PE files resources, for example imageres.dll (for startup sound)
  
- Classic colors speed improvements

***

# Bugs fixes:
- Fix issue of disappearing taskbar and start in preview
- Fix issue of conflict between custom DPI and metrics and fonts #179 #178
- Fix small icons fonts and other fonts on applying metrics and fonts with DPI higher than 125 and with delaying metrics and fonts effects option in settings is enabled 
- Fix bug of opening an old theme file causes null or non-found values for the newly loaded theme
- Fix WinPaletter theme load issue #173

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.8.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.0...v1.0.8.1) |
| Previous Beta   | `1.0.7.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.7.9...v1.0.8.1) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.8.1) |

---

### 1.0.8.0

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 02 Sep 2023  |

> **Warning**
> Please run `SFC /scannow` in Command Prompt as administrator if you used a previous beta version to fix imageres.dll corruption made by modifying Windows startup sound and then restart Windows. Don't use previous beta versions.

> **Warning**
> Opening this new release after using old stable releases will reset your WinPaletter settings as its mechanism has been changed to a new better one. Know what you have changed in WinPaletter settings before using this release.

# These are all new features, changes and improvements made since latest stable version: 1.0.7.6

> **What's New?**

# New Features:

### 1. WinPaletter Store:

- You can now apply themes from the Store either online or offline from local folder
- You can also upload your themes
- [Read this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/WinPaletter-Store-basics) to know more about WinPaletter Store
- WinPaletter Store is redesigned from latest beta versions

### 2. Help (Wiki)

- This will help you use WinPaletter, making it simpler than before
- [Visit it](https://github.com/Abdelrhman-AK/WinPaletter/wiki)
- Help buttons are added to most forms to redirect you to related wiki page

### 3. Sounds #138:

- Now you can modify most Windows sounds events entries, including Windows Vista to 11 startup

### 4. Screen Saver #138:

- You might not know it, it was an animated full-screen window to save\preserve CRT monitors in the past
- You can select a file that you can get online or from an old Windows, its extension is `.scr`
- You can find some screen savers in %windir%\System32

### 5. Wallpaper #137:

- Now you can select an image or a color to be set as your wallpaper, and you can also change its style
- Wallpaper tone feature has been moved here to Wallpaper

### 6. WinPaletter application theme:

- Now you can change the colors/style of WinPaletter itself, to make it consistent with a theme you design
- You can prevent or block its actions by going to Settings `>` Appearance and uncheck (Make WinPaletter appearance is managed by the loaded theme ......)

### 7. Theme file (*.wpth) reading/writing mechanism is rewritten:

- Now, theme file contents will be written as JSON that is better in coding, this new theme file format is valid for WinPaletter 1.0.7.7 and higher not lower
- You can convert themes made by old versions of WinPaletter to make them compatible with new WinPaletter versions
- New format can be compressed to save space especially for WinPaletter Store, to make loading themes from servers quick
- New format can have an external theme resources pack file that contains images. sounds, screen savers,... that is loaded each time you load its theme. And this is useful for sharing themes especially through WinPaletter Store
- External theme resources pack can be made by pressing edit button in main window (Pencil icon) and go to 'Theme resources pack' and check its choice

### 8. Theme file info:

- New options are added in WinPaletter theme info, including info for Store that is useful for uploading themes, options related to external themes resources pack and theme credits\licenses
- Edit these info by pressing edit button in main window (Pencil icon), open all tabs and then edit the info

### 9. Windows Effects new features:

- Colors Filter (Accessibility feature) #136
- Hide scrollbar in modern Windows 10/11 apps #135
- Full Screen start menu for Windows 10
- Enable Windows 7 taskbar volume mixer for Windows 10 only

### 10. Metrics and Fonts new features:

- You can change Shell Icon and Shell Small Icon for Windows XP #133
- In Miscellaneous, you can make the fonts "Single bit per pixel" to give your fonts the look of old versions of Windows

### 11. WinPaletter settings read/write mechanism is re-written

And this is to make it more organized (each section is in a separate key (folder)) and export/import mechanisms changed to be with JSON format. Open `HKEY_CURRENT_USER\Software\WinPaletter\Settings` after opening WinPaletter to know the changed mechanism if you are curious

# Improvements:

1. Color item info:
   
   - It was formely named nerd color info
   - There will be a dot inside color info rectangle to indicate that the choosen color is not as the default color
   - Color picker control is now condensed, the previous one was extremely big compared to its contents
   - New settings section:
     - Make color label more transparent
     - Use default Windows monospaced font instead of JetBrains Mono
     - Use classic color picker instead of WinPaletter's default one on pressing on a color palette item

2. Extensive reduction in memory (RAM) ussage:
   
   1. Removed fonts lists in consoles\terminals, being replaced by native Windows Fonts picker (to reduce large memory allocation while loading list of fonts)
   2. Improvements in loading language file (less memory usage and quick application loading time)
   3. Fix memory leak on palette extraction from image
   4. Improvements in loading wallpaper thumbnail, with fixing its bug #166

3. Updated JetbrainMono font to v2.304

4. Patching imageres.dll is blocked by defaults in settings, [read this for more info](https://github.com/Abdelrhman-AK/WinPaletter/wiki/unlock-patching-imageres.dll-to-change-Windows-startup-sound)

5. Save theme confirmation dialog redesigned, with new applying options (first theme and default Windows)

6. Bug reporting (Exceptions errors handling) improvements

7. Now WinPaletter will always start elevated (start as administrator) to make applying themes quicker as much as possible, and to avoid patching `imageres.dll` error (on changing Windows startup sound)

8. Drag and drop preview is removed (as this feature is not supported in any Windows application started as administrator)

9. Language items/texts of What's new labels will be neither loaded nor exported in/from it to avoid confusion between newly added labels' texts and old language translation

10. Embedded assemblies (references) are now compressed (to try to make WinPaletter with a small file size as much as possible)

# Bugs fixes:

1. OS detection method changed to avoid bug #164
2. Application startup crash #153
3. Fix no wallpaper preview with wallpaper sideshow or windows spotlight is enabled
4. WinPaletter update error #154
5. Fix wrong preview of color on Windows 11/10 taskbar
6. Fix wallpaper preview is not synchronized in Windows XP
7. Fix application crash on starting WinPaletter with classic theme enabled
8. Windows Terminal: 
   - Fixed edit button sets wrong text on pressing cancel
   - Fix error occuring during loading Windows Terminal preferences that inhibits the application from opening #147
9. Fix bug of Windows 8.1 Start screen black rectangles if designing theme from another version of Windows (not 8.1)
10. Fix Windows 7 and 8.1 logonUI/lock screen applying error
11. Fix applying fonts size trouble with high DPI #157 #160
12. Metrics and fonts form error #155
13. Fix error on pressing 'Logoff' button on main form #141 #167
14. Fix theme apply error on applying Windows Effects #163
15. Windows switcher crash #149
16. Fix bug of dark/light mode loading in 'Language add snippet' form
17. UI #134
18. Spelling correction #140

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.7.6`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.7.6...v1.0.8.0) |
| Previous Beta   | `1.0.7.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.7.9...v1.0.8.0) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.8.0) |

---

### 1.0.7.9

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Beta`   | 10 Jul 2023  |

> **What's New?**

Note: Opening this new release after using old releases will reset your WinPaletter settings as its mechanism has been changed to a new better one. Know what you have changed in WinPaletter settings before using this release.

The same as [1.0.7.7](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/CHANGELOG.md#1077) and [1.0.7.8](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/CHANGELOG.md#1078) with a new feature, improvements and some bugs fixes:
- **New feature:** WinPaletter settings read/write mechanism is re-written to be more organized (each section is in a separate key (folder)) and export/import mechanisms changed to be with JSON format. Open `HKEY_CURRENT_USER\Software\WinPaletter\Settings` after opening WinPaletter to know the changed mechanism if you are curious.

- Now WinPaletter will always start elevated (start as administrator) to make applying themes quicker as much as possible, and to avoid patching `imageres.dll` error (on changing Windows startup sound)
- Improvements in loading language file (less memory usage and quick application loading time)
- Language items/texts of What's new labels will be neither loaded nor exported in/from it to avoid confusion between newly added labels' texts and old language translation
- Drag and drop preview is removed (as this feature is not supported in any Windows application started as administrator)
- Embedded assemblies (references) are now compressed (to try to make WinPaletter with a small file size as much as possible)
- Fix bug of dark/light mode loading in Language add snippet form
- Fix bug of Windows 8.1 Start screen black rectangles if designing theme from another version of Windows (not 8.1)
- Fix fonts size trouble with high DPI #157 #160
- Fix theme apply error on applying Windows Effects #163
> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.7.6`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.7.6...v1.0.7.9) |
| Previous Beta   | `1.0.7.8`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.7.8...v1.0.7.9) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.7.9) |

---

### 1.0.7.8

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Beta`   | 18 Jun 2023  |

> **What's New?**
The same as [1.0.7.7](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/CHANGELOG.md#1077) with some bugs fixes:
- Fonts size trouble with high DPI #157
- Metrics and fonts form error #155
- WinPaletter update error #154
- Application startup crash #153
- Windows switcher crash #149

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.7.6`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.7.6...v1.0.7.8) |
| Previous Beta   | `1.0.7.7`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.7.7...v1.0.7.8) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.7.8) |

---

### 1.0.7.7

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Beta`   | 09 Jun 2023  |

> **What's New?**

# New Features:
### 1. WinPaletter Store (Beta):
- You can now apply themes from the Store either online or offline from local folder.
- You can also upload your themes.
- WinPaletter Store will have initially small amount of themes and it will grow more and more with progression of time.
- [Read this](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Documentations/Store/GettingStarted.md) to understand more about WinPaletter Store.

### 2. Sounds #138: 
- Now you can modify most Windows sounds events entries, including Windows Vista to 11 startup.

### 3. Screen Saver #138:
- You might not know it, it was an animated full-screen window to save\preserve CRT monitors in the past.
- You can select a file that you can get online or from an old Windows, its extension is (.scr).
- You can find some screen savers in %windir%\System32

### 4. Wallpaper #137:
- Now you can select an image or a color to be set as your wallpaper, and you can also change its style.
- Wallpaper tone feature has been moved here to Wallpaper.

### 5. WinPaletter application theme:
- Now you can change the colors/style of WinPaletter itself, to make it consistent with a theme you design
- You can prevent or block its actions by going to Settings > Appearance and uncheck (Make WinPaletter appearance is managed by the loaded theme ................)

### 6. Theme file (*.wpth) reading/writing mechanism is rewritten:
- Now, theme file contents will be written as JSON that is better in coding, this new theme file format is valid for WinPaletter 1.0.7.7 and higher not lower.
- You can convert themes made by old versions of WinPaletter to make them compatible with new WinPaletter versions.
- New format can be compressed to save space especially for WinPaletter Store, to make loading themes from servers quick.
- New format can have an external theme resources pack file that contains images. sounds, screen savers,... that is loaded each time you load its theme. And this is useful for sharing themes especially through WinPaletter Store.
- External theme resources pack can be made by pressing edit button in main window (Pencil icon) and go to "Theme resources pack" and check its choice.

### 7. Theme file info:
- New options are added in WinPaletter theme info, including info for Store that is useful for uploading themes, options related to external themes resources pack and theme credits\licenses.
- Edit these info by pressing edit button in main window (Pencil icon),open all tabs and then edit the info.

### 8. Windows Effects new features:
- Colors Filter (Accessibility feature) #136
- Hide scrollbar in modern Windows 10/11 apps #135
- Full Screen start menu for Windows 10

### 9. Metrics and Fonts new features:
- You can change Shell Icon and Shell Small Icon for Windows XP #133
- In Miscellaneous, you can make the fonts "Single bit per pixel" to give your fonts the look of old versions of Windows

---

# Improvements:
- Bug reporting (Exceptions errors handling) improvements
- Settings load\save improvements
- Save theme confirmation dialog redesigned, with new applying options (first theme and default Windows)
- Added an option to use the native classic Windows color picker instead of WinPaletter's one (Open Settings > Miscellaneous)
- Improvements for previewer in both main form and drag-drop
- Removed fonts lists in consoles\terminals, being replaced by native Windows Fonts picker (to reduce large memory allocation while loading list of fonts)

---

# Bugs fixes:
- Fix memory leak on palette extraction from image
- Fix logoff process is not found on pressing logoff button #141
- Fix error occuring during loading Windows Terminal preferences that inhibits the application from opening #147
- Fix no wallpaper preview with wallpaper sideshow or windows spotlight is enabled
- Fix wallpaper preview is not synchronized in Windows XP
- Fix wrong preview of color on Windows 11/10 taskbar
- Fix application crash on starting WinPaletter with classic theme enabled
- UI #134
- Spelling correction #140

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.7.6`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.7.6...v1.0.7.7) |
| Previous Beta   | `1.0.6.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.6.9...v1.0.7.7) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.7.7) |

---

### 1.0.7.6

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 01 Apr 2023  |

> **What's New?**

## The same as [1.0.7.5](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/CHANGELOG.md#1075), except for:

### 1. Windows 11 preview and description labels

- Windows 11 preview is redesigned to make it closer to the latest stable builds.
- Description labels are re-written, they are dependent on the latest stable Windows 11. If your Windows is outdated, these labels might not be the same as your current system.
- If you installed ExplorerPatcher and uninstalled it, WinPaletter will detect ExplorerPatcher is still installed (due do ExplorerPatcher registry remnants) and so the descriptions will be different. You can solve this by going to Settings > ExplorerPatcher and then disable preview synchronization.

### 2. Cursors Shadow:

- Now you can modify cursors shadow. You can control its color, blur power, opacity and its offset (X, Y).
- Each cursor has its own separate shadow.
- Shadow in "Miscellaneous" part is rendered by Windows, while custom shadow is rendered by WinPaletter itself.
- The more you enable custom shadow, the more WinPaletter will take to render the cursor.

### 3. Improvements and fixes:

- Fixing classic cursor main arrow to make it matches the classic NT cursor outline.
- Improvements in Windows 7/Vista preview speed while customizing titlebar height in metrics. (There was a lag)
- Fixing crashing bug during applying a classic color with classic theme enabled or applying classic colors with switching from a theme to classic mode in Windows XP, Vista & 7
- Making a visual-styles change in Windows XP being applied to login screen too.
- Fixing bug in reading "Fonts Substitutes" from theme file.
- Fixing bug in reading and saving "Fonts" from\to theme file.
- Mechanism of detecting internet connection becomes quicker than before.
- UI improvements.

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.7.5`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.7.5...v1.0.7.6) |
| Previous Beta   | `1.0.6.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.6.9...v1.0.7.6) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.7.6) |

---

### 1.0.7.5

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 24 Mar 2023  |

> **What's New?**

## The same as [1.0.7.4](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/CHANGELOG.md#1074), except for:

### 1. Windows Effects new items:

- Change Explorer Bar for Windows 11 (Requires [ExplorerPatcher](https://github.com/valinet/ExplorerPatcher)) #117, Windows 10 and 8.1 #123
- Disable Navigation bar #122 #105

### 2. Windows 10

- A new option is made to control Windows elements blur (Its toggle is located beside transparency's toggle)

### 3. Changes and fixes:

- UI improvements: #124
- Some options in Windows Effects and cursors lose their effects after logoff. Now they are fixed.
- Mechanism of reading from\writing to registry improved a bit
- New options are added in `Settings` > `Theme Applying Behavior` to extend their applying scope in registry (All users). The options are for User Preference Mask, Logoff Wallpaper (Tested in Windows XP), Classic Colors, Metrics and Fonts, Cursors and Consoles.

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.7.4`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.7.4...v1.0.7.5) |
| Previous Beta   | `1.0.6.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.6.9...v1.0.7.5) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.7.5) |

---

### 1.0.7.4

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 16 Mar 2023  |

> **What's New?**

#### The same as [1.0.7.3](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/CHANGELOG.md#1073), except for:

#### 1. Windows Effects new items (For Windows 11 only):

- Change Explorer Bar for Windows 11 (Requires [ExplorerPatcher](https://github.com/valinet/ExplorerPatcher)) #117
- Switch between Circle/Spinning Dots in Windows 11 boot screen

#### 2. Classic Colors schemes:

- The illustrator has been updated, it will follow the colors you choose.
- You can change the scope in which classic colors can be applied. Go to `Settings > Theme Applying Behavior` to change its preferences. #114

#### 3. Changes and fixes:

- UI improvements: #120 #119 #118 #116  #115 #113

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.7.3`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.7.3...v1.0.7.4) |
| Previous Beta   | `1.0.6.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.6.9...v1.0.7.4) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.7.4) |

---

### 1.0.7.3

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 11 Mar 2023  |

> **What's New?**

### The same as [1.0.7.1](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/CHANGELOG.md#1071) and [1.0.7.2](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/CHANGELOG.md#1072), except for:

### New features:

#### 1. Synchronize preview of Windows 11 with ExplorerPatcher #28:

- For example, if you set a Windows 10 taskbar in Windows 11, WinPaletter will adjust the preview to make the taskbar as Windows 10, and so on.
- If you didn't install ExplorerPatcher or even don't use Windows 11, you can force WinPaletter to pretend that ExplorerPatcher is used; Go to Settings > ExplorerPatcher > Check `Deflect` and save settings.

#### 2. Cursors Studio redesigned:

- Layout of Cursors studio is changed
- New Styles for Default Cursor and Loading Cursor are made, it can be `Aero`, `Classic` or `Modern`.
- If you use a language file, you may see this form incomplete or not perfect, so the translators should work on Cursors' section.

#### 3. Windows Switcher

- You can change its appearance now (Alt+Tab Switcher), it can be default Windows or Classic. #105
- You can change Windows 10 background opacity (or Windows 11 + ExplorerPatcher).

#### 4. New items in Windows Effects, including:

- Shake a window to minimize the others
- Underline menu items #110
- Classic Context Menu for Windows 11 #105
- Balloon notification duration #110 and _force it for Windows 10 (or Windows 11 + ExplorerPatcher)_
- Focus rectangle width and height (More seen in classic elements) #110
- Text Cursor (Caret) width #110
- Active Windows Tracking #110
- SysListView32 #105
- Snap cursor to default button
- Show seconds in taskbar of Windows 10 (or Windows 11 + ExplorerPatcher) #105
- Paint Windows version on desktop #105

#### 5. Windows 10:

- A new option is made to make the taskbar more transparent.

#### 6. Classic Colors schemes:

- Added Windows 1 and 2 classic schemes.
- Windows 3.1 schemes are improved to have a closer look to Windows 3.1.

### Changes and fixes:

- UI improvements: #98 #99 #100 #101 #106 #107 #109
- A new option is added in `Settings` > `Theme Applying Behavior` is made to delay applying the Metrics and Fonts until the user logs-off and logs-on, and this is to escape from the situation of crashing in #102 and #74

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.7.2`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.7.2...v1.0.7.3) |
| Previous Beta   | `1.0.6.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.6.9...v1.0.7.3) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.7.3) |

---

### 1.0.7.2

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 05 Mar 2023  |

> **What's New?**

### The same as [1.0.7.1](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/CHANGELOG.md#1071), except for:

#### New features:

- Adding Cursor Tracking (Sonar) option #88 and Cursor Trails option in Cursors
- Adding "Show content while dragging" option in Windows Effects #87

#### Changes and fixes:

- There was a lag during changing the preview in main form. Now it is improved a bit.
- Improvements in UI #97 #95 #93 #90
- Making Icons labels in metrics preview follow the preset of shadow in Windows Effects #94
- Making Menu in Classic Colors follows the preset of shadow in Windows Effects #91
- Making user in Command Prompt can't choose a font if raster font is checked, and that is to remove the vague preview #96
- Making Command Prompt have 6x9 & 8x9 raster fonts layouts. They are identical to 6x8 & 8x8 respectively #96
- Fixing bug in Windows Terminal #83
- Making alert if you are using a classic theme and opened Windows XP preview (as there are limitations in the component used in visual styles previewer) #89

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.7.1`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.7.1...v1.0.7.2) |
| Previous Beta   | `1.0.6.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.6.9...v1.0.7.2) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.7.2) |

---

### 1.0.7.1

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 28 Feb 2023  |

> **What's New?**

# New Features:

### 1. Extended support to Windows XP and Vista

- In Windows XP, you can change specifically the themes and logonui preferences.
- In Windows Vista, you can change specifically Windows colors.
- It is necessary to read their [documentation in GitHub](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Documentations/LegacyOS/LegacyOS.md)

### 2. Windows Effects:

- You can change now effects and animations in Windows, like window shadow, window transition animation, desktop icons label shadow, combobox animation and other effects.
- Partially suggested in #80 by @Annix

# Redesigns:

- Classic Colors’ 3D objects (Formerly Win32UI) redesigned to make it have an illustrative look, with doing this #79
- Changed some icons in Classic Colors as the previous ones were not obvious in relation to their functions.
- Main Form Extra Tools buttons are condensed to make their container able to contain new\more features.

# Improvements and bugs fixes:

1. Classic Colors and Metrics & Fonts now have a button that imports preferences from a Visual Styles file.
2. Windows Animations toggle is moved from “Windows Metrics and Fonts” to “Windows Effects”.
3. Fixed bug in color picker that occurs during picking a color from the screen.
4. Fixed issue of Font “Segoe UI”’s substitution not being saved into WinPaletter theme file.
5. Fixed issue of Wallpaper Tone, if any value of it changed, WinPaletter couldn’t detect the change happened and so the confirmation dialog didn’t appear when you exit in the previous situation.
6. Remove a code caused issue in GUI with high DPIs #70.
7. Fixed an issue during application’s startup when listing monospaced fonts #73.
8. Fixed an issue during rendering LogonUI images for Windows 8.1 & 7.
9. Bug reporter improved and will show more details.
10. Drag Previewer improved a bit.
11. Known issue might occur during application’s startup when monitoring wallpaper\registry changes. If this bug occurred, you will be notified by a message. It is fixable by resetting your desktop wallpaper.
12. Now you can control cursors shadow.

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.7.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.7.0...v1.0.7.1) |
| Previous Beta   | `1.0.6.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.6.9...v1.0.7.1) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.7.1) |

---

### 1.0.7.0

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 13 Feb 2023  |

> **What's New?**

# New Features:

### 1. Windows Metrics & Fonts:

- Suggested by @Anixx #17 
- Now you can change Windows Metrics and fonts (e.g., Titlebar Height, Titlebar Font, Window Border Width, Icons Spacing & Fonts, .... etc.)
- There are some limitations when you apply (not all items) that require logoff and logon
- `Known Issue (You may not face it)`: In Windows 11 22H2, Titlebar Font may not be changed from Segoe UI even if the font is changed correctly in registry and even if you restart your Windows.
- Fixed issue occurs during reading Metrics from Registry (Fixed bug from 1.0.6.9) #66

### 2. Fonts Substitutes (A part of Windows Metrics & Fonts):

- You can substitute some system fonts "MS Shell Dlg" and "Segoe UI" to give your Windows a different look. 
- Please read its instructions in Metrics & Fonts > Fonts Substitutes

### 3. Wallpaper Tone:

- For sure you saw some stock Windows wallpapers with different colors, you can now create your own wallpapers with the color you want.
- This is dependent on modification of image's HSL (Hue\Saturation\Lightness) and this isn't professional as much as advanced images editors (Paint.NET\GIMP\Photoshop\...)

### 4. New defined color in Windows 10/11:

- There were some undefined colors, now new 2 items are defined\known: 
  1) Background of UWP Dialogs Remnants from Windows 8.1 in Windows 10/11
  2) Taskbar Tray Overflow Background (Windows 11 22H2 with accent enabled in taskbar)

### 5. Languages:

- Languages become stable (not experimental), and its mechanism has been changed, becoming dependent on JSON files not the old file (*.wplng)
- This makes it easier to modify by a text\code editor like VSCode or by WinPaletter Language Developing Tools (In Settings > Language)
- You can load language quickly (there is no delay) and without restarting WinPaletter (Except for returning to English)
- You can use WinPaletter Language Developing Tools to create, modify or update language json files (The Tools were separated before in WinPaletter Language Translator.exe, now you won’t use it you will use WinPaletter itself).
- So, it is necessary for language creators\contributors to use the latest version of WinPaletter and update their translations\languages (As it is not complete compared to earlier versions)
- Crashes that occurred during startup while loading language files are mostly fixed. (Like: #56)
- `Known Issue`: People who are using old language files (*.wplng) and updated WinPaletter, an error will occur during loading the application, ignore this error, download newer language files (*.json), go to settings and load the new file and save.

### 6. Logs:

Extended Details will be shown during applying the theme (including time info and errors), you can change the preferences in Settings > Theme Logging.

### 7. Preview Switch

 In main page, you can navigate between all supported versions of Windows, making it easier to design one theme file for all supported Windows editions. 

### 8. Theme File Blocks:

Now the theme file that is saved by WinPaletter (.wpth) has 2 blocks for Windows 11/10 (before it was one block applied to both Windows 11/10), making it easier to design a theme presented in one file for both versions of Windows (To avoid conflict between colors applied between these Windows Versions) (Themes File of WinPaletter earlier than 1.0.6.9 can be loaded/saved too)

---

# Redesigns:

- Application’s UI improved a lot, especially the light mode becomes better, with the ability to change WinPaletter’s Colors and Backgrounds (Theming WinPaletter itself). Also, legacy dialogs like message boxes are modernized.
- Application's Controls become more consistent with System Dark\Light Mode
- Themes Previews in all forms are redesigned to match Windows Metrics and fonts (becoming more realistic compared to previous versions of WinPaletter)
- Drag and Drop preview are redesigned to have a bigger size
- Windows Terminal forms are redesigned to have tabs instead of the condensed ones.
- Name of "Win32UI" changed into "Classic Colors"
- New 3 colors schemes in Classic Colors (Win32UI): Luna Fullback Blue, Silver and Olive Green

---

# Multiple Improvements and Bugs Fixes:

1. Added the ability to uninstall from Control Panel, with fixing issue during uninstall #68
2. Loading Preferences of Windows Colors/Options from registry mechanism improved a lot, and WinPaletter will continue applying theme even if there is an error (exception error) happened (i.e., the error part will be skipped) (Issue of #59 won't be experienced and it won't disturb the whole process of applying theme, the error will be known separately after applying theme)
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
15. Fixed error in Classic Colors that caused not setting flat menus.
16. Add panels into LogonUI Forms that has buttons to open WinPaletter themes (File\Current Applied\Default Windows) (They were not added before)
17. If this option is enabled in settings <If Cursors Applying is disabled or skipped, automatic switch the cursors scheme to Windows Default "Aero"> and applied a theme without applying any custom cursors before, an error happens. It is now fixed (Actual cause of #68)
18. Fixed empty preview in Windows 7 classic (@Anixx #62 #65)
19. Fixed bug in icon preview in Metrics & Fonts (@Anixx #64)

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.6.3`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.6.3...v1.0.7.0) |
| Previous Beta   | `1.0.6.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.6.9...v1.0.7.0) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.7.0) |

---

### 1.0.6.9

> **Info:**

| Channel | Release Date |
|:-------:|:------------:|
| `Beta`  | 27 Jan 2023  |

> **What's New?**

# New Features:

### 1. Windows Metrics & Fonts:

- Suggested by @Anixx #17 
- Now you can change Windows Metrics and fonts (e.g., Titlebar Height, Titlebar Font, Window Border Width, Icons Spacing & Fonts, .... etc.)
- There are some limitations when you apply (not all items) that require logoff and logon
- `Known Issue (You may not face it)`: In Windows 11 22H2, Titlebar Font may not be changed from Segoe UI even if the font is changed correctly in registry and even if you restart your Windows.

### 2. Languages:

- Languages become stable (not experimental), and its mechanism has been changed, becoming dependent on JSON files not the old file (*.wplng)
- This makes it easier to modify by a text\code editor like VSCode or by WinPaletter Language Developing Tools (In Settings > Language)
- You can load language quickly (there is no delay) and without restarting WinPaletter (Except for returning to English)
- You can use WinPaletter Language Developing Tools to create, modify or update language json files (The Tools were separated before in WinPaletter Language Translator.exe, now you won’t use it you will use WinPaletter itself).
- So, it is necessary for language creators\contributors to use the latest version of WinPaletter and update their translations\languages (As it is not complete compared to earlier versions)
- Crashes that occurred during startup while loading language files are mostly fixed. (Like: #56)
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
2. Loading Preferences of Windows Colors/Options from registry mechanism improved a lot, and WinPaletter will continue applying theme even if there is an error (exception error) happened (i.e., the error part will be skipped) (Issue of #59 won't be experienced and it won't disturb the whole process of applying theme, the error will be known separately after applying theme)
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
