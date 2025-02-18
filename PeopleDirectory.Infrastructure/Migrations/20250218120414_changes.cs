using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleDirectory.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPersons_Persons_PersonId",
                table: "RelatedPersons");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPersons_Persons_PersonId",
                table: "RelatedPersons",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPersons_Persons_PersonId",
                table: "RelatedPersons");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPersons_Persons_PersonId",
                table: "RelatedPersons",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
