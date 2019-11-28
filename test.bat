@ECHO OFF
setlocal enabledelayedexpansion

rem cd script
rem install all file in those folders
rem for %%f in (duy, quang, hieu, nghi) do (
rem 	echo --------------------------------------------------------------------
rem 	echo <TAB> Install %%f script
rem 	for /f "delims=" %%# in ('dir %%f /s /b') do (
rem 		echo -
rem 	    echo Install %%#
rem 	    sqlcmd -i %%#
rem 	)
rem )
rem echo --------------------------------------------------------------------
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
echo -----------------------------------
echo --- Install DB for each Server  ---
echo -----------------------------------
for %%i in (%list%) do (
	echo -------------------------------
	echo Install !script[%%i]! on !server[%%i]!
	sqlcmd -S !server[%%i]! -i !script[%%i]! -U sa -P "123"
)
echo Done
echo -----------------------------------
echo ---       Add link server       ---
echo -----------------------------------
for %%i in (%list%) do (
	echo -------------------------------
	echo Current: !name[%%i]!
	for %%j in (%list%) do (
		if NOT "%%i" == "%%j" (
			echo -
			echo Add: !name[%%j]!
			echo EXEC master.dbo.sp_addlinkedserver @server=N'!name[%%j]!', @provider=N'SQLOLEDB', @datasrc=N'!server[%%j]!', @srvproduct='' | sqlcmd -S !server[%%i]! -U sa -P "123" -e
			echo Login: !name[%%j]!
			echo EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'!name[%%j]!', @useself=N'False', @locallogin=NULL, @rmtuser=N'sa', @rmtpassword='123' > temp
			type temp | sqlcmd -S !server[%%i]! -U sa -P "123" -e
		)
	)
)
del temp
echo -----------------------------------
echo ---    Add SP on each Server    ---
echo -----------------------------------
rem echo Install link server:
rem 		for /f %%a in ('type server.config') do (
rem 			for %%n in (QLTV_MAY_CHU QLTV_TRAM_1 QLTV_TRAM_2 QLTV_TRAM_3 QLTV_TRAM_4) do (
rem 				echo EXEC master.dbo.sp_addlinkedserver @server=N'%%a', @provider=N'SQLOLEDB', @datasrc=N'%%i, 1433', @srvproduct='' | sqlcmd -S %%i -e
				
rem 			)
rem 		)


rem echo EXEC master.dbo.sp_addlinkedserver @server=N'QLTV_MAY_CHU', @provider=N'SQLOLEDB', @datasrc=N'192.168.43.223\WIN-MD7EJ65P9NA\SQLEXPRESS, 1433', @srvproduct='' | sqlcmd -e
echo Done
pause


rem EXEC master.dbo.sp_addlinkedserver
rem @server=N'QLTV_TRAM_1',
rem @provider=N'SQLOLEDB',
rem @datasrc=N'192.168.43.24\DESKTOP-NE6TTO8\SQLEXPRESS, 1433',
rem @srvproduct=''

rem echo EXEC master.dbo.sp_addlinkedserver @server=N'!name[%%j]!', @provider=N'SQLOLEDB', @datasrc=N'!server[%%j]!', @srvproduct='' | sqlcmd -S !server[%%i]! -U sa -P "123" -e
rem echo EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=N'QLTV_TRAM_1', @useself=N'False', @locallogin=NULL, @rmtuser=N'sa', @rmtpassword='123' > temp
rem type temp > 
rem sqlcmd -S tcp:127.0.0.1,1433 -U sa -P "123" -e