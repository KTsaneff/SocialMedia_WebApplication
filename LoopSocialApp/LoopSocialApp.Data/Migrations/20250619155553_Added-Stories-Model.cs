using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoopSocialApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedStoriesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.RenameColumn(
                name: "NumberOfReposrts",
                table: "Posts",
                newName: "NumberOfReports");

            migrationBuilder.AddColumn<int>(
                name: "StoryId",
                table: "Reports",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StoryId",
                table: "Likes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StoryId",
                table: "Favorites",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StoryId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "PostId", "StoryId", "ApplicationUserId" });

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfReports = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stories_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_StoryId",
                table: "Reports",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_StoryId",
                table: "Likes",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_StoryId",
                table: "Favorites",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_StoryId",
                table: "Comments",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_ApplicationUserId",
                table: "Stories",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Stories_StoryId",
                table: "Comments",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Stories_StoryId",
                table: "Favorites",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Stories_StoryId",
                table: "Likes",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_Stories_StoryId",
                table: "Reports",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Stories_StoryId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Stories_StoryId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Stories_StoryId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_Stories_StoryId",
                table: "Reports");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_Reports_StoryId",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_StoryId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_StoryId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Comments_StoryId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "StoryId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "StoryId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "StoryId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "StoryId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "NumberOfReports",
                table: "Posts",
                newName: "NumberOfReposrts");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "PostId", "ApplicationUserId" });
        }
    }
}
