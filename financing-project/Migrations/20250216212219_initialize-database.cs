using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace financing_project.Migrations
{
    /// <inheritdoc />
    public partial class initializedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Cpf = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CellPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Cpf);
                });

            migrationBuilder.CreateTable(
                name: "Financing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(24)", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    LastExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Financing_Customer_Cpf",
                        column: x => x.Cpf,
                        principalTable: "Customer",
                        principalColumn: "Cpf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Installment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstallmentNumber = table.Column<int>(type: "int", nullable: false),
                    InstallmentValue = table.Column<float>(type: "real", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinancingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Installment_Financing_FinancingId",
                        column: x => x.FinancingId,
                        principalTable: "Financing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Financing_Cpf",
                table: "Financing",
                column: "Cpf");

            migrationBuilder.CreateIndex(
                name: "IX_Installment_FinancingId",
                table: "Installment",
                column: "FinancingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Installment");

            migrationBuilder.DropTable(
                name: "Financing");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
