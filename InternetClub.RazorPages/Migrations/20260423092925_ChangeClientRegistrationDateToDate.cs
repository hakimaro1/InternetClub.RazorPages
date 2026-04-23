using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternetClub.RazorPages.Migrations
{
    /// <inheritdoc />
    public partial class ChangeClientRegistrationDateToDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Convert legacy int YYYYMMDD to date
            migrationBuilder.Sql("""
                ALTER TABLE "Clients"
                ALTER COLUMN "RegistrationDate"
                TYPE date
                USING to_date("RegistrationDate"::text, 'YYYYMMDD');
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Clients"
                ALTER COLUMN "RegistrationDate"
                TYPE integer
                USING to_char("RegistrationDate", 'YYYYMMDD')::integer;
                """);
        }
    }
}
