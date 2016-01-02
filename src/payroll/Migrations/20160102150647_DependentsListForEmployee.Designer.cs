using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using payroll.Models;

namespace payroll.Migrations
{
    [DbContext(typeof(EmployeeDataContext))]
    [Migration("20160102150647_DependentsListForEmployee")]
    partial class DependentsListForEmployee
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("payroll.Models.Dependent", b =>
                {
                    b.Property<int>("DependentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EmployeeEmployeeID");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("MiddleName");

                    b.Property<int>("Relationship");

                    b.HasKey("DependentID");
                });

            modelBuilder.Entity("payroll.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("MiddleName");

                    b.Property<decimal>("Salary");

                    b.HasKey("EmployeeID");
                });

            modelBuilder.Entity("payroll.Models.Dependent", b =>
                {
                    b.HasOne("payroll.Models.Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeEmployeeID");
                });
        }
    }
}
