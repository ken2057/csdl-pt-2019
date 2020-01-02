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
go
create proc sp_add_muon @ma_tailieu varchar(20),
						@ma_bansao varchar(20),
						@ma_nhanvien_dua varchar(20),
						@ma_sinhvien varchar(20),						
						@tien_datcoc money
as
begin
	declare @ngay_muon datetime, @ngay_hethan datetime
	set @ngay_muon = getdate()
	set @ngay_hethan =  @ngay_muon + day(10)
	if not exists (select * from NhanVien where @ma_nhanvien_dua = ma_nhanvien)
		begin
			raiserror('Nhan vien khong ton tai',16,1)
			return
		end
	if not exists (select * from TaiLieu where @ma_tailieu = ma_tailieu)
		begin
			raiserror('Tai lieu khong ton tai',16,1)
			return
		end
	if not exists (select * from DocGia where @ma_sinhvien = ma_sinhvien)
		begin
			raiserror('Doc gia khong ton tai',16,1)
			return
		end
	if not exists (select * from BanSao where @ma_bansao = ma_bansao)
		begin
			raiserror('Ban sao khong ton tai',16,1)
			return
		end
	if not exists (select * from Muon where @ma_tailieu = @ma_tailieu and @ma_sinhvien = ma_sinhvien)
		begin
			raiserror('Doc gia da muon sach nay roi',16,1)
			return
		end
	insert into Muon values (@ma_tailieu,@ma_bansao,@ma_nhanvien_dua,@ma_sinhvien,@ngay_muon,@ngay_hethan,@tien_datcoc)
	insert into QLTV_MAY_CHU.qltv.dbo.Muon values (@ma_tailieu,@ma_bansao,@ma_nhanvien_dua,@ma_sinhvien,@ngay_muon,@ngay_hethan,@tien_datcoc)
end


go 
create proc sp_update_Muon  @ma_tailieu varchar(20),
							@ma_bansao varchar(20),
							@ma_nhanvien_dua varchar(20),
							@ma_sinhvien varchar(20),
							@tien_datcoc money
as 
begin
	--Tram 3
	update Muon
	set ma_tailieu = @ma_tailieu, 
		ma_bansao = @ma_bansao,
		ma_nhanvien_dua = @ma_nhanvien_dua,
		ma_sinhvien = @ma_sinhvien,
		tien_datcoc = @tien_datcoc
	where @ma_tailieu = ma_tailieu
	and @ma_sinhvien = ma_sinhvien
	--May chu
	update QLTV_MAY_CHU.qltv.dbo.Muon
	set ma_tailieu = @ma_tailieu, 
		ma_bansao = @ma_bansao,
		ma_nhanvien_dua = @ma_nhanvien_dua,
		ma_sinhvien = @ma_sinhvien,
		tien_datcoc = @tien_datcoc
	where @ma_tailieu = ma_tailieu
	and @ma_sinhvien = ma_sinhvien
end

go 
create proc sp_delete_Muon   @ma_tailieu varchar(20),
								@ma_bansao varchar(20),
								@ma_sinhvien varchar(20),
								@ma_nhanvien_dua varchar(20),
								@tien_datcoc money
as
begin
	begin try
		declare @ngay_muon datetime , @ngay_hethan datetime
		select @ngay_muon = ngay_muon, @ngay_hethan =ngay_hethan
		from Muon
		where ma_tailieu = @ma_tailieu and ma_bansao = @ma_bansao and @ma_sinhvien = ma_sinhvien 

		begin tran
			insert into QuaTrinhMuon(ma_sinhvien,ma_bansao,ma_nhanvien_dua,ma_nhanvien_nhan,ngay_muon,ngay_hethan,ngay_tra,tien_muon,tien_datcoc,tien_datra,ghichu)
			values(@ma_sinhvien,@ma_bansao,@ma_nhanvien_dua,'',@ngay_muon,@ngay_hethan,'','',@tien_datcoc,'','')
			delete from Muon where ma_tailieu = @ma_tailieu and ma_bansao = ma_bansao 
			-- May chu
			insert into QLTV_MAY_CHU.qltv.dbo.QuaTrinhMuon(ma_sinhvien,ma_bansao,ma_nhanvien_dua,ma_nhanvien_nhan,ngay_muon,ngay_hethan,ngay_tra,tien_muon,tien_datcoc,tien_datra,ghichu)
			values(@ma_sinhvien,@ma_bansao,@ma_nhanvien_dua,'',@ngay_muon,@ngay_hethan,'','',@tien_datcoc,'','')		
			delete from QLTV_MAY_CHU.qltv.dbo.Muon where ma_tailieu = @ma_tailieu and ma_bansao = ma_bansao 
		commit tran 
	end try
	begin catch
		begin
			raiserror('Loi',16,1)
			rollback tran
			return
		end
	end catch
end
go
create proc sp_update_qtMuon @ma_nhanvien_nhan varchar(20),
							 @tien_muon money,
							 @tien_datra money,
							 @ghi_chu ntext,
							 @ngay_tra datetime
as
begin
	declare @ma_sinhvien varchar(20)
	update QuaTrinhMuon
	set ma_nhanvien_nhan = @ma_nhanvien_nhan,
		tien_muon = @tien_muon,
		tien_datra = @tien_datra,
		ghichu = @ghi_chu,
		ngay_tra = @ngay_tra
	where
		ma_sinhvien = @ma_sinhvien
	--May chu
	update QLTV_MAY_CHU.qltv.dbo.QuaTrinhMuon
	set ma_nhanvien_nhan = @ma_nhanvien_nhan,
		tien_muon = @tien_muon,
		tien_datra = @tien_datra,
		ghichu = @ghi_chu,
		ngay_tra = @ngay_tra
	where
		ma_sinhvien = @ma_sinhvien
end