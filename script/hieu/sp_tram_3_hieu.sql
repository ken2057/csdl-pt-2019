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
	--Tạo mã nhân viên 
	declare @ma_nv varchar(20)
		if (@quyen = 'nhanvien')
		begin
			set @ma_nv =@quyen + cast((select count(*) from NhanVien where quyen= @quyen)+ 1 as varchar(20)) + @ma_chinhanh
		end
		else if (@quyen = 'thuthu')
		begin
			set @ma_nv = @quyen + @ma_chinhanh
		end
	--Thêm nhân viên ở trạm 3
	insert into NhanVien values (@ma_nv,@quyen,@ma_chinhanh,@matkhau,@sdt)
	--Thêm nhân viên ở máy chủ
	--insert into QLTV_MAY_CHU.qltv.dbo.NhanVien values (@ma_nv, @quyen, @ma_chinhanh,@matkhau,@sdt)
end

create proc sp_update_nhanvien
			@ma_nv varchar(20),
			@quyen varchar(15),
			@ma_chinhanh varchar(20),
			@matkhau varchar(50),
			@sdt varchar(50)
as
begin
		if not exists (select * from NhanVien where @ma_nv = ma_nhanvien)
			begin
				raiserror ('Không tồn tại nhân viên này',16,1)
			end
		--cập nhật mã nếu quyền là nhân viên
		if (@quyen = 'nhanvien')
		begin
			update Nhanvien
			set ma_nhanvien =@quyen + cast((select count(*) from NhanVien where quyen= @quyen)+ 1 as varchar(20)) + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			where @ma_nv = ma_nhanvien 

			--update QLTV_MAY_CHU.qltv.dbo.NhanVien
			--set ma_nhanvien =@quyen + cast((select count(*) from NhanVien where quyen= @quyen)+ 1 as varchar(20)) + @ma_chinhanh,
			--	quyen = @quyen, ma_chinhanh = @ma_chinhanh ,matkhau = @matkhau
			--where @ma_nv = ma_nhanvien 
		end
		--cập nhật mã nếu quyền là thủ thư
		if (@quyen = 'thuthu')
		begin
			update Nhanvien
			set ma_nhanvien =@quyen + @ma_chinhanh,
				quyen = @quyen, ma_chinhanh = @ma_chinhanh, sdt = @sdt, matkhau = @matkhau
			where @ma_nv = ma_nhanvien 
			
			--update QLTV_MAY_CHU.qltv.dbo.NhanVien
			--set ma_nhanvien =@quyen + @ma_chinhanh,
			--	quyen = @quyen, ma_chinhanh = @ma_chinhanh, sdt = @sdt, matkhau = @matkhau
			--where @ma_nv = ma_nhanvien 
		end
end

--Kiểm tra thủ thư có tồn tại ở trạm 3 không
create trigger trg_check_thuthu
on NhanVien
for insert,update
as
begin
	declare @ma_nv varchar(20),
			@quyen varchar(15)
	select @ma_nv = ma_nhanvien
	from inserted
	if exists ( select * from NhanVien where @quyen = 'thuthu')
	raiserror('Đã tồn tại thủ ở trạm này',16,1)
	rollback tran
end