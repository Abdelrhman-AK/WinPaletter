1. Download [abbodi1406/dotNetFx48-W8](https://gitlab.com/stdout12/adns/uploads/c7c9f583da309adfb5f7a621ff3cf218/W8_NetFx4_IE11_Prereqs.7z). This file is hosted on GitLab. You can read the full discussion on [MyDigitalLife](https://forums.mydigitallife.net/threads/abbodi1406s-batch-scripts-repo.74197/page-115#post-1775602) (for advanced users).

2. Extract the downloaded archive, then run `installer.cmd` as Administrator.

3. Download the official [.NET Framework 4.8 installer from Microsoft](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48).

4. Open an elevated Command Prompt (i.e., run as Administrator).

5. Navigate to the directory containing `ndp48-x86-x64-allos-enu.exe` and execute the following command:

```
ndp48-x86-x64-allos-enu.exe /extract
```

6. A. If you are using **Windows 8 x64**, run the following commands and restart your PC afterward:

```
DISM /Online /Add-Package /PackagePath:Windows8-RT-KB4019990-x64.cab
DISM /Online /Add-Package /PackagePath:x64-Windows8-RT-KB4486081-x64.cab
```

6. B. If you are using **Windows 8 x86**, run the following commands and restart your PC afterward:

```
DISM /Online /Add-Package /PackagePath:Windows8-RT-KB4019990-x86.cab
DISM /Online /Add-Package /PackagePath:x86-Windows8-RT-KB4486081-x86.cab
```


7. Finally, launch **WinPaletter**.

> [!IMPORTANT]  
> WinPaletter versions below `1.0.9.5` can run on Windows 8, but the Windows Colors and Start Menu styles may not be applied correctly, as these versions did not originally support Windows 8.

