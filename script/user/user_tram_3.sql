use master
drop login qltv_root
drop login qltv_duy
drop login qltv_nghi
drop login qltv_bui
<<<<<<< HEAD
drop login qltv_root
=======
drop login qltv_hieu
>>>>>>> 96ed2f599ceb308f6fd5efd65828921ba9155b1c
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
