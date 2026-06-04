using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureStore.Migrations
{
    /// <inheritdoc />
    public partial class MakeCartCouponOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Coupons_CouponId",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "CouponId",
                table: "Carts",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldMaxLength: 450);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Coupons_CouponId",
                table: "Carts",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "CouponId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Coupons_CouponId",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "CouponId",
                table: "Carts",
                type: "INTEGER",
                maxLength: 450,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Coupons_CouponId",
                table: "Carts",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "CouponId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
