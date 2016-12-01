# EasyDeploy

A tool which helps deploying applications using SCCM without any need to write scripts. 
This is very helpful for any application that does not offer an MSI installation package.

**Note:** in the future, releases will become available at GitHub. For now, you must compile
them yourself.

## Projects
### EasyDeploy GUI
With this application you create a JSON file which describes all steps of the installation.
Currently, the following type of steps are supported:

* Copy file
* Delete file
* Unzip
* Execute file
* to be continued :-)

### EasyDeploy CLI
The actual command line tool which executes steps described in the JSON file. 

### Setup
The setup projects are WiX projects which are used to create MSI files (which can also be deployed
using SCCM - this is useful for the CLI tool).

## Requirements
### Client
You need .NET Framework 4.5+.

### Development
You need Visual Studio 2015.

## Contribution
Any help is welcome :-)

## License
MIT 
