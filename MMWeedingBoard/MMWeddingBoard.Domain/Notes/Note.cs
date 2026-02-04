using MMWeddingBoard.Domain.Common;
using MMWeddingBoard.Domain.Weddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Domain.Notes
{
    /// <summary>
    /// Aggregate Root: Note (Apple-Notes-ähnliche Notiz)
    /// </summary>
    public class Note : Entity
    {
        private readonly List<NoteBlock> _blocks = new();

        // EF Core
        protected Note() { }

        public Note(Guid weddingId, string title)
        {
            if (weddingId == Guid.Empty)
                throw new ArgumentException("WeddingId is required.", nameof(weddingId));

            Id = Guid.NewGuid();
            WeddingId = weddingId;

            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;

            Rename(title);
            IsPinned = false;
        }

        // =================
        // Properties
        // =================

        public Guid WeddingId { get; private set; }

        public string Title { get; private set; } = default!;
        public bool IsPinned { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public IReadOnlyCollection<NoteBlock> Blocks => _blocks.AsReadOnly();

        // =========================
        // Fachliche Domain-Methoden
        // =========================

        /// <summary>
        /// Titel ändern (Pflichtfeld, kein Whitespace)
        /// </summary>
        public void Rename(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title must not be empty.", nameof(title));

            Title = title.Trim();
            Touch();
        }

        public void Pin()
        {
            IsPinned = true;
            Touch();
        }

        public void Unpin()
        {
            IsPinned = false;
            Touch();
        }

        /// <summary>
        /// Fügt einen neuen Block hinzu (Text, Liste, Checkbox)
        /// </summary>
        public NoteBlock AddBlock(NoteBlockType type, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content must not be empty.", nameof(content));

            var orderIndex = _blocks.Count;

            var block = new NoteBlock(
                noteId: Id,
                type: type,
                content: content.Trim(),
                orderIndex: orderIndex
            );

            _blocks.Add(block);
            Touch();
            return block;
        }

        public void RemoveBlock(Guid blockId)
        {
            var idx = _blocks.FindIndex(b => b.Id == blockId);
            if (idx < 0) return;

            _blocks.RemoveAt(idx);
            ReorderBlocks();
            Touch();
        }

        /// <summary>
        /// Wird beim Autosave aufgerufen
        /// </summary>
        public void Touch()
        {
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        private void ReorderBlocks()
        {
            for (int i = 0; i < _blocks.Count; i++)
                _blocks[i].ChangeOrder(i);
        }
    }
}

