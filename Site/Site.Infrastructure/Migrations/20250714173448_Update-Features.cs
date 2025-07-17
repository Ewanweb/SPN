using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Site.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentFeatures_AgentFeatureGroups_FeatureGroupId",
                table: "AgentFeatures");

            migrationBuilder.DropTable(
                name: "AgentFeatureGroups");

            migrationBuilder.DropIndex(
                name: "IX_AgentFeatures_FeatureGroupId",
                table: "AgentFeatures");

            migrationBuilder.DropColumn(
                name: "FeatureGroupId",
                table: "AgentFeatures");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "AgentFeatures",
                newName: "AgentId");

            migrationBuilder.AddColumn<string>(
                name: "MyProfienece",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "AgentFeatures",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_AgentFeatures_AgentId",
                table: "AgentFeatures",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentFeatures_Agents_AgentId",
                table: "AgentFeatures",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentFeatures_Agents_AgentId",
                table: "AgentFeatures");

            migrationBuilder.DropIndex(
                name: "IX_AgentFeatures_AgentId",
                table: "AgentFeatures");

            migrationBuilder.DropColumn(
                name: "MyProfienece",
                table: "Agents");

            migrationBuilder.RenameColumn(
                name: "AgentId",
                table: "AgentFeatures",
                newName: "GroupId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "AgentFeatures",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<Guid>(
                name: "FeatureGroupId",
                table: "AgentFeatures",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AgentFeatureGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AgentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentFeatureGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgentFeatureGroups_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentFeatures_FeatureGroupId",
                table: "AgentFeatures",
                column: "FeatureGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentFeatureGroups_AgentId",
                table: "AgentFeatureGroups",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentFeatures_AgentFeatureGroups_FeatureGroupId",
                table: "AgentFeatures",
                column: "FeatureGroupId",
                principalTable: "AgentFeatureGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
