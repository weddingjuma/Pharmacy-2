﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;


namespace PharmacyLogic.Database
{
    public class Stock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid StockId { get; set; }

        [Index("uq_stock_MedicineName", 1, IsUnique = true)]
        [Required]
        [MaxLength(200), MinLength(2)]
        public virtual string MedicineName { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public virtual int Quantity { get; set; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public virtual float Price { get; set; }

        [Required]
        public virtual DateTime NextSupply { get; set; } 
    }


    public class PharmacyContext : DbContext
    {
        public DbSet<Stock> Stocks { get; set; }

        public PharmacyContext()
            : base(@"Server=.\SQLEXPRESS;Initial Catalog=PharmacyDB;Integrated Security=SSPI;MultipleActiveResultSets=True")
        {
            System.Data.Entity.Database.SetInitializer(new PharmacyContextInitializer());
        }
    }

    public class PharmacyContextInitializer : IDatabaseInitializer<PharmacyContext>
    {
        public void InitializeDatabase(PharmacyContext context)
        {
            if (context.Database.Exists())
                context.Database.Delete();

            context.Database.Create();
        }
    }
}
