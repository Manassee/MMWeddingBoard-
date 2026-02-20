CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Notes" (
    "Id" uuid NOT NULL,
    "WeddingId" uuid NOT NULL,
    "Title" text NOT NULL,
    "IsPinned" boolean NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Notes" PRIMARY KEY ("Id")
);

CREATE TABLE "Weddings" (
    wedding_id uuid NOT NULL,
    "Title" text NOT NULL,
    event_date date,
    location character varying(300),
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Weddings" PRIMARY KEY (wedding_id)
);

CREATE TABLE "NoteBlocks" (
    "Id" uuid NOT NULL,
    "NoteId" uuid NOT NULL,
    "BlockType" integer NOT NULL,
    "Content" text NOT NULL,
    "IsChecked" boolean,
    "OrderIndex" integer NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NOT NULL,
    "Title" text NOT NULL,
    CONSTRAINT "PK_NoteBlocks" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_NoteBlocks_Notes_NoteId" FOREIGN KEY ("NoteId") REFERENCES "Notes" ("Id") ON DELETE CASCADE
);

CREATE TABLE budgets (
    budget_id uuid NOT NULL,
    wedding_id uuid NOT NULL,
    category character varying(200) NOT NULL,
    planned_amount numeric(12,2) NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone NOT NULL,
    CONSTRAINT "PK_budgets" PRIMARY KEY (budget_id),
    CONSTRAINT "FK_budgets_Weddings_wedding_id" FOREIGN KEY (wedding_id) REFERENCES "Weddings" (wedding_id) ON DELETE CASCADE
);

CREATE TABLE guests (
    guest_id uuid NOT NULL,
    wedding_id uuid NOT NULL,
    first_name character varying(120) NOT NULL,
    last_name character varying(120) NOT NULL,
    party_size integer NOT NULL,
    children_count integer NOT NULL,
    status integer NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone NOT NULL,
    CONSTRAINT "PK_guests" PRIMARY KEY (guest_id),
    CONSTRAINT "FK_guests_Weddings_wedding_id" FOREIGN KEY (wedding_id) REFERENCES "Weddings" (wedding_id) ON DELETE CASCADE
);

CREATE TABLE tasks (
    task_id uuid NOT NULL,
    wedding_id uuid NOT NULL,
    title character varying(200) NOT NULL,
    description text,
    status integer NOT NULL,
    priority integer NOT NULL,
    due_date date,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone NOT NULL,
    CONSTRAINT "PK_tasks" PRIMARY KEY (task_id),
    CONSTRAINT "FK_tasks_Weddings_wedding_id" FOREIGN KEY (wedding_id) REFERENCES "Weddings" (wedding_id) ON DELETE CASCADE
);

CREATE TABLE budget_items (
    budget_item_id uuid NOT NULL,
    budget_id uuid NOT NULL,
    title character varying(200) NOT NULL,
    amount numeric(12,2) NOT NULL,
    type integer NOT NULL,
    payment_status integer NOT NULL,
    paid_at timestamp with time zone,
    vendor_name character varying(200),
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone NOT NULL,
    CONSTRAINT "PK_budget_items" PRIMARY KEY (budget_item_id),
    CONSTRAINT "FK_budget_items_budgets_budget_id" FOREIGN KEY (budget_id) REFERENCES budgets (budget_id) ON DELETE CASCADE
);

CREATE TABLE subtasks (
    subtask_id uuid NOT NULL,
    task_id uuid NOT NULL,
    title character varying(200) NOT NULL,
    is_done boolean NOT NULL,
    order_index integer NOT NULL,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone NOT NULL,
    CONSTRAINT "PK_subtasks" PRIMARY KEY (subtask_id),
    CONSTRAINT "FK_subtasks_tasks_task_id" FOREIGN KEY (task_id) REFERENCES tasks (task_id) ON DELETE CASCADE
);

CREATE INDEX "IX_budget_items_budget_id" ON budget_items (budget_id);

CREATE INDEX "IX_budget_items_budget_id_type" ON budget_items (budget_id, type);

CREATE INDEX "IX_budgets_wedding_id" ON budgets (wedding_id);

CREATE UNIQUE INDEX "IX_budgets_wedding_id_category" ON budgets (wedding_id, category);

CREATE INDEX "IX_guests_wedding_id" ON guests (wedding_id);

CREATE INDEX "IX_guests_wedding_id_last_name_first_name" ON guests (wedding_id, last_name, first_name);

CREATE INDEX "IX_NoteBlocks_NoteId" ON "NoteBlocks" ("NoteId");

CREATE INDEX "IX_subtasks_task_id" ON subtasks (task_id);

CREATE UNIQUE INDEX "IX_subtasks_task_id_order_index" ON subtasks (task_id, order_index);

CREATE INDEX "IX_tasks_due_date" ON tasks (due_date);

CREATE INDEX "IX_tasks_wedding_id" ON tasks (wedding_id);

CREATE INDEX "IX_tasks_wedding_id_status" ON tasks (wedding_id, status);

CREATE INDEX "IX_Weddings_event_date" ON "Weddings" (event_date);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20260220134506_InitialCreate', '8.0.23');

COMMIT;

