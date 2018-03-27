namespace project.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<sheep> sheep { get; set; }
        public virtual DbSet<medication> medications { get; set; }
        public virtual DbSet<usertable> usertables { get; set; }
        public virtual DbSet<breed>breed { get; set; }
        public virtual DbSet<lamb>lamb { get; set;}
        public virtual DbSet<result>result { get; set; }
        public virtual DbSet<drug> drug { get; set; }
        public virtual DbSet<drugsupplier> drugsupplier { get; set; }
        public virtual DbSet<lambmed>lambmed { get; set; }
        public virtual DbSet<mealproduct>mealproduct { get; set; }
        public virtual DbSet<mealsupplier> mealsupplier{ get; set; }
        public virtual DbSet<movement> movement { get; set; }
        public virtual DbSet<_event>_event { get; set; }
        public virtual DbSet<calevent> calevent { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<sheep>()
                .Property(e => e.Breed)
                .IsUnicode(false);

            modelBuilder.Entity<lamb>()
               .Property(e => e.Breed)
               .IsUnicode(false);
        }


    }

    public partial class CalEventContext : DbContext
    {
        public CalEventContext()
            : base("name=CalEventContext")
        {
        }

        public virtual DbSet<calevent> calevents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }

    public partial class EventDataContext : DbContext
    {
        public EventDataContext()
            : base("name=EventDataContext")
        {
        }

        public virtual DbSet<_event> events { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<_event>()
                .Property(e => e.Eventcode)
                .IsUnicode(false);

            modelBuilder.Entity<_event>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }

    public partial class MovementDataContext : DbContext
    {
        public MovementDataContext()
            : base("name=MovementDataContext")
        {
        }

        public virtual DbSet<movement> movements { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
    public partial class LambMedDataContext : DbContext
    {
        public LambMedDataContext()
            : base("name=LambMedDataContext")
        {
        }

        public virtual DbSet<lambmed> lambmeds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<lambmed>()
                .Property(e => e.Issue)
                .IsUnicode(false);

            modelBuilder.Entity<lambmed>()
                .Property(e => e.Name)
                .IsUnicode(false);
        }
    }

    public partial class DrugSuplierDataContext : DbContext
    {
        public DrugSuplierDataContext()
            : base("name=DrugSuplierDataContext")
        {
        }

        public virtual DbSet<drugsupplier> drugsuppliers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<drugsupplier>()
                .Property(e => e.SupplierName)
                .IsUnicode(false);

            modelBuilder.Entity<drugsupplier>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<drugsupplier>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<drugsupplier>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
    public partial class DrugDataContext : DbContext
    {
        public DrugDataContext()
            : base("name=DrugDataContext")
        {
        }

        public virtual DbSet<drug> drugs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<drug>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<drug>()
                .Property(e => e.DrugSupplier)
                .IsUnicode(false);

            modelBuilder.Entity<drug>()
                .Property(e => e.Withdrawalperiod)
                .IsUnicode(false);
        }
    }
    public partial class MedicationModelContext : DbContext
    {
        public MedicationModelContext()
            : base("name=MedicationModelContext")
        {
        }

        public virtual DbSet<medication> medications { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<medication>()
                .Property(e => e.Issue)
                .IsUnicode(false);

            modelBuilder.Entity<medication>()
                .Property(e => e.Dosage)
                .IsUnicode(false);
        }
    }

    public partial class BreedDataContext : DbContext
    {
        public BreedDataContext()
            : base("name=BreedDataContext")
        {
        }

        public virtual DbSet<breed> breeds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<breed>()
                .Property(e => e.Breed1)
                .IsUnicode(false);

            modelBuilder.Entity<breed>()
                .Property(e => e.Colour)
                .IsUnicode(false);

            modelBuilder.Entity<breed>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }

    public partial class LambDataContext : DbContext
    {
        public LambDataContext()
            : base("name=LambDataContext")
        {
        }

        public virtual DbSet<lamb> lambs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<lamb>()
                .Property(e => e.Breed)
                .IsUnicode(false);
        }
    }

    public partial class Usermodelcontext : DbContext
    {
        public Usermodelcontext()
            : base("name=Usermodelcontext")
        {
        }

        public virtual DbSet<usertable> usertables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<usertable>()
                .Property(e => e.firstname)
                .IsUnicode(false);

            modelBuilder.Entity<usertable>()
                .Property(e => e.secondname)
                .IsUnicode(false);

            modelBuilder.Entity<usertable>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<usertable>()
                .Property(e => e.Postcode)
                .IsUnicode(false);

            modelBuilder.Entity<usertable>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<usertable>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<usertable>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<usertable>()
               .Property(e => e.Role)
               .IsUnicode(false);

        }
    }

    public partial class ResultDataContext : DbContext
    {
        public ResultDataContext()
            : base("name=ResultDataContext")
        {
        }

        public virtual DbSet<result> results { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<result>()
                .Property(e => e.Description)
                .IsUnicode(false);
        }
    }

    public partial class MealDataContext : DbContext
    {
        public MealDataContext()
            : base("name=MealDataContext")
        {
        }

        public virtual DbSet<mealproduct> mealproducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<mealproduct>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<mealproduct>()
                .Property(e => e.BagSize)
                .IsUnicode(false);

            modelBuilder.Entity<mealproduct>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<mealproduct>()
                .Property(e => e.MealSupplier)
                .IsUnicode(false);
        }
    }

    public partial class MealSupplierDataContext : DbContext
    {
        public MealSupplierDataContext()
            : base("name=MealSupplierDataContext")
        {
        }

        public virtual DbSet<mealsupplier> mealsuppliers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<mealsupplier>()
                .Property(e => e.SupplierName)
                .IsUnicode(false);

            modelBuilder.Entity<mealsupplier>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<mealsupplier>()
                .Property(e => e.Telephone)
                .IsUnicode(false);

            modelBuilder.Entity<mealsupplier>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }




}
