## How to Get WinPaletter

You can download and install WinPaletter from one of the following sources:

---

### 1. [GitHub Releases](https://github.com/Abdelrhman-AK/WinPaletter/releases)

- This is the main source and gets updates first.

---

### 2. Microsoft WinGet (Windows 10 and later)

- Run this command in **Command Prompt**:

```
winget install Abdelrhman-AK.WinPaletter -l "UnzipPath"
```

> [!Important]
> - You must use the `-l` or `--location` option because the download is a ZIP file, not a setup installer (WinPaletter is a portable app).
> - Use quotes `"` if the path contains spaces.
> - Example:
>  ```
>  winget install Abdelrhman-AK.WinPaletter -l "D:\My Apps\WinPaletter"
>  ```
> - If you’ve installed WinPaletter before, uninstall it first using:
> ```
> winget uninstall Abdelrhman-AK.WinPaletter
> ```

---

### 3. Chocolatey

- To install using Chocolatey, run:

```
choco install WinPaletter
```

- To install a specific version, use:

```
choco install WinPaletter --version <versionID>
```

> [!Important]
> Chocolatey uses a different version format. If WinPaletter's version is `x.x.x.x`, use `x.x.xx` for Chocolatey (drop the last dot and digit).
> - Example:
>  ```
>  choco install WinPaletter --version 1.0.95
>  ```
> - The first version available on Chocolatey is `1.0.81` and newer. Version `1.0.82` is the only one in that range that was not uploaded.