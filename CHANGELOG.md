# Changelog

---

### 1.0.9.2

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 12 Apr 2024  |

# Critical update
This update is pushed to fix an exception error that occurs when changing the cursor size, when the source is a file and not rendered by WinPaletter.

---

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.9.1`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.9.1...v1.0.9.2) |
| Previous Beta   | `1.0.8.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.9...v1.0.9.2) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.9.2) |

---

### 1.0.9.1

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 11 Apr 2024  |

# Critical update
This update is pushed to fix an exception error occured on substituting font Segoe UI and opening back WinPaletter.

---

# Application improvements:

- Change cursors sizes as you like in Windows 10 and higher.

---

# Bugs fixes

- Wrong wiki (help) URL for cursors is fixed.

---

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.9.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.9.0...v1.0.9.1) |
| Previous Beta   | `1.0.8.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.9...v1.0.9.1) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.9.1) |

---

### 1.0.9.0

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Stable` | 01 Apr 2024  |

## 🛑 Announcement: Project Development Discontinuation:

Dear WinPaletter Users,

It is with a heavy heart that I announce the discontinuation of further development on the WinPaletter project. While there's an extremely slim possibility that I may find time in the distant future, perhaps years from now, to resume maintenance, I must inform you that version `1.0.9.0` marks the end of active development. Subsequent versions (`1.0.9.x`) has extremely weak possibility to be developed.

In the coming days or weeks, I will proceed to archive this repository. However, please note that the existing version will remain accessible for continued use, albeit without updates or maintenance.

You can certainly contribute to the WinPaletter Store for themes; this repository won't be archived. [Open the Wiki](https://github.com/Abdelrhman-AK/WinPaletter/wiki) and navigate to the WinPaletter Store section in the side panel.

I want to express my deepest gratitude for your support and for choosing WinPaletter. Your enthusiasm and feedback have been invaluable throughout this journey. It has been an immense honor for me to contribute to a project that aimed to enhance your user experience. If WinPaletter has caused any inconvenience or disruption to your Windows setup, I sincerely apologize.

---

## ⚠️ **Warning**
### If you are using command lines, type `WinPaletter -?` in any terminal to know new commands formats

## ℹ️ **Info**
### This stable release is identical to the last 5 beta updates. If you have read them before, don't read this long changelog to save your time. If not, then you can read this changelog.

---

# New features:

### 1. WinPaletter new look

- In this release, WinPaletter boasts a revamped appearance with a massive UI refresh, including the introduction of a new tabbed navigation system for an organized and intuitive user experience. The addition of tabs enhances visual appeal and facilitates seamless task-switching. Embrace the improved visuals and navigation as I strive for a more user-friendly interface in every aspect of the application.

### 2. Windows Visual Styles
- In this release, WinPaletter introduces a new feature that allows you to change Windows Visual Styles (part of #225).
- This feature requires patching UxTheme.dll. There will be a guide available within its form to patch it, either by using SecureUxTheme from its [official releases in its repository (recommended)](https://github.com/namazso/SecureUxTheme/releases) or by utilizing the SecureUxTheme setup wrapper provided by WinPaletter.

### 3. Windows Icons

- This new feature will allow you to modify desktop icons, certain explorer icons, control panel icons on the main page, and shell32.dll.
- It won't patch system files; this process involves modifying the registry exclusively.

### 4. Changes in all Windows aspects

- Some aspects (Windows Colors, Classic Colors, Cursors, Metrics, Fonts, and Wallpaper) will have two modes: Advanced Mode and Simple Mode. This change aims to make modifying Windows easier for users who found WinPaletter complex to use #190.

- All aspects will feature a toggle controller (including aspects that didn't have it before, such as Windows Colors and Classic Colors), allowing users to control all Windows aspects and disable aspects they do not wish to modify.

- All aspects will now include a toolbar featuring a simplified button layout (instead of the old and condensed buttons). Additionally, a split button is introduced to provide more functions, such as importing schemes from Windows defaults or current preferences.

### 5. Changes in Windows aspect; Windows Colors

- Fixes in previewer and performance improvements.

- Per-element modifier; click on an elements on the preview to change its color.

- Windows 11/10 Accent Colors Generator: Modify the titlebar color, then click 'Generate Accent Colors from Active Titlebar Color' to make WinPaletter generate nine accent colors based on this selected color.

- Palette Generator: Improved performance and bug fixes, featuring a trials panel that records previous attempts to generate a palette, making it easier for users to revisit and select a preferred trial.

### 6. Changes in Windows aspect; Classic Colors

- Per-element modifier; right click on an elements on the preview to change its color.

- 3D and flat degrees modification in 3D objects improved.

- Magnifier: Use it to view small details in the preview, such as shadow borders and dark shadow lines. This feature makes it easier to utilize the per-element modifier.

- Schemes Gallery: Preview all Classic Colors schemes in a single form, aiding users in visualizing and selecting their preferred scheme.

- Fullscreen Preview: Expand the preview to fullscreen for a better examination.

- Resolved exception error when importing from visual styles designed for Windows versions newer than XP.

### 7. Changes in Windows aspect; Metrics and Fonts

- Per-element modifier: Click on an element in the preview to alter its font and drag-and-drop to adjust its sizes.

- Resolved a visual bug related to applying fonts with high DPI settings, preventing issues such as small or large fonts and irregular icon sizes not aligned with the selected DPI (scaling) value #205.

### 8. Changes in Windows aspect; Cursors Studio

- Enhanced rendering of classic styles, introducing pixelation for a more accurate and similar appearance to classic Windows cursors.

- Fixed: the center point animation of the hourglass in classic style.

- ⚠️ Known issue: Higher DPI scaling will render classic cursors wrongly.

### 9. Changes in Windows aspect; Windows Terminals and Consoles

- The JSON read/write mechanism for the terminal has been upgraded to improve code readability for developers and enhance the structure of theme files to match terminal JSON precisely. 

- Fixed a bug in creating a new external terminal (incorrect registry key path).

- Resolved issue where opening PowerShell for testing launches 'takeown' PowerShell executable instead of the intended PowerShell.

### 10. Changes in Windows aspect; Others

- LogonUI now supports preview for Windows 10 and 11.

- Screen Saver: Lists installed screensavers on the system to assist users in selecting one.

- Sounds: Added a 'Restore Defaults' button to facilitate the restoration of a sound entry.

- Sounds: WinPaletter will no longer install System Events Sounds by default at application startup. Users can manually install it in the Sounds form or Settings. However, if already installed, it will be updated automatically with a newer service version if available #206.

- Increased speed of Wallpaer Tone previewer.

- WinPaletter Application Themer: Introducing new colors for errors (secondary color) and tips (tertiary color), along with an option for animations.

### 11. Themes backup and restore

- WinPaletter now introduces a new feature that allows you to automatically backup your themes.

- Backing up before applying themes is enabled by default in WinPaletter.

- You can also back up upon application startup or when opening a theme file (these options are not enabled by default).

- You can open theme backups from the toolbar on the home page (main form).

- You can control the backup folder location and other settings in Settings > Themes Backup.

### 12. Aspects control

- WinPaletter now introduces a new feature that allows you to increase your control over Windows aspects. This will be helpful for users who don't want to change a Windows aspect they like (acts like a secure lock).

- For example, a user edited Windows Colors to an accent they like and doesn't want it to change again with any theme. This user can enable this feature and uncheck Windows Colors to disable editing it in the future.

- Another example is a user who likes their own Metrics and Fonts; using WinPaletter's store may override their preferences. This user can enable this feature and uncheck Metrics and Fonts to disable editing them in the future.

- You can find this feature in Settings > Aspects control.

---

# Application improvements:

- Update assembly `System.Resources.Extensions` to `9.0.0-preview.2.24128.5`
- Making WinPaletter can block aspect applying in its specific form if it was blocked in settings by 'Aspects control'
- Toggle compact\expanded layout in the main form (for small resolutions)
- Minor tweaks to make WinPaletter can handle Windows 12 (arbitary, exactly the same as Windows 11 until new developers work on it with Windows 12 release)
- Welcome dialog: it will give you quick tips about editing aspects, themes backup and finally an alert (#212) about using third-party tools.
- New automatic backup timing: after pressing 'Apply' in a single Windows aspect form.
- Parital support for Rectify11 (by fixing an issue in the last release of reapplying aero.msstyles instead of the previous msstyles) #210.
- Metrics and Fonts: Now, you can import preferences from a classic .theme file.
- Metrics and Fonts: Presets (like Classic Colors) from different Windows editions are introduced.
- Ability to generate .theme files including classic colors, metrics and fonts.
- Minor changes in Windows Effects layout: new section: Taskbar including show seconds, enable Windows 7 volume mixer in taskbar for Windows 10. and Windows 11 spinning dots boot screen is moved to Miscellaneous.
- Aero peek and hibernate thumbnails are moved from Windows 7/8.1 colors, to be available in Windows Effects > Taskbar for all Windows editions.
- Making Windows Effect's toggle enabled will display confirmation alert.
- Explorer ribbon and bar are applicable now without depending on ExplorerPatcher and StartAllBack.
- Store item redesign, and new patterns are introduced in theme edit form.
- Making close with tabs open dialog is shown first before saving theme dialog.
- Making bug report shows error message instead of error type in bug report title. (indirect issue in #224)
- Double-click on a tab to detach it, middle click to close it.
- Aero Lite theme for Windows 10/11 #38, with an option to skip setting it.
- New 4 Classic Colors schemes: Windows 11 Contrast themes: Aquatic, Desert, Dusk, and Night Sky.
- New sounds (Wi-Fi connection\disconnection\connection failure) in Devices section as requested in #218.
- Help button for the current active tab (won't be visible if a tab doesn't provide a help link).
- Changed message box of Windows Effects alert to another dialog with important tips, with a choice for not showing this dialog again #219.
- Command argument: Silent apply (For example: `WinPaletter -a Theme.wpth -s` or `WinPaletter --apply Theme.wpth --silent`).
- Modification of Windows Effects is controlled by a message. If you press 'Yes,' the application of these changes will continue, but please note that this may conflict with ExplorerPatcher settings. #217
- Starting now, the inclusion of User Preference Mask (UPM) modification for all users from current theme will be disabled by default. Look at Settings > Theme applying behavior. #217
### 13. WinPaletter Store for themes: Faster opening of the WinPaletter Store, Memory cleanup for store items upon closing the Store has been fixed.
- Command lines changes; type 'WinPaletter.exe -?' in any terminal for help.
- Replaced 'Save theme file and apply theme on exit' dialog with a straightforward Windows dialog.
- Resolved an exception error that occurred when applying a theme in a separate thread, ensuring the program does not exit before the thread completes its task.
- The mechanism for downloading data from the web has been improved, transitioning from WebClient to HTTPClient.
- Addressed increased memory usage during cursor rendering and after closing Wallpaper and LogonUI forms.
- Remove all registry residuals during uninstallation #196 #191.
- Enable the 'Colors history' feature within the 'Custom colors' section if the Windows Classic Colors picker is enabled in settings.
- Making Metrics and Fonts applies values to open windows in a separate thread as a trial to reduce hanging #209.
- UI improvements

---

# Bugs fixes

- Fix the issue where a system user account (NT SERVICE) appears among normal users (part of #225).
- Fix the issue where using the command line option "--apply" with a non-existing theme file still opens WinPaletter instead of exiting.
- Fix the issue where using the command line option "--apply" with Windows 11 applied with a custom visual style brings back aero.msstyles, while the GUI doesn't (part of #225).
- Fix the UI issue of a black area in tabs when the title bar is not.
- Fix Terminal two exception errors in a list on loading a WinPaletter theme file due to the inability to deserialize the current JSON array into type 'WinPaletter.WinTerminal+Types+Profiles' because the type requires a JSON object to deserialize.
- Fix merging user preference mask (UPM) to default account even if this is disabled in settings.
- Fix issues related to toggles, where Windows Switcher (Alt+Tab) and LogonUI (Windows 10/11) are not saveable #212.
- Fix the problem where pressing "apply" in Windows color doesn't load edited preferences in the current theme; apply should apply edited preferences and load them into the current open theme.
- Fix the absence of "Classic Colors" in the WinPaletter Store aspects check dialog.
- Fix in metrics and fonts: caption font not synced with menu and misc previews.
- Fix in metrics and fonts: icons font not synced with icons previews.
- Fix Windows XP metrics loading padded border width with 4 as a default value to fix the wrong classic theme preview.
- Fix cursor render (help) when font "Segoe UI Black" doesn't exist #224.
- Fix misc. section preview in metrics and fonts with classic mode is on due to wrong WindowText colors.
- Fix window caption buttons preview with classic mode is on and maladjusted close content 'x' and other control box buttons using the 'Marlett' font.
- Fix the issue of System.OutOfMemoryException: 'Out of memory.' occurring when a new theme is applied, followed by opening a new tab or form with a combo box in it.
- Fix the app crash that occurs when starting with the classic theme enabled.
- Fix bug of Windows 11/10 colors are not applicable (part of issue #212)
- Refix bug #214 when WinPaletter store is opened and closed, the bug reoccurs (home form is closed while the app is not closed, leaving a white rectangle).
- Fix bug #220: Windows Terminal exception error (ex error on returning null to be set as a color for background and unfocusedBackground in tabSettings in themes).
- Fix sounds not included in themes resources pack (Charger connected, Charger disconnected, Windows lock).
- Fix Classic Colors Gallery scheme picker not showing the background color in thumbnails.
- Fix per element modifier: clicking on the menu with flat menu enabled modified button face instead.
- Fix ex error on starting WinPaletter with a desktop with slideshow and directory of images doesn't exist.
- Metrics: Fix visual styles import if using a msstyles newer than Windows XP.
- Fix ex error on importing a visual styles (*.msstyles) if a .theme file is selected from the open file dialog.
- Fix press edit button in the store doesn't fully apply WinPaletter theme in open forms.
- Fix dragging area below store tab moves the form with glitches.
- Fix exception error when adding a form into tabs (inaccessible form icon) #218.
- Fix browse for wav wrong titles in sounds.
- Fix Windows Terminal exception error (incorrect character error in JSON file when using ColorSchemePair as a type for colorScheme or numeric font weight instead of a value from an enum). #215
- Fix the bug where the application continues running in tabs when the home or main form is closed. #214
- Fix exception error when opening the Windows XP Colors editor and Metrics and Fonts editor with Windows XP selected.
- Windows XP preview design fixes (increased titlebar height).
- Fix inability to select the menu font in the metrics editor.
- Fix preview: status font is not selected at the first Metrics and Fonts load.
- Fix incorrect label colors and menu bar color in Windows Vista Metrics and Fonts preview.
- Fix bug in WinPaletter application themes: selecting the GitHub scheme making the accent color the same as Reddit's scheme.
- Resolved exception error with ITaskbar3 on Windows XP and Vista.
- Fixed exception errors occurring when applying cursors.
- Fixed error when applying Windows 8.1 lock screen background and Windows 7 LogonUI background.
- Fixed bug applying dark mode after opening WinPaletter Application Themer with light mode.
- Fixed sync issue between WinPaletter and Wallpaper change preview and automatic dark/light mode.

---

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.8.4`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.4...v1.0.9.0) |
| Previous Beta   | `1.0.8.9`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.9...v1.0.9.0) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.9.0) |

---

### 1.0.8.9

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Beta`   | 27 Mar 2024  |

## 🛑 Announcement: Project Development Discontinuation:
### While there's an extremely weak possibility that I may find time in the distant future (maybe years) to resume maintenance of WinPaletter, as of now, Version `1.0.9.0` is likely to be the final release within the next few days or weeks (or subsequent `1.0.9.x` versions). Comprehensive details will be provided in the `readme.md`, and I'll proceed to archive this repository accordingly. Thank you sincerely for your support and for choosing WinPaletter.

⚠️ **Warning**
If you are using command lines, type `WinPaletter -?` in any terminal to know new commands formats

# The same as 1.0.8.5, 1.0.8.6, 1.0.8.7, and 1.0.8.8 except for a new feature, some application improvements and bugs fixes:

### New feature: Windows Visual Styles
- In this release, WinPaletter introduces a new feature that allows you to change Windows Visual Styles (part of #225).
- This feature requires patching UxTheme.dll. There will be a guide available within its form to patch it, either by using SecureUxTheme from its [official releases in its repository (recommended)](https://github.com/namazso/SecureUxTheme/releases) or by utilizing the SecureUxTheme setup wrapper provided by WinPaletter.

### Application improvements
- A new option is made to skip setting theme from (default\aero lite) in Windows 10/11
- Making WinPaletter can block aspect applying in its specific form if it was blocked in settings by 'Aspects control'
- Toggle compact\expanded layout in the main form (for small resolutions)

- Minor tweaks to make WinPaletter can handle Windows 12 (arbitary, exactly the same as Windows 11 until new developers work on it with Windows 12 release)

### Bug fixes
- Fix the issue where a system user account (NT SERVICE) appears among normal users (part of #225).
- Fix the issue where using the command line option "--apply" with a non-existing theme file still opens WinPaletter instead of exiting.
- Fix the issue where using the command line option "--apply" with Windows 11 applied with a custom visual style brings back aero.msstyles, while the GUI doesn't (part of #225).
- Fix the UI issue of a black area in tabs when the title bar is not.

---

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.8.4`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.4...v1.0.8.9) |
| Previous Beta   | `1.0.8.8`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.8...v1.0.8.9) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.8.9) |

---

### 1.0.8.8

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Beta`   | 19 Mar 2024  |

⚠️ **Warning**
If you are using command lines, type `WinPaletter -?` in any terminal to know new commands formats

# The same as 1.0.8.5, 1.0.8.6, and 1.0.8.7 except for a new feature, some application improvements and bugs fixes:

### New feature: Windows Icons

- This new feature will allow you to modify desktop icons, certain explorer icons, control panel icons on the main page, and shell32.dll.
- It won't patch system files; this process involves modifying the registry exclusively.

### Application improvements
1- Welcome dialog: it will give you quick tips about editing aspects, themes backup and finally an alert (#212) about using third-party tools.
2- New automatic backup timing: after pressing 'Apply' in a single Windows aspect form.
3- Parital support for Rectify11 (by fixing an issue in the last release of reapplying aero.msstyles instead of the previous msstyles) #210.
4- Metrics and Fonts: Now, you can import preferences from a classic .theme file.
5- Metrics and Fonts: Presets (like Classic Colors) from different Windows editions are introduced.
6- Ability to generate .theme files including classic colors, metrics and fonts.
7- Minor changes in Windows Effects layout: new section: Taskbar including show seconds, enable win 7 sndvol. and Win 11 spinning dots boot screen is moved to Miscellaneous.
8- Aero peek and hibernate thumbnails are moved from Windows 7/8.1 colors, to be available in Windows Effects > Taskbar for all Windows editions.
9- Making Windows Effect's toggle enabled will display confirmation alert.
10- Explorer ribbon and bar are applicable now without depending on ExplorerPatcher and StartAllBack.
11- Store item redesign, and new patterns are introduced in theme edit form.
12- Making close with tabs open dialog is shown first before saving theme dialog.
13- Making bug report shows error message instead of error type in bug report title. (indirect issue in #224)
14- UI improvements

### Bug fixes
1- Fix Terminal two exception errors in a list on loading a WinPaletter theme file due to the inability to deserialize the current JSON array into type 'WinPaletter.WinTerminal+Types+Profiles' because the type requires a JSON object to deserialize.
2- Fix merging user preference mask (UPM) to default account even if this is disabled in settings.
3- Fix issues related to toggles, where Windows Switcher (Alt+Tab) and LogonUI (Windows 10/11) are not saveable #212.
4- Fix the problem where pressing "apply" in Windows color doesn't load edited preferences in the current theme; apply should apply edited preferences and load them into the current open theme.
5- Fix the absence of "Classic Colors" in the WinPaletter Store aspects check dialog.
6- Fix in metrics and fonts: caption font not synced with menu and misc previews.
7- Fix in metrics and fonts: icons font not synced with icons previews.
8- Fix Windows XP metrics loading padded border width with 4 as a default value to fix the wrong classic theme preview.
9- Fix cursor render (help) when font "Segoe UI Black" doesn't exist #224.
10- Fix misc. section preview in metrics and fonts with classic mode is on due to wrong WindowText colors.
11- Fix window caption buttons preview with classic mode is on and maladjusted close content 'x' and other control box buttons using the 'Marlett' font.
12- Fix the issue of System.OutOfMemoryException: 'Out of memory.' occurring when a new theme is applied, followed by opening a new tab or form with a combo box in it.
13- Fix the app crash that occurs when starting with the classic theme enabled.

---

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.8.4`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.4...v1.0.8.8) |
| Previous Beta   | `1.0.8.7`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.7...v1.0.8.8) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.8.8) |

---

### 1.0.8.7

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Beta`   | 02 Mar 2024  |

⚠️ **Warning**
If you are using command lines, type `WinPaletter -?` in any terminal to know new commands formats

⚠️ **Known issue**
Due to massive changes in Windows Terminal structure in WinPaletter, an error may occur when opening a theme file as the structure has been completely revised. You can safely ignore this error and continue using the theme file; however, note that the old preferences from the older theme file for the terminal will be disregarded until you resave the theme.

# The same as 1.0.8.5 and 1.0.8.6, except for some application improvements and bugs fixes:

### Application improvements
- Double-click on a tab to detach it, middle click to close it.
- Aero Lite theme for Windows 10/11 #38.
- New 4 Classic Colors schemes: Windows 11 Contrast themes: Aquatic, Desert, Dusk, and Night Sky.
- New sounds (Wi-Fi connection\disconnection\connection failure) in Devices section as requested in #218.
- Help button for the current active tab (won't be visible if a tab doesn't provide a help link).
- Changed message box of Windows Effects alert to another dialog with important tips, with a choice for not showing this dialog again #219.
- Command argument: Silent apply (For example: `WinPaletter -a Theme.wpth -s` or `WinPaletter --apply Theme.wpth --silent`).
- Minor UI improvements.

### Bug fixes
- Fix bug of Windows 11/10 colors are not applicable (part of issue #212)
- Refix bug #214 when WinPaletter store is opened and closed, the bug reoccurs (home form is closed while the app is not closed, leaving a white rectangle).
- Fix bug #220: Windows Terminal exception error (ex error on returning null to be set as a color for background and unfocusedBackground in tabSettings in themes).
- Fix sounds not included in themes resources pack (Charger connected, Charger disconnected, Windows lock).
- Fix Classic Colors Gallery scheme picker not showing the background color in thumbnails.
- Fix per element modifier: clicking on the menu with flat menu enabled modified button face instead.
- Fix ex error on starting WinPaletter with a desktop with slideshow and directory of images doesn't exist.
- Metrics: Fix visual styles import if using a msstyles newer than Windows XP.
- Fix ex error on importing a visual styles (*.msstyles) if a .theme file is selected from the open file dialog.
- Fix press edit button in the store doesn't fully apply WinPaletter theme in open forms.
- Fix dragging area below store tab moves the form with glitches.
- Fix exception error when adding a form into tabs (inaccessible form icon) #218.
- Fix browse for wav wrong titles in sounds.

### This is not the final look. In the new beta releases, UI changes will continue to be made, improved, and any discovered bugs will be fixed.

---

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.8.4`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.4...v1.0.8.7) |
| Previous Beta   | `1.0.8.6`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.6...v1.0.8.7) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.8.7) |

---

### 1.0.8.6

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Beta`   | 25 Feb 2024  |

⚠️ **Warning**
If you are using command lines, type `WinPaletter -?` in any terminal to know new commands formats

⚠️ **Known issue**
Due to massive changes in Windows Terminal structure in WinPaletter, an error may occur when opening a theme file as the structure has been completely revised. You can safely ignore this error and continue using the theme file; however, note that the old preferences from the older theme file for the terminal will be disregarded until you resave the theme.

# The same as 1.0.8.5, except for some application improvements and bugs fixes:

### Application improvements:
- Modification of Windows Effects is controlled by a message. If you press 'Yes,' the application of these changes will continue, but please note that this may conflict with ExplorerPatcher settings. #217
- Starting now, the inclusion of User Preference Mask (UPM) modification for all users from current theme will be disabled by default. Look at Settings > Theme applying behavior. #217

### Bugs fixes:
- Fix Windows Terminal exception error (incorrect character error in JSON file when using ColorSchemePair as a type for colorScheme or numeric font weight instead of a value from an enum). #215
- Fix the bug where the application continues running in tabs when the home or main form is closed. #214
- Fix exception error when opening the Windows XP Colors editor and Metrics and Fonts editor with Windows XP selected.
- Windows XP preview design fixes (increased titlebar height).
- Fix inability to select the menu font in the metrics editor.
- Fix preview: status font is not selected at the first Metrics and Fonts load.
- Fix incorrect label colors and menu bar color in Windows Vista Metrics and Fonts preview.
- Fix bug in WinPaletter application themes: selecting the GitHub scheme making the accent color the same as Reddit's scheme.

### This is not the final look. In the new beta releases, UI changes will continue to be made, improved, and any discovered bugs will be fixed.

---

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.8.4`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.4...v1.0.8.6) |
| Previous Beta   | `1.0.8.5`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.5...v1.0.8.6) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.8.6) |

---

### 1.0.8.5

> **Info:**

| Channel  | Release Date |
|:--------:|:------------:|
| `Beta`   | 18 Feb 2024  |

⚠️**Warning**
If you are using command lines, type `WinPaletter -?` in any terminal to know new commands formats

⚠️**Known issue**
Due to massive changes in Windows Terminal structure in WinPaletter, an error may occur when opening a theme file as the structure has been completely revised. You can safely ignore this error and continue using the theme file; however, note that the old preferences from the older theme file for the terminal will be disregarded until you resave the theme.

# New Features:

### 1. WinPaletter new look

- In this release, WinPaletter boasts a revamped appearance with a massive UI refresh, including the introduction of a new tabbed navigation system for an organized and intuitive user experience. The addition of tabs enhances visual appeal and facilitates seamless task-switching. Embrace the improved visuals and navigation as I strive for a more user-friendly interface in every aspect of the application.
- This is not the final look. In the new beta releases, UI changes will continue to be made, improved, and any discovered bugs will be fixed.

### 2. Changes in all Windows aspects

- Some aspects (Windows Colors, Classic Colors, Cursors, Metrics, Fonts, and Wallpaper) will have two modes: Advanced Mode and Simple Mode. This change aims to make modifying Windows easier for users who found WinPaletter complex to use #190.
- All aspects will feature a toggle controller (including aspects that didn't have it before, such as Windows Colors and Classic Colors), allowing users to control all Windows aspects and disable aspects they do not wish to modify.
- All aspects will now include a toolbar featuring a simplified button layout (instead of the old and condensed buttons). Additionally, a split button is introduced to provide more functions, such as importing schemes from Windows defaults or current preferences.

### 3. Changes in Windows aspect; Windows Colors

- Fixes in previewer and performance improvements.
- Per-element modifier; click on an elements on the preview to change its color.
- Windows 11/10 Accent Colors Generator: Modify the titlebar color, then click 'Generate Accent Colors from Active Titlebar Color' to make WinPaletter generate nine accent colors based on this selected color.
- Palette Generator: Improved performance and bug fixes, featuring a trials panel that records previous attempts to generate a palette, making it easier for users to revisit and select a preferred trial.

### 4. Changes in Windows aspect; Classic Colors

- Per-element modifier; right click on an elements on the preview to change its color.
- 3D and flat degrees modification in 3D objects improved.
- Magnifier: Use it to view small details in the preview, such as shadow borders and dark shadow lines. This feature makes it easier to utilize the per-element modifier.
- Schemes Gallery: Preview all Classic Colors schemes in a single form, aiding users in visualizing and selecting their preferred scheme.
- Fullscreen Preview: Expand the preview to fullscreen for a better examination.
- Resolved exception error when importing from visual styles designed for Windows versions newer than XP.

### 5. Changes in Windows aspect; Metrics and Fonts

- Per-element modifier: Click on an element in the preview to alter its font and drag-and-drop to adjust its sizes.
- Resolved a visual bug related to applying fonts with high DPI settings, preventing issues such as small or large fonts and irregular icon sizes not aligned with the selected DPI (scaling) value #205.

### 6. Changes in Windows aspect; Cursors Studio

- Enhanced rendering of classic styles, introducing pixelation for a more accurate and similar appearance to classic Windows cursors.
- Fixed: the center point animation of the hourglass in classic style.
- ⚠️ Known issue: Higher DPI scaling will render classic cursors wrongly.

### 7. Changes in Windows aspect; Windows Terminals and Consoles

- The JSON read/write mechanism for the terminal has been upgraded to improve code readability for developers and enhance the structure of theme files to match terminal JSON precisely. 
- ⚠️ Known issue: Due to these changes, an error may occur when opening a theme file as the structure has been completely revised. You can safely ignore this error and continue using the theme file; however, note that the old preferences from the older theme file for the terminal will be disregarded until you resave the theme.
- Fixed a bug in creating a new external terminal (incorrect registry key path).
- Resolved issue where opening PowerShell for testing launches 'takeown' PowerShell executable instead of the intended PowerShell.

### 8. Changes in Windows aspect; Others

- LogonUI now supports preview for Windows 10 and 11.
- Screen Saver: Lists installed screensavers on the system to assist users in selecting one.
- Sounds: Added a 'Restore Defaults' button to facilitate the restoration of a sound entry.
- Sounds: WinPaletter will no longer install System Events Sounds by default at application startup. Users can manually install it in the Sounds form or Settings. However, if already installed, it will be updated automatically with a newer service version if available #206.
- Increased speed of Wallpaer Tone previewer.
- WinPaletter Application Themer: Introducing new colors for errors (secondary color) and tips (tertiary color), along with an option for animations.

### 9. Themes backup and restore

- WinPaletter now introduces a new feature that allows you to automatically backup your themes.
- Backing up before applying themes is enabled by default in WinPaletter.
- You can also back up upon application startup or when opening a theme file (these options are not enabled by default).
- You can open theme backups from the toolbar on the home page (main form).
- You can control the backup folder location and other settings in Settings > Themes Backup.

### 10. Aspects control

- WinPaletter now introduces a new feature that allows you to increase your control over Windows aspects. This will be helpful for users who don't want to change a Windows aspect they like (acts like a secure lock).
- For example, a user edited Windows Colors to an accent they like and doesn't want it to change again with any theme. This user can enable this feature and uncheck Windows Colors to disable editing it in the future.
- Another example is a user who likes their own Metrics and Fonts; using WinPaletter's store may override their preferences. This user can enable this feature and uncheck Metrics and Fonts to disable editing them in the future.
- You can find this feature in Settings > Aspects control.

### 11. WinPaletter Store for themes

- Faster opening of the WinPaletter Store.
- Memory cleanup for store items upon closing the Store has been fixed.

---

# Application improvements:

1. Command lines changes; type 'WinPaletter.exe -?' in any terminal for help.
2. Replaced 'Save theme file and apply theme on exit' dialog with a straightforward Windows dialog.
3. Resolved an exception error that occurred when applying a theme in a separate thread, ensuring the program does not exit before the thread completes its task.
4. The mechanism for downloading data from the web has been improved, transitioning from WebClient to HTTPClient.
5. Addressed increased memory usage during cursor rendering and after closing Wallpaper and LogonUI forms.
6. Remove all registry residuals during uninstallation #196 #191.
7. Enable the 'Colors history' feature within the 'Custom colors' section if the Windows Classic Colors picker is enabled in settings.
8. Making Metrics and Fonts applies values to open windows in a separate thread as a trial to reduce hanging #209.

---

# Bugs fixes

1. Resolved exception error with ITaskbar3 on Windows XP and Vista.
2. Fixed exception errors occurring when applying cursors.
3. Fixed error when applying Windows 8.1 lock screen background and Windows 7 LogonUI background.
4. Fixed bug applying dark mode after opening WinPaletter Application Themer with light mode.
5. Fixed sync issue between WinPaletter and Wallpaper change preview and automatic dark/light mode.

---

> **Compare source code with:**

| Version Type    | Version Code | Link                                                                                |
|:---------------:|:------------:|:-----------------------------------------------------------------------------------:|
| Previous Stable | `1.0.8.4`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.4...v1.0.8.5) |
| Previous Beta   | `1.0.8.2`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.8.2...v1.0.8.5) |
| Initial Release | `1.0.0.0`    | [Compare](https://github.com/Abdelrhman-AK/WinPaletter/compare/v1.0.0.0...v1.0.8.5) |

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
