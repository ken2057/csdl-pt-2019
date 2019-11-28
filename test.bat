@ECHO OFF
setlocal enabledelayedexpansion

rem -------------------------------
rem Declare variables
rem -------------------------------
set "list=0 1 2 3 4"
rem -------------------------------
set script[0]=script/maychu.sql
set script[1]=script/tram1.sql
set script[2]=script/tram2.sql
set script[3]=script/tram3.sql
set script[4]=script/tram4.sql
rem -------------------------------
set sp[0]=sp_maychu
set sp[1]=sp_tram_1
set sp[2]=sp_tram_2
set sp[3]=sp_tram_3
set sp[4]=sp_tram_4
rem -------------------------------
set name[0]=QLTV_MAY_CHU
set name[1]=QLTV_TRAM_1
set name[2]=QLTV_TRAM_2
set name[3]=QLTV_TRAM_3
set name[4]=QLTV_TRAM_4
rem -------------------------------
set server[0]=""
set server[1]=""
set server[2]=""
set server[3]=""
set server[4]=""
rem -------------------------------
rem Get all Server into array
rem -------------------------------
set index=0
for /f %%s in ('type server.config') do (
	set server[!index!]=%%s
	set /a index=!index!+1
)
rem -------------------------------
rem Get account login to SQL for all
rem -------------------------------
set account = ""
set password = ""
for /f %%s in ('type sqllogin.config') do (
	set text=%%s
	rem remove space
	set text=!text: =!
	rem set to varible
	if "!account!"=="" (set account=!text!) else (if "!password!"=="" (set password=!text!))
)
echo ----------------------------------------
echo ---  Install DB, SP for each Server  ---
echo ----------------------------------------
for %%i in (%list%) do (
	echo --------------------
	echo - Install !script[%%i]! on !server[%%i]!
	rem Install db
	sqlcmd -S !server[%%i]! -i !script[%%i]! -U !account! -P "!password!"
	rem Install other
)
echo Done
echo ----------------------------------------
echo ---          Add link server         ---
echo ----------------------------------------
for %%i in (%list%) do (
	echo -------------------------------
	echo Current: !name[%%i]!
	rem write into install.sql file and then execute it
	echo use master > install.sql
	for %%j in (%list%) do (
		if NOT "%%i" == "%%j" (
			echo go >> install.sql
			echo print 'Add !name[%%j]!' >> install.sql
			echo go >> install.sql
			echo EXEC master.dbo.sp_addlinkedserver @server=N'!name[%%j]!', @provider=N'SQLOLEDB', @datasrc=N'!server[%%j]!', @srvproduct='' >> install.sql
			echo go >> install.sql
			echo EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'!name[%%j]!', @useself=N'False', @locallogin=NULL, @rmtuser=N'sa', @rmtpassword='!password!' >> install.sql
			echo go >> install.sql
		)
	)
	sqlcmd -S !server[%%i]! -U !account! -P "!password!" -i install.sql
	del install.sql
	rem wait for not error handshake
	TIMEOUT /T 3 /NOBREAK
)
echo ----------------------------------------
echo ---      Add SP on each Server       ---
echo ----------------------------------------
for %%i in (%list%) do (
	echo --------------------
	echo - Install SP on !server[%%i]!
	for /f "delims=" %%s in ('dir script /b /s ^| findstr !sp[0]!') do (
		echo -- Installing %%s
		sqlcmd -S !server[%%i]! -i %%s -U !account! -P "!password!"
	)
)

echo Done
pause