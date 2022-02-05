using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace premiumCalculatorWebAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    IdPeriod = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    NamePeriod = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Factor = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.IdPeriod);
                });

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    PlanId = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    PlanName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "PremiumRols",
                columns: table => new
                {
                    PremiumRolID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Carrier = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Plan = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 3, nullable: true),
                    MonthOfBirth = table.Column<string>(type: "TEXT", maxLength: 15, nullable: true),
                    Age = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Premium = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PremiumRols", x => x.PremiumRolID);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<string>(type: "TEXT", maxLength: 5, nullable: false),
                    StateName = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                });

            migrationBuilder.InsertData(
                table: "Periods",
                columns: new[] { "IdPeriod", "Factor", "NamePeriod" },
                values: new object[] { "A", 12, "Annually" });

            migrationBuilder.InsertData(
                table: "Periods",
                columns: new[] { "IdPeriod", "Factor", "NamePeriod" },
                values: new object[] { "M", 1, "Montly" });

            migrationBuilder.InsertData(
                table: "Periods",
                columns: new[] { "IdPeriod", "Factor", "NamePeriod" },
                values: new object[] { "Q", 3, "Quartely" });

            migrationBuilder.InsertData(
                table: "Periods",
                columns: new[] { "IdPeriod", "Factor", "NamePeriod" },
                values: new object[] { "S", 6, "Semi-Anually" });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "PlanId", "PlanName" },
                values: new object[] { "A", "Plan A" });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "PlanId", "PlanName" },
                values: new object[] { "B", "Plan B" });

            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "PlanId", "PlanName" },
                values: new object[] { "C", "Plan C" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 1, "21-45", "Qwerty", "September", "A", 150.0, "NY" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 2, "46-65", "Qwerty", "January", "B", 200.5, "NY" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 3, "18-65", "Qwerty", "*", "A,C", 120.98999999999999, "NY" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 4, "18-65", "Qwerty", "November", "A", 85.5, "AL" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 5, "18-65", "Qwerty", "*", "C", 100.0, "AL" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 6, "65+", "Qwerty", "December", "A", 175.19999999999999, "AK" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 7, "18-64", "Qwerty", "December", "A", 125.16, "AK" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 8, "18-65", "Qwerty", "*", "B", 100.8, "AK" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 9, "18-65", "Qwerty", "*", "A,C", 90.0, "*" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 10, "21-45", "Asdf", "October", "A", 150.0, "NY" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 11, "46-65", "Asdf", "January", "B", 184.5, "NY" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 12, "18-65", "Asdf", "*", "A", 129.94999999999999, "NY" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 13, "18-65", "Asdf", "November", "A", 84.5, "AL" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 14, "18-65", "Asdf", "*", "B", 100.0, "WY" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 15, "18-65", "Asdf", "*", "B,C", 100.8, "AK" });

            migrationBuilder.InsertData(
                table: "PremiumRols",
                columns: new[] { "PremiumRolID", "Age", "Carrier", "MonthOfBirth", "Plan", "Premium", "State" },
                values: new object[] { 16, "18-65", "Asdf", "*", "A,C", 89.989999999999995, "*" });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "StateName" },
                values: new object[] { "AK", "AK - Arkansas" });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "StateName" },
                values: new object[] { "AL", "AL - Alamaba" });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "StateName" },
                values: new object[] { "NJ", "NJ - New Jersey" });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "StateName" },
                values: new object[] { "NY", "NY - New York" });

            migrationBuilder.InsertData(
                table: "States",
                columns: new[] { "StateId", "StateName" },
                values: new object[] { "WY", "WY - Wyoming" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropTable(
                name: "PremiumRols");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}
