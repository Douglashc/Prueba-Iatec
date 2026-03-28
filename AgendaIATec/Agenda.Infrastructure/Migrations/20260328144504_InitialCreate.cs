using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Agenda.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Alias = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Location = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    ParticipantIds = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitations_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreatorId", "Description", "EndDate", "Location", "Name", "ParticipantIds", "StartDate", "Status", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, 1, "Revisión del sprint actual", new DateTime(2026, 3, 28, 12, 45, 3, 904, DateTimeKind.Local).AddTicks(5249), "Oficina", "Sprint Review", "[]", new DateTime(2026, 3, 28, 8, 45, 3, 904, DateTimeKind.Local).AddTicks(5233), true, 1, null },
                    { 2, 1, "Discusión de arquitectura", new DateTime(2026, 3, 28, 13, 45, 3, 904, DateTimeKind.Local).AddTicks(5252), "Zoom", "Reunión técnica", "[]", new DateTime(2026, 3, 28, 9, 45, 3, 904, DateTimeKind.Local).AddTicks(5251), true, 0, null },
                    { 3, 1, "Repechaje mundialista", new DateTime(2026, 3, 29, 12, 45, 3, 904, DateTimeKind.Local).AddTicks(5255), "Estadio Hernando Siles", "Partido Bolivia vs Surinam", "[]", new DateTime(2026, 3, 29, 10, 45, 3, 904, DateTimeKind.Local).AddTicks(5253), true, 0, null },
                    { 4, 1, "Presentación del sistema", new DateTime(2026, 3, 30, 11, 45, 3, 904, DateTimeKind.Local).AddTicks(5257), "Oficina", "Demo al cliente", "[]", new DateTime(2026, 3, 30, 10, 45, 3, 904, DateTimeKind.Local).AddTicks(5257), true, 1, null },
                    { 5, 1, "Rutina gym", new DateTime(2026, 3, 31, 12, 45, 3, 904, DateTimeKind.Local).AddTicks(5260), "Gym", "Entrenamiento personal", "[]", new DateTime(2026, 3, 31, 10, 45, 3, 904, DateTimeKind.Local).AddTicks(5259), true, 1, null },
                    { 6, 1, "Salida social", new DateTime(2026, 4, 1, 13, 45, 3, 904, DateTimeKind.Local).AddTicks(5262), "Restaurante", "Cena con amigos", "[]", new DateTime(2026, 4, 1, 10, 45, 3, 904, DateTimeKind.Local).AddTicks(5262), true, 0, null },
                    { 7, 1, "Capacitación frontend", new DateTime(2026, 3, 23, 13, 45, 3, 904, DateTimeKind.Local).AddTicks(5265), "Centro TI", "Curso de Angular", "[]", new DateTime(2026, 3, 23, 10, 45, 3, 904, DateTimeKind.Local).AddTicks(5264), true, 1, null },
                    { 8, 1, "Amistoso internacional", new DateTime(2026, 3, 25, 12, 45, 3, 904, DateTimeKind.Local).AddTicks(5267), "Estadio", "Partido Bolivia vs Perú", "[]", new DateTime(2026, 3, 25, 10, 45, 3, 904, DateTimeKind.Local).AddTicks(5266), true, 0, null },
                    { 9, 1, "Liberación versión 1.0", new DateTime(2026, 3, 26, 11, 45, 3, 904, DateTimeKind.Local).AddTicks(5269), "Servidor", "Deploy producción", "[]", new DateTime(2026, 3, 26, 10, 45, 3, 904, DateTimeKind.Local).AddTicks(5269), true, 1, null },
                    { 10, 2, "Reunión diaria", new DateTime(2026, 3, 28, 11, 45, 3, 904, DateTimeKind.Local).AddTicks(5272), "Teams", "Daily Scrum", "[]", new DateTime(2026, 3, 28, 9, 45, 3, 904, DateTimeKind.Local).AddTicks(5271), true, 1, null },
                    { 11, 2, "Resolución de bugs", new DateTime(2026, 3, 28, 12, 45, 3, 904, DateTimeKind.Local).AddTicks(5274), "Oficina", "Debug sesión", "[]", new DateTime(2026, 3, 28, 8, 45, 3, 904, DateTimeKind.Local).AddTicks(5274), true, 0, null },
                    { 12, 2, "Organización tareas", new DateTime(2026, 3, 29, 12, 45, 3, 904, DateTimeKind.Local).AddTicks(5277), "Zoom", "Planificación sprint", "[]", new DateTime(2026, 3, 29, 10, 45, 3, 904, DateTimeKind.Local).AddTicks(5276), true, 1, null },
                    { 13, 2, "Partido entre amigos", new DateTime(2026, 3, 30, 12, 45, 3, 904, DateTimeKind.Local).AddTicks(5279), "Cancha", "Partido fútbol", "[]", new DateTime(2026, 3, 30, 10, 45, 3, 904, DateTimeKind.Local).AddTicks(5278), true, 0, null },
                    { 14, 2, "Reunión familiar", new DateTime(2026, 3, 31, 13, 45, 3, 904, DateTimeKind.Local).AddTicks(5281), "Casa", "Cena familiar", "[]", new DateTime(2026, 3, 31, 10, 45, 3, 904, DateTimeKind.Local).AddTicks(5281), true, 1, null },
                    { 15, 2, "Code review", new DateTime(2026, 4, 1, 11, 45, 3, 904, DateTimeKind.Local).AddTicks(5284), "GitHub", "Revisión código", "[]", new DateTime(2026, 4, 1, 10, 45, 3, 904, DateTimeKind.Local).AddTicks(5283), true, 0, null },
                    { 16, 2, "Corrección urgente", new DateTime(2026, 3, 24, 11, 45, 3, 904, DateTimeKind.Local).AddTicks(5286), "Servidor", "Deploy hotfix", "[]", new DateTime(2026, 3, 24, 10, 45, 3, 904, DateTimeKind.Local).AddTicks(5286), true, 1, null },
                    { 17, 2, "Feedback sistema", new DateTime(2026, 3, 25, 12, 45, 3, 904, DateTimeKind.Local).AddTicks(5289), "Oficina", "Reunión cliente", "[]", new DateTime(2026, 3, 25, 10, 45, 3, 904, DateTimeKind.Local).AddTicks(5288), true, 0, null },
                    { 18, 2, "Pruebas del sistema", new DateTime(2026, 3, 26, 12, 45, 3, 904, DateTimeKind.Local).AddTicks(5291), "QA Lab", "Testing QA", "[]", new DateTime(2026, 3, 26, 10, 45, 3, 904, DateTimeKind.Local).AddTicks(5290), true, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Alias", "Email", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "Admin", "douglash.dcz@gmail.com", "YWRtaW4xMjM=", "admin" },
                    { 2, "John", "juan@gmail.com", "anVhbjEyMw==", "juan" },
                    { 3, "Maria", "maria@gmail.com", "bWFyaWExMjM=", "maria" },
                    { 4, "Carlos", "carlos@gmail.com", "Y2FybG9zMTIz", "carlos" }
                });

            migrationBuilder.InsertData(
                table: "Invitations",
                columns: new[] { "Id", "EventId", "ReceiverId", "Status" },
                values: new object[,]
                {
                    { 1, 2, 2, 1 },
                    { 2, 3, 2, 0 },
                    { 3, 6, 2, 1 },
                    { 4, 8, 2, 2 },
                    { 5, 11, 1, 1 },
                    { 6, 13, 1, 0 },
                    { 7, 15, 1, 1 },
                    { 8, 17, 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_UserId",
                table: "Events",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_EventId",
                table: "Invitations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_ReceiverId",
                table: "Invitations",
                column: "ReceiverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
