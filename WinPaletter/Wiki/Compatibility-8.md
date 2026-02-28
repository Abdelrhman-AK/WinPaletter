# Running WinPaletter on Windows 8

## Steps to Install .NET Framework 4.8 on Windows 8

### 1. **Download Pre-Requisites**  
   - Get the [abbodi1406/dotNetFx48-W8](https://gitlab.com/stdout12/adns/uploads/c7c9f583da309adfb5f7a621ff3cf218/W8_NetFx4_IE11_Prereqs.7z) archive from GitLab.  
   - For advanced users, you can read the discussion on [MyDigitalLife](https://forums.mydigitallife.net/threads/abbodi1406s-batch-scripts-repo.74197/page-115#post-1775602).

### 2. **Extract and Install Pre-Requisites**  
   - Extract the downloaded archive.  
   - Run `installer.cmd` **as Administrator**.

### 3. **Download the Official .NET Framework 4.8 Installer**  
   - Get it from Microsoft: [Download .NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48).

### 4. **Extract the Installer**  
   - Open an **elevated Command Prompt** (Run as Administrator).  
   - Navigate to the folder containing `ndp48-x86-x64-allos-enu.exe`.  
   - Execute the command:

   ```cmd
   ndp48-x86-x64-allos-enu.exe /extract
   ```

### 5. Install the Windows Updates Required for .NET Framework 4.8
- For Windows 8 x64:
```
DISM /Online /Add-Package /PackagePath:Windows8-RT-KB4019990-x64.cab
DISM /Online /Add-Package /PackagePath:x64-Windows8-RT-KB4486081-x64.cab
```
> [!IMPORTANT]
> Restart your PC after running the above commands.

- For Windows 8 x86:
```
DISM /Online /Add-Package /PackagePath:Windows8-RT-KB4019990-x86.cab
DISM /Online /Add-Package /PackagePath:x86-Windows8-RT-KB4486081-x86.cab
```
> [!IMPORTANT]
> Restart your PC after running the above commands.

### 6. Launch WinPaletter
After completing the steps above, run WinPaletter.

> [!Important]
> WinPaletter versions below 1.0.9.5 can run on Windows 8, but Windows Colors and Start Menu styles may not apply correctly, as these versions were not originally designed for Windows 8.