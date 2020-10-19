using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace P01_StudentSystem.Migrations
{
    public partial class changeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisteredOn",
                table: "Students",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 5, 5, 20, 54, 19, 245, DateTimeKind.Utc).AddTicks(3784));

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmissionTime",
                table: "HomeworkSubmissions",
                nullable: false,
                defaultValue: new DateTime(2020, 5, 6, 13, 16, 38, 872, DateTimeKind.Utc).AddTicks(6368),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 5, 5, 20, 54, 19, 232, DateTimeKind.Utc).AddTicks(9170));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisteredOn",
                table: "Students",
                nullable: false,
                defaultValue: new DateTime(2020, 5, 5, 20, 54, 19, 245, DateTimeKind.Utc).AddTicks(3784),
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "SubmissionTime",
                table: "HomeworkSubmissions",
                nullable: false,
                defaultValue: new DateTime(2020, 5, 5, 20, 54, 19, 232, DateTimeKind.Utc).AddTicks(9170),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 5, 6, 13, 16, 38, 872, DateTimeKind.Utc).AddTicks(6368));
        }
    }
}
