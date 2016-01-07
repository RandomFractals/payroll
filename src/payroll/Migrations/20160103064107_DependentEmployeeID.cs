using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace payroll.Migrations
{
	public partial class DependentEmployeeID : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(name: "FK_Dependent_Employee_EmployeeEmployeeID", table: "Dependent");
			migrationBuilder.DropColumn(name: "EmployeeEmployeeID", table: "Dependent");
			migrationBuilder.AddColumn<int>(
					name: "EmployeeID",
					table: "Dependent",
					nullable: false,
					defaultValue: 0);
			migrationBuilder.AddForeignKey(
					name: "FK_Dependent_Employee_EmployeeID",
					table: "Dependent",
					column: "EmployeeID",
					principalTable: "Employee",
					principalColumn: "EmployeeID",
					onDelete: ReferentialAction.Cascade);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(name: "FK_Dependent_Employee_EmployeeID", table: "Dependent");
			migrationBuilder.DropColumn(name: "EmployeeID", table: "Dependent");
			migrationBuilder.AddColumn<int>(
					name: "EmployeeEmployeeID",
					table: "Dependent",
					nullable: true);
			migrationBuilder.AddForeignKey(
					name: "FK_Dependent_Employee_EmployeeEmployeeID",
					table: "Dependent",
					column: "EmployeeEmployeeID",
					principalTable: "Employee",
					principalColumn: "EmployeeID",
					onDelete: ReferentialAction.Restrict);
		}
	}
}
