use qltv
go
create proc sp_get_dsNhanVien
as
begin
	select * from NhanVien
end

go
alter proc sp_get_quyen
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
		if (@quyen = 'admin') 
		begin
			set @ma_nv = @quyen + cast((select count(*) from NhanVien where quyen= @quyen)+ 1 as varchar(20))
		end
		if (@quyen = 'nhanvien')
		begin
			set @ma_nv =@quyen + cast((select count(*) from NhanVien where @quyen = quyen and @ma_chinhanh = ma_ChiNhanh)+ 1 as varchar(20)) + @ma_chinhanh
		end
		else if  exists (select * from NhanVien where ma_ChiNhanh = @ma_chinhanh and @quyen = 'thuthu')
		begin
			set @ma_nv = @quyen + @ma_chinhanh
		end
	--Thêm admin
		insert into NhanVien values (@ma_nv,@quyen,@ma_chinhanh,@matkhau,@sdt)
	--Thêm admin vào các trạm khác
		--insert into QLTV_TRAM_1.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
		--insert into QLTV_TRAM_2.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
		--insert into QLTV_TRAM_3.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
		--insert into QLTV_TRAM_4.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
end
go 
create proc sp_update_nhanvien
			@ma_nv varchar(20),
			@quyen varchar(15),
			@ma_chinhanh varchar(20),
			@matkhau varchar(50),
			@sdt varchar(50)
as
begin
		if not exists (select * from NhanVien where ma_nhanvien = @ma_nv)
		begin
			raiserror('Không tồn tại nhân viên này',16,1)
			return
		end
		update NhanVien
		set quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
		where ma_nhanvien = @ma_nv
		
		--update QLTV_TRAM_1.qltv.dbo.NhanVien 
		--set quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
		--where ma_nhanvien = @ma_nv
		
		--update QLTV_TRAM_2.qltv.dbo.NhanVien 
		--set quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
		--where ma_nhanvien = @ma_nv
		
		--update QLTV_TRAM_3.qltv.dbo.NhanVien 
		--set quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
		--where ma_nhanvien = @ma_nv
		
		--update QLTV_TRAM_4.qltv.dbo.NhanVien 
		--set quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
		--where ma_nhanvien = @ma_nv
		
end

use qltv
select quyen from NhanVien
				where ma_nhanvien in (select ORIGINAL_LOGIN())

				select * from NhanVien
