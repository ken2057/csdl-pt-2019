use qltv
go
create proc sp_get_dsNhanVien
as
begin
	-- do LoaiTaiLieu nằm ở tất cả các trạm, nên không cần search các trạm khác
	select * from NhanVien
end