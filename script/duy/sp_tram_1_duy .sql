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
	-- update ở trạm hiện tại (trạm 1)
	update LoaiTaiLieu
	set ten_loai = @tenLoai, ghichu = @ghiChu
	where ma_loai = @maLoai
	
	--update ở trạm khác
	update QLTV_MAY_CHU.qltv.dbo.LoaiTaiLieu
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
	
	insert into QLTV_MAY_CHU.qltv.dbo.LoaiTaiLieu values (@ma, @tenLoai, @ghiChu)
	insert into QLTV_TRAM_2.qltv.dbo.LoaiTaiLieu values (@ma, @tenLoai, @ghiChu)
	insert into QLTV_TRAM_3.qltv.dbo.LoaiTaiLieu values (@ma, @tenLoai, @ghiChu)
	insert into QLTV_TRAM_4.qltv.dbo.LoaiTaiLieu values (@ma, @tenLoai, @ghiChu)
end