# Running WinPaletter on Windows Vista

> [!Important]
> Modern builds of WinPaletter rely on Windows APIs not present in the original Vista kernel.  
> To run WinPaletter successfully, you must install the **Windows Vista Extended Kernel**.

## Installing Windows Vista Extended Kernel

The Extended Kernel backports newer Windows 7 APIs to Windows Vista, allowing modern applications like WinPaletter to launch correctly.

### Steps:

1. Ensure your system meets the following requirements:  
   - Windows Vista **Service Pack 2**  
   - Fully updated Platform Update (recommended but not mandatory)

2. Download the latest [**Vista Extended Kernel**](https://win32subsystem.live/extended-kernel/download/) package from its official project repository.  
3. Read the project documentation carefully before installing.  
4. Run the installer included in the Extended Kernel package.  
5. Restart your system after installation.

> [!Warning]
> It is strongly recommended to create a **full system backup or snapshot** before installing the Extended Kernel, as it modifies core system components.

## Prerequisites:

1. Install **.NET Framework 4.0** and restart your system.  
2. Follow the documentation to install **.NET Framework 4.8** for legacy operating systems:  
   [Build .NET Framework 4.8 for legacy operating systems (Windows XP, Vista)](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Build-.NET-Framework-4.8-for-legacy-operation-systems-(Windows-XP,-Vista))

After installing the Extended Kernel and .NET Framework 4.8, WinPaletter should launch normally on Windows Vista.