using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMWeddingBoard.Domain.Weddings;
using MMWeddingBoard.Domain.Guests;
using MMWeddingBoard.Domain.Budgets;
using MMWeddingBoard.Infrastructure.Persistence;

namespace MMWeddingBoard.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(WeddingDbContext db)
        {
            // Seed nur, wenn noch keine Hochzeit existiert
            if (await db.Set<Wedding>().AnyAsync())
                return;

            // -------------------------
            // 1) Hochzeit (M&M)
            // -------------------------
            var wedding = new Wedding(
                title: "M&M WeddingBoard – Manasse & Myriam",
                eventDate: new DateOnly(2026, 10, 03),
                location: "München"
            );

            await db.AddAsync(wedding);

            // -------------------------
            // 2) Gäste (realistisch gemischt)
            // -------------------------
            var guests = new List<Guest>
            {
                new Guest(wedding.Id, "Manasse", "Mazumbu", partySize: 1, childrenCount: 0),
                new Guest(wedding.Id, "Myriam", "Mazumbu", partySize: 1, childrenCount: 0),

                new Guest(wedding.Id, "Matthieu", "Yombo", partySize: 1, childrenCount: 0),
                new Guest(wedding.Id, "Colette", "Yombo", partySize: 1, childrenCount: 0),

                new Guest(wedding.Id, "Guyslain", "Mazumbu", partySize: 2, childrenCount: 1),
                new Guest(wedding.Id, "Dikando", "Mazumbu", partySize: 2, childrenCount: 1),

                new Guest(wedding.Id, "Micael", "Yombo", partySize: 2, childrenCount: 0),

                new Guest(wedding.Id, "David", "Keller", partySize: 2, childrenCount: 0),
                new Guest(wedding.Id, "Sarah", "Keller", partySize: 2, childrenCount: 1),

                new Guest(wedding.Id, "Jean", "Mukendi", partySize: 3, childrenCount: 1),
                new Guest(wedding.Id, "Clarisse", "Mukendi", partySize: 4, childrenCount: 2),

                new Guest(wedding.Id, "Patrick", "Ngoma", partySize: 1, childrenCount: 0),
                new Guest(wedding.Id, "Esther", "Ngoma", partySize: 2, childrenCount: 0),

                new Guest(wedding.Id, "Daniel", "Schmidt", partySize: 2, childrenCount: 0),
                new Guest(wedding.Id, "Lea", "Schmidt", partySize: 2, childrenCount: 0),

                new Guest(wedding.Id, "Noah", "Weber", partySize: 1, childrenCount: 0),
                new Guest(wedding.Id, "Miriam", "Weber", partySize: 2, childrenCount: 1),

                new Guest(wedding.Id, "Jonathan", "Meier", partySize: 3, childrenCount: 0),
                new Guest(wedding.Id, "Chiara", "Meier", partySize: 2, childrenCount: 0),

                new Guest(wedding.Id, "Khalil", "Bamba", partySize: 2, childrenCount: 0),
                new Guest(wedding.Id, "Amina", "Bamba", partySize: 4, childrenCount: 2),

                new Guest(wedding.Id, "Louis", "Dubois", partySize: 1, childrenCount: 0),
                new Guest(wedding.Id, "Camille", "Dubois", partySize: 2, childrenCount: 0),

                new Guest(wedding.Id, "Kevin", "Fischer", partySize: 2, childrenCount: 0),
                new Guest(wedding.Id, "Sophie", "Fischer", partySize: 3, childrenCount: 1),

                new Guest(wedding.Id, "Onkel", "Beispiel", partySize: 2, childrenCount: 0),
                new Guest(wedding.Id, "Tante", "Beispiel", partySize: 2, childrenCount: 0),
            };

            // Status gemischt setzen (robust per TryParse)
            // Passe die Namen an, falls deine Enum-Werte anders heißen.
            SetGuestStatus(guests[0], "Accepted");
            SetGuestStatus(guests[1], "Accepted");

            SetGuestStatus(guests[2], "Accepted");
            SetGuestStatus(guests[3], "Accepted");

            SetGuestStatus(guests[4], "Invited");
            SetGuestStatus(guests[5], "Invited");

            SetGuestStatus(guests[6], "Declined");
            SetGuestStatus(guests[7], "Accepted");

            SetGuestStatus(guests[8], "Maybe");
            SetGuestStatus(guests[9], "Accepted");

            SetGuestStatus(guests[10], "Invited");
            SetGuestStatus(guests[11], "Accepted");

            SetGuestStatus(guests[12], "Invited");
            SetGuestStatus(guests[13], "Maybe");

            SetGuestStatus(guests[14], "Accepted");
            SetGuestStatus(guests[15], "Invited");

            SetGuestStatus(guests[16], "Invited");
            SetGuestStatus(guests[17], "Accepted");

            SetGuestStatus(guests[18], "Declined");
            SetGuestStatus(guests[19], "Accepted");

            SetGuestStatus(guests[20], "Invited");
            SetGuestStatus(guests[21], "Invited");

            await db.AddRangeAsync(guests);

            // -------------------------
            // 3) Budgets (Kategorien + Items)
            // -------------------------
            var budgets = new List<Budget>
            {
                new Budget(wedding.Id, "Location", plannedAmount: 6500m),
                new Budget(wedding.Id, "Catering", plannedAmount: 5200m),
                new Budget(wedding.Id, "Fotograf/Video", plannedAmount: 2200m),
                new Budget(wedding.Id, "Outfits", plannedAmount: 1800m),
                new Budget(wedding.Id, "Ringe", plannedAmount: 1200m),
                new Budget(wedding.Id, "Deko & Blumen", plannedAmount: 1500m),
                new Budget(wedding.Id, "Musik (DJ/Band)", plannedAmount: 1400m),
                new Budget(wedding.Id, "Einladungen & Papeterie", plannedAmount: 450m),
                new Budget(wedding.Id, "Transport", plannedAmount: 600m),
                new Budget(wedding.Id, "Sonstiges/Puffer", plannedAmount: 1000m),
            };

            // Items hinzufügen (Actual/Planned + Paid/Open)
            // Hier nutzen wir wieder TryParse, damit es auch bei abweichenden Enum-Namen läuft.
            AddBudgetItem(budgets[0], "Anzahlung Location", 1500m, type: "Actual", payment: "Paid", vendor: "Eventlocation München", paidDaysAgo: 40);
            AddBudgetItem(budgets[0], "Restzahlung Location", 5000m, type: "Planned", payment: "Open", vendor: "Eventlocation München");

            AddBudgetItem(budgets[1], "Catering Angebot (Schätzung)", 5200m, type: "Planned", payment: "Open", vendor: "Catering Partner");

            AddBudgetItem(budgets[2], "Fotograf Vertrag Anzahlung", 400m, type: "Actual", payment: "Paid", vendor: "Studio Lens", paidDaysAgo: 25);
            AddBudgetItem(budgets[2], "Fotograf Restbetrag", 1800m, type: "Planned", payment: "Open", vendor: "Studio Lens");

            AddBudgetItem(budgets[3], "Anzug", 650m, type: "Actual", payment: "Paid", vendor: "Herrenausstatter", paidDaysAgo: 15);
            AddBudgetItem(budgets[3], "Kleid + Anpassung", 1150m, type: "Planned", payment: "Open", vendor: "Brautmoden");

            AddBudgetItem(budgets[4], "Eheringe (Anzahlung)", 300m, type: "Actual", payment: "Paid", vendor: "Juwelier", paidDaysAgo: 10);
            AddBudgetItem(budgets[4], "Eheringe (Rest)", 900m, type: "Planned", payment: "Open", vendor: "Juwelier");

            AddBudgetItem(budgets[5], "Blumen (Schätzung)", 900m, type: "Planned", payment: "Open", vendor: "Floristik");
            AddBudgetItem(budgets[5], "Deko Material", 180m, type: "Actual", payment: "Paid", vendor: "Deko-Shop", paidDaysAgo: 7);

            AddBudgetItem(budgets[6], "DJ Reservierung", 300m, type: "Actual", payment: "Paid", vendor: "DJ Service", paidDaysAgo: 20);
            AddBudgetItem(budgets[6], "DJ Restbetrag", 1100m, type: "Planned", payment: "Open", vendor: "DJ Service");

            AddBudgetItem(budgets[7], "Einladungskarten Druck", 220m, type: "Actual", payment: "Paid", vendor: "Druckerei", paidDaysAgo: 5);
            AddBudgetItem(budgets[7], "Briefmarken", 60m, type: "Actual", payment: "Paid", vendor: "Post", paidDaysAgo: 3);

            AddBudgetItem(budgets[8], "Shuttle/Taxi Puffer", 600m, type: "Planned", payment: "Open", vendor: "Transport");

            AddBudgetItem(budgets[9], "Notfall-Puffer", 1000m, type: "Planned", payment: "Open", vendor: null);

            await db.AddRangeAsync(budgets);

            var invalidBudgets = budgets.Where(b => string.IsNullOrWhiteSpace(b.Category)).ToList();
            if (invalidBudgets.Count > 0)
            {
                throw new InvalidOperationException(
                    $"Seeder: {invalidBudgets.Count} Budget(s) ohne Category gefunden. Category ist Pflicht (NOT NULL).");
            }

            await db.SaveChangesAsync();
        }

        // -------------------------
        // Helpers (robust)
        // -------------------------

        private static void SetGuestStatus(Guest guest, string statusName)
        {
            if (Enum.TryParse<GuestStatus>(statusName, ignoreCase: true, out var parsed))
            {
                guest.ChangeStatus(parsed);
                return;
            }

            // Fallback: bleibt (oder setze) Invited
            guest.ChangeStatus(GuestStatus.Invited);
        }

        private static void AddBudgetItem(
            Budget budget,
            string title,
            decimal amount,
            string type,
            string payment,
            string? vendor,
            int? paidDaysAgo = null)
        {
            var itemType = ParseOrDefault<BudgetItemType>(type, defaultValueName: "Actual");
            var payStatus = ParseOrDefault<PaymentStatus>(payment, defaultValueName: "Open");

            DateTimeOffset? paidAt = null;
            if (payStatus.ToString().Equals("Paid", StringComparison.OrdinalIgnoreCase))
            {
                paidAt = DateTimeOffset.UtcNow.AddDays(-(paidDaysAgo ?? 0));
            }

            budget.AddItem(
                title: title,
                amount: amount,
                type: itemType,
                paymentStatus: payStatus,
                vendorName: vendor,
                paidAt: paidAt
            );
        }

        private static TEnum ParseOrDefault<TEnum>(string name, string defaultValueName)
            where TEnum : struct, Enum
        {
            if (Enum.TryParse<TEnum>(name, ignoreCase: true, out var parsed))
                return parsed;

            // Default-Enum-Wert über Name (z.B. "Open", "Actual")
            if (Enum.TryParse<TEnum>(defaultValueName, ignoreCase: true, out var fallback))
                return fallback;

            // Letzter Fallback: 0
            return default;
        }
    }
}