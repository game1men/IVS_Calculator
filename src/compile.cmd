@echo off
set home=%~dp0
set application=Calculator

dotnet publish %application% -v m -c Release -p:PublishProfileFullPath="%home%CompileProfile.pubxml" -p:PublishDir="%home%%application%\bin\deploy"