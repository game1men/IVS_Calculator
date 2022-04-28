#######################
# Description: Makefile for IVS project, 2022
# Author: Josef Uncovsky
# Team: Themix
#######################

# DEPENDENCY FOR WINDOWS: Chocolatey (https://chocolatey.org/install)
# DEPENDENCY FOR WINDOWS: GNU Make (http://gnuwin32.sourceforge.net/)

REMOVE_DIRS = rd obj /S /Q >nul 2>nul & rd bin /S /Q >nul 2>nul & echo "" >nul
ARCHIVE_NAME = xvecer29_xceska06_xuncov00_xsmida06
# default command
all: run
	
# command for packing the project folder into a .zip file
pack: install
	@cd .. && md $(ARCHIVE_NAME) & echo "" >nul
	@cd .. && xcopy "src\Calculator setup.exe" $(ARCHIVE_NAME)\src\ /S /I /y
	@call clean-repo.cmd
	@cd .. && xcopy src\* $(ARCHIVE_NAME)\src\ /S /I /y
	@cd .. && xcopy mockup\* $(ARCHIVE_NAME)\mockup\ /S /I /y
	@cd .. && xcopy profiling\* $(ARCHIVE_NAME)\profiling\ /S /I /y
	@cd .. && xcopy plan\* $(ARCHIVE_NAME)\plan\ /S /I /y
	@cd .. && xcopy .git\* $(ARCHIVE_NAME)\.git\ /S /I /y /K
	@cd .. && xcopy *.* $(ARCHIVE_NAME) /I /y
	@cd .. && powershell Compress-Archive $(ARCHIVE_NAME) $(ARCHIVE_NAME).zip
	@cd .. && rd /S /Q $(ARCHIVE_NAME)
	

	
# command for removing files which won't be a part of evaluation
clean:
	@clean-repo.cmd
	
# command for compilation and launch of the app
run: compile
	@cd Calculator/bin/deploy && start Calculator.exe

# command for executing the compile-cmd script
compile:
	@compile.cmd 

# command for running the profiler on sample data
profile:
	@profiler-run.cmd

# command for running tests of Mathlib
test:
	@cd MathLib.Tests && @dotnet test

# command for doc generation
doc: 
	@doxygen Doxyfile
# command for installer creation
install: compile
	@iscc Installer.iss

