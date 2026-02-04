using MMWeddingBoard.Domain.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Domain.Guests
{
    public class Guest : Entity
    {
        protected Guest() { }

        public Guest(Guid weddingId, string firstName, string lastName, int partySize = 1, int childrenCount = 0)
        {
            if(weddingId == Guid.Empty) throw new ArgumentNullException("WeddingId ist notwendig.", nameof(weddingId));

            Id = Guid.NewGuid();
            WeddingId = weddingId;

            SetName(firstName, lastName);
            SetPartySize(partySize);
            SetChildrenCount(childrenCount);

            Status = GuestStatus.Invited;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;

        }

        // =================
        // Properties
        // =================

        public Guid WeddingId { get; private set; }

        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;

        public int PartySize { get; private set; }
        public int ChildrenCount { get; private set; }

        public GuestStatus Status { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        // =========================
        // Fachliche Domain-Methoden
        // =========================

        /// <summary>
        /// Vor- und Nachname setzen/ändern
        /// </summary>
        public void SetName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("FirstName must not be empty.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("LastName must not be empty.", nameof(lastName));

            FirstName = firstName.Trim();
            LastName = lastName.Trim();
            Touch();
        }

        /// <summary>
        /// Anzahl der Personen (Gast + Begleitungen)
        /// </summary>
        public void SetPartySize(int partySize)
        {
            if (partySize < 1)
                throw new ArgumentOutOfRangeException(nameof(partySize), "Begleitperson muss mindestens 1.");

            PartySize = partySize;
            Touch();
        }

        /// <summary>
        /// Anzahl der Kinder (optional, >= 0)
        /// </summary>
        public void SetChildrenCount(int childrenCount)
        {
            if (childrenCount < 0)
                throw new ArgumentOutOfRangeException(nameof(childrenCount), "ChildrenCount cannot be negative.");

            ChildrenCount = childrenCount;
            Touch();
        }

        /// <summary>
        /// Status ändern (z. B. Invited → Accepted → Declined)
        /// </summary>
        public void ChangeStatus(GuestStatus status)
        {
            Status = status;
            Touch();
        }

        private void Touch() => UpdatedAt = DateTimeOffset.UtcNow;
    }
}
