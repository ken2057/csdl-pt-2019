use master
go
if exists (select * from sysdatabases where name='qltv')
	drop database qltv
go
drop login qltv_root
drop login qltv_duy
drop login qltv_nghi
drop login qltv_bui
drop login qltv_hieu
go
create database qltv
go
use qltv
go
Create table [TacGia]
(
	[ma_tacgia] Varchar(20) NOT NULL,
	[ten_tacgia] Nvarchar(100) NULL,
	[ghichu] Ntext NULL,
Primary Key ([ma_tacgia])
) 
go
Create table [DangKy]
(
	[ma_tailieu] Varchar(20) NOT NULL,
	[ma_sinhvien] Varchar(20) NOT NULL,
	[ngaygio_dk] Datetime NULL,
	[ghichu] Ntext NULL,
Primary Key ([ma_tailieu],[ma_sinhvien])
) 
go
Create table [TaiLieu]
(
	[ma_tailieu] Varchar(20) NOT NULL,
	[ma_loai] Varchar(20) NOT NULL,
	[ngonngu] Nvarchar(15) NULL,
	[ten_tailieu] Nvarchar(100) NULL,
	[tomtat] Ntext NULL,
Primary Key ([ma_tailieu])
)
go
Create table [LoaiTaiLieu]
(
	[ma_loai] Varchar(20) NOT NULL,
	[ten_loai] Nvarchar(20) NULL,
	[ghichu] Ntext NULL,
Primary Key ([ma_loai])
) 
go
Create table [BanSao]
(
	[ma_tailieu] Varchar(20) NOT NULL,
	[ma_bansao] Varchar(20) NOT NULL,
	[ma_ChiNhanh] Varchar(20) NOT NULL,
	[tinhtrang] Char(1) NULL Check (tinhtrang = 'Y' or tinhtrang = 'N' or tinhtrang = 'K'),
Primary Key ([ma_tailieu],[ma_bansao])
) 
go
Create table [DocGia]
(
	[ma_sinhvien] Varchar(20) NOT NULL,
	[hoten] Nvarchar(50) NULL,
	[NgaySinh] Datetime NULL,
	[diachi] Varchar(50) NULL,
	[sdt] Varchar(15) NULL,
Primary Key ([ma_sinhvien])
) 
go
Create table [Muon]
(
	[ma_tailieu] Varchar(20) NOT NULL,
	[ma_bansao] Varchar(20) NOT NULL,
	[ma_nhanvien_dua] Varchar(20) NOT NULL,
	[ma_sinhvien] Varchar(20) NOT NULL,
	[ngay_muon] Datetime NULL,
	[ngay_hethan] Datetime NULL,
	[tien_datcoc] Money NULL,
Primary Key ([ma_tailieu],[ma_bansao])
) 
go

Create table [QuaTrinhMuon]
(
	[ma_tailieu] Varchar(20) NOT NULL,
	[ma_bansao] Varchar(20) NOT NULL,
	[ma_nhanvien_dua] Varchar(20) NOT NULL,
	[ma_nhanvien_nhan] Varchar(20) NOT NULL,
	[ma_sinhvien] Varchar(20) NOT NULL,
	[ngay_hethan] Datetime NULL,
	[ngay_tra] Datetime NULL,
	[ngay_muon] Datetime not NULL,
	[tien_muon] Money NULL,
	[tien_datra] Money NULL,
	[tien_datcoc] Money NULL,
	[ghichu] Ntext NULL,
Primary Key ([ma_tailieu],[ma_bansao],[ngay_muon] )
) 
go

Create table [NhanVien]
(
	[ma_nhanvien] Varchar(20) NOT NULL,
	[quyen] Varchar(15) NOT NULL,
	[ma_ChiNhanh] Varchar(20) NOT NULL,
	[matkhau] Varchar(50) NULL,
	[sdt] Varchar(15) NULL,
Primary Key ([ma_nhanvien])
) 
go

Create table [Quyen]
(
	[quyen] Varchar(15) NOT NULL,
	[ghichu] Varchar(100) NULL,
Primary Key ([quyen])
) 
go

Create table [ChiNhanh]
(
	[ma_ChiNhanh] Varchar(20) NOT NULL,
	[diachi] Nvarchar(100) NULL,
Primary Key ([ma_ChiNhanh])
) 
go

go


Alter table [BanSao] add  foreign key([ma_tailieu]) references [TaiLieu] ([ma_tailieu])  on update no action on delete no action 
go
Alter table [DangKy] add  foreign key([ma_tailieu]) references [TaiLieu] ([ma_tailieu])  on update no action on delete no action 
go
Alter table [TaiLieu] add  foreign key([ma_loai]) references [LoaiTaiLieu] ([ma_loai])  on update no action on delete no action 
go
Alter table [Muon] add  foreign key([ma_tailieu],[ma_bansao]) references [BanSao] ([ma_tailieu],[ma_bansao])  on update no action on delete no action 
go
Alter table [QuaTrinhMuon] add  foreign key([ma_tailieu],[ma_bansao]) references [BanSao] ([ma_tailieu],[ma_bansao])  on update no action on delete no action 
go
Alter table [Muon] add  foreign key([ma_sinhvien]) references [DocGia] ([ma_sinhvien])  on update no action on delete no action 
go
Alter table [DangKy] add  foreign key([ma_sinhvien]) references [DocGia] ([ma_sinhvien])  on update no action on delete no action 
go
Alter table [QuaTrinhMuon] add  foreign key([ma_sinhvien]) references [DocGia] ([ma_sinhvien])  on update no action on delete no action 
go
Alter table [QuaTrinhMuon] add  foreign key([ma_nhanvien_dua]) references [NhanVien] ([ma_nhanvien])  on update no action on delete no action 
go
Alter table [Muon] add  foreign key([ma_nhanvien_dua]) references [NhanVien] ([ma_nhanvien])  on update no action on delete no action 
go
Alter table [QuaTrinhMuon] add  foreign key([ma_nhanvien_nhan]) references [NhanVien] ([ma_nhanvien])  on update no action on delete no action 
go
Alter table [NhanVien] add  foreign key([quyen]) references [Quyen] ([quyen])  on update no action on delete no action 
go
Alter table [NhanVien] add  foreign key([ma_ChiNhanh]) references [ChiNhanh] ([ma_ChiNhanh])  on update no action on delete no action 
go
Alter table [BanSao] add  foreign key([ma_ChiNhanh]) references [ChiNhanh] ([ma_ChiNhanh])  on update no action on delete no action 
go
-- remove linkedservers if exist
exec sp_dropserver @server = 'QLTV_MAY_CHU', @droplogins = 'droplogins'
exec sp_dropserver @server = 'QLTV_TRAM_1', @droplogins = 'droplogins'
exec sp_dropserver @server = 'QLTV_TRAM_2', @droplogins = 'droplogins'
exec sp_dropserver @server = 'QLTV_TRAM_3', @droplogins = 'droplogins'
exec sp_dropserver @server = 'QLTV_TRAM_4', @droplogins = 'droplogins'
go

-- EXEC master.dbo.sp_addlinkedserver
-- @server = N'QLTV_MAY_CHU',
-- @provider = N'SQLOLEDB',
-- @datasrc = N'192.168.43.223\WIN-MD7EJ65P9NA\SQLEXPRESS, 1433',
-- @srvproduct = ''
-- go
-- EXEC master.dbo.sp_addlinkedserver
-- @server = N'QLTV_TRAM_1',
-- @provider = N'SQLOLEDB',
-- @datasrc = N'192.168.43.24\DESKTOP-NE6TTO8\SQLEXPRESS, 1433',
-- @srvproduct = ''
-- go
-- --EXEC master.dbo.sp_addlinkedserver
-- --@server = N'QLTV_TRAM_2',
-- --@provider = N'SQLOLEDB',
-- --@datasrc = N'192.168.43.176\WIN-MD7EJ65P9NA\SQLEXPRESS, 1433',
-- --@srvproduct = ''
-- go
-- EXEC master.dbo.sp_addlinkedserver
-- @server = N'QLTV_TRAM_3',
-- @provider = N'SQLOLEDB',
-- @datasrc = N'192.168.43.104\WIN-MD7EJ65P9NA\SQLEXPRESS, 1433',
-- @srvproduct = ''
-- go
-- EXEC master.dbo.sp_addlinkedserver
-- @server = N'QLTV_TRAM_4',
-- @provider = N'SQLOLEDB',
-- @datasrc = N'192.168.43.94\WIN-MD7EJ65P9NA\SQLEXPRESS, 1433',
-- @srvproduct = ''
-- go
-- -- conenct to server
-- EXEC master.dbo.sp_addlinkedsrvlogin
-- @rmtsrvname = N'QLTV_MAY_CHU',
-- @useself = N'False',
-- @locallogin = NULL,
-- @rmtuser = N'duy',
-- @rmtpassword = 'duy'
-- go
-- EXEC master.dbo.sp_addlinkedsrvlogin
-- @rmtsrvname = N'QLTV_TRAM_1',
-- @useself = N'False',
-- @locallogin = NULL,
-- @rmtuser = N'duy',
-- @rmtpassword = 'duy'
-- go
-- --EXEC master.dbo.sp_addlinkedsrvlogin
-- --@rmtsrvname = N'QLTV_TRAM_2',
-- --@useself = N'False',
-- --@locallogin = NULL,
-- --@rmtuser = N'duy',
-- --@rmtpassword = 'duy'
-- go
-- EXEC master.dbo.sp_addlinkedsrvlogin
-- @rmtsrvname = N'QLTV_TRAM_3',
-- @useself = N'False',
-- @locallogin = NULL,
-- @rmtuser = N'duy',
-- @rmtpassword = 'duy'
-- go
-- EXEC master.dbo.sp_addlinkedsrvlogin
-- @rmtsrvname = N'QLTV_TRAM_4',
-- @useself = N'False',
-- @locallogin = NULL,
-- @rmtuser = N'duy',
-- @rmtpassword = 'duy'
