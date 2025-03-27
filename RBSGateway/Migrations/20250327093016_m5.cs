using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RBSGateway.Migrations
{
    /// <inheritdoc />
    public partial class m5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Resources_ParentID_CompanyID",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_ParentID_CompanyID",
                table: "Resources");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                table: "Resources",
                columns: new[] { "ResourceID", "CompanyID", "TenantID" });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ParentID_CompanyID_TenantID",
                table: "Resources",
                columns: new[] { "ParentID", "CompanyID", "TenantID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Resources_ParentID_CompanyID_TenantID",
                table: "Resources",
                columns: new[] { "ParentID", "CompanyID", "TenantID" },
                principalTable: "Resources",
                principalColumns: new[] { "ResourceID", "CompanyID", "TenantID" },
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Resources_ParentID_CompanyID_TenantID",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resources",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_ParentID_CompanyID_TenantID",
                table: "Resources");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resources",
                table: "Resources",
                columns: new[] { "ResourceID", "CompanyID" });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_ParentID_CompanyID",
                table: "Resources",
                columns: new[] { "ParentID", "CompanyID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Resources_ParentID_CompanyID",
                table: "Resources",
                columns: new[] { "ParentID", "CompanyID" },
                principalTable: "Resources",
                principalColumns: new[] { "ResourceID", "CompanyID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
