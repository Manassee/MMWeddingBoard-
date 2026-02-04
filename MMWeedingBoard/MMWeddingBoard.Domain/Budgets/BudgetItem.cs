using MMWeddingBoard.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Domain.Budgets
{
    public class BudgetItem : Entity
    {
        // EF Core
        protected BudgetItem() { }

        public BudgetItem(
            Guid budgetId,
            string title,
            decimal amount,
            BudgetItemType type,
            PaymentStatus paymentStatus,
            string? vendorName,
            DateTimeOffset? paidAt)
        {
            if (budgetId == Guid.Empty)
                throw new ArgumentException("BudgetId is required.", nameof(budgetId));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title must not be empty.", nameof(title));

            if (amount <= 0m)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than 0.");

            Id = Guid.NewGuid();
            BudgetId = budgetId;

            Title = title.Trim();
            Amount = amount;
            Type = type;

            VendorName = string.IsNullOrWhiteSpace(vendorName) ? null : vendorName.Trim();

            PaymentStatus = paymentStatus;
            PaidAt = paidAt;

            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = CreatedAt;
        }

        public Guid BudgetId { get; private set; }

        public string Title { get; private set; } = default!;
        public decimal Amount { get; private set; }

        public BudgetItemType Type { get; private set; }

        public PaymentStatus PaymentStatus { get; private set; }
        public DateTimeOffset? PaidAt { get; private set; }

        public string? VendorName { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }

        // =========================
        // Fachliche Domain-Methoden
        // =========================

        public void Rename(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title must not be empty.", nameof(title));

            Title = title.Trim();
            Touch();
        }

        public void ChangeAmount(decimal amount)
        {
            if (amount <= 0m)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than 0.");

            Amount = amount;
            Touch();
        }

        public void ChangeVendor(string? vendorName)
        {
            VendorName = string.IsNullOrWhiteSpace(vendorName) ? null : vendorName.Trim();
            Touch();
        }

        public void MarkPaid(DateTimeOffset? paidAt = null)
        {
            PaymentStatus = PaymentStatus.Paid;
            PaidAt = paidAt ?? DateTimeOffset.UtcNow;
            Touch();
        }

        public void MarkOpen()
        {
            PaymentStatus = PaymentStatus.Open;
            PaidAt = null;
            Touch();
        }

        private void Touch() => UpdatedAt = DateTimeOffset.UtcNow;
    }
}
