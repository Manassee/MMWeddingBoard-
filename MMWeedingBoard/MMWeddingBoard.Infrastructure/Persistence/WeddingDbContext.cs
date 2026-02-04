using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MMWeddingBoard.Domain.Budgets;
using MMWeddingBoard.Domain.Guests;
using MMWeddingBoard.Domain.Tasks;
using MMWeddingBoard.Domain.Weddings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMWeddingBoard.Infrastructure.Persistence
{
    public class WeddingDbContext : DbContext
    {
       public WeddingDbContext(DbContextOptions<WeddingDbContext> options) : base(options) { }

        public DbSet<Wedding> Weddings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetItem> BudgetsItems { get; set; }
        public DbSet<WeddingTask> Tasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }






    }
}
