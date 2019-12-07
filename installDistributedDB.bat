@ECHO OFF
setlocal enabledelayedexpansion

rem -------------------------------
rem Declare variables
rem -------------------------------
set "list=0 1 2 3 4"
set data=script/data/data_maychu.sql
set transfer=script/transfer.sql
rem -------------------------------
set db[0]=script/db/db_maychu.sql
set db[1]=script/db/db_tram_1.sql
set db[2]=script/db/db_tram_2.sql
set db[3]=script/db/db_tram_3.sql
set db[4]=script/db/db_tram_4.sql
rem -------------------------------
set user[0]=script/user/user_maychu.sql
set user[1]=script/user/user_tram_1.sql
set user[2]=script/user/user_tram_2.sql
set user[3]=script/user/user_tram_3.sql
set user[4]=script/user/user_tram_4.sql
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
	if !index! LEQ 4 (
		set server[!index!]=%%s
		set /a index=!index!+1
	)
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
	echo - Install !db[%%i]! on !server[%%i]!
	rem Install db
	sqlcmd -S !server[%%i]! -i !db[%%i]! -U !account! -P "!password!"
	rem Install other
)
TIMEOUT /T 3 /NOBREAK
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
	for /f "delims=" %%s in ('dir script /b /s ^| findstr !sp[%%i]!') do (
		echo -- Installing %%s
		sqlcmd -S !server[%%i]! -i %%s -U !account! -P "!password!"
	)
)
TIMEOUT /T 3 /NOBREAK
echo ----------------------------------------
echo ---             Add data             ---
echo ----------------------------------------
echo Add data to MayChu from !data!
sqlcmd -S !server[0]! -U !account! -P "!password!" -i !data!
echo Add data to Tram from !transfer!
sqlcmd -S !server[0]! -U !account! -P "!password!" -i !transfer!
TIMEOUT /T 3 /NOBREAK
echo ----------------------------------------
echo ---     Add login on each Server     ---
echo ----------------------------------------
for %%i in (%list%) do (
	echo --------------------
	echo - Install login/user on !server[%%i]! from !user[%%i]!
	sqlcmd -S !server[%%i]! -U !account! -P "!password!" -i !user[%%i]!
)
TIMEOUT /T 3 /NOBREAK
echo ----------------------------------------
echo Done
pause