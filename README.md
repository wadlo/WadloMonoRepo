This mono-repo is split up into packages. Every game and addon should have

-  its own project.godot file
-  its own .csproj file, with correct dependencies
-  a src directory. Anything inside the src directory will be included with the github repo

Addons will have

-  An addons folder and an addon inside. Anything inside that addon will be included in the published addon

All contents of the Addons directory are MIT licensed.
Anything contained in the Games directory is Proprietary software

Developing in the MonoRepo:

-  At the root of the MonoRepo is a single .sln file. Godot will auto-generate one, but you can delete it and move the definition to the mono .sln

## How to create a new project

-  Create a folder
-  Create a Src folder
-  Create a new godot project inside that Src folder
-  Generate a new cs proj
-  Make sure to remove all spaces in the generated files and fix capitalization. For example, "Godot Open Ecs" should be "GodotOpenECS"
-  Remove the .sln file and add it to the mono .sln file
