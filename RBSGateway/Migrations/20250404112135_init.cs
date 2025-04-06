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
                name: "Tenant",
                columns: table => new
                {
                    TenantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.TenantID);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    TenantID = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    LastUpdateUser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => new { x.CompanyID, x.TenantID });
                    table.ForeignKey(
                        name: "FK_Company_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    ResourceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantID = table.Column<int>(type: "int", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    ResourceNameId = table.Column<int>(type: "int", nullable: false),
                    ParentID = table.Column<int>(type: "int", nullable: true),
                    DepartmentID = table.Column<int>(type: "int", nullable: true),
                    SiteID = table.Column<int>(type: "int", nullable: true),
                    SectorID = table.Column<int>(type: "int", nullable: true),
                    SectionID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdatedDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => new { x.ResourceID, x.CompanyID, x.TenantID });
                    table.ForeignKey(
                        name: "FK_Resources_Company_CompanyID_TenantID",
                        columns: x => new { x.CompanyID, x.TenantID },
                        principalTable: "Company",
                        principalColumns: new[] { "CompanyID", "TenantID" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Resources_ResourceNames_ResourceNameId",
                        column: x => x.ResourceNameId,
                        principalTable: "ResourceNames",
                        principalColumn: "ResourceNameID");
                    table.ForeignKey(
                        name: "FK_Resources_Resources_ParentID_CompanyID_TenantID",
                        columns: x => new { x.ParentID, x.CompanyID, x.TenantID },
                        principalTable: "Resources",
                        principalColumns: new[] { "ResourceID", "CompanyID", "TenantID" });
                    table.ForeignKey(
                        name: "FK_Resources_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_TenantID",
                table: "Company",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CompanyID_TenantID",
                table: "Resources",
                columns: new[] { "CompanyID", "TenantID" });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ParentID_CompanyID_TenantID",
                table: "Resources",
                columns: new[] { "ParentID", "CompanyID", "TenantID" });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ResourceNameId",
                table: "Resources",
                column: "ResourceNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_TenantID",
                table: "Resources",
                column: "TenantID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "ResourceNames");

            migrationBuilder.DropTable(
                name: "Tenant");
        }
    }
}
