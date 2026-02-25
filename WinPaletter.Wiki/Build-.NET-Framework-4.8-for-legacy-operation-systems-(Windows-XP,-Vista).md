1. Visit [abbodi1406/dotNetFx4xW7](https://github.com/abbodi1406/dotNetFx4xW7) repository on GitHub (Archived version: https://web.archive.org/web/20250618155158/https://github.com/abbodi1406/dotNetFx4xW7)

2. Click on the green button `< > Code` and click on `download ZIP`.

3. Download official [.Net Framework 4.8 from Microsoft](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48).

4. Extract the ZIP file you downloaded in step `2`.

5. Move the .Net Framework installer into the main extracted folder, it will be like that:
   
    ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Asset/dotNet/0.png?raw=true)

6. Run `dotNetFx.cmd` as administrator and wait until it finishes processing the installer.

7. Run `7zSFX.cmd` as administrator to recreate the modified installer.

8. You will find the new file like that: 
   
    ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Asset/dotNet/1.png?raw=true)

9. Copy the new file to your real\virtual machine and setup it

10. Restart the target Windows (on which, WinPaletter will work)

11. In Windows Vista, when you run WinPaletter you might face a `KERNEL32.DLL` error. If so, download a modified `CLR.dll` from either `Framework` or `Framework64` folder from [this link](https://github.com/Abdelrhman-AK/WinPaletter/tree/master/References/NETFX48Fix) and then copy `CLR.dll` to `%windir%\Microsoft.NET\Framework\v4.0.30319\Framework` or `%windir%\Microsoft.NET\Framework\v4.0.30319\Framework64` and replace. If you found both `Framework` and `Framework64`, then you will download two `CLR.dll`s and move them to their corresponding folders, and finally restart the target Windows.
