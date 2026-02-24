## How to Get WinPaletter

> [!WARNING]
> **Do not use unofficial builds.** Some "cracked" versions appear on GitHub. WinPaletter is completely **free** and does **not require a paid license**. With GitHub integration for uploading and managing themes directly in the app, always use the **official build** for your safety.

You can download and install WinPaletter from one of the following official sources:

---

### 1. [GitHub Releases](https://github.com/Abdelrhman-AK/WinPaletter/releases)

- This is the primary source and receives updates first.  
- Download the ZIP file for the latest version and extract it to your preferred location.

---

### 2. Microsoft WinGet (Windows 10 and later)

Run this command in **Command Prompt**:

```powershell
winget install Abdelrhman-AK.WinPaletter -l "UnzipPath"
```

> [!IMPORTANT]
> - You **must** use the `-l` or `--location` option because WinPaletter is a **portable app** (ZIP file), not a setup installer.  
> - Use quotes `"` if the path contains spaces.  
> - Example:  
> ```powershell
> winget install Abdelrhman-AK.WinPaletter -l "D:\My Apps\WinPaletter"
> ```  
> - If you have a previous installation, uninstall it first:  
> ```powershell
> winget uninstall Abdelrhman-AK.WinPaletter
> ```

---

### 3. Chocolatey

To install using Chocolatey, run:

```powershell
choco install WinPaletter
```
To install a **specific version** using Chocolatey, run:

```powershell
choco install WinPaletter --version <versionID>
```
> [!IMPORTANT]
> - Chocolatey uses a slightly different version format: if WinPaletter's version is `x.x.x.x`, use `x.x.xx` (drop the last digit).  
> - Example:  
> ```powershell
> choco install WinPaletter --version 1.0.95
> ```  
> - The first version available on Chocolatey is `1.0.81`. Version `1.0.82` is the only one in that range **not uploaded**.