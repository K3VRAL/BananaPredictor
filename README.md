# BananaPredictor

BananaPredictor is a CLI application that allows for the modification and manipulation of each Banana Shower's bananas x-axis through the utilisation of Juice Streams with their easy to manipulate RNG features.

## Table of Contents

**[Installation](#install)**

**[Documentation](#doc)**

<a name='install'></a>

## Installation

Before you can do anything with the project, you will need to [download and install libosu](https://github.com/K3VRAL/libosu).

The tools you will also need before you can do anything is `make`, `gcc`/`gcc-mingw` and `pkg-config`.

### For Linux:

Once you have the necessary tools and the library has been installed, in your terminal while still in the same folder location of this project's `Makefile`, write `make`, to compile the project to a binary, then `sudo make install` to install the binary.

If you wish to uninstall the program, do `sudo make uninstall` and that should get rid of everything.

### For Windows:

Once you have the necessary tools and the project has been cloned, move the contents of the compiled `libosu.dll` project to the same folder location of this project's Makefile, and in your terminal, write `make win` to compile the project to a dynamic linked library. To execute the application, both the compiled application and the DLL must be in the same folder.

<a name='doc'></a>

## Documentation

### [BananaPredictor](/doc/BananaPredictor.md)