using MMWeddingBoard.Domain.Common;
using MMWeddingBoard.Domain.Weddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Domain.Tasks
{
    public class Task:Entity
    {
        protected Task() { }

        public Task(Guid weddingId, string title, string? description = null, DateOnly? dueDate = null)
        {
            if (weddingId == Guid.Empty)
                throw new ArgumentException("WeddingId ist nötig.", nameof(weddingId));

            Id = Guid.NewGuid();
            WeddingId = weddingId;

            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;

            Rename(title);
            SetDescription(description);
            SetDueDate(dueDate);

            Status = TaskStatus.Open;
            Priority = TaskPriority.Medium;
        }

        // =================
        // Properties
        // =================

        public Guid WeddingId { get; private set; }

        public string Title { get; private set; } = default!;
        public string? Description { get; private set; }

        public TaskStatus Status { get; private set; }
        public TaskPriority Priority { get; private set; }

        public DateOnly? DueDate { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        // =========================
        // Fachliche Domain-Methoden
        // =========================

        /// <summary>
        /// Titel ändern (Pflicht)
        /// </summary>
        public void Rename(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title must not be empty.", nameof(title));

            Title = title.Trim();
            Touch();
        }

        /// <summary>
        /// Beschreibung setzen (optional).
        /// Whitespace wird zu null.
        /// </summary>
        public void SetDescription(string? description)
        {
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
            Touch();
        }

        /// <summary>
        /// Fälligkeitsdatum setzen/entfernen (optional)
        /// </summary>
        public void SetDueDate(DateOnly? dueDate)
        {
            DueDate = dueDate;
            Touch();
        }

        public void SetPriority(TaskPriority priority)
        {
            Priority = priority;
            Touch();
        }

        public void Start()
        {
            if (Status == TaskStatus.Done)
                throw new InvalidOperationException("A completed task cannot be started.");

            Status = TaskStatus.InProgress;
            Touch();
        }

        public void MarkDone()
        {
            Status = TaskStatus.Done;
            Touch();
        }

        public void Reopen()
        {
            // Optional: wenn du Reopen nicht willst, dann throw.
            if (Status != TaskStatus.Done)
                throw new InvalidOperationException("Only a completed task can be reopened.");

            Status = TaskStatus.Open;
            Touch();
        }

        private void Touch() => UpdatedAt = DateTimeOffset.UtcNow;

    }
}
