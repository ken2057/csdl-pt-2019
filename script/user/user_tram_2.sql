use master
drop login qltv_root
drop login qltv_duy
drop login qltv_nghi
drop login qltv_bui
drop login qltv_hieu
go
create login qltv_nghi with password='123'
create login qltv_root with password='123'
go
use qltv
go
create user nghi for login qltv_nghi
create user root for login qltv_root
go
sp_addrolemember @rolename='db_owner', @membername='nghi'
go
sp_addrolemember @rolename='db_owner', @membername='root'
go
