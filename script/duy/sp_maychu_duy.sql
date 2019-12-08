use qltv
go
create proc sp_get_dsLoai
as
begin
	-- do LoaiTaiLieu nằm ở tất cả các trạm, nên không cần search các trạm khác
	select * from LoaiTaiLieu
end
go
create proc sp_update_loai
	@maLoai varchar(20),
	@tenLoai nvarchar(20),
	@ghiChu ntext
as
begin
	if not exists (select * from LoaiTaiLieu where ma_loai = @maLoai)
	begin
		raiserror('Không tồn tại loại này', 16, 1)
		return
	end
	-- update ở trạm hiện tại (Máy chủ)
	update LoaiTaiLieu
	set ten_loai = @tenLoai, ghichu = @ghiChu
	where ma_loai = @maLoai
	--------- Do hiện tại chỉ code ở local nên k cần chạy mấy cái dưới
	--------- nhưng vẫn phải ghi ra để mốt test
	-- update ở trạm khác
	update QLTV_TRAM_1.qltv.dbo.LoaiTaiLieu
	set ten_loai = @tenLoai, ghichu = @ghiChu
	where ma_loai = @maLoai
	--
	update QLTV_TRAM_2.qltv.dbo.LoaiTaiLieu
	set ten_loai = @tenLoai, ghichu = @ghiChu
	where ma_loai = @maLoai
	--
	update QLTV_TRAM_3.qltv.dbo.LoaiTaiLieu
	set ten_loai = @tenLoai, ghichu = @ghiChu
	where ma_loai = @maLoai
	--
	update QLTV_TRAM_4.qltv.dbo.LoaiTaiLieu
	set ten_loai = @tenLoai, ghichu = @ghiChu
	where ma_loai = @maLoai
end
go
create proc sp_add_loai
	@tenLoai nvarchar(20),
	@ghiChu ntext
as
begin
	-- tạo mã loại
	declare @ma varchar(20)
	set @ma = (select count(*) from LoaiTaiLieu)+1 + ''
	-- insert
	insert into LoaiTaiLieu values (@ma, @tenLoai, @ghiChu)
	--------- Do hiện tại chỉ code ở local nên k cần chạy mấy cái dưới
	--------- nhưng vẫn phải ghi ra để mốt test
	insert into QLTV_TRAM_1.qltv.dbo.LoaiTaiLieu values (@ma, @tenLoai, @ghiChu)
	insert into QLTV_TRAM_2.qltv.dbo.LoaiTaiLieu values (@ma, @tenLoai, @ghiChu)
	insert into QLTV_TRAM_3.qltv.dbo.LoaiTaiLieu values (@ma, @tenLoai, @ghiChu)
	insert into QLTV_TRAM_4.qltv.dbo.LoaiTaiLieu values (@ma, @tenLoai, @ghiChu)
end
go
create proc sp_delete_loai
	@ma_loai varchar(20)
as
begin
	-- kiểm tra đang sử dụng
	if exists (select * from TaiLieu where ma_loai = @ma_loai)
	begin
		raiserror('Loại đang gắn vào Tài Liệu, không thể xoá', 16, 1)
		return
	end
	--
	delete LoaiTaiLieu where ma_loai = @ma_loai
	delete QLTV_TRAM_1.qltv.dbo.LoaiTaiLieu where ma_loai = @ma_loai
	delete QLTV_TRAM_2.qltv.dbo.LoaiTaiLieu where ma_loai = @ma_loai
	delete QLTV_TRAM_3.qltv.dbo.LoaiTaiLieu where ma_loai = @ma_loai
	delete QLTV_TRAM_4.qltv.dbo.LoaiTaiLieu where ma_loai = @ma_loai
end
go
create proc sp_get_dsNhanVien_tram
as
begin
	select ma_nhanvien, quyen, CN.diachi, sdt
	from NhanVien NV, ChiNhanh CN
	where NV.ma_ChiNhanh = CN.ma_ChiNhanh 
end
go
create proc sp_get_ThongTin_TaiLieu
as
begin
	select ma_tailieu, ten_tailieu, ten_loai, tinhtrang, gia, ngonngu
	from (select ma_tailieu, ma_loai, ngonngu, tinhtrang, gia, ten_tailieu from TaiLieu) TL, 
			LoaiTaiLieu L
	where TL.ma_loai = L.ma_loai
		and TL.ma_tailieu in (select ma_tailieu from BanSao)
end
go
create proc sp_get_bansao_sl
as
begin
	select distinct ma_tailieu, count(tinhtrang) as sl
	from BanSao
	group by ma_tailieu
end
go
create proc sp_get_lsMuon_taiLieu
	@maTaiLieu varchar(20),
	@maNV varchar(20),
	@ma_sinhvien varchar(20),
	@treHan varchar(10)
as
begin
	if @treHan = 'tre'
		select ma_tailieu, ma_bansao, ngay_hethan, ngay_muon, ngay_tra, ma_sinhvien, ma_nhanvien_dua, ma_nhanvien_nhan
		from QuaTrinhMuon
		where ma_tailieu like '%'+@maTaiLieu+'%'
			and (ma_nhanvien_dua like '%'+@maNV+'%' or ma_nhanvien_nhan like '%'+@maNV+'%')
			and ma_sinhvien like '%'+@ma_sinhvien+'%'
			and ngay_tra > ngay_hethan
	else if @treHan = 'khong tre'
		select ma_tailieu, ma_bansao, ngay_hethan, ngay_muon, ngay_tra, ma_sinhvien, ma_nhanvien_dua, ma_nhanvien_nhan
		from QuaTrinhMuon
		where ma_tailieu like '%'+@maTaiLieu+'%'
			and (ma_nhanvien_dua like '%'+@maNV+'%' or ma_nhanvien_nhan like '%'+@maNV+'%')
			and ma_sinhvien like '%'+@ma_sinhvien+'%'
			and ngay_tra <= ngay_hethan
	else
		select ma_tailieu, ma_bansao, ngay_hethan, ngay_muon, ngay_tra, ma_sinhvien, ma_nhanvien_dua, ma_nhanvien_nhan
		from QuaTrinhMuon
		where ma_tailieu like '%'+@maTaiLieu+'%'
			and (ma_nhanvien_dua like '%'+@maNV+'%' or ma_nhanvien_nhan like '%'+@maNV+'%')
			and ma_sinhvien like '%'+@ma_sinhvien+'%'
end
go