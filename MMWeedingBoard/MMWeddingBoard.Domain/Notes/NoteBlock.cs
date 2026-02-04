using MMWeddingBoard.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Domain.Notes
{
    /// <summary>
    /// Entity: NoteBlock (Text, Liste, Checkbox)
    /// </summary>
    public class NoteBlock : Entity
    {
        // EF Core
        protected NoteBlock() { }

        public NoteBlock(Guid noteId, NoteBlockType type, string content, int orderIndex)
        {
            if (noteId == Guid.Empty)
                throw new ArgumentException("NoteId is required.", nameof(noteId));

            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content must not be empty.", nameof(content));

            Id = Guid.NewGuid();
            NoteId = noteId;

            BlockType = type;
            Content = content.Trim();
            OrderIndex = orderIndex;

            IsChecked = type == NoteBlockType.Checkbox ? false : null;

            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;
        }

        // =================
        // Properties
        // =================

        public Guid NoteId { get; private set; }

        public NoteBlockType BlockType { get; private set; }

        public string Content { get; private set; } = default!;

        /// <summary>
        /// Nur relevant bei Checkbox-Blöcken
        /// </summary>
        public bool? IsChecked { get; private set; }

        public int OrderIndex { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        // =========================
        // Fachliche Domain-Methoden
        // =========================

        public void UpdateContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content must not be empty.", nameof(content));

            Content = content.Trim();
            Touch();
        }

        public void ToggleCheckbox()
        {
            if (BlockType != NoteBlockType.Checkbox)
                throw new InvalidOperationException("Only checkbox blocks can be toggled.");

            IsChecked = !IsChecked;
            Touch();
        }

        public void ChangeOrder(int orderIndex)
        {
            if (orderIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(orderIndex));

            OrderIndex = orderIndex;
            Touch();
        }

        private void Touch()
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}
