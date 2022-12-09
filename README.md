<img src="/Content/Images/anxious.jpg" alt="drawing" style="width:150%;"/>

# anxY

This is a nerve wrecking Nightmare Jump & Run developed by mayfly studios 

</br>

## Table of Contents:
* [Installation of the Application](#installation)
* [Application Info](#info)
    * [Idea](#idea)
    * [First Use](#first)
* [Technical documentation](#technical)
    * [Structure](#structure)
    * [Class Diagram](#diagram)
    * [Pattern used](#pattern)
    * [Tests](#tests)
* [Contributors](#contributors)

<h2 id="installation">Installation</h2>

1. First, make sure you meet the following dependencies:

2. Get the repository with:

3. Import the project into your IDE:

<h2 if="howToBuild">How To Build</h2>

in the commandline do the following:

install dotnet GameBundle
`dotnet tool install --global GameBundle`

change to the ANXY solution directory and go into the ANXY Application.
`ls` should show you something like ANXY
<a target="_blank" href="/Content/Images/installGameBundle-LS.jpg">
    <img src="/Content/Images/installGameBundle-LS.jpg" alt="Folder Structure" style="width:50%;"/>
</a>

with `gambundle --help` you can show any possible commands.

to build the windows app write `gamebundle -w -v`

and to build the macOS app write `gamebundle -m --mac-bundle -v`

if the console output shows something along the lines of ==dotnet is missing or not installed== go to the provided link or follow this link [dotnet install link](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-2.1.30-windows-x64-installer?cid=getdotnetcore)

the game output is located in `cd .\bin\Bundled\`

<h2 id="info">Application Info</h2>
<h3 id="idea">Idea</h3>

#### About the game


<h4 id="diagram">Class Diagram</h4>
<a target="_blank" href="/Content/Images/ClassDiagram.svg">
    <img src="/Content/Images/ClassDiagram.svg" alt="Class Diagram Drawing" style="width:150%;"/>
</a>

<h4 id="pattern">Pattern used</h4>
<p>
<a href="https://en.wikipedia.org/wiki/Entity_component_system">Entity Component System</a>
is a pattern mostly used in video game development for the representation of game world objects. An ECS comprises entities composed from components of data, with systems which operate on entities' components. ECS follows the principle of composition over inheritance, meaning that every entity is defined not by a type hierarchy, but by the components that are associated with it. Systems act globally over all entities which have the required components.
</p>
<p>
<a href="https://en.wikipedia.org/wiki/Singleton_pattern">Singleton pattern</a>
is a software design pattern that restricts the instantiation of a class to a singular instance. The pattern is useful when exactly one object is needed to coordinate actions across a system.
</p>

<h4 id="tests">Tests</h4>

<h4 id="Builds">Builds</h4>
<img alt="Builds Passing" src="https://github.com/milattom/ANXY/actions/workflows/MainPR.yml/badge.svg">

-----

<h2 id="contributors">Contributors</h2>

<p align="center">
<img class="c" src="https://avatars.githubusercontent.com/u/79468061?v=4" width="65" height="60"> <br />
   <a href="mailto:milattom@students.zhaw.c">Milata Tomas Stefan (milattom)</a> 
<br />

<p align="center">
<img class="a" src="https://avatars.githubusercontent.com/u/79446856?v=4" width="60" height="60"> <br />
<a href="mailto:akeredom@students.zhaw.ch">Akeret Dominic (akeredom)</a> 
<br />


