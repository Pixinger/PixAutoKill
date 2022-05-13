# PixAutoKill
A small commandline tool to kill processes that exceed a certain amount of memory usage.

# Language
C# / .NET6 / Visual Studio 2022

# Installation
Just extract the 7z and run the exe from the commandline (see sample below). The console programm will only check ONE TIME and terminate after the check is completed. For continous checks, you need to execute this program by some kind of scheduler.
* Return-Code = 0: No critical processes found and terminated
* Return-Code > 0: Number of terminated processes.
* Return-Code < 0: Uppsi... Check the console output. Something went wrorong.

# Sample
<code>PixAutoKill.exe /name:Notepad /maxmem:4000000 /timeout:5000 /retry:3 /log</code> 

* <code>name</code>: Name of the process(es) that will be verfied agains the memory limit (name without .exe extension).
* <code>maxmem</code>: Maximum allowed number of bytes before process will be terminated.
* <code>timeout</code>: Time (ms) after termination request until process is checked if termination was successfull - or not (default 5000).
* <code>retry</code>: number of retry attemts to terminate the process, before giving up (default 3).
* <code>log</code>: Activate a simple logger that will write to a file called "PixAutoKill.log".

# Tips
"Cammel" provided a simple script, to automatically poll every 60 seconds. Thank you!

```
@echo off
:start
Start "" /min "C:\Program Files (x86)\pix_autokill\PixAutoKill.exe" /name:Notepad /maxmem:12884901888 /retry:3 /timeout:5000
timeout /T 60 /nobreak >nul
goto :start 
```
