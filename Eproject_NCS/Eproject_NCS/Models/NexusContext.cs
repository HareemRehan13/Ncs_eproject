using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Eproject_NCS.Models;

public partial class NexusContext : DbContext
{
    public NexusContext()
    {
    }

    public NexusContext(DbContextOptions<NexusContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Billing> Billings { get; set; }

    public virtual DbSet<Connection> Connections { get; set; }

    public virtual DbSet<ConnectionOrder> ConnectionOrders { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DiscountScheme> DiscountSchemes { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<RetailShop> RetailShops { get; set; }

    public virtual DbSet<ServicePlan> ServicePlans { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("data source=.;initial catalog=Nexus;user id=sa;password=aptech; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Billing>(entity =>
        {
            entity.HasKey(e => e.BillingId).HasName("PK__Billing__F1656D13E5E904EF");

            entity.ToTable("Billing");

            entity.HasIndex(e => e.CustomerId, "idx_Billing_CustomerID");

            entity.Property(e => e.BillingId).HasColumnName("BillingID");
            entity.Property(e => e.BillDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Connection).WithMany(p => p.Billings)
                .HasForeignKey(d => d.ConnectionId)
                .HasConstraintName("FK__Billing__Connect__440B1D61");

            entity.HasOne(d => d.Customer).WithMany(p => p.Billings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Billing__Custome__45F365D3");
        });

        modelBuilder.Entity<Connection>(entity =>
        {
            entity.HasKey(e => e.ConnectionId).HasName("PK__Connecti__404A64F3B03DF55D");

            entity.Property(e => e.ConnectionId).HasColumnName("ConnectionID");
            entity.Property(e => e.ConnectionNo).HasMaxLength(16);
            entity.Property(e => e.ConnectionType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("('pending')");

            entity.HasOne(d => d.Customer).WithMany(p => p.Connections)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Connectio__Custo__48CFD27E");

            entity.HasOne(d => d.Plan).WithMany(p => p.Connections)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Connectio__PlanI__49C3F6B7");
        });

        modelBuilder.Entity<ConnectionOrder>(entity =>
        {
            entity.HasKey(e => e.ConordId).HasName("PK__Connecti__FCE98506CF2161DC");

            entity.Property(e => e.ConordId)
                .HasMaxLength(11)
                .HasColumnName("conordId");
            entity.Property(e => e.PlanId).HasColumnName("planId");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("('pending')")
                .HasColumnName("status");

            entity.HasOne(d => d.Customer).WithMany(p => p.ConnectionOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConnectionOrders_ToCustomers");

            entity.HasOne(d => d.Plan).WithMany(p => p.ConnectionOrders)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConnectionOrders_ToPlans");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8E8E2DFA0");

            entity.HasIndex(e => e.AccountId, "UQ__Customer__349DA5879BA29343").IsUnique();

            entity.HasIndex(e => e.Name, "idx_Customers_Name");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.AccountId)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasDefaultValueSql("('0000000000')")
                .IsFixedLength()
                .HasColumnName("AccountID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("('pending')");
        });

        modelBuilder.Entity<DiscountScheme>(entity =>
        {
            entity.HasKey(e => e.SchemeId).HasName("PK__Discount__DB7E1A426754E104");

            entity.Property(e => e.SchemeId).HasColumnName("SchemeID");
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(5, 2)");
        });


        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF6582408D0");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.FeedbackDate).HasColumnType("datetime");
            entity.Property(e => e.OrderId)
                .HasMaxLength(11)
                .HasColumnName("OrderID");

            entity.HasOne(d => d.Customer).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Feedback__Custom__4BAC3F29");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__Feedback__OrderI__4CA06362");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAFE60A586B");

            entity.HasIndex(e => e.CustomerId, "idx_Orders_CustomerID");

            entity.Property(e => e.OrderId)
                .HasMaxLength(11)
                .HasColumnName("OrderID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValueSql("('PENDING')");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Customer__4D94879B");

            entity.HasOne(d => d.Equipment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Equipmen__4E88ABD4");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A581993BB7E");

            entity.HasIndex(e => e.CustomerId, "idx_Payments_CustomerID");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.BillingId).HasColumnName("BillingID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");

            entity.HasOne(d => d.Billing).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BillingId)
                .HasConstraintName("FK__Payments__Billin__5070F446");

            entity.HasOne(d => d.Customer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Payments__Custom__4F7CD00D");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED92EB0FFE");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.ProductType).HasMaxLength(50);
        });

        modelBuilder.Entity<RetailShop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("PK__RetailSh__67C55629873A27B9");

            entity.Property(e => e.ShopId).HasColumnName("ShopID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ShopName).HasMaxLength(100);
        });

        modelBuilder.Entity<ServicePlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__ServiceP__755C22D7B7ACA704");

            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.PlanType).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SecurityDeposit).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC078E472B0F");

            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.RoleId)
                .HasDefaultValueSql("((2))")
                .HasColumnName("roleId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__Vendors__FC8618D3A6FAAD53");

            entity.Property(e => e.VendorId).HasColumnName("VendorID");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ContactPerson).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.VendorName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
