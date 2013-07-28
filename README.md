Munchies
========

A re-creation of Michael Fan's 1996-2001 game, Munchies [(Official Website)](mikefan.com/munchies/)

Munchies was originally created by Michael Fan in 1996 for Apple Macintosh OS 7. Its final version 1.0.7, was released in 2001.

This re-creation, made in 2012 by me, Andrew Scott, was my first foray into "real" programming after working only with scripting languages for 3+ years. It is coded entirely in C# targeting Microsoft's .NET 4.0 framework, and uses all of the images and sounds from the original game.

The motivation behind this recreation came from my search for a productive project to complete over winter break. The favorite game from the childhood of my siblings and I was the perfect place to begin.

Mr. Fan has graciously given permission for me to make the source of this re-creation, including the resources, available. Under no circumstances may this work be used for commercial purposes or otherwise for profit.

Release
=======

The latest release of this software may be found at https://github.com/ascott18/munchies/releases.

Development
===========

The main development environment for this project is Visual Studio 2012. The installer is created using [InstallShield LE for Visual Studio](http://learn.flexerasoftware.com/content/IS-EVAL-InstallShield-Limited-Edition-Visual-Studio)


Build
-----

Building should be done from within Visual Studio. The installer will fail to build unless the build configuration is set to `Release` or `Single Image`. Once built, the installer may be found at `\Installer\Installer\Express\SingleImage\DiskImages\Disk1\MunchiesSetup.exe`. Munchies.exe and requisite DLLs will be found in `\Munchies\bin\*\`



Libraries
---------

In addition to two small libraries of my own creation (SimpleCommandManager and SettingsSerializer), Munchies uses [irrKlang by Ambiera](http://www.ambiera.com/irrklang/license.html).