use qltv
go
create proc sp_get_dsTacGia
as
begin
	--TacGia chi co o may chu va tram 1
	--search o may chu
	--select * from QLTV_MAY_CHU.qltv.dbo.TacGia order by ten_tacgia ASC
	--search o tram 1
	--select * from QLTV_TRAM_1.qltv.dbo.TacGia order by ten_tacgia ASC
end
go

create proc sp_add_tacgia
					@tentacgia nvarchar(100),
					@ghichu ntext
as
begin
	declare @ma varchar(20)
	set @ma = (select count(*) from TacGia) + 1 + ''
	if not exists (select * from TacGia where ma_tacgia = @ma)
	begin
		raiserror('Không tồn tại tác giả này',16,1)
		return
	end
	begin tran
		--insert into QLTV_MAY_CHU.qltv.dbo.TacGia values(@ma, @tentacgia, @ghichu)
		--insert into QLTV_TRAM_1.qltv.dbo.TacGia values(@ma, @tentacgia, @ghichu)
	commit tran
end
go

create proc sp_update_tacgia
					@ma varchar(20),
					@ten nvarchar(100),
					@ghichu ntext
as
begin
	if not exists (select * from TacGia where ma_tacgia = @ma)
	begin
		raiserror('Không tồn tại tác giả này',16,1)
		return
	end
	begin tran
		update QLTV_MAY_CHU.qltv.dbo.TacGia
		set ten_tacgia = @ten, ghichu = @ghichu
		where ma_tacgia = @ma

		--update QLTV_TRAM_1.qltv.dbo.TacGia
		--set ten_tacgia = @ten, ghichu = @ghichu
		--where ma_tacgia = @ma
	commit tran
end
go
create proc sp_xoa_tacgia
						@ma varchar(20)
as
begin
	if not exists (select * from TacGia where ma_tacgia = @ma)
	begin
		raiserror('Không tồn tại tác giả này',16,1)
		return
	end
	begin tran
		--Delete from QLTV_MAY_CHU.qltv.dbo.TacGia where ma_tacgia = @ma
		--Delete from QLTV_TRAM_1.qltv.dbo.TacGia where ma_tacgia = @ma
	commit tran
end
go