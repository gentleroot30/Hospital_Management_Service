using Microsoft.EntityFrameworkCore;
using System;
using HospitalMgmtService.Database;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Net;
using System.Linq;
using System.Reflection.Emit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HospitalMgmtService.Model
{


    public class ImageEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public DateTime UploadDate { get; set; }
    }

    

    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
       
        }
      
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
        

        public DbSet<Feature> Features { get; set; }
        public DbSet<RoleFeature> RoleFeatures { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<CustomerCategory> CustomerCategory { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PoRecords> PoRecords { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<PurchaseRecord> PurchaseRecords { get; set; }
        public DbSet<PurchaseDocument> PurchaseDocuments { get; set; }
        public DbSet<PurchaseDocumentShareLink> PurchaseDocumentShareLinks { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseReturn> PurchaseReturns { get; set; }
        public DbSet<PurchasePayment> Purchasepayment { get; set; }
        public DbSet<PurchaseReturnRecord> PurchaseReturnRecord { get; set; }
        public DbSet<Quotation> Quotation { get; set; }
        public DbSet<QuotationDocument> QuotationDocuments { get; set; }
        public DbSet<QuotationRecord> QuotationRecord { get; set; }
        public DbSet<QuotationTemp> QuotationTemp { get; set; }
        public DbSet<QuotationTempRecord> QuotationTempRecord { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategory { get; set; }
        public DbSet<SalesReturn> SalesReturn { get; set; }
        public DbSet<SalesReturnRecord> SalesReturnRec { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<SalesPayment> SalesPayment { get; set; }
       public DbSet<Sales> Sales { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }

        public DbSet<SalesReturnPaymentRecord> SalesReturnPayments { get; set; }

        public DbSet<PurchaseReturnPaymentRecord> PurchaseReturnPayment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           // modelBuilder.seed();
            modelBuilder.Entity<PurchaseReturnRecord>()
            .HasKey(pr => new { pr.ReturnIdFk, pr.ProductIdFk });

            modelBuilder.Entity<QuotationRecord>()
            .HasKey(pr => new { pr.QuotationIdFk, pr.ProductIdFk });


            modelBuilder.Entity<SalesRecord>()
            .HasKey(pr => new { pr.SaleIdFk, pr.ProductIdFk });
        

            modelBuilder.Entity<SalesReturnRecord>()
            .HasKey(pr => new { pr.ProductIdFk, pr.SalesReturnIdFk });

            modelBuilder.Entity<RoleFeature>()
            .HasKey(rf => new { rf.RoleIdFk, rf.FeatureIdFk });


            modelBuilder.Entity<PoRecords>()
            .HasKey(rf => new { rf.PoIdFk, rf.ProductIdFk });

            modelBuilder.Entity<PurchaseRecord>()
            .HasKey(pr => new { pr.PurchaseIdFk, pr.ProductIdFk });


            modelBuilder.Entity<QuotationTempRecord>()
            .HasKey(pr => new { pr.TemplateIdFk, pr.ProductIdFk });

            modelBuilder.Entity<User>()
                .HasOne(rf => rf.Roles)
                .WithMany(r => r.User)
                .HasForeignKey(rf => rf.RoleIdFk)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoleFeature>()
                .HasOne(rf => rf.Role)
                .WithMany(r => r.RoleFeatures)
                .HasForeignKey(rf => rf.RoleIdFk)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoleFeature>()
                .HasOne(rf => rf.Feature)
                .WithMany(f => f.RoleFeatures)
                .HasForeignKey(rf => rf.FeatureIdFk)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
               .HasOne(rf => rf.CustomerCategory)
               .WithMany(f => f.Customers)
               .HasForeignKey(rf => rf.CustomerCategoryIdFk)
               .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Product>()
            .HasOne(p => p.Brand)
            .WithMany(b => b.Product)
            .HasForeignKey(p => p.BrandIdFk)
             .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany(c => c.Product)
                .HasForeignKey(p => p.ProductCategoryIdFk)
                 .OnDelete(DeleteBehavior.NoAction);
            

            modelBuilder.Entity<Batch>()
                .HasOne(b=>b.Product)
                .WithMany(b=>b.Batch)
                .HasForeignKey(b=>b.ProductIdFk)
                 .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(b => b.Supplier)
                .WithMany(b => b.PurchaseOrders)
                .HasForeignKey(b => b.SupplierIdFk)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PoRecords>()
                .HasOne(rf => rf.Po)
                .WithMany(r => r.PoRecords)
                .HasForeignKey(rf => rf.PoIdFk)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PoRecords>()
                .HasOne(rf => rf.Product)
                .WithMany(f => f.PoRecords)
                .HasForeignKey(rf => rf.ProductIdFk)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Purchase>()
               .HasOne(b => b.Supplier)
               .WithMany(b => b.Purchases)
               .HasForeignKey(b => b.SupplierIdFk)
                .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<PurchaseRecord>()
                  .HasOne(rf => rf.Purchase)
                  .WithMany(r => r.PurchaseRecords)
                  .HasForeignKey(rf => rf.PurchaseIdFk).    
                  OnDelete(DeleteBehavior.Restrict); 


               modelBuilder.Entity<PurchaseRecord>()
                   .HasOne(rf => rf.Product)
                   .WithMany(f => f.PurchaseRecord)
                   .HasForeignKey(rf => rf.ProductIdFk).
                  OnDelete(DeleteBehavior.Restrict); 

               modelBuilder.Entity<PurchaseRecord>()
                  .HasOne(b => b.Batch)
                  .WithMany(b => b.PurchaseRecords)
                  .HasForeignKey(b => b.BatchIdFk).
                  OnDelete(DeleteBehavior.Restrict);
          

           modelBuilder.Entity<PurchaseDocument>()
              .HasOne(b => b.Purchase)
               .WithMany(b => b.PurchaseDocuments)
             .HasForeignKey(b => b.PurchaseIdFk)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PurchaseDocumentShareLink>()
                .HasOne(b => b.PurchaseDocument)
                .WithMany(b => b.PurchaseDocumentShareLinks)
                .HasForeignKey(b => b.PurchaseDocumentIdFk)
            .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<PurchaseDocumentShareLink>()
                   .Property(d => d.UUID)
                   .HasDefaultValueSql("CONVERT(VARCHAR(16), ABS(CHECKSUM(NEWID())), 16)");

               modelBuilder.Entity<PurchaseDocumentShareLink>()
                .HasOne(b => b.PurchaseDocument)
                .WithMany(b => b.PurchaseDocumentShareLinks)
                .HasForeignKey(b => b.PurchaseDocumentIdFk)
            .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<PurchasePayment>()
                .HasOne(b => b.Purchase)
                .WithMany(b => b.PurchasePayments)
                .HasForeignKey(b => b.PurchaseIdFk)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PurchaseReturn>()
                .HasOne(b => b.Supplier)
                .WithMany(b => b.PurchaseReturns)
                .HasForeignKey(b => b.SupplierIdFk)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PurchaseReturnRecord>()
                .HasOne(b => b.PurchaseReturn)
                .WithMany(b => b.PurchaseReturnRecords)
                .HasForeignKey(b => b.ReturnIdFk)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<PurchaseReturnRecord>()
                .HasOne(b => b.Product)
                .WithMany(b => b.PurchaseReturnRecords)
                .HasForeignKey(b => b.ProductIdFk)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Quotation>()
                .HasOne(b => b.Customer)
                .WithMany(b => b.Quotation)
                .HasForeignKey(b => b.CustomerIdFk)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuotationRecord>()
            .HasOne(b => b.Quotation)
            .WithMany(b => b.QuotationRecord)
            .HasForeignKey(b => b.QuotationIdFk)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<QuotationRecord>()
            .HasOne(b => b.Product)
            .WithMany(b => b.QuotationRecords)
            .HasForeignKey(b => b.ProductIdFk)
            .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<QuotationDocument>()
          .HasOne(b => b.Quotation)
          .WithMany(b => b.QuotationDocuments)
          .HasForeignKey(b => b.QuotationIdFk)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<QuotationDocumentShareLink>()
                  .Property(d => d.UUID)
                  .HasDefaultValueSql("CONVERT(VARCHAR(16), ABS(CHECKSUM(NEWID())), 16)");

               modelBuilder.Entity<QuotationDocumentShareLink>()
                .HasOne(b => b.QuotationDocument)
                .WithMany(b => b.QuotationDocumentShareLinks)
                .HasForeignKey(b => b.QuotationDocumentIdFk)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuotationTempRecord>()
               .HasOne(b => b.Products)
               .WithMany(b => b.QuotationTempRecords)
               .HasForeignKey(b => b.ProductIdFk)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<QuotationTempRecord>()
               .HasOne(b => b.QuotationTemp)
               .WithMany(b => b.QuotationTempRecords)
               .HasForeignKey(b => b.TemplateIdFk)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sales>()
               .HasOne(b => b.Customer)
               .WithMany(b => b.Sales)
               .HasForeignKey(b => b.CustomerIdFk)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesRecord>()
               .HasOne(b => b.Sales)
               .WithMany(b => b.SalesRecords)
               .HasForeignKey(b => b.SaleIdFk)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesRecord>()
              .HasOne(b => b.Product)
              .WithMany(b => b.SalesRecords)
              .HasForeignKey(b => b.ProductIdFk)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesPayment>()
              .HasOne(b => b.Sales)
              .WithMany(b => b.SalesPayments)
              .HasForeignKey(b => b.SalesIdFk)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<SalesReturn>()
              .HasOne(b => b.Customers)
              .WithMany(b => b.SalesReturns)
              .HasForeignKey(b => b.CustomerIdFk)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesReturnRecord>()
              .HasOne(b => b.SalesReturn)
              .WithMany(b => b.SalesReturnRecord)
              .HasForeignKey(b => b.SalesReturnIdFk)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<SalesReturnRecord>()
              .HasOne(b => b.Product)
              .WithMany(b => b.SalesReturnRecord)
              .HasForeignKey(b => b.ProductIdFk)
            .OnDelete(DeleteBehavior.Restrict
            );


            modelBuilder.Entity<Expense>()
              .HasOne(b => b.ExpenseCategory)
              .WithMany(b => b.Expenses)
              .HasForeignKey(b => b.ExpenseCategoryIdFK)
            .OnDelete(DeleteBehavior.Restrict);


       modelBuilder.seed();

        }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           {
               optionsBuilder.UseSqlServer().UseSnakeCaseNamingConvention();
              optionsBuilder.UseSqlServer("Data Source=DESKTOP-F8PAMO4\\SQLEXPRESS;Initial Catalog=HospitalDB;Integrated Security=True;", builder =>
                {

                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    
                });
        }
        */

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-FCEDALE\\SQLEXPRESS;Initial Catalog=HospitalDB;Integrated Security=True;");
            optionsBuilder.UseLoggerFactory(loggerFactory);
            base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer("Data Source=DESKTOP-FCEDALE\\SQLEXPRESS;Initial Catalog=HospitalDB;Integrated Security=True;").LogTo(Console.WriteLine).EnableDetailedErrors();
            base.OnConfiguring(optionsBuilder);
        
    }
        ILoggerFactory loggerFactory = new LoggerFactory();

    }
}

