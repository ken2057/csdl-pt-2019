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

create proc sp_add_nhanvien	
			@quyen varchar(15),
			@ma_chinhanh varchar(20),
			@matkhau varchar(50),
			@sdt varchar(50)
as
begin
	--Check quyền đăng nhập 
	if (select quyen from NhanVien
				where ma_nhanvien in (select ORIGINAL_LOGIN())) = 'admin'
	begin
	--Tạo mã admin
	declare @ma_nv varchar(20)
		if exists (select * from NhanVien where ma_nhanvien = @ma_nv) 
		begin
			raiserror('Mã nhân viên đã tồn tại',16,1)
			return
		end 
		if (@quyen = 'admin') 
		begin
			set @ma_nv = @quyen + cast((select count(*) from NhanVien where @quyen= quyen)+ 1 as varchar(20))
		end
		if (@quyen = 'nhanvien')
		begin
			set @ma_nv =@quyen + cast((select count(*) from NhanVien where @quyen = quyen and @ma_chinhanh = ma_ChiNhanh)+ 1 as varchar(20)) + @ma_chinhanh
		end
		else if (@quyen = 'thuthu')
		begin
			set @ma_nv = @quyen + @ma_chinhanh
		end
	--Thêm admin
		insert into NhanVien values (@ma_nv,@quyen,@ma_chinhanh,@matkhau,@sdt)
	--Thêm admin vào các trạm khác
		insert into QLTV_TRAM_1.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
		insert into QLTV_TRAM_2.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
		insert into QLTV_TRAM_3.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
		insert into QLTV_TRAM_4.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
	end
	else 
		begin
		raiserror ('Bạn không thể thực hiện chức năng này',16,1)
		rollback tran
		end
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
		if (select quyen from NhanVien
				where ma_nhanvien in (select ORIGINAL_LOGIN())) = 'admin'
		begin
		if exists (select * from NhanVien where ma_nhanvien = @ma_nv)
		begin
			raiserror('Mã nhân viên này đã tồn tại',16,1)
			return
		end
		--Cập nhật nếu quyền là admin
		if (@quyen = 'admin') 
		begin
			--Cập nhật máy chủ
			update NhanVien
			set ma_nhanvien = @quyen + cast((select count(*) from NhanVien where @quyen= quyen )+ 1 as varchar(20)),quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 
			--Cập nhật trạm 1
			update QLTV_TRAM_1.qltv.dbo.NhanVien
			set ma_nhanvien = @quyen + cast((select count(*) from NhanVien where @quyen= quyen )+ 1 as varchar(20)),quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 
			-- Cập nhật trạm 2
			update QLTV_TRAM_2.qltv.dbo.NhanVien
			set ma_nhanvien = @quyen + cast((select count(*) from NhanVien where @quyen= quyen )+ 1 as varchar(20)),quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 
			--Cập nhật trạm 3
			update QLTV_TRAM_3.qltv.dbo.NhanVien
			set ma_nhanvien = @quyen + cast((select count(*) from NhanVien where @quyen= quyen )+ 1 as varchar(20)),quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 
			-- Cập nhật trạm 4
			update QLTV_TRAM_4.qltv.dbo.NhanVien
			set ma_nhanvien = @quyen + cast((select count(*) from NhanVien where @quyen= quyen )+ 1 as varchar(20)),quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 
		end
		--Cập nhật nếu quyền là nhân viên
		if (@quyen = 'nhanvien')
		begin
			--Cập nhật máy chủ
			update Nhanvien
			set ma_nhanvien = @quyen + cast((select count(*) from NhanVien where @quyen = quyen and @ma_chinhanh = ma_ChiNhanh)+ 1 as varchar(20)) + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 

			--Cập nhật trạm 1
			update QLTV_TRAM_1.qltv.dbo.NhanVien
			set ma_nhanvien = @quyen + cast((select count(*) from NhanVien where @quyen = quyen and @ma_chinhanh = ma_ChiNhanh)+ 1 as varchar(20)) + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien

			-- Cập nhật trạm 2
			update QLTV_TRAM_2.qltv.dbo.NhanVien
			set ma_nhanvien = @quyen + cast((select count(*) from NhanVien where @quyen = quyen and @ma_chinhanh = ma_ChiNhanh)+ 1 as varchar(20)) + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien

			-- Cập nhật trạm 3
			update QLTV_TRAM_3.qltv.dbo.NhanVien
			set ma_nhanvien = @quyen + cast((select count(*) from NhanVien where @quyen = quyen and @ma_chinhanh = ma_ChiNhanh)+ 1 as varchar(20)) + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien

			-- Cập nhật trạm 4
			update QLTV_TRAM_4.qltv.dbo.NhanVien
			set ma_nhanvien = @quyen + cast((select count(*) from NhanVien where @quyen = quyen and @ma_chinhanh = ma_ChiNhanh)+ 1 as varchar(20)) + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien
		end
		--cập nhật mã nếu quyền là thủ thư
		if (@quyen = 'thuthu')
		begin
			--Cập nhật máy chủ
			update Nhanvien
			set ma_nhanvien =@quyen + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 
			--Cập nhật trạm 1
			update QLTV_TRAM_1.qltv.dbo.NhanVien
			set ma_nhanvien =@quyen + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 

			-- Cập nhật trạm 2
			update QLTV_TRAM_2.qltv.dbo.NhanVien
			set ma_nhanvien =@quyen + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 

			-- Cập nhật trạm 3
			update QLTV_TRAM_3.qltv.dbo.NhanVien
			set ma_nhanvien =@quyen + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 

			-- Cập nhật trạm 4
			update QLTV_TRAM_4.qltv.dbo.NhanVien
			set ma_nhanvien =@quyen + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 
		end
		end
			else 
				begin
					raiserror ('Bạn không thể thực hiện chức năng này',16,1)
				end
end
go