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
	--Tạo mã admin
	declare @ma_nv varchar(20)
	if exists (select * from NhanVien where ma_ChiNhanh = @ma_chinhanh and quyen = 'admin') 
		set @ma_nv =@quyen + cast((select count(*) from NhanVien where ma_ChiNhanh= @ma_chinhanh and quyen='admin')+ 1 as varchar(20))
	--Thêm admin
		insert into NhanVien values (@ma_nv,@quyen,@ma_chinhanh,@matkhau,@sdt)
	--Thêm admin vào các trạm khác
		--insert into QLTV_TRAM_1.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
		--insert into QLTV_TRAM_2.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
		--insert into QLTV_TRAM_3.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
		--insert into QLTV_TRAM_4.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
end
