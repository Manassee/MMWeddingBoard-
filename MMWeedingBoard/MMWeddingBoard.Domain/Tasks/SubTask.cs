using MMWeddingBoard.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Domain.Tasks
{
    public class SubTask: Entity
    {
        // EF Core
        protected SubTask() { }

        public SubTask(Guid taskId, string title, int orderIndex = 0)
        {
            if (taskId == Guid.Empty)
                throw new ArgumentException("TaskId is required.", nameof(taskId));

            Id = Guid.NewGuid();
            TaskId = taskId;

            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;

            Rename(title);
            OrderIndex = orderIndex;
            IsDone = false;
        }

        // =================
        // Properties
        // =================

        public Guid TaskId { get; private set; }

        public string Title { get; private set; } = default!;

        public bool IsDone { get; private set; }

        /// <summary>
        /// Reihenfolge innerhalb der Task (UI/UX)
        /// </summary>
        public int OrderIndex { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        // =========================
        // Fachliche Domain-Methoden
        // =========================

        /// <summary>
        /// Titel ändern (Pflichtfeld)
        /// </summary>
        public void Rename(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title must not be empty.", nameof(title));

            Title = title.Trim();
            Touch();
        }

        /// <summary>
        /// SubTask als erledigt markieren
        /// </summary>
        public void MarkDone()
        {
            if (IsDone) return;

            IsDone = true;
            Touch();
        }

        /// <summary>
        /// SubTask wieder öffnen
        /// </summary>
        public void Reopen()
        {
            if (!IsDone) return;

            IsDone = false;
            Touch();
        }

        /// <summary>
        /// Reihenfolge ändern (Drag & Drop im UI)
        /// </summary>
        public void ChangeOrder(int orderIndex)
        {
            if (orderIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(orderIndex));

            OrderIndex = orderIndex;
            Touch();
        }

        private void Touch() => UpdatedAt = DateTimeOffset.UtcNow;
    }
}
