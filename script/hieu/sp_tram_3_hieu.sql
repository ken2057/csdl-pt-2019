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
	where quyen = 'nhanvien' and quyen='thuthu'
end

go
create proc sp_get_chinhanh
as
begin
	select ma_ChiNhanh from ChiNhanh 
	where ma_ChiNhanh ='TS'
end

go
create proc sp_add_nhanvien	
			@quyen varchar(15),
			@ma_chinhanh varchar(20),
			@matkhau varchar(50),
			@sdt varchar(50)
as
begin
	if (select quyen from NhanVien
				where ma_nhanvien in (select ORIGINAL_LOGIN())) = 'admin'
	begin
	--Tạo mã nhân viên 
	declare @ma_nv varchar(20)
		if (@quyen = 'admin') 
		begin
			set @ma_nv = @quyen + cast((select count(*) from NhanVien where quyen= @quyen)+ 1 as varchar(20))
		end
		if (@quyen = 'nhanvien')
		begin
			set @ma_nv =@quyen + cast((select count(*) from NhanVien where @quyen = quyen and @ma_chinhanh = ma_ChiNhanh)+ 1 as varchar(20)) + @ma_chinhanh
		end
		if (@quyen = 'thuthu')
		begin
			set @ma_nv = @quyen + @ma_chinhanh
		end
	--Thêm nhân viên ở trạm 3
	insert into NhanVien values (@ma_nv,@quyen,@ma_chinhanh,@matkhau,@sdt)
	--Thêm nhân viên ở máy chủ
	insert into QLTV_MAY_CHU.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
	end
		else
		begin
			raiserror('Bạn không thể thực hiện chức năng này',16,1)
		end
end

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
		if not exists (select * from NhanVien where @ma_nv = ma_nhanvien)
			begin
				raiserror ('Không tồn tại nhân viên này',16,1)
			end
		--Cập nhật nếu quyền là admin
		if (@quyen = 'admin') 
		begin
			--Cập nhật trạm 3
			update NhanVien
			set ma_nhanvien = @quyen + cast((select count(*) from NhanVien where @quyen= quyen )+ 1 as varchar(20)),quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 

			--Cập nhật máy chủ
			update QLTV_MAY_CHU.qltv.dbo.NhanVien
			set ma_nhanvien = @quyen + cast((select count(*) from NhanVien where @quyen= quyen )+ 1 as varchar(20)),quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 
		end
		--cập nhật mã nếu quyền là nhân viên
		if (@quyen = 'nhanvien')
		begin
			--Cập nhật trạm 3
			update Nhanvien
			set ma_nhanvien = @quyen + cast((select count(*) from NhanVien where @quyen = quyen and @ma_chinhanh = ma_ChiNhanh)+ 1 as varchar(20)) + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien  
			--Cập nhật máy chủ
			update QLTV_MAY_CHU.qltv.dbo.NhanVien
			set ma_nhanvien = @quyen + cast((select count(*) from NhanVien where @quyen = quyen and @ma_chinhanh = ma_ChiNhanh)+ 1 as varchar(20)) + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien  
		end
		--cập nhật mã nếu quyền là thủ thư
		if (@quyen = 'thuthu')
		begin
			--Cập nhật trạm 3
			update Nhanvien
			set ma_nhanvien =@quyen + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 
			--Cập nhật máy chủ
			update QLTV_MAY_CHU.qltv.dbo.NhanVien
			set ma_nhanvien =@quyen + @ma_chinhanh,
			quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 
		end
		end
		else
			begin
				raiserror('Bạn không thể thực hiện chức năng này',16,1)
			end
end
