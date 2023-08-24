# Getting Windows XP ready to make it can launch WinPaletter

1. Download latest release of [Skulltrail192/One-Core-API](https://github.com/Skulltrail192/One-Core-Api).

2. Install it in the target real\virtual machine.

3. Restart Windows XP.

4. Follow the documentation here to [install .Net Framework 4.8 on Windows XP](https://github.com/Abdelrhman-AK/WinPaletter/tree/master/Documentations/LegacyOS/dotNet.md)

5. Restart Windows XP.

6. Read the important notes below.

7. Try to launch WinPaletter now.

> Notes:
>
1. WinPaletter is not stable with Windows XP at all, try it first on a virtual machine.
2. You might face unexplainable crashes on starting, during using, or during closing WinPaletter.
3. WinPaletter might not start correctly from the first time, always try again until it opens successfully.
4. Sometime, for unknown reason, WinPaletter remains open even if you close it. You should always check in `Task Manager` > `Processes` if WinPaletter is running or not.
5. If classic theme is enabled and you open WinPaletter, the preview won't work for other themes due to some limitations in Visual Styles Previewer. Apply another theme first then reopen WinPaletter.
6. External Theme\Visual Styles require a UX-Theme-Patched Windows

> Known issue\s:
1. Updates by WinPaletter itself won't work as TLS 1.2 protocol isn't enabled in Windows XP

---

# Getting Windows Vista ready to run WinPaletter

You won't do too much steps like Windows XP, you will install [.net framework 4.0](https://www.microsoft.com/en-us/download/details.aspx?id=17718) first and restart your Windows then follow the documentation here to [install .Net Framework 4.8 on Windows Vista](https://github.com/Abdelrhman-AK/WinPaletter/tree/master/Documentations/LegacyOS/dotNet.md)

> Important note for Windows Vista:
> 
When you run WinPaletter you might face a `KERNEL32.DLL` error. If so, download a modified `CLR.dll` from either `Framework` or `Framework64` folder from [this link](https://github.com/Abdelrhman-AK/WinPaletter/tree/master/References/NETFX48Fix) and then copy `CLR.dll` to `%windir%\Microsoft.NET\Framework\v4.0.30319\Framework` or `%windir%\Microsoft.NET\Framework\v4.0.30319\Framework64` and replace. If you found both `Framework` and `Framework64`, then you will download two `CLR.dll`s and move them to their corresponding folders, and finally restart the target Windows.

> Known issue\s:
1. Updates by WinPaletter itself won't work as TLS 1.2 protocol isn't enabled in Windows Vista
