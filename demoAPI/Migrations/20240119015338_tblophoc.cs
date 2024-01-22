using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace demoAPI.Migrations
{
    public partial class tblophoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SinhVienDBMaSV",
                table: "SinhVien",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LopHoc",
                columns: table => new
                {
                    MaLop = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenLop = table.Column<string>(type: "text", nullable: false),
                    MaSV = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHoc", x => x.MaLop);
                    table.ForeignKey(
                        name: "FK_LopHoc_SinhVien_MaSV",
                        column: x => x.MaSV,
                        principalTable: "SinhVien",
                        principalColumn: "MaSV");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_SinhVienDBMaSV",
                table: "SinhVien",
                column: "SinhVienDBMaSV");

            migrationBuilder.CreateIndex(
                name: "IX_LopHoc_MaSV",
                table: "LopHoc",
                column: "MaSV");

            migrationBuilder.AddForeignKey(
                name: "FK_SinhVien_SinhVien_SinhVienDBMaSV",
                table: "SinhVien",
                column: "SinhVienDBMaSV",
                principalTable: "SinhVien",
                principalColumn: "MaSV");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SinhVien_SinhVien_SinhVienDBMaSV",
                table: "SinhVien");

            migrationBuilder.DropTable(
                name: "LopHoc");

            migrationBuilder.DropIndex(
                name: "IX_SinhVien_SinhVienDBMaSV",
                table: "SinhVien");

            migrationBuilder.DropColumn(
                name: "SinhVienDBMaSV",
                table: "SinhVien");
        }
    }
}
