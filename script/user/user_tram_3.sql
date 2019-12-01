use master
drop login qltv_root
drop login qltv_duy
drop login qltv_nghi
drop login qltv_bui
drop login qltv_hieu
go
create login qltv_bui with password='123'
go
use qltv
go
create user bui for login qltv_bui
go
sp_addrolemember @rolename='db_owner', @membername='bui'
go