using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todolist.Api.Migrations
{
    /// <inheritdoc />
    public partial class SnakeCaseNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                schema: "todolist",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                schema: "todolist",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                schema: "todolist",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                schema: "todolist",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                schema: "todolist",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                schema: "todolist",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_TodoItems_AspNetUsers_OwnerId",
                schema: "todolist",
                table: "TodoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoItems",
                schema: "todolist",
                table: "TodoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                schema: "todolist",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                schema: "todolist",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                schema: "todolist",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                schema: "todolist",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                schema: "todolist",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                schema: "todolist",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                schema: "todolist",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "TodoItems",
                schema: "todolist",
                newName: "todo_items",
                newSchema: "todolist");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "todolist",
                newName: "asp_net_user_tokens",
                newSchema: "todolist");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "todolist",
                newName: "asp_net_users",
                newSchema: "todolist");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "todolist",
                newName: "asp_net_user_roles",
                newSchema: "todolist");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "todolist",
                newName: "asp_net_user_logins",
                newSchema: "todolist");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "todolist",
                newName: "asp_net_user_claims",
                newSchema: "todolist");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "todolist",
                newName: "asp_net_roles",
                newSchema: "todolist");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "todolist",
                newName: "asp_net_role_claims",
                newSchema: "todolist");

            migrationBuilder.RenameColumn(
                name: "Title",
                schema: "todolist",
                table: "todo_items",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Priority",
                schema: "todolist",
                table: "todo_items",
                newName: "priority");

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "todolist",
                table: "todo_items",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "todolist",
                table: "todo_items",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                schema: "todolist",
                table: "todo_items",
                newName: "owner_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                schema: "todolist",
                table: "todo_items",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CompletedAt",
                schema: "todolist",
                table: "todo_items",
                newName: "completed_at");

            migrationBuilder.RenameIndex(
                name: "IX_TodoItems_OwnerId_Title",
                schema: "todolist",
                table: "todo_items",
                newName: "IX_todo_items_owner_id_title");

            migrationBuilder.RenameIndex(
                name: "IX_TodoItems_Id",
                schema: "todolist",
                table: "todo_items",
                newName: "IX_todo_items_id");

            migrationBuilder.RenameColumn(
                name: "Value",
                schema: "todolist",
                table: "asp_net_user_tokens",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "todolist",
                table: "asp_net_user_tokens",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                schema: "todolist",
                table: "asp_net_user_tokens",
                newName: "login_provider");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "todolist",
                table: "asp_net_user_tokens",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "todolist",
                table: "asp_net_users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "todolist",
                table: "asp_net_users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "todolist",
                table: "asp_net_users",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                schema: "todolist",
                table: "asp_net_users",
                newName: "two_factor_enabled");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                schema: "todolist",
                table: "asp_net_users",
                newName: "security_stamp");

            migrationBuilder.RenameColumn(
                name: "PhoneNumberConfirmed",
                schema: "todolist",
                table: "asp_net_users",
                newName: "phone_number_confirmed");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "todolist",
                table: "asp_net_users",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                schema: "todolist",
                table: "asp_net_users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "NormalizedUserName",
                schema: "todolist",
                table: "asp_net_users",
                newName: "normalized_user_name");

            migrationBuilder.RenameColumn(
                name: "NormalizedEmail",
                schema: "todolist",
                table: "asp_net_users",
                newName: "normalized_email");

            migrationBuilder.RenameColumn(
                name: "LockoutEnd",
                schema: "todolist",
                table: "asp_net_users",
                newName: "lockout_end");

            migrationBuilder.RenameColumn(
                name: "LockoutEnabled",
                schema: "todolist",
                table: "asp_net_users",
                newName: "lockout_enabled");

            migrationBuilder.RenameColumn(
                name: "EmailConfirmed",
                schema: "todolist",
                table: "asp_net_users",
                newName: "email_confirmed");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                schema: "todolist",
                table: "asp_net_users",
                newName: "concurrency_stamp");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                schema: "todolist",
                table: "asp_net_users",
                newName: "access_failed_count");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "todolist",
                table: "asp_net_user_roles",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "todolist",
                table: "asp_net_user_roles",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "todolist",
                table: "asp_net_user_roles",
                newName: "IX_asp_net_user_roles_role_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "todolist",
                table: "asp_net_user_logins",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ProviderDisplayName",
                schema: "todolist",
                table: "asp_net_user_logins",
                newName: "provider_display_name");

            migrationBuilder.RenameColumn(
                name: "ProviderKey",
                schema: "todolist",
                table: "asp_net_user_logins",
                newName: "provider_key");

            migrationBuilder.RenameColumn(
                name: "LoginProvider",
                schema: "todolist",
                table: "asp_net_user_logins",
                newName: "login_provider");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "todolist",
                table: "asp_net_user_logins",
                newName: "IX_asp_net_user_logins_user_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "todolist",
                table: "asp_net_user_claims",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "todolist",
                table: "asp_net_user_claims",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                schema: "todolist",
                table: "asp_net_user_claims",
                newName: "claim_value");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                schema: "todolist",
                table: "asp_net_user_claims",
                newName: "claim_type");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "todolist",
                table: "asp_net_user_claims",
                newName: "IX_asp_net_user_claims_user_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "todolist",
                table: "asp_net_roles",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "todolist",
                table: "asp_net_roles",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "NormalizedName",
                schema: "todolist",
                table: "asp_net_roles",
                newName: "normalized_name");

            migrationBuilder.RenameColumn(
                name: "ConcurrencyStamp",
                schema: "todolist",
                table: "asp_net_roles",
                newName: "concurrency_stamp");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "todolist",
                table: "asp_net_role_claims",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                schema: "todolist",
                table: "asp_net_role_claims",
                newName: "role_id");

            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                schema: "todolist",
                table: "asp_net_role_claims",
                newName: "claim_value");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                schema: "todolist",
                table: "asp_net_role_claims",
                newName: "claim_type");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "todolist",
                table: "asp_net_role_claims",
                newName: "IX_asp_net_role_claims_role_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_todo_items",
                schema: "todolist",
                table: "todo_items",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_asp_net_user_tokens",
                schema: "todolist",
                table: "asp_net_user_tokens",
                columns: new[] { "user_id", "login_provider", "name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_asp_net_users",
                schema: "todolist",
                table: "asp_net_users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_asp_net_user_roles",
                schema: "todolist",
                table: "asp_net_user_roles",
                columns: new[] { "user_id", "role_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_asp_net_user_logins",
                schema: "todolist",
                table: "asp_net_user_logins",
                columns: new[] { "login_provider", "provider_key" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_asp_net_user_claims",
                schema: "todolist",
                table: "asp_net_user_claims",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_asp_net_roles",
                schema: "todolist",
                table: "asp_net_roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_asp_net_role_claims",
                schema: "todolist",
                table: "asp_net_role_claims",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_role_claims_asp_net_roles_role_id",
                schema: "todolist",
                table: "asp_net_role_claims",
                column: "role_id",
                principalSchema: "todolist",
                principalTable: "asp_net_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_user_claims_asp_net_users_user_id",
                schema: "todolist",
                table: "asp_net_user_claims",
                column: "user_id",
                principalSchema: "todolist",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_user_logins_asp_net_users_user_id",
                schema: "todolist",
                table: "asp_net_user_logins",
                column: "user_id",
                principalSchema: "todolist",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_user_roles_asp_net_roles_role_id",
                schema: "todolist",
                table: "asp_net_user_roles",
                column: "role_id",
                principalSchema: "todolist",
                principalTable: "asp_net_roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_user_roles_asp_net_users_user_id",
                schema: "todolist",
                table: "asp_net_user_roles",
                column: "user_id",
                principalSchema: "todolist",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_asp_net_user_tokens_asp_net_users_user_id",
                schema: "todolist",
                table: "asp_net_user_tokens",
                column: "user_id",
                principalSchema: "todolist",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_todo_items_asp_net_users_owner_id",
                schema: "todolist",
                table: "todo_items",
                column: "owner_id",
                principalSchema: "todolist",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_role_claims_asp_net_roles_role_id",
                schema: "todolist",
                table: "asp_net_role_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_claims_asp_net_users_user_id",
                schema: "todolist",
                table: "asp_net_user_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_logins_asp_net_users_user_id",
                schema: "todolist",
                table: "asp_net_user_logins");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_roles_asp_net_roles_role_id",
                schema: "todolist",
                table: "asp_net_user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_roles_asp_net_users_user_id",
                schema: "todolist",
                table: "asp_net_user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_asp_net_user_tokens_asp_net_users_user_id",
                schema: "todolist",
                table: "asp_net_user_tokens");

            migrationBuilder.DropForeignKey(
                name: "FK_todo_items_asp_net_users_owner_id",
                schema: "todolist",
                table: "todo_items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_todo_items",
                schema: "todolist",
                table: "todo_items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asp_net_users",
                schema: "todolist",
                table: "asp_net_users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asp_net_user_tokens",
                schema: "todolist",
                table: "asp_net_user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asp_net_user_roles",
                schema: "todolist",
                table: "asp_net_user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asp_net_user_logins",
                schema: "todolist",
                table: "asp_net_user_logins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asp_net_user_claims",
                schema: "todolist",
                table: "asp_net_user_claims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asp_net_roles",
                schema: "todolist",
                table: "asp_net_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_asp_net_role_claims",
                schema: "todolist",
                table: "asp_net_role_claims");

            migrationBuilder.RenameTable(
                name: "todo_items",
                schema: "todolist",
                newName: "TodoItems",
                newSchema: "todolist");

            migrationBuilder.RenameTable(
                name: "asp_net_users",
                schema: "todolist",
                newName: "AspNetUsers",
                newSchema: "todolist");

            migrationBuilder.RenameTable(
                name: "asp_net_user_tokens",
                schema: "todolist",
                newName: "AspNetUserTokens",
                newSchema: "todolist");

            migrationBuilder.RenameTable(
                name: "asp_net_user_roles",
                schema: "todolist",
                newName: "AspNetUserRoles",
                newSchema: "todolist");

            migrationBuilder.RenameTable(
                name: "asp_net_user_logins",
                schema: "todolist",
                newName: "AspNetUserLogins",
                newSchema: "todolist");

            migrationBuilder.RenameTable(
                name: "asp_net_user_claims",
                schema: "todolist",
                newName: "AspNetUserClaims",
                newSchema: "todolist");

            migrationBuilder.RenameTable(
                name: "asp_net_roles",
                schema: "todolist",
                newName: "AspNetRoles",
                newSchema: "todolist");

            migrationBuilder.RenameTable(
                name: "asp_net_role_claims",
                schema: "todolist",
                newName: "AspNetRoleClaims",
                newSchema: "todolist");

            migrationBuilder.RenameColumn(
                name: "title",
                schema: "todolist",
                table: "TodoItems",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "priority",
                schema: "todolist",
                table: "TodoItems",
                newName: "Priority");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "todolist",
                table: "TodoItems",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "todolist",
                table: "TodoItems",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "owner_id",
                schema: "todolist",
                table: "TodoItems",
                newName: "OwnerId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                schema: "todolist",
                table: "TodoItems",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "completed_at",
                schema: "todolist",
                table: "TodoItems",
                newName: "CompletedAt");

            migrationBuilder.RenameIndex(
                name: "IX_todo_items_owner_id_title",
                schema: "todolist",
                table: "TodoItems",
                newName: "IX_TodoItems_OwnerId_Title");

            migrationBuilder.RenameIndex(
                name: "IX_todo_items_id",
                schema: "todolist",
                table: "TodoItems",
                newName: "IX_TodoItems_Id");

            migrationBuilder.RenameColumn(
                name: "email",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_name",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "two_factor_enabled",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "TwoFactorEnabled");

            migrationBuilder.RenameColumn(
                name: "security_stamp",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "phone_number_confirmed",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "PhoneNumberConfirmed");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "normalized_user_name",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "NormalizedUserName");

            migrationBuilder.RenameColumn(
                name: "normalized_email",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "NormalizedEmail");

            migrationBuilder.RenameColumn(
                name: "lockout_end",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "LockoutEnd");

            migrationBuilder.RenameColumn(
                name: "lockout_enabled",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "LockoutEnabled");

            migrationBuilder.RenameColumn(
                name: "email_confirmed",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "EmailConfirmed");

            migrationBuilder.RenameColumn(
                name: "concurrency_stamp",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "access_failed_count",
                schema: "todolist",
                table: "AspNetUsers",
                newName: "AccessFailedCount");

            migrationBuilder.RenameColumn(
                name: "value",
                schema: "todolist",
                table: "AspNetUserTokens",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "todolist",
                table: "AspNetUserTokens",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "login_provider",
                schema: "todolist",
                table: "AspNetUserTokens",
                newName: "LoginProvider");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "todolist",
                table: "AspNetUserTokens",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "role_id",
                schema: "todolist",
                table: "AspNetUserRoles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "todolist",
                table: "AspNetUserRoles",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_asp_net_user_roles_role_id",
                schema: "todolist",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "todolist",
                table: "AspNetUserLogins",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "provider_display_name",
                schema: "todolist",
                table: "AspNetUserLogins",
                newName: "ProviderDisplayName");

            migrationBuilder.RenameColumn(
                name: "provider_key",
                schema: "todolist",
                table: "AspNetUserLogins",
                newName: "ProviderKey");

            migrationBuilder.RenameColumn(
                name: "login_provider",
                schema: "todolist",
                table: "AspNetUserLogins",
                newName: "LoginProvider");

            migrationBuilder.RenameIndex(
                name: "IX_asp_net_user_logins_user_id",
                schema: "todolist",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "todolist",
                table: "AspNetUserClaims",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "todolist",
                table: "AspNetUserClaims",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "claim_value",
                schema: "todolist",
                table: "AspNetUserClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claim_type",
                schema: "todolist",
                table: "AspNetUserClaims",
                newName: "ClaimType");

            migrationBuilder.RenameIndex(
                name: "IX_asp_net_user_claims_user_id",
                schema: "todolist",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "todolist",
                table: "AspNetRoles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "todolist",
                table: "AspNetRoles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "normalized_name",
                schema: "todolist",
                table: "AspNetRoles",
                newName: "NormalizedName");

            migrationBuilder.RenameColumn(
                name: "concurrency_stamp",
                schema: "todolist",
                table: "AspNetRoles",
                newName: "ConcurrencyStamp");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "todolist",
                table: "AspNetRoleClaims",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "role_id",
                schema: "todolist",
                table: "AspNetRoleClaims",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "claim_value",
                schema: "todolist",
                table: "AspNetRoleClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "claim_type",
                schema: "todolist",
                table: "AspNetRoleClaims",
                newName: "ClaimType");

            migrationBuilder.RenameIndex(
                name: "IX_asp_net_role_claims_role_id",
                schema: "todolist",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoItems",
                schema: "todolist",
                table: "TodoItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                schema: "todolist",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                schema: "todolist",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                schema: "todolist",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                schema: "todolist",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                schema: "todolist",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                schema: "todolist",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                schema: "todolist",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                schema: "todolist",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalSchema: "todolist",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                schema: "todolist",
                table: "AspNetUserClaims",
                column: "UserId",
                principalSchema: "todolist",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                schema: "todolist",
                table: "AspNetUserLogins",
                column: "UserId",
                principalSchema: "todolist",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                schema: "todolist",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalSchema: "todolist",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                schema: "todolist",
                table: "AspNetUserRoles",
                column: "UserId",
                principalSchema: "todolist",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                schema: "todolist",
                table: "AspNetUserTokens",
                column: "UserId",
                principalSchema: "todolist",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItems_AspNetUsers_OwnerId",
                schema: "todolist",
                table: "TodoItems",
                column: "OwnerId",
                principalSchema: "todolist",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
