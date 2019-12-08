@echo off
setlocal enabledelayedexpansion
rem --------------------------------------------------------
set currentDir=%cd%
set slnFile=""
set msbuildFolder=""
set currentFile="App CSDLPT\csdl-pt\App.config"
set tempFile="App CSDLPT\csdl-pt\_App.config"
set writeFile="App CSDLPT\csdl-pt\write_App.config"
set "list=0 1 2 3 4"
rem ------------------
set src[0]=SRC_MAY_CHU
set src[1]=SRC_TRAM_1
set src[2]=SRC_TRAM_2
set src[3]=SRC_TRAM_3
set src[4]=SRC_TRAM_4
rem ---------------------
set tram[0]=qltv_maychu
set tram[1]=qltv_tram_1
set tram[2]=qltv_tram_2
set tram[3]=qltv_tram_3
set tram[4]=qltv_tram_4
rem ---------------------
set server[0]=""
set server[1]=""
set server[2]=""
set server[3]=""
set server[4]=""
rem --------------------------------------------------------
echo Make temp file
copy %currentFile% %tempFile%
rem --------------------------------------------------------
rem Get Hostname in file
set index=0
for /f %%s in ('type server.config') do (
	if !index! LEQ 4 (
		set server[!index!]=%%s
		set /a index=!index!+1
	)
)
rem --------------------------------------------------------
@echo:
echo Fix DB source in App.config
for %%i in (%list%) do (
	echo - Change !src[%%i]! to !server[%%i]!
	rem Run through all line in App.config and change the src[]
	copy /y NUL %writeFile%
	for /f "delims=" %%t in ('type %currentFile%') do (
		echo "%%t" | findstr "!src[%%i]!" > nul
		if errorlevel 1 (
			echo %%t
		) else (
			echo ^<add name="!tram[%%i]!" connectionString="Data Source=!server[%%i]!;Initial Catalog=qltv;" providerName="System.Data.SqlClient" /^>
		)
	) >> %writeFile%

	del %currentFile%
	rename %writeFile% "App.config"
)
rem --------------------------------------------------------
@echo:
echo Move to msbuild folder
cd /d %SystemRoot%\Microsoft.net\framework\
for /f %%s in ('dir /AD /B') do set msbuildFolder=%%s
cd %msbuildFolder%
echo - Current dir: %cd%
rem --------------------------------------------------------
@echo:
echo Rebuild WPF app
rem get .sln path
for /f "delims=" %%s in ('dir %currentDir% /b /s ^| findstr ".sln"') do set slnFile=%%s
echo - Rebuild from %slnFile%
msbuild "%slnFile%"
rem --------------------------------------------------------
@echo:
pause
cd /d %currentDir%
echo Current path %cd%
del %currentFile%
rename %tempFile% "App.config"
echo Done
pause