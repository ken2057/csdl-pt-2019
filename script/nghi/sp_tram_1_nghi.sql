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
	insert into QLTV_TRAM_2.qltv.dbo.DocGia values (@maSinhVien, @hoTen, @ngaySinh,@diaChi,@sDT)
	insert into QLTV_TRAM_3.qltv.dbo.DocGia values (@maSinhVien, @hoTen, @ngaySinh,@diaChi,@sDT)
	insert into QLTV_TRAM_4.qltv.dbo.DocGia values (@maSinhVien, @hoTen, @ngaySinh,@diaChi,@sDT)
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
		raiserror('Không tồn tại độc giả này',16,1)
		return
	end
	else
	begin
		update DocGia
		set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
		where ma_sinhvien = @maSinhVien
		select * from DocGia where ma_sinhvien = @maSinhVien
	end

	update QLTV_MAY_CHU.qltv.dbo.DocGia
	set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
	where ma_sinhvien = @maSinhVien

	update QLTV_TRAM_2.qltv.dbo.DocGia
	set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
	where ma_sinhvien = @maSinhVien

	update QLTV_TRAM_3.qltv.dbo.DocGia
	set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
	where ma_sinhvien = @maSinhVien

	update QLTV_TRAM_4.qltv.dbo.DocGia
	set hoten = @hoTen, NgaySinh = @ngaySinh, diachi = @diaChi, sdt = @sDT
	where ma_sinhvien = @maSinhVien
end



go
create proc sp_get_dsTaiLieu
as
begin
	select tram234.ma_tailieu, ngonngu,ten_tailieu, sl_kho,
				(select ten_tacgia from TacGia where TacGia.ma_tacgia = ma_tacgia_1) as ten_tacgia
		from
		(select  
			tram23.ma_tailieu,ma_loai, tomtat, ngonngu,ten_tailieu, bia, gia, ngay_phathanh, tinhtrang,sl_kho
		from 	
			(select tram2.ma_tailieu,ma_loai, tomtat, ngonngu,ten_tailieu, bia, gia, ngay_phathanh 
			from
				(select * from QLTV_TRAM_2.qltv.dbo.TaiLieu) tram2 
					 join 
				(select * from QLTV_TRAM_3.qltv.dbo.TaiLieu) tram3 
					on tram2.ma_tailieu = tram3.ma_tailieu
			) tram23
				join
			(select * from QLTV_TRAM_4.qltv.dbo.TaiLieu) tram4
				on tram23.ma_tailieu = tram4.ma_tailieu) tram234
			join
		(select * 
		from TaiLieu) tram1
			on tram234.ma_tailieu = tram1.ma_tailieu
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
	select tram234.ma_tailieu,ma_loai, tomtat, ngonngu,ten_tailieu, bia, gia, ngay_phathanh, tinhtrang,sl_kho,
				ma_tacgia_1,ma_tacgia_2,ma_tacgia_3
		from
		(select  
			tram23.ma_tailieu,ma_loai, tomtat, ngonngu,ten_tailieu, bia, gia, ngay_phathanh, tinhtrang,sl_kho
		from 	
			(select tram2.ma_tailieu,ma_loai, tomtat, ngonngu,ten_tailieu, bia, gia, ngay_phathanh 
			from
				(select * from QLTV_TRAM_2.qltv.dbo.TaiLieu where ma_tailieu = @maTaiLieu) tram2 
					 join 
				(select * from QLTV_TRAM_3.qltv.dbo.TaiLieu where ma_tailieu = @maTaiLieu) tram3 
					on tram2.ma_tailieu = tram3.ma_tailieu
			) tram23
				join
			(select * from QLTV_TRAM_4.qltv.dbo.TaiLieu where ma_tailieu = @maTaiLieu) tram4
				on tram23.ma_tailieu = tram4.ma_tailieu) tram234
			join
		(select * 
		from TaiLieu where ma_tailieu = @maTaiLieu) tram1
			on tram234.ma_tailieu = tram1.ma_tailieu
end

go 

create proc sp_add_tailieu
						@maTG1 varchar(20),
						@maTG2 varchar(20),
						@maTG3 varchar(20),
						@maLoaiTL varchar(20),
						@ngonNgu nvarchar(15),
						@bia nvarchar(20),
						@tinhTrang char(1),
						@giaTL money,
						@tenTaiLieu nvarchar(100),
						@soLuong tinyint,
						@tomTat ntext,
						@ngayPhatHanh datetime
as
begin
	declare @maTL varchar(20)
	select @maTL = count(ma_tailieu) + 1 from TaiLieu
	insert into QLTV_MAY_CHU.qltv.dbo.TaiLieu values(
						@maTL, 
						@maTG1, 
						@maTG2, 
						@maTG3, 
						@maLoaiTL, 
						@ngonNgu, 
						@bia, 
						@tinhTrang, 
						@giaTL, 
						@tenTaiLieu, 
						@soLuong, 
						@tomTat, 
						@ngayPhatHanh
	)
	insert into TaiLieu(ma_tailieu,ma_tacgia_1,ma_tacgia_2,ma_tacgia_3) values(
						@maTL, 
						@maTG1, 
						@maTG2, 
						@maTG3
	)
	insert into QLTV_TRAM_2.qltv.dbo.TaiLieu(ma_tailieu,ma_loai,ten_tailieu,ngonngu,tomtat) values(
						@maTL, 
						@maLoaiTL, 
						@tenTaiLieu,
						@ngonNgu, 
						@tomTat
	)
	insert into QLTV_TRAM_3.qltv.dbo.TaiLieu(ma_tailieu,bia,gia,ngay_phathanh) values(
						@maTL, 
						@bia, 
						@giaTL, 
						@ngayPhatHanh
	)
	insert into QLTV_TRAM_4.qltv.dbo.TaiLieu(ma_tailieu,tinhtrang,sl_kho) values(
						@maTL, 
						@tinhTrang, 
						@soLuong
	)
end

go

create proc sp_update_tailieu
						@maTL varchar(20),
						@maTG1 varchar(20),
						@maTG2 varchar(20),
						@maTG3 varchar(20),
						@maLoaiTL varchar(20),
						@ngonNgu nvarchar(15),
						@bia nvarchar(20),
						@tinhTrang char(1),
						@giaTL money,
						@tenTaiLieu nvarchar(100),
						@soLuong tinyint,
						@tomTat ntext,
						@ngayPhatHanh datetime
as
begin
	update QLTV_MAY_CHU.dbo.TaiLieu
	set ma_tacgia_1 = @maTG1, 
		ma_tacgia_2 = @maTG2,
		ma_tacgia_3 = @maTG3,
		ma_loai = @maLoaiTL,
		ngonngu = @ngonNgu,
		bia=@bia,
		tinhtrang = @tinhTrang,
		gia = @giaTL,
		ten_tailieu = @tenTaiLieu,
		sl_kho = @soLuong,
		tomtat = @tomTat,
		ngay_phathanh = @ngayPhatHanh
	where ma_tailieu = @maTL
	exec sp_get_tailieu @maTL

	update TaiLieu
	set ma_tacgia_1 = @maTG1, 
		ma_tacgia_2 = @maTG2,
		ma_tacgia_3 = @maTG3
	where ma_tailieu = @maTL
	exec sp_get_tailieu @maTL

	update QLTV_TRAM_2.qltv.dbo.TaiLieu
	set ma_loai = @maLoaiTL,
		ngonngu = @ngonNgu,
		ten_tailieu = @tenTaiLieu,
		tomtat = @tomTat
	where ma_tailieu = @maTL
	exec sp_get_tailieu @maTL

	update QLTV_TRAM_3.qltv.dbo.TaiLieu
	set bia=@bia,
		gia = @giaTL,
		ngay_phathanh = @ngayPhatHanh
	where ma_tailieu = @maTL
	exec sp_get_tailieu @maTL

	update QLTV_TRAM_4.qltv.dbo.TaiLieu
	set tinhtrang = @tinhTrang,
		sl_kho = @soLuong
	where ma_tailieu = @maTL
	exec sp_get_tailieu @maTL
end
