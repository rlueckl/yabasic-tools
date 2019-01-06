# yabasic-tools

Please see my blogpost (here) on more information about this repo.

# Usage

### Prerequisites

For playtesting:
  - PS2 Demo Disc with YaBasic, for example: *PBPX-95205*
  - A good ol' PlayStation 2 or PS2 emulator (PCSX2 for example)

For the tools:
  - Jacksum and mymc (found in this repo)
  - Java installed and in your $PATH (for Jacksum)
  - Optionally (the Batch file should take care of this): PowerShell with RemoteSigned execution policy enabled ([how-to](http://technet.microsoft.com/en-us/library/bb613481.aspx) or in short):
    - Open a PowerShell as Administrator
    - `Set-ExecutionPolicy RemoteSigned`
    - Choose "A" (Yes to All)

### How-To

  - Check out this repository
  - Change the `BASEDIR` in the **crc_and_memcard.bat** to your work directory
  - Change the `MEMDIR` to the directory with your PCSX2 memory card file
    - Alternatively you can use **crc_only.bat** if you want to copy the source to a real memory card and play on your PlayStation 2)
  - I've included a memory card file (**Mcd001.ps2**) with a sample source code (**BESCES-50008SAMPLE**) so you know how the structure needs to look like (icon.sys, icon.ico and the code with checksum)
  - Put your source code in the "Source" directory with *.vb* (*) as extension
  - Run the Batch file

*: I'm using *.vb* (Visual Basic) as an extension, because that's the closest thing to good syntax highlighting in Notepad++ as you can get with Basic. Feel free to make you own YaBasic syntax highlighting with UDL and share the plugin!

# Tech

  - [Jacksum](https://sourceforge.net/projects/jacksum/): calculate CRC32_BZIP2
  - [mymc](http://www.csclub.uwaterloo.ca:11068/mymc/): manipulate PCSX2 memory cards
  - PowerShell and Batch code by me
  - Game source code by me

# Development

Want to contribute? Great! Just create a merge request.
Having a problem? Open an issue and I'll try to help.

# License

MIT


**Free Software, Hell Yeah!**
