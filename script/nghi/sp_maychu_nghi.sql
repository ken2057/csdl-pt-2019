﻿use qltv
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
	-- insert into QLTV_TRAM_1.qltv.dbo.DocGia values (@maSinhVien, @hoTen, @ngaySinh,@diaChi,@sDT)
	-- insert into QLTV_TRAM_2.qltv.dbo.DocGia values (@maSinhVien, @hoTen, @ngaySinh,@diaChi,@sDT)
	-- insert into QLTV_TRAM_3.qltv.dbo.DocGia values (@maSinhVien, @hoTen, @ngaySinh,@diaChi,@sDT)
	-- insert into QLTV_TRAM_4.qltv.dbo.DocGia values (@maSinhVien, @hoTen, @ngaySinh,@diaChi,@sDT)
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
	else
	begin
		update DocGia
		set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
		where ma_sinhvien = @maSinhVien
		select * from DocGia where ma_sinhvien = @maSinhVien
	end
	--update QLTV_TRAM_1.qltv.dbo.DocGia
	--set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
	--where ma_sinhvien = @maSinhVien

	--update QLTV_TRAM_2.qltv.dbo.DocGia
	--set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
	--where ma_sinhvien = @maSinhVien

	--update QLTV_TRAM_3.qltv.dbo.DocGia
	--set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
	--where ma_sinhvien = @maSinhVien

	--update QLTV_TRAM_4.qltv.dbo.DocGia
	--set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
	--where ma_sinhvien = @maSinhVien
end

go
create proc sp_get_dsTaiLieu
as
begin
	select ten_tailieu, 
		(select ten_tacgia from TacGia where TacGia.ma_tacgia = TaiLieu.ma_tacgia_1) as ten_tacgia, 
		ngonngu ,sl_kho 
	from TaiLieu
end

go

create proc sp_get_tailieu
						@maTaiLieu varchar(20)
as
begin
	if not exists (select * from TaiLieu where ma_tailieu = @maTaiLieu)
	begin
		raiserror(N'Không tồn tại tài liệu này',16,1)
		return
	end
	select  
		ma_tailieu,
		ten_tailieu, 
		(select ten_tacgia from TacGia where TacGia.ma_tacgia = TaiLieu.ma_tacgia_1) as ten_tacgia, 
		(select ten_tacgia from TacGia where TacGia.ma_tacgia = TaiLieu.ma_tacgia_2) as ten_tacgia, 
		(select ten_tacgia from TacGia where TacGia.ma_tacgia = TaiLieu.ma_tacgia_3) as ten_tacgia, 
		ngonngu,
		bia,
		gia ,
		sl_kho,
		(select ten_loai from LoaiTaiLieu where LoaiTaiLieu.ma_loai = TaiLieu.ma_loai) as ten_loai,
		tinhtrang,
		tomtat,
		ngay_phathanh
	from TaiLieu
	where ma_tailieu = @maTaiLieu
end