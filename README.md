# BananaPredictor

Under heavy development.

Using a CLI instead of a GUI (windows exclusive?) since I had many development issues with it and it got very unstable. If you want to use the GUI (outdated), git clone this and build it in Visual Studio Preview.

Using [.Net Core SDK 5](https://dotnet.microsoft.com/download/dotnet/5.0) and [osu Catch Rulesets](https://www.nuget.org/packages/ppy.osu.Game.Rulesets.Catch/) from lazer

This program will be able to predict where the next banana xoffset will be placed and either remove or leave it be depending on the area you want the banana to be placed in.

**How To Install**

Look at releases or click here; 


**CLI Commands**

THIS IS CURRENTLY UNDOCUMENTED; THIS WILL BE FIXED IN THE FUTURE

Normal Commands:

    tpath   [ path:string ]     -       The target osu file to manipulate
    edit                        -       The bananapredictor editor to manipulate the positioning and timing of the bananashower
    run                         -       Run the application to manipulate the bananapredictor
    info                        -       Display all the inputted values
    help                        -       Display all the possible commands
    exit                        -       Exits the application

Edit Commands:
    add                         -   Adds a new bananapredictor point
        [ time:int ] [ invert:bool ] [ left:int ] [ right:int ]