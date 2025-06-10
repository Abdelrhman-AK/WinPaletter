# WinPaletter

# ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/GithubBannerIntro.jpg?raw=true)

![Github All Releases](https://img.shields.io/github/downloads/Abdelrhman-AK/WinPaletter/total?color=0078D4&style=for-the-badge) ![GitHub Release](https://img.shields.io/github/v/release/Abdelrhman-AK/WinPaletter?color=05227A&style=for-the-badge) [![GitHub stars](https://img.shields.io/github/stars/Abdelrhman-AK/WinPaletter?color=F4870A&style=for-the-badge)](https://github.com/Abdelrhman-AK/WinPaletter/stargazers) [![GitHub issues](https://img.shields.io/github/issues/Abdelrhman-AK/WinPaletter?color=FF0000&style=for-the-badge)](https://github.com/Abdelrhman-AK/WinPaletter/issues) [![GitHub forks](https://img.shields.io/github/forks/Abdelrhman-AK/WinPaletter?color=00AF00&style=for-the-badge)](https://github.com/Abdelrhman-AK/WinPaletter/network) [![License: MIT AND LGPL-2.1](https://img.shields.io/badge/License-MIT%20AND%20LGPL--2.1-FF0C4F?style=for-the-badge)](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/License.md)

## WinPaletter Returning in June 2025: Primary Development Objectives

### Project Scope

This return release concentrates on **cleaning up legacy issues** and providing a better-honed experience.
While significant new features are not anticipated, the goal is to place the project in a stable, workable condition.

| Area | Update Description | Progress |
|---------------------------------|-----------------------------------------------------------|---------------------|
| System Restore Point | Automatically create a restore point before applying any theme | ⌛ In progress |
| Bugs fixes | Fix reported and non reported bugs and issues | ⌛ In progress |
| Simplified UI Components | Streamlined user interface for enhanced workflow | ⌛ In progress |
| Multi-user Improvements | More stable and predictable behavior in multi-user scenarios | ⌛ In progress |
| Logging System | New "WinPaletter Log" for sophisticated user diagnostics | ⌛ In progress |
| Developer Support | Better in-code documentation and organized comments for developers | ⌛ In progress |
| WinPaletter setup | A setup dialog will appear on first launch, allowing you to configure WinPaletter preferences to best suit your needs | ⌛ In progress |

> [!IMPORTANT]
> Development during this comeback phase will be limited. The developer will be available only one or two days per week (or every two weeks), and not for the full day. As a result, progress will be slower compared to the more active development period prior to archiving.

> [!WARNING]
> Development may stop in **February 2026**, and the project will be archived once more. This end is not confirmed.

---

## WinPaletter 1.0.9.4 changelog (still in progress) (Spoiler)

<details>
  <summary>Toggle changelog</summary>
  
```
**New features:**  
- System restore points
- New aspect: Accessibility (High Contrast + Color Filters)

**User Profiles Switching:**  
- Switching to a user profile protected by a password now uses the Windows Security (Credentials) dialog for better security, instead of WinPaletter's own password prompt.  
- Opening WinPaletter will no longer show a list of users; it will directly target the user who launched WinPaletter (mimicking Windows 10/11 login behavior). If you want to switch to another user in the current session, do so from the main form. (This method slightly improves startup speed, but you likely won’t notice the difference.)  
- The option to continue without a password has been removed to avoid conflicts between two users (the target user and the user who opened WinPaletter).

**Other improvements:**  
- The .NET Framework dependency has been upgraded from version 4.7.2 to 4.8, which is now required (Not a problem to any user).
- The "What's New" form has been removed (the GitHub releases page is a better alternative).  
- Bug and crash report system redesigned, now includes the ability to save theme files.  
- Added "Plus! 95 For Kids" CD schemes for classic colors, metrics, and fonts.  
- Rescue tools renamed to SOS, with command line options: `WinPaletter -f` or `WinPaletter --SOS`.  
- SOS mode will automatically activate when WinPaletter is opened in safe mode.  
- When you download a theme from WinPaletter store, there is a check list of aspects will be edited. In this dialog, a button called "Proceed with all selected" is removed and replaced by two buttons "Check all" and "Uncheck all" to eliminate ambiguity.  
- Improved default Windows themes; now all aspects will be restored correctly.  
- Significant speed improvements in languages and the Language Editor.  
- Startup speed has been slightly optimized.  
- WinPaletter Store massive speed improvements.  
- The "Save as theme" feature now recommends a filename based on the theme name instead of an empty string.  
- Minor memory usage optimizations.

**Bugs fixes:**  
- Fixed store items not being downloaded and listed.
- Fixed an issue where pressing "Apply" in the Lighthouse at sunset theme store and then returning to the Windows previewer in the store caused an exception error.
- Fixed an issue where the store previewer did not show the wallpaper of a downloaded theme pack the first time in the current session, but displayed it correctly on subsequent attempts.
- Skipping listing the WsiAccount user as it is part of system users.
- Closing the theme applying process form when Explorer is already killed now correctly restores Explorer.
- The Windows Effects alert dialog is now brought to the top. Previously, it was hidden behind the applying form, causing confusion that the applying process was not working.
- Fixed the magnifier with high DPI settings not centering correctly on the cursor position in classic colors.
- Fixed an issue where applying Classic Colors reset the cursors.
- Fixed an issue where the taskbar and Start menu showed a clipped, blurred portion of a different wallpaper if the theme had a wallpaper different from the current one.
- Fixed icon label preview by correcting the shadow algorithm.
- Fixed exception errors when deleting store cache and logs in Settings > Storage Details.
```
</details>

---

#### WinPaletter is a portable tool designed to elevate your Windows desktop experience. Whether you're a designer, developer, or someone who loves personalization, WinPaletter offers an intuitive interface and robust features to streamline the management and application of colors and effects on your Windows system.

#### With WinPaletter, you can customize a wide range of Windows aspects, including Windows Colors, Visual Styles, Classic Colors, Lock screen (LogonUI), Cursors, Metrics and Fonts, Terminals and Consoles, wallpaper, sounds, screen savers, Windows effects (tweaks), and Windows icons according to your preferences.

## ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Mini-Icons/Features.png?raw=true) Key Features

- **Intuitive Interface:** WinPaletter boasts a user-friendly interface, making color palette management accessible to users of all levels of expertise.

- **Themes Import/Export:** Explore a world of creativity with the ability to import and export themes. Visit the WinPaletter Store to discover a diverse collection of themes shared by the community.

- **Real-time Preview:** Witness your color choices come to life with the real-time preview feature, allowing you to fine-tune your color scheme effortlessly.
  
![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Preview.png?raw=true)

## ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Mini-Icons/GettingStarted.png?raw=true) Getting Started

1. **Requirements:**

| **Windows Version** | **Supported WinPaletter Version** | **.NET Framework 4.8 Requirement** |
|---------------------|-----------------------------------|------------------------------------|
| **11**              | `Any version`                     | Pre-installed                      |
| **10**              | `Any version`                     | Might require [manual update](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48) if build is less than `1709` |
| **8.1**             | `1.0.5.0+`                        | Install from [Microsoft](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48) |
| **8**               | `1.0.9.5+`                        | Install via [custom method](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Install-.NET-Framework-4.8-on-Windows-8-Build-9200) |
| **7**               | `1.0.5.0+`                        | Install from [Microsoft](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48) |
| **Vista**           | `1.0.7.1+`                        | Requires repacked version — [see guide](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Getting-Windows-XP-and-Vista-ready-to-make-them-can-launch-WinPaletter#2-windows-vista) |
| **XP**              | `1.0.7.1+`                        | Requires OneCoreAPI + repacked version — [see guide](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Getting-Windows-XP-and-Vista-ready-to-make-them-can-launch-WinPaletter) |

3. **Download:** 
   
   - You can download the latest release from the [releases page](https://github.com/Abdelrhman-AK/WinPaletter/releases).
     > **Note:** It is the first source to be updated.
   
   - Alternatively, you can use:
     
     - Microsoft WinGet: 
       
       `winget install Abdelrhman-AK.WinPaletter -l "UnzipPath"`
     
     - Chocolatey:
       
        `choco install WinPaletter` or `choco install WinPaletter --version x.x.xx`
     
     - [Visit this](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Get-WinPaletter) for advanced instructions.

4. **Launch:** Once downloaded, launch WinPaletter and start exploring its features to enhance your desktop aesthetics.

## ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Mini-Icons/Help.png?raw=true) Wiki (Help)

[Click here](https://github.com/Abdelrhman-AK/WinPaletter/wiki) to learn more about WinPaletter.

## ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Mini-Icons/Help.png?raw=true) Changelog

[Click here](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/CHANGELOG.md) to view all changes that have been made to WinPaletter since its initial release.

## ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Mini-Icons/Languages.png?raw=true) Languages

[Click here](https://github.com/Abdelrhman-AK/WinPaletter/tree/master/Languages) to get languages for WinPaletter.

## ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Mini-Icons/Antivirus.png?raw=true) Do you have an antivirus or browser issue?

[Click here to read instructions](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Antiviruses-or-browsers-download-issue).

## ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Mini-Icons/Credits.png?raw=true) Credits

WinPaletter is developed and maintained by [Abdelrhman-AK](https://github.com/Abdelrhman-AK) and the incredible open-source community:

- Modifying Modern Windows Elements Inspired by u/aveyo and u/Egg-Tricky on Reddit: [Link 1](https://www.reddit.com/r/Windows11/comments/sw15u0/dark_theme_did_you_notice_the_ugly_pale_accent), [Link 2](https://www.reddit.com/r/Windows11/comments/tkvet4/pitch_black_themereg_now_for_ctrlaltdel_as_well)

- [Patching UxTheme.dll to apply unsigned Visual Styles by SecureUxTheme, developed by namazso](https://github.com/namazso/SecureUxTheme)

- [3D and flat degrees modification in 3D objects (Classic Colors) is inspired by Desktop Architect](https://en.wikipedia.org/wiki/Desktop_Architect)

- [Colors picking controls by Cyotek](https://github.com/cyotek/Cyotek.Windows.Forms.ColorPicker)

- [Image to palette conversion mechanism by ColorThief, developed by KSemenenko](https://github.com/KSemenenko/ColorThief)

- [Bitmap effects powered by ImageProcessor](https://imageprocessor.org)

- [Bitmaps to cursors conversion mechanism developed by Evan Olds](https://github.com/evanolds/AnimCur)

- [Retrieving elements of Windows XP visual styles (*.msstyles) using the Advanced UxTheme wrapper](https://www.codeproject.com/Articles/18603/Advanced-UxTheme-wrapper)
  
- [Extracting elements from visual styles (*.msstyles) using nptr/msstyleEditor](https://github.com/nptr/msstyleEditor)

- [Patching PE files by Ressy, developed by Tyrrrz](https://github.com/Tyrrrz/Ressy)

- [Handling JSON files using Newtonsoft JSON by James Newton-King](https://github.com/JamesNK/Newtonsoft.Json)

- [Processing and handling command lines arguments by CommandLineParser](https://github.com/commandlineparser/commandline)

- [Icons designed by Pichon](https://icons8.com/app/windows)

- [Animation and transition effects for controls by FluentTransitions, developed by Andreas Wäscher](https://github.com/awaescher/FluentTransitions)

- [Animation for Controls by Pavel Torgashov](https://www.codeproject.com/Articles/548769/Animator-for-WinForms)

- [Modern dialogs design (messages boxes) by Ookii.Dialogs.WinForms](https://github.com/ookii-dialogs/ookii-dialogs-winforms)
  
- [Using JetBrainsMono as a monospaced font for WinPaletter](https://github.com/JetBrains/JetBrainsMono)

- [These items are provided by Microsoft: Classic color schemes, Luna theme preview (Luna.msstyles) and Command Prompt and PowerShell raster fonts previews](https://www.microsoft.com)

## ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Mini-Icons/License.png?raw=true) License

WinPaletter is licensed under the [MIT/LGPL Dual License](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/License.md). Feel free to use, modify, and distribute it in accordance with the terms of the license.
