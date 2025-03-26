using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RBSGateway.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceNames",
                columns: table => new
                {
                    ResourceNameID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceNames", x => x.ResourceNameID);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ResourceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    ResourceNameId = table.Column<int>(type: "int", nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: true),
                    SiteID = table.Column<int>(type: "int", nullable: true),
                    SectorID = table.Column<int>(type: "int", nullable: true),
                    SectionID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => new { x.ResourceID, x.CompanyID });
                    table.ForeignKey(
                        name: "FK_Resources_ResourceNames_ResourceNameId",
                        column: x => x.ResourceNameId,
                        principalTable: "ResourceNames",
                        principalColumn: "ResourceNameID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resources_Resources_ParentID_CompanyID",
                        columns: x => new { x.ParentID, x.CompanyID },
                        principalTable: "Resources",
                        principalColumns: new[] { "ResourceID", "CompanyID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ParentID_CompanyID",
                table: "Resources",
                columns: new[] { "ParentID", "CompanyID" });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceNameId",
                table: "Resources",
                column: "ResourceNameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "ResourceNames");
        }
    }
}
