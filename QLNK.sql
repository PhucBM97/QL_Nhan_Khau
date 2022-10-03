create database QLNK
go
use QLNK
go
--
create table users
(
	TaiKhoan nvarchar(50),
	Matkhau nvarchar(50),
)
insert into users
values('admin','admin')
--
create table NhanVien
(
	MaNv varchar(10),
	TenNv nvarchar(50),
	diaChi nvarchar(50),
	Sdt char(12),
	constraint PK_NV primary key (Manv)
) 
--
create table SoHoKhau
(
	MaSHK varchar(10),
	TenChuHo nvarchar(50)
	constraint PK_shk primary key (MaSHK)
)
--
create table GiayTamVang
(
	MaGTV varchar(10) ,
	TenTamVang nvarchar(50),
	NgayTamVang Datetime,
	MaNv varchar(10),
	constraint PK_GTV primary key (MaGTV),
	constraint FK_STv_NV foreign key (MaNv) references Nhanvien(MaNv),
	
)
create table SoTamTru 
(
	MaSTT varchar(10) unique not null,
	TenTamTru nvarchar(50),
	NgayTamTru Datetime,
	MaNv varchar(10),
	constraint PK_STT primary key (MaSTT),
	constraint FK_STT_NV foreign key (MaNv) references Nhanvien(MaNv)
)
--
create table NhanKhau
(
	Socmnd varchar(10),
	TenNK nvarchar(50),
	Ngaysinh datetime,
	gioitinh nvarchar(50),
	Diachi nvarchar(50),
	sodt int, 
	MaSHK varchar(10),
	MaGTV varchar(10),
	MaSTT varchar(10),
	constraint PK_Nk primary key (socmnd),
	constraint FK_SHK foreign key (MaSHK) references SoHoKhau(MaSHK),
	constraint FK_GTV foreign key (maGTV) references GiayTamVang(MaGTV),
	constraint FK_STT foreign key (MaSTT) references SoTamTru(MaSTT)
)
--
use QLNK
--
insert into SoHoKhau
values('SHK1','Bui Minh Phuc');
insert into SoHoKhau
values('SHK2','Bui Minh Linh');
insert into SoHoKhau
values('SHK3','Bui Minh Khoa');
--
insert into NhanVien
values('Nv1','Heo1','Long An',01234567);
insert into NhanVien
values('Nv2','Heo2','quang Nam',09090909);
insert into NhanVien
values('Nv3','Heo3','tphcm',08080808);
insert into NhanVien
values('Nv4','Heo4','Tam Vu',0707070707);
insert into NhanVien
values('Nv5','Heo5','Tan An',0606060606);
--
select * from nhanvien
--
insert into SoTamTru
values('STT1','TamTru1',GETDATE(),'nv1');
insert into SoTamTru
values('STT2','TamTru3',GETDATE(),'nv2');
insert into SoTamTru
values('STT3','TamTru2',GETDATE(),'nv3');
--
insert into GiayTamVang
values('GTV1','TamVang1',GETDATE(),'nv2');
insert into GiayTamVang
values('GTV2','TamVang2',GETDATE(),'nv4');
insert into GiayTamVang
values('GTV3','TamVang2',GETDATE(),'nv5');
--
insert into NhanKhau
values('cmnd1','Bui Minh Phuc','1997-05-16','Nam','Long An',012345,'SHK1','GTV1','STT1');
