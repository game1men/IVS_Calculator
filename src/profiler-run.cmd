@echo off
set home=%~dp0
set application=Profiling

rd /s /q "%home%%application%\bin\deploy" 2>nul
dotnet publish %application% -v m -c Release -p:PublishProfileFullPath="%home%Profiler.pubxml" -p:PublishDir="%home%%application%\bin\deploy"
echo.
echo.
echo 10:
call "%home%%application%\bin\deploy\%application%.exe" <"%home%%application%\profiling_samples\10.txt"
echo.
echo 100:
call "%home%%application%\bin\deploy\%application%.exe" <"%home%%application%\profiling_samples\100.txt"
echo.
echo 1000:
call "%home%%application%\bin\deploy\%application%.exe" <"%home%%application%\profiling_samples\1000.txt"