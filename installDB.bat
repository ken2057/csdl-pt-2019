@ECHO OFF
echo Install DB qltv
sqlcmd -i script/db/db_maychu.sql
echo Add Data into qltv
sqlcmd -i script/data.sql
echo Install SP
for /f "delims=" %%s in ('dir script /b /s ^| findstr "sp_maychu"') do (
	@echo:
	echo - Installing: %%s
	sqlcmd -i %%s
)
echo Done
pause