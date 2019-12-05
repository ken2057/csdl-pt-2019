use master
drop login qltv_bui
drop login qltv_root
go
create login qltv_bui with password='123'
create login qltv_root with password='123'
go
use qltv
go
create user bui for login qltv_bui
create user root for login qltv_root
go
sp_addrolemember @rolename='db_owner', @membername='bui'
sp_addrolemember @rolename='db_owner', @membername='root'
go
