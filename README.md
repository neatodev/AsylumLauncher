# Batman: Arkham Asylum - Advanced Launcher

This is a replacement application for the original BmLauncher.exe of the game. Alongside vastly superior configuration options, this Launcher also offers:

- Tooltips for every configuration option
- Option to toggle Startup Movies
- Very high customizability
- Color settings (Including color palette presets)
- Two pre-made color profiles for HDR injection
- Customizable Field of View Hotkeys
- Compatibility Fixes for Texture Packs such as [Asylum Reborn](https://www.nexusmods.com/batmanarkhamasylum/mods/1) and [others](https://www.nexusmods.com/batmanarkhamasylum/mods/177)!
- NVIDIA API Implementation. Toggle HBAO+ directly in the Launcher! (Powered by [NvAPIWrapper](https://github.com/falahati/NvAPIWrapper))
- Extensive Logging Functionality (Powered by [NLog](https://github.com/NLog/NLog))
- ... and more!

Supports the GOTY version on STEAM, GOG and EPIC GAMES.

**This Application is built with .NET 6**. If you are using Windows 10 and above you shouldn't have any issues simply running the program. Some users might need to install [.NET 6 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) manually.

A standalone, dependency free executable is also available.

## Preview

![AsylumLauncher Preview](https://user-images.githubusercontent.com/49599979/201522680-351ff4fb-92b9-4ce5-8193-f30a68c36d06.png)

## Download

**Download: [Current Release](https://github.com/neatodev/AsylumLauncher/releases/latest)** or **[visit Nexusmods](https://www.nexusmods.com/batmanarkhamasylum/mods/117)**

If you like this application, please consider donating.

[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/donate/?hosted_button_id=LG7YTKP4JYN5S)

## Installation

Drag the contents of the .zip file into the 'Batman Arkham Asylum GOTY\Binaries' folder.

To find this folder for the ***Steam*** version, just right-click the game in Steam, select Properties->Local Files->Browse Local Files and navigate from there.

To find it for the ***EGS*** version, right-click the game and select "Manage". Then click the folder icon in the "Installation" tab and navigate from there.

For the ***GOG*** version, click the icon next to the PLAY button and select "Manage installation->Show folder" and navigate from there.

## Usage

You can just launch your game via Steam, GOG or EGS as you normally would, though in some cases you might need to unblock the BmLauncher application for it to work properly.

Once you're happy with your settings, **you can skip the launcher entirely by using the `-nolauncher` launch option.**

- On ***EGS*** you can do this by going into Settings->Arkham Asylum->Additional Command Line Arguments. 
- On ***GOG GALAXY*** you have to select Customize->Manage Installation->Configure, enable Launch parameters, select Duplicate. It should be added under Additional executables.
- If you use a shortcut in Windows, right click->Properties->Shortcut and add it at the end of Target.
- On ***Steam***, right click the game's entry->Properties->Launch Options


## Info for Linux users

Install the **Calibri** and **Impact** fonts for your Wine environment so you don't encounter any display issues.

`winetricks -q calibri impact`


## Bug Reports

To file a bug report, or if you have suggestions for the Launcher in general, please file an [issue](https://github.com/neatodev/AsylumLauncher/issues/new). I read these regularly and should normally be able to respond within a reasonable amount of time. Please also include the most recent asylumlauncher_report in the issue (if available). You can find the reports in the 'Batman Arkham Asylum GOTY\Binaries\logs' folder.

##### License: [![CC BY 4.0][cc-by-shield]][cc-by]

[cc-by]: https://creativecommons.org/licenses/by-nc-sa/4.0/
[cc-by-shield]: https://licensebuttons.net/l/by-nc-sa/4.0/80x15.png
