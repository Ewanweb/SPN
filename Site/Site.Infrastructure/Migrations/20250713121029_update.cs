using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Site.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AgentFeatureGroups");

            migrationBuilder.RenameColumn(
                name: "Percent",
                table: "AgentFeatures",
                newName: "Percentage");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "AgentFeatures",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AgentFeatureGroups",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "AgentFeatures");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AgentFeatureGroups");

            migrationBuilder.RenameColumn(
                name: "Percentage",
                table: "AgentFeatures",
                newName: "Percent");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AgentFeatureGroups",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }
    }
}
