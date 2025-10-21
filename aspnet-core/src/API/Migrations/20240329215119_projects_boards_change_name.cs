using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class projects_boards_change_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tks.Columns_Tks.Projects_ProjectId",
                table: "Tks.Columns");

            migrationBuilder.DropTable(
                name: "Tks.Projects");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Tks.Columns",
                newName: "BoardId");

            migrationBuilder.RenameIndex(
                name: "IX_Tks.Columns_ProjectId",
                table: "Tks.Columns",
                newName: "IX_Tks.Columns_BoardId");

            migrationBuilder.CreateTable(
                name: "Tks.Boards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tks.Boards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tks.Boards_Tks.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Tks.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tks.Boards_Tks.Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Tks.Workspaces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tks.Boards_UserId",
                table: "Tks.Boards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tks.Boards_WorkspaceId",
                table: "Tks.Boards",
                column: "WorkspaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tks.Columns_Tks.Boards_BoardId",
                table: "Tks.Columns",
                column: "BoardId",
                principalTable: "Tks.Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tks.Columns_Tks.Boards_BoardId",
                table: "Tks.Columns");

            migrationBuilder.DropTable(
                name: "Tks.Boards");

            migrationBuilder.RenameColumn(
                name: "BoardId",
                table: "Tks.Columns",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Tks.Columns_BoardId",
                table: "Tks.Columns",
                newName: "IX_Tks.Columns_ProjectId");

            migrationBuilder.CreateTable(
                name: "Tks.Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    WorkspaceId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tks.Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tks.Projects_Tks.Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Tks.Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tks.Projects_Tks.Workspaces_WorkspaceId",
                        column: x => x.WorkspaceId,
                        principalTable: "Tks.Workspaces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tks.Projects_UserId",
                table: "Tks.Projects",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tks.Projects_WorkspaceId",
                table: "Tks.Projects",
                column: "WorkspaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tks.Columns_Tks.Projects_ProjectId",
                table: "Tks.Columns",
                column: "ProjectId",
                principalTable: "Tks.Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
