using Microsoft.EntityFrameworkCore.Migrations;

namespace EdukuJez.Migrations
{
    public partial class submissions_name_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Substitution_SubstitutionId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Substitution_Users_SubTeacherId",
                table: "Substitution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Substitution",
                table: "Substitution");

            migrationBuilder.RenameTable(
                name: "Substitution",
                newName: "Substitutions");

            migrationBuilder.RenameIndex(
                name: "IX_Substitution_SubTeacherId",
                table: "Substitutions",
                newName: "IX_Substitutions_SubTeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Substitutions",
                table: "Substitutions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Substitutions_SubstitutionId",
                table: "Classes",
                column: "SubstitutionId",
                principalTable: "Substitutions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Substitutions_Users_SubTeacherId",
                table: "Substitutions",
                column: "SubTeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Substitutions_SubstitutionId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Substitutions_Users_SubTeacherId",
                table: "Substitutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Substitutions",
                table: "Substitutions");

            migrationBuilder.RenameTable(
                name: "Substitutions",
                newName: "Substitution");

            migrationBuilder.RenameIndex(
                name: "IX_Substitutions_SubTeacherId",
                table: "Substitution",
                newName: "IX_Substitution_SubTeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Substitution",
                table: "Substitution",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Substitution_SubstitutionId",
                table: "Classes",
                column: "SubstitutionId",
                principalTable: "Substitution",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Substitution_Users_SubTeacherId",
                table: "Substitution",
                column: "SubTeacherId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
