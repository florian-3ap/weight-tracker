using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeightTracker.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abteilung",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abteilung", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    SecondName = table.Column<string>(nullable: false),
                    Street = table.Column<string>(nullable: false),
                    Plz = table.Column<string>(nullable: false),
                    Place = table.Column<string>(nullable: false),
                    AbteilungId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_Abteilung_AbteilungId",
                        column: x => x.AbteilungId,
                        principalTable: "Abteilung",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Person_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_Person_AbteilungId",
                table: "Person",
                column: "AbteilungId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_ProjectId",
                table: "Person",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Abteilung");

            migrationBuilder.DropTable(
                name: "Project");
        }
    }
}
