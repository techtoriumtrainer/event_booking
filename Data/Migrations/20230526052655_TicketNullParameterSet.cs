﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace event_booking.Data.Migrations
{
    /// <inheritdoc />
    public partial class TicketNullParameterSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                  name: "LastName",
                  schema: "evnt",
                  table: "Tickets",
                  type: "varchar(50)",
                  unicode: false,
                  maxLength: 50,
                  nullable: true,
                  oldClrType: typeof(string),
                  oldType: "varchar(50)",
                  oldUnicode: false,
                  oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "evnt",
                table: "Tickets",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                 name: "LastName",
                 schema: "evnt",
                 table: "Tickets",
                 type: "varchar(50)",
                 unicode: false,
                 maxLength: 50,
                 nullable: false,
                 oldClrType: typeof(string),
                 oldType: "varchar(50)",
                 oldUnicode: false,
                 oldMaxLength: 50,
                 oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "evnt",
                table: "Tickets",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
