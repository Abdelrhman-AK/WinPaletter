# 1. Windows XP

1. Download the latest release of [Skulltrail192/One-Core-API-Binaries](https://github.com/Skulltrail192/One-Core-API-Binaries).

2. Install it on the target real/virtual machine.

3. Restart Windows XP.

4. Follow the documentation [here](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Build-.NET-Framework-4.8-for-legacy-operation-systems-(Windows-XP,-Vista)) to install .NET Framework 4.8 on Windows XP.

5. Restart Windows XP.

6. Read the important notes below.

7. Try to launch WinPaletter now.

> [!Important]
> 1. WinPaletter is not stable with Windows XP at all; try it first on a virtual machine.
> 2. You might face unexplainable crashes on starting, during using, or during closing WinPaletter.
> 3. If the classic theme is enabled and you open WinPaletter, the preview won't work for other themes due to some limitations in Visual Styles Previewer. Apply another theme first, then reopen WinPaletter.

---

# 2. Windows Vista

First, install **.NET Framework 4.0**, then restart your system.  
After that, follow the documentation here to install **.NET Framework 4.8 for legacy operating systems (Windows XP, Vista)**:

[Build .NET Framework 4.8 for legacy operating systems (Windows XP, Vista)](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Build-.NET-Framework-4.8-for-legacy-operation-systems-(Windows-XP,-Vista))

> [!Important]
> Modern builds of WinPaletter rely on Windows APIs that do not exist in the original Vista kernel.  
> To run WinPaletter successfully, you must install the **Windows Vista Extended Kernel**.

## Installing Windows Vista Extended Kernel

The Extended Kernel project backports newer Windows 7 APIs to Windows Vista, allowing modern applications (including WinPaletter) to launch correctly.

### Steps:

1. Make sure you are running:
   - Windows Vista **Service Pack 2**
   - Fully updated Platform Update (recommended but not mandatory)

2. Download the latest **Vista Extended Kernel** package from its official project repository.

3. Carefully read the project’s documentation before installation.

4. Run the installer included in the Extended Kernel package.

5. Restart your system after installation completes.

> [!Warning]
> It is strongly recommended to create a full system backup or snapshot before installing the Extended Kernel, as it modifies core system components.

After installing the Extended Kernel and .NET Framework 4.8, WinPaletter should launch normally on Windows Vista.
