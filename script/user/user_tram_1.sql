use master
drop login qltv_duy
drop login qltv_root
go
create login qltv_duy with password='123'
create login qltv_root with password='123'
go
use qltv
go
create user duy for login qltv_duy
create user root for login qltv_root
go
sp_addrolemember @rolename='db_owner', @membername='duy'
sp_addrolemember @rolename='db_owner', @membername='root'
go
