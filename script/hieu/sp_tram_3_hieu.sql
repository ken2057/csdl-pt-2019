use qltv
go
create proc sp_get_dsNhanVien
as
begin
	select * from NhanVien
end

go
create proc sp_get_quyen
as
begin
	select quyen from Quyen 
end

go
create proc sp_get_chinhanh
as
begin
	select ma_ChiNhanh from ChiNhanh 
end

go
alter proc sp_add_nhanvien	
			@quyen varchar(15),
			@ma_chinhanh varchar(20),
			@matkhau varchar(50),
			@sdt varchar(50)
as
begin
	--Tạo mã nhân viên 
	declare @ma_nv varchar(20)
	if exists (select * from NhanVien where ma_ChiNhanh = @ma_chinhanh and quyen = 'nhanvien') 
		set @ma_nv =@quyen + cast((select count(*) from NhanVien where ma_ChiNhanh= 'TS' and quyen='nhanvien')+ 1 as varchar(20)) + @ma_chinhanh
	--Thêm nhân viên ở trạm 3
		insert into NhanVien values (@ma_nv,@quyen,@ma_chinhanh,@matkhau,@sdt)
	--Thêm nhân viên vào máy chủ
		--insert into QLTV_MAY_CHU.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
		
end

