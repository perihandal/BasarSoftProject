using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace App.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class iki : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "StartPoint",
                table: "GeometryMetrics",
                type: "geometry(Point,4326)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,4326)");

            migrationBuilder.AlterColumn<Point>(
                name: "EndPoint",
                table: "GeometryMetrics",
                type: "geometry(Point,4326)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,4326)");

            migrationBuilder.AlterColumn<Point>(
                name: "Centroid",
                table: "GeometryMetrics",
                type: "geometry(Point,4326)",
                nullable: true,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,4326)");

            migrationBuilder.AlterColumn<Polygon>(
                name: "BoundingBox",
                table: "GeometryMetrics",
                type: "geometry(Polygon,4326)",
                nullable: true,
                oldClrType: typeof(Polygon),
                oldType: "geometry(Polygon,4326)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Point>(
                name: "StartPoint",
                table: "GeometryMetrics",
                type: "geometry(Point,4326)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,4326)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "EndPoint",
                table: "GeometryMetrics",
                type: "geometry(Point,4326)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,4326)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Point>(
                name: "Centroid",
                table: "GeometryMetrics",
                type: "geometry(Point,4326)",
                nullable: false,
                oldClrType: typeof(Point),
                oldType: "geometry(Point,4326)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Polygon>(
                name: "BoundingBox",
                table: "GeometryMetrics",
                type: "geometry(Polygon,4326)",
                nullable: false,
                oldClrType: typeof(Polygon),
                oldType: "geometry(Polygon,4326)",
                oldNullable: true);
        }
    }
}
