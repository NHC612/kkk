using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Testthi.Migrations
{
    /// <inheritdoc />
    public partial class Create_Table_SanPham : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    MaSanPham = table.Column<string>(type: "TEXT", nullable: false),
                    TenSanPham = table.Column<string>(type: "TEXT", nullable: true),
                    MaNhaCungCap = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.MaSanPham);
                    table.ForeignKey(
                        name: "FK_SanPham_NhaCungCap_MaNhaCungCap",
                        column: x => x.MaNhaCungCap,
                        principalTable: "NhaCungCap",
                        principalColumn: "MaNhaCungCap");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_MaNhaCungCap",
                table: "SanPham",
                column: "MaNhaCungCap");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SanPham");
        }
    }
}
