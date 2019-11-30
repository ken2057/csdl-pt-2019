use master
drop login qltv_nghi
go
create login qltv_nghi with password='123'
go
use qltv
go
create user nghi for login qltv_nghi
go
sp_addrolemember @rolename='db_owner', @membername='nghi'
go