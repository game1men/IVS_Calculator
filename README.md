Prostredi
---------

Windows 64bit

Autori
------

Themix
- xvecer29 Petr Večeřa
- xceska06 René Češka
- xuncov00 Josef Unčovský
- xsmida06 Matěj Šmida
  
Licence
-------

Tento program je poskytovan pod linceci GPLv3. Zneni licence je v souboru LICENSE.md v koreni repozitare.



Pro vývoj
-----------

Je nutné mít nainstalované následující programy, které dovolí program vyvíjet:

1. Visual Studio 2019 nebo novější s .NET 6 SDK
	- verzi SDK a správnou integraci s CLI lze zkontrolovat příkazem "dotnet --version"
2. GNU makefile pro používání nachystaného makefile ve složce src/ - stažení dostupné na [GnuWin](http://gnuwin32.sourceforge.net/)
	- správnou integraci s CLI lze zkontrolovat příkazem "make --version"
3. [Inno setup](https://jrsoftware.org/isdl.php) pro kompilaci instalačky pomocí souboru *Installer.iss*
	- správnost integrace Inno setup s CLI lze zkontrolovat pomocí příkazu *"iscc"*
	- je nutné program nejdříve zkompilovat pomocí *"make compile"* nebo rovnou spustit příkaz *"make install"*, který vytvoří instalačku v kořeni složky src/
4. Doxygen pro vygenerování dokumentace
	- Pro správnou funkčnost je nutné zkontrolovat integraci s CLI, lze ověřit příkazem *"doxygen --version"*

\* pokud integrace příkazů v CLI nebude korektní, program se přes soubor make nezkompiluje
	
Poznámky:
- dokumentaci lze otevřít souborem index.html ve složce doc/ (po vygenerování makefile)
- \*.cs soubory v projektu se řídí konvencí .editorconfig pro .NET a je nutné si zkontrolovat [nastavení Visual Studia](https://docs.microsoft.com/cs-cz/visualstudio/ide/create-portable-custom-editor-options?view=vs-2022#troubleshoot-editorconfig-settings) v případě, že nefunguje
