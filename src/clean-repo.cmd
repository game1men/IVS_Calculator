@echo off
dotnet clean -v m
del /Q "Calculator setup.exe" 2>nul
rd Calculator\obj /S /Q >nul 2>nul
rd Calculator\bin /S /Q >nul 2>nul

rd Mathlib\obj /S /Q >nul 2>nul
rd Mathlib\bin /S /Q >nul 2>nul 

rd MathLib.Tests\obj /S /Q >nul 2>nul
rd MathLib.Tests\bin /S /Q >nul 2>nul

rd Mockup\obj /S /Q >nul 2>nul
rd Mockup\bin /S /Q >nul 2>nul

rd Profiling\obj /S /Q >nul 2>nul
rd Profiling\bin /S /Q >nul 2>nul

echo "" >nul