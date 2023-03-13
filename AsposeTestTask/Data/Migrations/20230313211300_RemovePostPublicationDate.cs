﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Avangardum.AsposeTestTask.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovePostPublicationDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicationTime",
                table: "Posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PublicationTime",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
