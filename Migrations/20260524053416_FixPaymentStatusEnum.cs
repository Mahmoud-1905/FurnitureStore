using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureStore.Migrations
{
    /// <inheritdoc />
    public partial class FixPaymentStatusEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentProvider",
                table: "Payments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProviderReferenceId",
                table: "Payments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 30,
                oldDefaultValue: "Pending");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentStatus",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20,
                oldDefaultValue: "Unpaid");

            migrationBuilder.AddColumn<int>(
                name: "ShippingMethodId",
                table: "Orders",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CouponId",
                table: "Carts",
                type: "INTEGER",
                maxLength: 450,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShippingMethods",
                columns: table => new
                {
                    ShippingMethodId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    EstimatedDays = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingMethods", x => x.ShippingMethodId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingMethodId",
                table: "Orders",
                column: "ShippingMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CouponId",
                table: "Carts",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Coupons_CouponId",
                table: "Carts",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "CouponId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingMethods_ShippingMethodId",
                table: "Orders",
                column: "ShippingMethodId",
                principalTable: "ShippingMethods",
                principalColumn: "ShippingMethodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Coupons_CouponId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingMethods_ShippingMethodId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "ShippingMethods");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShippingMethodId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Carts_CouponId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "PaymentProvider",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ProviderReferenceId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ShippingMethodId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Carts");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "TEXT",
                maxLength: 30,
                nullable: false,
                defaultValue: "Pending",
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "Orders",
                type: "TEXT",
                maxLength: 20,
                nullable: false,
                defaultValue: "Unpaid",
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 0);
        }
    }
}
