using MMWeddingBoard.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Domain.Budgets
{
    public class Budget : Entity
    {
        private readonly List<BudgetItem> _items = new();

        // EF Core
        protected Budget() { }

        public Budget(Guid weddingId, string category, decimal plannedAmount = 0m)
        {
            if (weddingId == Guid.Empty)
                throw new ArgumentException("WeddingId is required.", nameof(weddingId));

            Id = Guid.NewGuid();
            WeddingId = weddingId;

            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;

            SetCategory(category);
            SetPlannedAmount(plannedAmount);
        }

        public Guid WeddingId { get; private set; }

        public string Category { get; private set; } = default!;
        public decimal PlannedAmount { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        public IReadOnlyCollection<BudgetItem> Items => _items.AsReadOnly();

        // =========================
        // Fachliche Domain-Methoden
        // =========================

        public void SetCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException("Category must not be empty.", nameof(category));

            Category = category.Trim();
            Touch();
        }

        public void SetPlannedAmount(decimal plannedAmount)
        {
            if (plannedAmount < 0m)
                throw new ArgumentOutOfRangeException(nameof(plannedAmount), "Planned amount cannot be negative.");

            PlannedAmount = plannedAmount;
            Touch();
        }

        /// <summary>
        /// Fügt eine Budget-Position hinzu (geplant oder echte Ausgabe).
        /// </summary>
        public BudgetItem AddItem(
            string title,
            decimal amount,
            BudgetItemType type,
            PaymentStatus paymentStatus = PaymentStatus.Open,
            string? vendorName = null,
            DateTimeOffset? paidAt = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title must not be empty.", nameof(title));

            if (amount <= 0m)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than 0.");

            // Regel: PaidAt darf nur gesetzt sein, wenn Status Paid ist
            if (paidAt is not null && paymentStatus != PaymentStatus.Paid)
                throw new InvalidOperationException("PaidAt can only be set when PaymentStatus is Paid.");

            // Regel: wenn Paid ist und PaidAt fehlt, setze jetzt
            if (paymentStatus == PaymentStatus.Paid && paidAt is null)
                paidAt = DateTimeOffset.UtcNow;

            var item = new BudgetItem(
                budgetId: Id,
                title: title.Trim(),
                amount: amount,
                type: type,
                paymentStatus: paymentStatus,
                vendorName: string.IsNullOrWhiteSpace(vendorName) ? null : vendorName.Trim(),
                paidAt: paidAt);

            _items.Add(item);
            Touch();
            return item;
        }

        public void RemoveItem(Guid budgetItemId)
        {
            var idx = _items.FindIndex(x => x.Id == budgetItemId);
            if (idx < 0) return;

            _items.RemoveAt(idx);
            Touch();
        }

        /// <summary>
        /// Rechenwert: Summe der echten Ausgaben (Actual).
        /// (Wird NICHT in der DB gespeichert -> 3NF)
        /// </summary>
        public decimal GetActualTotal()
        {
            decimal total = 0m;
            foreach (var item in _items)
            {
                if (item.Type == BudgetItemType.Actual)
                    total += item.Amount;
            }
            return total;
        }

        /// <summary>
        /// Rechenwert: verbleibendes Budget = PlannedAmount - ActualTotal.
        /// </summary>
        public decimal GetRemaining()
            => PlannedAmount - GetActualTotal();

        private void Touch() => UpdatedAt = DateTimeOffset.UtcNow;
    }
}
