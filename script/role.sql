use master
go
create login qltv_root with password='123'
create login qltv_duy with password='123'
create login qltv_nghi with password='123'
create login qltv_hieu with password='123' 
create login qltv_bui with password='123'
go 
use qltv
go 
create user root for login qltv_root 
create user duy for login qltv_duy
create user nghi for login qltv_nghi
create user hieu for login qltv_hieu
create user bui for login qltv_bui
go
sp_addrolemember @rolename='db_owner', @membername='root'
go
create role thuthu
go
grant * on dangky to thuthu
grant * on tailieu to thuthu
grant * on loaitailieu to thuthu
grant * on bansao to thuthu
grant * on muon to thuthu
grant * on quatrinhmuon to thuthu
grant * on docgia to thuthu
grant * on docgia to tacgia
go
sp_addrolemember @rolename='db_owner', @membername='duy'
sp_addrolemember @rolename='db_owner', @membername='nghi'
sp_addrolemember @rolename='db_owner', @membername='hieu'
sp_addrolemember @rolename='db_owner', @membername='bui'
go
create role nhanvien
go
grant insert, update on dangky to thuthu
grant insert, update on tailieu to thuthu
grant insert, update on loaitailieu to thuthu
grant insert, update on bansao to thuthu
grant insert, update on muon to thuthu
grant insert, update on quatrinhmuon to thuthu
grant insert, update on docgia to thuthu
grant insert, update on docgia to tacgia
go