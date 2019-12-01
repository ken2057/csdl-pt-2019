use master
go
drop login qltv_root
drop login qltv_duy
drop login qltv_nghi
drop login qltv_bui
drop login qltv_hieu
go
create login qltv_root with password='123'
go
use qltv
go
create user root for login qltv_root
go
sp_addrolemember @rolename='db_owner', @membername='root'
go