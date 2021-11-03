using Microsoft.EntityFrameworkCore.Migrations;

namespace Asp.netCoreMvcCrud.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "data_kendaraan",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Model= table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Merek = table.Column<string>(type: "varchar(10)", nullable: true),
                    transmisi = table.Column<string>(type: "varchar(100)", nullable: true),
                    tahun = table.Column<string>(type: "varchar(100)", nullable: true),
                    harga = table.Column<string>(type: "varchar(100)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kendaraan", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "data_kendaraan");
        }
    }
}
