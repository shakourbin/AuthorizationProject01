using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthorizationProject01.Migrations
{
    /// <inheritdoc />
    public partial class AddClaimCategoryIdToUserClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_ClaimCategory_ClaimCategoryId",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClaimCategory",
                table: "ClaimCategory");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "ClaimCategory",
                newName: "ClaimCategories");

            migrationBuilder.AlterColumn<int>(
                name: "ClaimCategoryId",
                table: "AspNetUserClaims",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClaimCategories",
                table: "ClaimCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_ClaimCategories_ClaimCategoryId",
                table: "AspNetUserClaims",
                column: "ClaimCategoryId",
                principalTable: "ClaimCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_ClaimCategories_ClaimCategoryId",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClaimCategories",
                table: "ClaimCategories");

            migrationBuilder.RenameTable(
                name: "ClaimCategories",
                newName: "ClaimCategory");

            migrationBuilder.AlterColumn<int>(
                name: "ClaimCategoryId",
                table: "AspNetUserClaims",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserClaims",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClaimCategory",
                table: "ClaimCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_ClaimCategory_ClaimCategoryId",
                table: "AspNetUserClaims",
                column: "ClaimCategoryId",
                principalTable: "ClaimCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
