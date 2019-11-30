use master
drop login qltv_duy
go
create login qltv_duy with password='123'
go
use qltv
go
create user duy for login qltv_duy
go
sp_addrolemember @rolename='db_owner', @membername='duy'
go