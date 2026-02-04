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
        protected Wedding()   
        { 
            
        }

        public Wedding(string title, DateOnly? eventDate = null, string? location = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Titel ist nötig.", nameof(title));

            Id = Guid.NewGuid();
            Title = title.Trim();
            EventDate = eventDate;
            Location = location?.Trim();

            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;
        }

        public string Title { get; private set; }
        public DateOnly? EventDate { get; private set; }
        public string? Location { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

/* ========================================================================================================================================
 */
        public void Rename(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title darf nicht leer sein.", nameof(title));

            Title = title.Trim();
            Touch();
        }

        /// <summary>
        /// Datum setzen/entfernen (optional). Kann auch null sein.
        /// </summary>
        public void SetEventDate(DateOnly? date)
        {
            EventDate = date;
            Touch();
        }

        /// <summary>
        /// Location setzen (optional).
        /// - null / "" / "   " wird zu null
        /// - sonst wird getrimmt gespeichert
        /// </summary>
        public void SetLocation(string? location)
        {
            if (string.IsNullOrWhiteSpace(location))
                Location = null;
            else
                Location = location.Trim();

            Touch();
        }

        private void Touch() => UpdatedAt = DateTimeOffset.UtcNow;


    }
}
