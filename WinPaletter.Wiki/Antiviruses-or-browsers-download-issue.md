## WinPaletter: False Positives, Security Warnings, and Unsigned Executable

> [!warning]
> 1. Please read carefully: WinPaletter is developed by a **single developer**. The application is **not digitally signed** due to the high financial cost of annual certificates. All warnings about unknown publishers are expected behavior and **do not indicate malware**.
>
> 2. **Do not use unofficial builds.** Some "cracked" versions appear on GitHub. WinPaletter is completely **free** and does **not require a paid license**. With GitHub integration for uploading and managing themes directly in the app, always use the **official build** for your safety.

---

## 1. Reason for Warnings

1. **Unsigned Executable**  
   WinPaletter’s `.exe` file is not digitally signed. Signing certificates are expensive and require annual renewal. As a single developer maintaining this project, purchasing these certificates is not feasible.  

2. **Extensive Windows Registry Editing**  
   WinPaletter modifies a large number of registry keys to customize Windows themes, colors, fonts, cursors, and startup sounds. This is a legitimate behavior for a theming tool but can trigger warnings in antiviruses, Windows SmartScreen, and browsers.

3. **Patching `imageres.dll`** – Changing Windows startup sound involves patching system files. This action is **flagged by security software** as potentially harmful, even though it is safe.

4. **WinPaletter System Events Sounds Service** – WinPaletter installs a lightweight **service** to handle Windows system event sounds (logon, logoff, shutdown, etc.) for Windows 8 and higher.  

   - This service is fully optional and only created if you installed it manually in WinPaletter.  
   - It operates in the background and triggers the assigned sounds when the corresponding system events occur.  
   - Windows Security (Defender) may flag this as suspicious because it interacts with system events.  
   - No malicious activity occurs; the service simply plays the sounds you configured in WinPaletter.  

- Because of the above, browsers and antivirus programs often display warnings such as:
  - “Unknown Publisher”
  - “This file may harm your computer”
  - “Windows SmartScreen prevented an unrecognized app from starting”

- These detections are **false positives**.  

> [!note]
> Every change WinPaletter makes to the registry is **fully logged**. You can review all actions in the **log window** while applying themes and in the **status bar** of the application.  
> WinPaletter is **open-source**, so developers or advanced users can inspect the code and verify exactly what it does to the system.

## 2. Antivirus Scan Results

WinPaletter has been tested across several popular antiviruses:

| Antivirus / Security Software         | Result                   |
| ------------------------------------ | ------------------------ |
| Avast, Avast One                      | ✅ Safe                  |
| Kaspersky                             | ✅ Safe                  |
| Malwarebytes                          | ✅ Safe                  |
| SmadAV                                 | ✅ Safe                  |
| BitDefender                           | ✅ Safe                  |
| Windows Security (Defender)           | ✅ Safe                  |
| [VirusTotal scan](https://www.virustotal.com/gui/file/a903562206d556144aad9481831a66de0f2619e7b6144434aff563f868bbd8cc/detection) | 2 detections / 72 AV engines |

> [!note]
> - Ensure your antivirus definitions are up to date before running WinPaletter.  
> - Some detections may appear as `IDP.Generic`, `SuspiciousBehavior`, or similar names.  
> - If warnings persist after updating definitions, restore WinPaletter from quarantine, then report the detection. The developer can contact antivirus vendors for deeper analysis.

---

## 3. Common Causes of Windows Security (Defender) Alerts

> [!NOTE]
> This was a common issue on **Windows 10** around 2 years ago and has been largely mitigated in recent updates.  
> If you still encounter warnings for WinPaletter, follow the usual steps for allowing apps from unknown publishers or consult your antivirus settings.
 
1. **Unsigned Executable** – Windows Security treats any unsigned program as potentially untrusted.  

2. **Registry Key Modifications** – WinPaletter writes to extensive registry paths. Defender may flag keys such as:
```
HKCU@S-1-5-21-957280099-1924274324-3775045331-1001\SOFTWARE\MICROSOFT\WINDOWS\CURRENTVERSION\UNINSTALL\WinPaletter
```

> [!tip]
> If Defender continues flagging WinPaletter, you can refresh its definitions and signatures:

```cmd
CD C:\Program Files\Windows Defender
MpCmdRun.exe -removedefinitions -dynamicsignatures
MpCmdRun.exe -SignatureUpdate
```
- After updating, restart your system and run WinPaletter again.
- Ensure your Windows updates are current to reduce false positives.

## 4. Browser Download Warnings

- Browsers like Googel Chrome, Microsoft Edge or Mozilla Firefox may warn that WinPaletter “may harm your computer.”
- This is a false positive triggered by:
1. The program being unsigned
2. Its registry and system modifications

> [!tip]
> You can safely proceed with the download of WinPaletter. The warnings are **false positives** due to the program being unsigned and modifying Windows settings.  
> 
> For the safest approach:
> 1. Always download from the official repository.
> 2. Scan the file with your antivirus before opening.  
> 3. Only run WinPaletter from trusted locations (like your personal Downloads folder or a non-shared folder).  
>
> Avoid permanently disabling Windows SmartScreen or antivirus protection. If SmartScreen blocks the app temporarily, you can use the built-in “More info → Run anyway” option to proceed safely.