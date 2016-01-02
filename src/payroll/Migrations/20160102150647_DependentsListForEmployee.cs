using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace payroll.Migrations
{
    public partial class DependentsListForEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employee",
                nullable: false);
            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employee",
                nullable: false);
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Dependent",
                nullable: false);
            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Dependent",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employee",
                nullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employee",
                nullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Dependent",
                nullable: true);
            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Dependent",
                nullable: true);
        }
    }
}
