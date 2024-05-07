using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTAuthTemplate.Migrations
{
    /// <inheritdoc />
    public partial class MemberOnlyNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_MemberOnlyPrimaryPrices_DbMemberOnlyPrimaryPriceId",
                table: "Offers");

            migrationBuilder.AlterColumn<int>(
                name: "DbMemberOnlyPrimaryPriceId",
                table: "Offers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_MemberOnlyPrimaryPrices_DbMemberOnlyPrimaryPriceId",
                table: "Offers",
                column: "DbMemberOnlyPrimaryPriceId",
                principalTable: "MemberOnlyPrimaryPrices",
                principalColumn: "MemberOnlyPrimaryPriceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_MemberOnlyPrimaryPrices_DbMemberOnlyPrimaryPriceId",
                table: "Offers");

            migrationBuilder.AlterColumn<int>(
                name: "DbMemberOnlyPrimaryPriceId",
                table: "Offers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_MemberOnlyPrimaryPrices_DbMemberOnlyPrimaryPriceId",
                table: "Offers",
                column: "DbMemberOnlyPrimaryPriceId",
                principalTable: "MemberOnlyPrimaryPrices",
                principalColumn: "MemberOnlyPrimaryPriceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
