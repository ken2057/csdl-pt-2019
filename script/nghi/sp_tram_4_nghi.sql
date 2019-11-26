use qltv
go
create proc sp_get_dsDocGia
as
begin
	select * from DocGia
end
go

create proc sp_add_docgia
					@maSinhVien varchar(20),
					@hoTen nvarchar(50),
					@ngaySinh datetime,
					@diaChi varchar(50),
					@sDT varchar(15)
as
begin
	
	insert into DocGia values(@maSinhVien, @hoTen, @ngaySinh,@diaChi,@sDT)
	insert into QLTV_MAY_CHU.qltv.dbo.DocGia values (@maSinhVien, @hoTen, @ngaySinh,@diaChi,@sDT)
	insert into QLTV_TRAM_1.qltv.dbo.DocGia values (@maSinhVien, @hoTen, @ngaySinh,@diaChi,@sDT)
	insert into QLTV_TRAM_2.qltv.dbo.DocGia values (@maSinhVien, @hoTen, @ngaySinh,@diaChi,@sDT)
	insert into QLTV_TRAM_3.qltv.dbo.DocGia values (@maSinhVien, @hoTen, @ngaySinh,@diaChi,@sDT)
end

go

create proc sp_update_docgia
					@maSinhVien varchar(20),
					@hoTen nvarchar(50),
					@ngaySinh datetime,
					@diaChi varchar(50),
					@sDT varchar(15)
as
begin
	if not exists (select * from DocGia where ma_sinhvien = @maSinhVien)
	begin
		raiserror(N'Không tồn tại độc giả này',16,1)
		return
	end

	update DocGia
	set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
	where ma_sinhvien = @maSinhVien

	--update QLTV_MAY_CHU.qltv.dbo.DocGia
	--set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
	--where ma_sinhvien = @maSinhVien

	--update QLTV_TRAM_1.qltv.dbo.DocGia
	--set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
	--where ma_sinhvien = @maSinhVien

	--update QLTV_TRAM_2.qltv.dbo.DocGia
	--set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
	--where ma_sinhvien = @maSinhVien

	--update QLTV_TRAM_3.qltv.dbo.DocGia
	--set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
	--where ma_sinhvien = @maSinhVien
end
