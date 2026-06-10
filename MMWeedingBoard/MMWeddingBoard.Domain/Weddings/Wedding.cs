using MMWeddingBoard.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Domain.Weddings
{
    public class Wedding : Entity
    {
        public string BrideName { get; private set; } = default!;
        public string GroomName { get; private set; } = default!;
        public DateOnly? EventDate { get; private set; }
        public string? Location { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        // EF Core
        protected Wedding() { }

        public Wedding(string brideName, string groomName,
                       DateOnly? eventDate = null, string? location = null)
        {
            if (string.IsNullOrWhiteSpace(brideName))
                throw new ArgumentException("BrideName darf nicht leer sein.", nameof(brideName));
            if (string.IsNullOrWhiteSpace(groomName))
                throw new ArgumentException("GroomName darf nicht leer sein.", nameof(groomName));

            Id = Guid.NewGuid();
            BrideName = brideName.Trim();
            GroomName = groomName.Trim();
            EventDate = eventDate;
            Location = location?.Trim();
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;
        }

        public void RenameBride(string brideName)
        {
            if (string.IsNullOrWhiteSpace(brideName))
                throw new ArgumentException("BrideName darf nicht leer sein.", nameof(brideName));
            BrideName = brideName.Trim();  // ← war: Title = ...
            Touch();
        }

        public void RenameGroom(string groomName)
        {
            if (string.IsNullOrWhiteSpace(groomName))
                throw new ArgumentException("GroomName darf nicht leer sein.", nameof(groomName));
            GroomName = groomName.Trim();  // ← war: Title = ...
            Touch();
        }

        public void SetEventDate(DateOnly? date)
        {
            EventDate = date;
            Touch();
        }

        public void SetLocation(string? location)
        {
            Location = string.IsNullOrWhiteSpace(location) ? null : location.Trim();
            Touch();
        }

        private void Touch() => UpdatedAt = DateTimeOffset.UtcNow;
    }
}
