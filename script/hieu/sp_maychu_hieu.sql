use qltv
go
create proc sp_get_dsNhanVien
as
begin
	-- do LoaiTaiLieu nằm ở tất cả các trạm, nên không cần search các trạm khác
	select * from NhanVien
end

go
create proc sp_get_quyen
as
begin
	-- do LoaiTaiLieu nằm ở tất cả các trạm, nên không cần search các trạm khác
	select quyen from Quyen 
end

go
create proc sp_get_chinhanh
as
begin
	-- do LoaiTaiLieu nằm ở tất cả các trạm, nên không cần search các trạm khác
	select ma_ChiNhanh from ChiNhanh 
end
