#######################
# Description: Makefile for IVS project, 2022
# Author: Josef Uncovsky
# Team: Themix
#######################

# DEPENDENCY FOR WINDOWS: Chocolatey (https://chocolatey.org/install)
# DEPENDENCY FOR WINDOWS: GNU Make (http://gnuwin32.sourceforge.net/)

REMOVE_DIRS = rmdir obj /S /Q && md obj && rmdir bin /S /Q && md bin
ARCHIVE_NAME = xvecer29_xceska06_xuncov00_xsmida06
# default command
all: run
	
# command for packing the project folder into a .zip file
pack:
	md $(ARCHIVE_NAME) && copy "Calculator setup.exe" $(ARCHIVE_NAME) && cd $(ARCHIVE_NAME) && md install && copy "Calculator setup.exe" install && del "Calculator setup.exe" \
	cd.. && cd.. 

	
# command for removing files which won't be a part of evaluation
clean:
	
	@dotnet clean -v m && del Calculator setup.exe && rd /s /q .vs && cd Calculator && $(REMOVE_DIRS) && cd.. && cd Mathlib && $(REMOVE_DIRS) && cd.. && cd MathLib.Tests && $(REMOVE_DIRS) && cd.. && cd Mockup && $(REMOVE_DIRS) && cd.. && cd Profiling && $(REMOVE_DIRS)
	
# command for compilation and launch of the app
run: compile

# command for executing the compile-cmd script
compile:
	@compile.cmd && cd Calculator/bin/deploy && Calculator.exe

# command for running the profiler on sample data
profile:

	 @profiler-run.cmd

# command for running tests of Mathlib
test:
	cd MathLib.Tests && @dotnet test

# command for doc generation
doc: 
	@doxygen Doxyfile
# command for installer creation
install:
	@iscc Installer.iss

