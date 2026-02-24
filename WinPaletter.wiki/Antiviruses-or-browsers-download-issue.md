## 1. Reason of these issues

   1. The program is not signed (as I can't afford to buy annual certificates to make WinPaletter's executable file not from unknown publisher)

   2. WinPaletter is editing Windows Registry in an extensive way

- So the browser, Windows and antivirus will tell you that the program is from unknown source\organization and may be harmful

- And this is a false positive in browsers and antiviruses

***

## 2. Scan attempts

| Tested antiviruses             | Result                   |
| ------------------------------ | ------------------------ |
| Avast, Avast One               | :white_check_mark: safe  |
| Kaspersky                      | :white_check_mark: safe  |
| Malwarebytes                   | :white_check_mark: safe  |
| SmadAV                         | :white_check_mark: safe  |
| BitDefender                    | :white_check_mark: safe  |
| Windows Security (Defender)    | :white_check_mark: safe  |
|  [VirusTotal scan](https://www.virustotal.com/gui/file/c300f8055165b6bfc74883f2a3ca155562ef6e17e71ec45ab77029fcab9e64bf?nocache=1) | `2` detections from total `71` antiviruses |

- Your Antivirus should be updated to last definition updates

- Sometimes, you may face that the program is infected with `IDP.Generic`, `SuspiciusBehavior` or something else. If so, open your Antivirus `>` Updates `>` Update Definitions and then reopen WinPaletter after restoring it from quarantine

- If it still infected, create a new issue with descriptions in it and I will send this app for antivirus (that has the problem) developers for a deep analysis

***

## 3. Causes of Windows Security (Defender) detection:

1. The program is not signed

2. Extensive registry keys modification. Windows Security commented on this key modification although WinPaletter doesn't modify it:
  
  `HKCU@S-1-5-21-957280099-1924274324-3775045331-1001\SOFTWARE\MICROSOFT\WINDOWS\CURRENTVERSION\UNINSTALL\WinPaletter`
  
3. Patching `imageres.dll` to change startup sound is considered as a malware action for Windows Security

4. Creating tasks in Task Scheduler by Command Prompt (unofficial method), to create sounds events for logoff, logon and shutdown for Windows 8.1 and higher

- Microsoft deep analysis found it clean. [Click here for scan details](https://www.microsoft.com/en-us/wdsi/submission/90ed38b7-6df9-44fa-aa9f-387b2eb1b7c0)

- You may need to do extra steps to remove this suspicion:

   1. Run Command Prompt as administrator and enter the following lines

   ```
   CD C:\Program Files\Windows Defender
   MpCmdRun.exe -removedefinitions -dynamicsignatures
   MpCmdRun.exe -SignatureUpdate
   ```

   2. Update your Windows from Settings

***

## 4. Browsers download issue

- Some browsers like Edge and Firefox will prompt you that the program may harm the computer. It is a false positive and you can download it anyway.
