@ECHO OFF
echo Install DB qltv
sqlcmd -i script/maychu.sql
echo Add Data into QLMuaHang
sqlcmd -i script/data.sql
echo Done
pause