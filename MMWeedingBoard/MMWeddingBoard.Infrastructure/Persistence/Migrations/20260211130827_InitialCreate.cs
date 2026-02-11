using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMWeddingBoard.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WeddingId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    IsPinned = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weddings",
                columns: table => new
                {
                    wedding_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    event_date = table.Column<DateOnly>(type: "date", nullable: true),
                    location = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weddings", x => x.wedding_id);
                });

            migrationBuilder.CreateTable(
                name: "NoteBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NoteId = table.Column<Guid>(type: "uuid", nullable: false),
                    BlockType = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    IsChecked = table.Column<bool>(type: "boolean", nullable: true),
                    OrderIndex = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoteBlocks_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "budgets",
                columns: table => new
                {
                    budget_id = table.Column<Guid>(type: "uuid", nullable: false),
                    wedding_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    planned_amount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_budgets", x => x.budget_id);
                    table.ForeignKey(
                        name: "FK_budgets_Weddings_wedding_id",
                        column: x => x.wedding_id,
                        principalTable: "Weddings",
                        principalColumn: "wedding_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guests",
                columns: table => new
                {
                    guest_id = table.Column<Guid>(type: "uuid", nullable: false),
                    wedding_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    last_name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    party_size = table.Column<int>(type: "integer", nullable: false),
                    children_count = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guests", x => x.guest_id);
                    table.ForeignKey(
                        name: "FK_guests_Weddings_wedding_id",
                        column: x => x.wedding_id,
                        principalTable: "Weddings",
                        principalColumn: "wedding_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    wedding_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    due_date = table.Column<DateOnly>(type: "date", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.task_id);
                    table.ForeignKey(
                        name: "FK_tasks_Weddings_wedding_id",
                        column: x => x.wedding_id,
                        principalTable: "Weddings",
                        principalColumn: "wedding_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "budget_items",
                columns: table => new
                {
                    budget_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    budget_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    payment_status = table.Column<int>(type: "integer", nullable: false),
                    paid_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    vendor_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_budget_items", x => x.budget_item_id);
                    table.ForeignKey(
                        name: "FK_budget_items_budgets_budget_id",
                        column: x => x.budget_id,
                        principalTable: "budgets",
                        principalColumn: "budget_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subtasks",
                columns: table => new
                {
                    subtask_id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    is_done = table.Column<bool>(type: "boolean", nullable: false),
                    order_index = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subtasks", x => x.subtask_id);
                    table.ForeignKey(
                        name: "FK_subtasks_tasks_task_id",
                        column: x => x.task_id,
                        principalTable: "tasks",
                        principalColumn: "task_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_budget_items_budget_id",
                table: "budget_items",
                column: "budget_id");

            migrationBuilder.CreateIndex(
                name: "IX_budget_items_budget_id_type",
                table: "budget_items",
                columns: new[] { "budget_id", "type" });

            migrationBuilder.CreateIndex(
                name: "IX_budgets_wedding_id",
                table: "budgets",
                column: "wedding_id");

            migrationBuilder.CreateIndex(
                name: "IX_budgets_wedding_id_category",
                table: "budgets",
                columns: new[] { "wedding_id", "category" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_guests_wedding_id",
                table: "guests",
                column: "wedding_id");

            migrationBuilder.CreateIndex(
                name: "IX_guests_wedding_id_last_name_first_name",
                table: "guests",
                columns: new[] { "wedding_id", "last_name", "first_name" });

            migrationBuilder.CreateIndex(
                name: "IX_NoteBlocks_NoteId",
                table: "NoteBlocks",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_subtasks_task_id",
                table: "subtasks",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_subtasks_task_id_order_index",
                table: "subtasks",
                columns: new[] { "task_id", "order_index" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tasks_due_date",
                table: "tasks",
                column: "due_date");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_wedding_id",
                table: "tasks",
                column: "wedding_id");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_wedding_id_status",
                table: "tasks",
                columns: new[] { "wedding_id", "status" });

            migrationBuilder.CreateIndex(
                name: "IX_Weddings_event_date",
                table: "Weddings",
                column: "event_date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "budget_items");

            migrationBuilder.DropTable(
                name: "guests");

            migrationBuilder.DropTable(
                name: "NoteBlocks");

            migrationBuilder.DropTable(
                name: "subtasks");

            migrationBuilder.DropTable(
                name: "budgets");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "Weddings");
        }
    }
}
