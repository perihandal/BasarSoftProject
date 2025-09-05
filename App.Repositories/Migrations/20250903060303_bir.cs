using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace App.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class bir : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Geometry>(
                name: "Geoloc",
                table: "Geometries",
                type: "geometry",
                nullable: true,
                oldClrType: typeof(Geometry),
                oldType: "geometry(Point,4326)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Geometry>(
                name: "Geoloc",
                table: "Geometries",
                type: "geometry(Point,4326)",
                nullable: false,
                oldClrType: typeof(Geometry),
                oldType: "geometry",
                oldNullable: true);
        }
    }
}
