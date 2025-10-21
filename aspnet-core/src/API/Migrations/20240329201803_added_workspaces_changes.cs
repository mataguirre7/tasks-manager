using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class added_workspaces_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tks.ProjectUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "WorkspaceId",
                table: "Tks.Projects",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tks.Workspaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tks.Workspaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tks.Workspaces_Tks.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Tks.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tks.Workspaces_Users",
                columns: table => new
                {
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tks.Workspaces_Users", x => new { x.WorkspaceId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Tks.Workspaces_Users_Tks.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Tks.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tks.Workspaces_Users_Tks.Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Tks.Workspaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tks.Projects_WorkspaceId",
                table: "Tks.Projects",
                column: "WorkspaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tks.Workspaces_UserId",
                table: "Tks.Workspaces",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tks.Workspaces_Users_UserId",
                table: "Tks.Workspaces_Users",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tks.Projects_Tks.Workspaces_WorkspaceId",
                table: "Tks.Projects",
                column: "WorkspaceId",
                principalTable: "Tks.Workspaces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tks.Projects_Tks.Workspaces_WorkspaceId",
                table: "Tks.Projects");

            migrationBuilder.DropTable(
                name: "Tks.Workspaces_Users");

            migrationBuilder.DropTable(
                name: "Tks.Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Tks.Projects_WorkspaceId",
                table: "Tks.Projects");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Tks.Projects");

            migrationBuilder.CreateTable(
                name: "Tks.ProjectUsers",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tks.ProjectUsers", x => new { x.ProjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Tks.ProjectUsers_Tks.Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Tks.Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tks.ProjectUsers_Tks.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Tks.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tks.ProjectUsers_UserId",
                table: "Tks.ProjectUsers",
                column: "UserId");
        }
    }
}
