using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todolist.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserOtps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_otps",
                schema: "todolist",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: false),
                    purpose = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    code_salt = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    code_hash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    token_protected = table.Column<string>(type: "text", nullable: false),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    consumed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    attempt_count = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_otps", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_otps_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "todolist",
                        principalTable: "asp_net_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_otps_user_id_purpose_expires_at",
                schema: "todolist",
                table: "user_otps",
                columns: new[] { "user_id", "purpose", "expires_at" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_otps",
                schema: "todolist");
        }
    }
}
