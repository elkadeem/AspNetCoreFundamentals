﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore.Fundamentals.WebApp.IdentityMigration
{
    public partial class AddOpenIddictApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpenIddictApplications",
                schema: "Identity",
                columns: table => new
                {
                    ClientId = table.Column<string>(nullable: false),
                    ClientSecret = table.Column<string>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    ConsentType = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    Permissions = table.Column<string>(nullable: true),
                    PostLogoutRedirectUris = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    RedirectUris = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictScopes",
                schema: "Identity",
                columns: table => new
                {
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Properties = table.Column<string>(nullable: true),
                    Resources = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictScopes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictAuthorizations",
                schema: "Identity",
                columns: table => new
                {
                    ApplicationId = table.Column<string>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    Properties = table.Column<string>(nullable: true),
                    Scopes = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictAuthorizations_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "Identity",
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpenIddictTokens",
                schema: "Identity",
                columns: table => new
                {
                    ApplicationId = table.Column<string>(nullable: true),
                    AuthorizationId = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTimeOffset>(nullable: true),
                    ExpirationDate = table.Column<DateTimeOffset>(nullable: true),
                    ConcurrencyToken = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: false),
                    Payload = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    ReferenceId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "Identity",
                        principalTable: "OpenIddictApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId",
                        column: x => x.AuthorizationId,
                        principalSchema: "Identity",
                        principalTable: "OpenIddictAuthorizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictApplications_ClientId",
                schema: "Identity",
                table: "OpenIddictApplications",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictAuthorizations_ApplicationId",
                schema: "Identity",
                table: "OpenIddictAuthorizations",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictScopes_Name",
                schema: "Identity",
                table: "OpenIddictScopes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ApplicationId",
                schema: "Identity",
                table: "OpenIddictTokens",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_AuthorizationId",
                schema: "Identity",
                table: "OpenIddictTokens",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictTokens_ReferenceId",
                schema: "Identity",
                table: "OpenIddictTokens",
                column: "ReferenceId",
                unique: true,
                filter: "[ReferenceId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenIddictScopes",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "OpenIddictTokens",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "OpenIddictAuthorizations",
                schema: "Identity");

            migrationBuilder.DropTable(
                name: "OpenIddictApplications",
                schema: "Identity");
        }
    }
}
