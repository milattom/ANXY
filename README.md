![A very dark drawing of a small room. In the middle stands a barely visible person holding a flashlight. The Flashlight is the only source of light, but still nothing really is visible](./Content/Images/anxious.jpg "anxY, Person in the Shadow"){: style="width:100%"}

# anxY
anxY is a nerve wrecking Nightmare Jump & Run Game developed by mayfly studios with our own GameEngine using MonoGame as a helping tool. The Game was developed in the scope of a Bachelor Degree during one Semester.

# Table of Contents
- [Game Installation](#Installation)
    - [Supported Platforms](#Installation-SupportedPlatforms)
    - [Prerequisites](#Installation-Prerequisites)
    - [Download](#Installation-Download)
    - [Play](#Installation-Play)
    - [Stop](#Installation-Stop)
- [How To Build](#HowToBuild)
    - [Prerequisites](#HowToBuild-Prerequisites)
    - [Build](#HowToBuild-Build)
- [Make it your own Game](#DIY-Game)
    - [Map](#DIY-Game-Map)
- [About](#About)
    - [Idea](#About-Idea)
    - [Class Diagram](#About-ClassDiagram)
    - [Pattern](#About-Pattern)
    - [Tests](#About-Tests)
    - [Tools Used](#About-Tools-Used)
    - [Builds](#About-Builds)
- [Contributors](#Contributors)

---

# <a id="Installation"></a>Game Installation

## <a id="Installation-SupportedPlatforms"></a>Supported Platforms
For now, only Windows and MacOS are supported.

## <a id="Installation-Prerequisites"></a>Prerequisites

### .NET
If you don't have .NET installed, install it from [here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

## <a id="Installation-Download"></a>Download
Download and unpack the latest zip for your operating system (Windows or Mac) from the [GitHub Releases Page](https://github.com/milattom/ANXY/releases).

## <a id="Installation-Play"></a>Play
On Windows simply start the exe file from the unpacked zip.

On Mac TODO.

### Mac Security Warning
TODO.

## <a id="Installation-Stop"></a>Stop
There is no menu yet, so simply *ALT+F4* or press the *Window Closing Button*.

---

# <a id="HowToBuild"></a>How To Build

## <a id="HowToBuild-Prerequisites"></a>Prerequisites

### Visual Studio (2022)
We recommend [Visual Studio](https://visualstudio.microsoft.com/) but you can try a different IDE. Be aware that if you use a differnt IDE, you might need to figure out on your own how to set everything up to be compatible with anxY.

### Git Client
Clone the repo or download the zip from GitHub and unpack it.
We recommend [Git Fork](https://git-fork.com/).

### dotnet
Should come with Visual Studio, if not download and install [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0).

### MonoGame
[Full MonoGame Installation Instructions](https://docs.monogame.net/articles/getting_started/1_setting_up_your_development_environment_windows.html).

### MonoGame Content Pipeline
In the terminal write `dotnet tool install -g dotnet-mgcb` and then `dotnet tool restore`. You can check it works with running `dotnet mgcb-editor`.

[Full Content Pipeline Installation Instructions](https://docs.monogame.net/articles/tools/mgcb_editor.html).

### MonoGame Extended Content Pipeline
In Visual Studio, in the Solution Explorer (*View/Solution Explorer*) right click on the Solution and choose *Manage NuGet Packages for Solution*.

![Screenshot describing where to click to Manage NuGet Packages](./Content/Images/SolutionExplorer-NuGetPackages.jpg "Manage NuGet Packages for Solution")

On the top right of the NuGet Package Manager click on the Settings (*gear icon*) and add the Package Source for MyGet.org `https://www.myget.org/F/lithiumtoast/api/v3/index.json`. Press *OK* to save the changes.

![Screenshot describing how to add MyGet.org as a Package Source](./Content/Images/NuGet-ManagePackages.jpg "Add MyGet as Package Source")

Again in the Package Manager, press *Browse* and search for `newtonsoft.json` and install the latest stabel version for our project (including Tests). Make sure that the Package source is *nuget.org*.

![Screenshot describing how to install NuGet Packages](./Content/Images/NuGet-InstallPackages.jpg "Install Newtonsoft.Json Package")

Now change the Package source to *MyGet.org* and check the *Include prerelease* Checkbox and search for the following Packages and install them whenever possible in the newest *alpha version*.

- `MonoGame.Content.Builder.Task`
- `MonoGame.Extended`
- `MonoGame.Extended.Contetn.Pipeline`
- `MonoGame.Extended.Tiled`
- `MonoGame.Framework.DesktopGL`

![Screenshot describing which MyGet Packages to install](./Content/Images/MyGet-InstallPackages.jpg "Add all these Packages from MyGet.org, whenever possible newest Alpha Version")

#### <a id="Change-MonoGame-Content-Manager-Settings"></a>Change MonoGame Content Manager Settings
In the Solution Explorer right click the *Content.mgcb* in the folder *Content* and choose *open with*.

![Screenshot describing where to find Content.mgcb](./Content/Images/MonoGame-ContentManager-OpenWith.jpg "Open With...")

If you have can find the option to open it with *MGCB Editor*, perfect! Choose that and *Set as Default*, you can skip the next few steps and continue [here](#jumpToContent.mgcb). If you can't find the *MGCB Editor*, press *Add...*

![Screenshot showing the Open With Dialoague](./Content/Images/MonoGame-ContentManager-OpenWith-MGCB-Editor.jpg "Open With \"MGCB Editor (Default)\"")

The Installation Location of *MGCB-Editor* is `C:\Users\YOUR_USER_NAME\.nuget\packages\dotnet-mgcb-editor-windows\3.8.1.303\tools\net6.0\any\mgcb-editor-windows-data` replace YOUR_USER_NAME with your windows user name.

If you can't find the MGCB-Editor try running `dotnet tool install -g dotnet-mgcb` in the terminal and make sure it's installed. Then run `dotnet tool restore` and start *MGCB-Editor* with `dotnet mgcb-editor` and check in the Task Manager (CTLR+ALT+Delete) where the program is installed. Right click on the running Program, Properties.

![Screenshot showing the Task Manager](./Content/Images/MGCB-Editor-TaskManager.jpg "Find the installation path of MonoGame MGCB-Editor through the Windows Task Manager")

<a id="jumpToContent.mgcb"></a>
If you couldn't open it with the *MGCB Editor*, go [back](#Change-MonoGame-Content-Manager-Settings).

Open the Content.mgcb.
Click on the topmost red symbol with "content" written next to it. In the *Properties* Window-Section scroll to References and left click it. A new Window is opened.

![Screenshot showing MGCB-Editor and the content Properties section](./Content/Images/MGCB-Editor-ContentReferences.jpg "In the MGCB-Editor find \"content\" and it's Properties/References")

In this new Window *Add* a new Location. The new alpha dll is installed under `C:\Users\YOUR_USER_NAME\.nuget\packages\monogame.extended.content.pipeline\3.9.0-alpha0084\tools\MonoGame.Extended.Content.Pipeline.dll`

After finding it and adding the new alpha version, remove the old one.

![Screenshot showing MGCB-Editor and how to add the alpha version of the Content Pipeline](./Content/Images/MGCB-Editor-newAlphaVersion.jpg "In the new Window click *Add* and find the new alpha of the Content Pipeline")

### GameBundle
In the Terminal:

Install dotnet Tool GameBundle `dotnet tool install --global GameBundle`

### Tiled
Only needed if you want to make your own Maps/Levels.

From [Tiled](https://www.mapeditor.org/) install the newest Version.

---

## <a id="HowToBuild-Build"></a>Build 

1. ### Build Map
    Only needed if you want to make your own Maps/Levels.

    In Tiled, select the *.tmx* file and click `File/Save As` and save it as a `Tiled map files (*.tmx *.xml)` file in the *Game/Content* folder. You can find the *Game/Content* folder by right clicking it in the Solution Explorer and choosing `Open Folder in File Explorer`.

    Do the same for the *.tsx* file but save it as *tsx*.

    ![Screenshot showing which files from Tiled to save](./Content/Images/Tiled-SaveAs.jpg "Save both files, \".tmx\" and \".tsx\", in the Game/Content folder")

1. ### Delete bin, obj and Tiled folder in Content
    Back in the Visual Studio Solution Explorer delete the folders `Content/bin`, `Content/obj` and `Content/Tiled`.

    ![Screenshot showing which folders to delete](./Content/Images/SolutionExplorer-DeleteFolders-bin-obj-Tiled.jpg "Delete bin, obj and Tiled")

1. ### Build Content
    Open the Content.mgcb again and press `save`, `clean` and `build`. If you've added the *MGCB-Editor* as the default option before, you can simply double click the Content.mgcb file.

    ![Screenshot showing the MGCB-Editor building process](./Content/Images/MGCB-Save-Clean-Build.jpg "Save, Clean and Build the Content")

1. ### Check Tiled folder to Copy on Build
    After building there again should be those three folders we deleted before: *bin, obj and Tiled*. Open Tiled and select all files in this folder. `Right click` and choose `Properties`. In the now open Properties View, under *Copy to Output Directory* choose the option `Copy if newer`.

    ![Screenshot showing Properties of the Tiled folder files. Change to "Copy if newer"](./Content/Images/SolutionExplorer-Tiled-CopyIfNewer.jpg "Select all Tiled folder files, Right Click/Properties and change to \"Copy if newer\"")

1. ### Delete Debug folder in bin
    Don't worry, the hard part is now done.

    In the Solution Explorer right click the solution and choose `Open Folder in File Explorer`. Go to *ANXY/bin* and delete any `Debug` folders that are in there.

    ![Screenshot showing the ANXY/bin and the Debug folder"](./Content/Images/Delete-Debug-Folder.jpg "Delete all Debug folders in here")


1. ### Build with Visual Studio    
    In the Solution Explorer, Right Click the topmost thing called `Solution 'ANXY'` and choose `Build    Solution`.

1. ### Build with GameBundle
    In the Terminal:
    
    Change to the ANXY solution directory and go into the ANXY  Application. `cd .\ANXY\`

    The command `ls` should show you something like this:
    
    ![Screenshot showing all files and folders of ANXY in the Terminal with the ls command"](./Content/Images/installGameBundle-LS.jpg "This needs to look similar otherwise you're in the wrong directory.")
    
    With `gamebundle --help` you can show any possible commands.
    
    To build a *windows app* write `gamebundle -w -v`
    
    And to build the *macOS app* write `gamebundle -m --mac-bundle     -v`
    
    If the console output shows something along the lines of *dotnet is missing or not installed* go to the provided  link in the Terminal-Output or follow this link [dotnet install link](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-2.1.30-windows-x64-installer?cid=getdotnetcore).
        
    The game output is located in `cd .\bin\Bundled\`. You've successfully build the app and it's ready for deployment!

---

# <a id="DIY-Game"></a>Make it your own Game

## <a id="DIY-Game-Map"></a>Create your own Map

---

# <a id="About"></a>About the game

## <a id="About-Idea"></a>Idea

## <a id="About-ClassDiagram"></a>Class Diagram
[![Drawing of the Class Diagram for anxY](./Content/Images/ClassDiagram.svg "anxY Class Diagram"){: style="width:100%"}](./Content/Images/ClassDiagram.svg)

## <a id="About-Pattern"></a>Pattern used

[Entity Component System](https://en.wikipedia.org/wiki/Entity_component_system) is a pattern mostly used in video game development for the representation of game world objects. An ECS comprises entities composed from components of data, with systems which operate on entities' components. ECS follows the principle of composition over inheritance, meaning that every entity is defined not by a type hierarchy, but by the components that are associated with it. Systems act globally over all entities which have the required components.


[Singleton pattern](https://en.wikipedia.org/wiki/Singleton_pattern) is a software design pattern that restricts the instantiation of a class to a singular instance. The pattern is useful when exactly one object is needed to coordinate actions across a system.


## <a id="About-Tests"></a>Tests

## <a id="About-Tools-Used"></a>Tools Used

Many thanks for all these great tools which made it possible to develop anxY!

- [Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/)
- [Git Fork](https://git-fork.com/)
- [GitHub](https://github.com/)
- [GitHub Actions](https://github.com/features/actions)
- [Jira](https://www.atlassian.com/software/jira)
- [LucidChart](https://www.lucidchart.com/pages/)
- [MonoGame](https://www.monogame.net/)
- [MonoGame Extended Content Pipeline](https://github.com/craftworkgames/MonoGame.Extended)
- [MonoGame Extended Content Pipeline Alpha](https://www.nuget.org/packages/MonoGame.Extended/3.9.0-alpha0084)
- [GameBundle](https://github.com/Ellpeck/GameBundle)

## <a id="About-Builds"></a>Builds

<img alt="Builds Passing" src="https://github.com/milattom/ANXY/actions/workflows/MainPR.yml/badge.svg">

-----

## <a id="Contributors"></a>Contributors

<div align="center">

[![Profile Picture of GitHub User "milattom"](https://avatars.githubusercontent.com/u/79468061?v=4 "GitHub user \"milattom\""){: style="width:65px"}](https://github.com/milattom)

[Milata Tomas Stefan (milattom)](mailto:milattom@students.zhaw.ch)

[![Profile Picture of GitHub User "D-akeret"](https://avatars.githubusercontent.com/u/79446856?v=4 "GitHub user \"D-akeret\""){: style="width:65px"}](https://github.com/D-akeret)

[Akeret Dominic (akeredom)](mailto:akeredom@students.zhaw.ch)

</div>
