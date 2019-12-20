use qltv
go
-- Insert Quyen
insert into QLTV_Tram_1.qltv.dbo.Quyen select * from Quyen
insert into QLTV_Tram_2.qltv.dbo.Quyen select * from Quyen
insert into QLTV_Tram_3.qltv.dbo.Quyen select * from Quyen
insert into QLTV_Tram_4.qltv.dbo.Quyen select * from Quyen
go
-- Insert ChiNhanh
insert into QLTV_Tram_1.qltv.dbo.ChiNhanh select * from ChiNhanh
insert into QLTV_Tram_2.qltv.dbo.ChiNhanh select * from ChiNhanh
insert into QLTV_Tram_3.qltv.dbo.ChiNhanh select * from ChiNhanh
insert into QLTV_Tram_4.qltv.dbo.ChiNhanh select * from ChiNhanh
go
-- Insert DocGia
insert into QLTV_Tram_1.qltv.dbo.DocGia select * from DocGia
insert into QLTV_Tram_2.qltv.dbo.DocGia select * from DocGia
insert into QLTV_Tram_3.qltv.dbo.DocGia select * from DocGia
insert into QLTV_Tram_4.qltv.dbo.DocGia select * from DocGia
go
-- Insert TacGia
insert into QLTV_Tram_1.qltv.dbo.TacGia select * from TacGia
insert into QLTV_Tram_2.qltv.dbo.TacGia select * from TacGia
insert into QLTV_Tram_3.qltv.dbo.TacGia select * from TacGia
insert into QLTV_Tram_4.qltv.dbo.TacGia select * from TacGia
go
-- Insert DangKy
insert into QLTV_Tram_1.qltv.dbo.DocGia select * from DocGia
insert into QLTV_Tram_2.qltv.dbo.DocGia select * from DocGia
insert into QLTV_Tram_3.qltv.dbo.DocGia select * from DocGia
insert into QLTV_Tram_4.qltv.dbo.DocGia select * from DocGia
go
-- Insert LoaiTaiLieu
insert into QLTV_Tram_1.qltv.dbo.LoaiTaiLieu select * from LoaiTaiLieu
insert into QLTV_Tram_2.qltv.dbo.LoaiTaiLieu select * from LoaiTaiLieu
insert into QLTV_Tram_3.qltv.dbo.LoaiTaiLieu select * from LoaiTaiLieu
insert into QLTV_Tram_4.qltv.dbo.LoaiTaiLieu select * from LoaiTaiLieu
go
-- Insert NhanVien
insert into QLTV_Tram_1.qltv.dbo.NhanVien select * from NhanVien where ma_chinhanh = 'SVH' or quyen = 'root'
insert into QLTV_Tram_2.qltv.dbo.NhanVien select * from NhanVien where ma_chinhanh = 'HM' or quyen = 'root'
insert into QLTV_Tram_3.qltv.dbo.NhanVien select * from NhanVien where ma_chinhanh = 'TS' or quyen = 'root'
insert into QLTV_Tram_3.qltv.dbo.NhanVien select * from NhanVien where ma_chinhanh = 'CT' or quyen = 'root'
go
-- Insert TaiLieu
insert into QLTV_Tram_1.qltv.dbo.TaiLieu select ma_tailieu, ma_tacgia_1, ma_tacgia_2, ma_tacgia_3 from TaiLieu
insert into QLTV_Tram_2.qltv.dbo.TaiLieu select ma_tailieu, ma_loai, ngonngu, ten_tailieu, tomtat from TaiLieu
insert into QLTV_Tram_3.qltv.dbo.TaiLieu select ma_tailieu, bia, gia, ngay_phathanh from TaiLieu
insert into QLTV_Tram_4.qltv.dbo.TaiLieu select ma_tailieu, tinhtrang, sl_kho from TaiLieu
go
-- Insert BanSao
insert into QLTV_Tram_1.qltv.dbo.BanSao select * from BanSao where ma_chinhanh = 'SVH'
insert into QLTV_Tram_2.qltv.dbo.BanSao select * from BanSao where ma_chinhanh = 'HM'
insert into QLTV_Tram_3.qltv.dbo.BanSao select * from BanSao where ma_chinhanh = 'TS'
insert into QLTV_Tram_4.qltv.dbo.BanSao select * from BanSao where ma_chinhanh = 'CT'
go
