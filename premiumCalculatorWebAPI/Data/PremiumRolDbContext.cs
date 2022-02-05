using Microsoft.EntityFrameworkCore;

namespace TranzactProgrammingChallengeWebAPI.Data
{
    public class PremiumRolDbContext : DbContext
    {
        /// <summary>
        /// Implementing the DbContext 
        /// </summary>

        public DbSet<PremiumRol> PremiumRols { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<State> States { get; set; }

#pragma warning disable CS8618 // El elemento propiedad "PremiumRols" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        public PremiumRolDbContext(DbContextOptions<PremiumRolDbContext> options) : base(options)
#pragma warning restore CS8618 // El elemento propiedad "PremiumRols" que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declarar el elemento propiedad como que admite un valor NULL.
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSqlite(@"DataSource=Data\\app.db;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PremiumRol>(entity =>
            {
                entity.HasKey(e => e.PremiumRolID);
                entity.Property(e => e.Carrier).HasMaxLength(10);
                entity.Property(e => e.Plan).HasMaxLength(10);
                entity.Property(e => e.State).HasMaxLength(3);
                entity.Property(e => e.MonthOfBirth).HasMaxLength(15);
                entity.Property(e => e.Age).HasMaxLength(10);
                entity.Property(e => e.Premium);

                //Data seeding
                entity.HasData(
                    new PremiumRol { PremiumRolID = 1, Carrier = "Qwerty", Plan = "A", State = "NY", MonthOfBirth = "September", Age = "21-45", Premium = 150.00 },
                    new PremiumRol { PremiumRolID = 2, Carrier = "Qwerty", Plan = "B", State = "NY", MonthOfBirth = "January", Age = "46-65", Premium = 200.50 },
                    new PremiumRol { PremiumRolID = 3, Carrier = "Qwerty", Plan = "A,C", State = "NY", MonthOfBirth = "*", Age = "18-65", Premium = 120.99 },
                    new PremiumRol { PremiumRolID = 4, Carrier = "Qwerty", Plan = "A", State = "AL", MonthOfBirth = "November", Age = "18-65", Premium = 85.50 },
                    new PremiumRol { PremiumRolID = 5, Carrier = "Qwerty", Plan = "C", State = "AL", MonthOfBirth = "*", Age = "18-65", Premium = 100.00 },
                    new PremiumRol { PremiumRolID = 6, Carrier = "Qwerty", Plan = "A", State = "AK", MonthOfBirth = "December", Age = "65+", Premium = 175.20 },
                    new PremiumRol { PremiumRolID = 7, Carrier = "Qwerty", Plan = "A", State = "AK", MonthOfBirth = "December", Age = "18-64", Premium = 125.16 },
                    new PremiumRol { PremiumRolID = 8, Carrier = "Qwerty", Plan = "B", State = "AK", MonthOfBirth = "*", Age = "18-65", Premium = 100.80 },
                    new PremiumRol { PremiumRolID = 9, Carrier = "Qwerty", Plan = "A,C", State = "*", MonthOfBirth = "*", Age = "18-65", Premium = 90.00 },


                    new PremiumRol { PremiumRolID = 10, Carrier = "Asdf", Plan = "A", State = "NY", MonthOfBirth = "October", Age = "21-45", Premium = 150.00 },
                    new PremiumRol { PremiumRolID = 11, Carrier = "Asdf", Plan = "B", State = "NY", MonthOfBirth = "January", Age = "46-65", Premium = 184.50 },
                    new PremiumRol { PremiumRolID = 12, Carrier = "Asdf", Plan = "A", State = "NY", MonthOfBirth = "*", Age = "18-65", Premium = 129.95 },
                    new PremiumRol { PremiumRolID = 13, Carrier = "Asdf", Plan = "A", State = "AL", MonthOfBirth = "November", Age = "18-65", Premium = 84.50 },
                    new PremiumRol { PremiumRolID = 14, Carrier = "Asdf", Plan = "B", State = "WY", MonthOfBirth = "*", Age = "18-65", Premium = 100.00 },
                    new PremiumRol { PremiumRolID = 15, Carrier = "Asdf", Plan = "B,C", State = "AK", MonthOfBirth = "*", Age = "18-65", Premium = 100.80 },
                    new PremiumRol { PremiumRolID = 16, Carrier = "Asdf", Plan = "A,C", State = "*", MonthOfBirth = "*", Age = "18-65", Premium = 89.99 }

                    );
            }
            );

            modelBuilder.Entity<Period>(entity =>
            {
                entity.HasKey(e => e.IdPeriod);
                entity.Property(e => e.IdPeriod).HasMaxLength(5);
                entity.Property(e => e.NamePeriod).HasMaxLength(20);
                entity.Property(e => e.Factor);

                entity.HasData(
                    new Period { IdPeriod = "M", NamePeriod = "Montly", Factor = 1 },
                    new Period { IdPeriod = "Q", NamePeriod = "Quartely", Factor = 3 },
                    new Period { IdPeriod = "S", NamePeriod = "Semi-Anually", Factor = 6 },
                    new Period { IdPeriod = "A", NamePeriod = "Annually", Factor = 12 }
                    );
            }
            );


            modelBuilder.Entity<Plan>(entity =>
            {
                entity.HasKey(e => e.PlanId);
                entity.Property(e => e.PlanId).HasMaxLength(5);
                entity.Property(e => e.PlanName).HasMaxLength(20);

                entity.HasData(
                    new Plan { PlanId = "A", PlanName = "Plan A" },
                    new Plan { PlanId = "B", PlanName = "Plan B" },
                    new Plan { PlanId = "C", PlanName = "Plan C" }
                    );
            }
            );


            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.StateId);
                entity.Property(e => e.StateId).HasMaxLength(5);
                entity.Property(e => e.StateName).HasMaxLength(20);

                entity.HasData(
                    new State { StateId = "AL", StateName = "AL - Alamaba" },
                    new State { StateId = "AK", StateName = "AK - Arkansas" },
                    new State { StateId = "NJ", StateName = "NJ - New Jersey" },
                    new State { StateId = "NY", StateName = "NY - New York" },
                    new State { StateId = "WY", StateName = "WY - Wyoming" }

                    );
            }
            );


        }


    }
}
