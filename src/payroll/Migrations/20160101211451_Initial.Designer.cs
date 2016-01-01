using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using payroll.Models;

namespace payroll.Migrations
{
    [DbContext(typeof(EmployeeContext))]
    [Migration("20160101211451_Initial")]
    partial class Initial
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

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<int>("PersonID");

                    b.Property<int>("Relationship");

                    b.HasKey("DependentID");
                });

            modelBuilder.Entity("payroll.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MiddleName");

                    b.Property<int>("PersonID");

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
