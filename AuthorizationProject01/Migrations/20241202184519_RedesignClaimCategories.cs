using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthorizationProject01.Migrations
{
    /// <inheritdoc />
    public partial class RedesignClaimCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClaimCategoryId",
                table: "AspNetUserClaims",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserClaims",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ClaimCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_ClaimCategoryId",
                table: "AspNetUserClaims",
                column: "ClaimCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_ClaimCategory_ClaimCategoryId",
                table: "AspNetUserClaims",
                column: "ClaimCategoryId",
                principalTable: "ClaimCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_ClaimCategory_ClaimCategoryId",
                table: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "ClaimCategory");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserClaims_ClaimCategoryId",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "ClaimCategoryId",
                table: "AspNetUserClaims");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserClaims");
        }
    }
}
