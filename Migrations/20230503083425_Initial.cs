using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BanDongHoSolution.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KHUYENMAI",
                columns: table => new
                {
                    MAKM = table.Column<string>(type: "char(7)", nullable: false),
                    TENKM = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    NGAYBD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NGAYKT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KHUYENMAI", x => x.MAKM);
                });

            migrationBuilder.CreateTable(
                name: "LOAISANPHAM",
                columns: table => new
                {
                    MALOAISP = table.Column<string>(type: "char(7)", nullable: false),
                    TENLOAISP = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOAISANPHAM", x => x.MALOAISP);
                });

            migrationBuilder.CreateTable(
                name: "LOAITK",
                columns: table => new
                {
                    MALOAITK = table.Column<string>(type: "char(7)", nullable: false),
                    TENLOAITK = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOAITK", x => x.MALOAITK);
                });

            migrationBuilder.CreateTable(
                name: "THUONGHIEU",
                columns: table => new
                {
                    MATH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TENTH = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    HINHTH = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THUONGHIEU", x => x.MATH);
                });

            migrationBuilder.CreateTable(
                name: "TAIKHOAN",
                columns: table => new
                {
                    MATK = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MALOAITK = table.Column<string>(type: "char(7)", nullable: true),
                    TENDN = table.Column<string>(type: "varchar(20)", nullable: true),
                    MATKHAU = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    NGAYDANGKY = table.Column<DateTime>(type: "datetime", nullable: true),
                    TRANGTHAI = table.Column<int>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAIKHOAN", x => x.MATK);
                    table.ForeignKey(
                        name: "FK_TAIKHOAN_LOAITK_MALOAITK",
                        column: x => x.MALOAITK,
                        principalTable: "LOAITK",
                        principalColumn: "MALOAITK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SANPHAM",
                columns: table => new
                {
                    MASP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MATH = table.Column<int>(type: "int", nullable: false),
                    MALOAISP = table.Column<string>(type: "char(7)", nullable: true),
                    TENSP = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    HINHLON = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    HINHNHO = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    MOTA = table.Column<string>(type: "ntext", nullable: true),
                    DANHGIA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SOLUONG = table.Column<int>(type: "int", nullable: false),
                    DONGIA = table.Column<float>(type: "decimal(18, 0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SANPHAM", x => x.MASP);
                    table.ForeignKey(
                        name: "FK_SANPHAM_LOAISANPHAM_MALOAISP",
                        column: x => x.MALOAISP,
                        principalTable: "LOAISANPHAM",
                        principalColumn: "MALOAISP",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SANPHAM_THUONGHIEU_MATH",
                        column: x => x.MATH,
                        principalTable: "THUONGHIEU",
                        principalColumn: "MATH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KHACHHANG",
                columns: table => new
                {
                    MAKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MATK = table.Column<int>(type: "int", nullable: false),
                    TENKH = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    EMAIL = table.Column<string>(type: "varchar(50)", nullable: true),
                    SDT = table.Column<string>(type: "varchar(12)", nullable: true),
                    GIOITINH = table.Column<string>(type: "nvarchar(3)", nullable: true),
                    DIACHI = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KHACHHANG", x => x.MAKH);
                    table.ForeignKey(
                        name: "FK_KHACHHANG_TAIKHOAN_MATK",
                        column: x => x.MATK,
                        principalTable: "TAIKHOAN",
                        principalColumn: "MATK",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CHITIETKM",
                columns: table => new
                {
                    MAKM = table.Column<string>(type: "char(7)", nullable: false),
                    MASP = table.Column<int>(type: "int", nullable: false),
                    PHANTRAMKM = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIETKM", x => new { x.MASP, x.MAKM });
                    table.ForeignKey(
                        name: "FK_CHITIETKM_KHUYENMAI_MAKM",
                        column: x => x.MAKM,
                        principalTable: "KHUYENMAI",
                        principalColumn: "MAKM",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETKM_SANPHAM_MASP",
                        column: x => x.MASP,
                        principalTable: "SANPHAM",
                        principalColumn: "MASP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DONHANG",
                columns: table => new
                {
                    MADH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MAKH = table.Column<int>(type: "int", nullable: false),
                    TRANGTHAI = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    DIACHIGIAO = table.Column<string>(type: "ntext", nullable: true),
                    SDT = table.Column<string>(type: "varchar(12)", nullable: true),
                    NGAYDAT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NGAYGIAO = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MOTA = table.Column<string>(type: "ntext", nullable: true),
                    TONGTIEN = table.Column<float>(type: "decimal(18, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DONHANG", x => x.MADH);
                    table.ForeignKey(
                        name: "FK_DONHANG_KHACHHANG_MAKH",
                        column: x => x.MAKH,
                        principalTable: "KHACHHANG",
                        principalColumn: "MAKH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CHITIETDONHANG",
                columns: table => new
                {
                    MADH = table.Column<int>(type: "int", nullable: false),
                    MASP = table.Column<int>(type: "int", nullable: false),
                    SOLUONG = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHITIETDONHANG", x => new { x.MASP, x.MADH });
                    table.ForeignKey(
                        name: "FK_CHITIETDONHANG_DONHANG_MADH",
                        column: x => x.MADH,
                        principalTable: "DONHANG",
                        principalColumn: "MADH",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CHITIETDONHANG_SANPHAM_MASP",
                        column: x => x.MASP,
                        principalTable: "SANPHAM",
                        principalColumn: "MASP",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETDONHANG_MADH",
                table: "CHITIETDONHANG",
                column: "MADH");

            migrationBuilder.CreateIndex(
                name: "IX_CHITIETKM_MAKM",
                table: "CHITIETKM",
                column: "MAKM");

            migrationBuilder.CreateIndex(
                name: "IX_DONHANG_MAKH",
                table: "DONHANG",
                column: "MAKH");

            migrationBuilder.CreateIndex(
                name: "IX_KHACHHANG_MATK",
                table: "KHACHHANG",
                column: "MATK");

            migrationBuilder.CreateIndex(
                name: "IX_SANPHAM_MALOAISP",
                table: "SANPHAM",
                column: "MALOAISP");

            migrationBuilder.CreateIndex(
                name: "IX_SANPHAM_MATH",
                table: "SANPHAM",
                column: "MATH");

            migrationBuilder.CreateIndex(
                name: "IX_TAIKHOAN_MALOAITK",
                table: "TAIKHOAN",
                column: "MALOAITK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CHITIETDONHANG");

            migrationBuilder.DropTable(
                name: "CHITIETKM");

            migrationBuilder.DropTable(
                name: "DONHANG");

            migrationBuilder.DropTable(
                name: "KHUYENMAI");

            migrationBuilder.DropTable(
                name: "SANPHAM");

            migrationBuilder.DropTable(
                name: "KHACHHANG");

            migrationBuilder.DropTable(
                name: "LOAISANPHAM");

            migrationBuilder.DropTable(
                name: "THUONGHIEU");

            migrationBuilder.DropTable(
                name: "TAIKHOAN");

            migrationBuilder.DropTable(
                name: "LOAITK");
        }
    }
}
