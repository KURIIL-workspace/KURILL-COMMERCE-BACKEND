using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KCommerceAPI.DataAccess.EfCore
{
    public partial class KComDbContext : DbContext
    {
        public KComDbContext()
        {
        }

        public KComDbContext(DbContextOptions<KComDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<DocumentRef> DocumentRefs { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeLogin> EmployeeLogins { get; set; } = null!;
        public virtual DbSet<EmployeeStatus> EmployeeStatuses { get; set; } = null!;
        public virtual DbSet<GoodsRecieveNote> GoodsRecieveNotes { get; set; } = null!;
        public virtual DbSet<GoodsRecieveNoteItem> GoodsRecieveNoteItems { get; set; } = null!;
        public virtual DbSet<PurchaseInvoice> PurchaseInvoices { get; set; } = null!;
        public virtual DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; } = null!;
        public virtual DbSet<PurchaseInvoiceStatus> PurchaseInvoiceStatuses { get; set; } = null!;
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; } = null!;
        public virtual DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; } = null!;
        public virtual DbSet<PurchaseOrderStatus> PurchaseOrderStatuses { get; set; } = null!;
        public virtual DbSet<SalesOrder> SalesOrders { get; set; } = null!;
        public virtual DbSet<SalesOrderItem> SalesOrderItems { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;
        public virtual DbSet<StockStatus> StockStatuses { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("User ID=postgres;Password=Annites99#;Server=localhost;Port=5432;Database=k_com_db_1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address", "contact");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.AddLine1)
                    .HasMaxLength(20)
                    .HasColumnName("add_line_1");

                entity.Property(e => e.AddLine2)
                    .HasMaxLength(20)
                    .HasColumnName("add_line_2");

                entity.Property(e => e.Country)
                    .HasMaxLength(10)
                    .HasColumnName("country");

                entity.Property(e => e.Customer).HasColumnName("customer");

                entity.Property(e => e.Employee).HasColumnName("employee");

                entity.Property(e => e.PostalCode).HasColumnName("postal_code");

                entity.Property(e => e.Supplier).HasColumnName("supplier");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.Customer)
                    .HasConstraintName("address_fk_3");

                entity.HasOne(d => d.EmployeeNavigation)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.Employee)
                    .HasConstraintName("address_fk_2");

                entity.HasOne(d => d.SupplierNavigation)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.Supplier)
                    .HasConstraintName("address_fk_1");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category", "category");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(20)
                    .HasColumnName("brand_name");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .HasColumnName("category_name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer", "person");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CustomerContact)
                    .HasMaxLength(10)
                    .HasColumnName("customer_contact");

                entity.Property(e => e.CustomerName)
                    .HasMaxLength(20)
                    .HasColumnName("customer_name");
            });

            modelBuilder.Entity<DocumentRef>(entity =>
            {
                entity.ToTable("document_ref", "settings");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Format)
                    .HasMaxLength(100)
                    .HasColumnName("format");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee", "person");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("birth_date");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code");

                entity.Property(e => e.Contact)
                    .HasMaxLength(10)
                    .HasColumnName("contact");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("employee_fk");
            });

            modelBuilder.Entity<EmployeeLogin>(entity =>
            {
                entity.ToTable("employee_login", "person");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .HasColumnName("password");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("user_name");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeLogins)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("employee_login_fk_1");
            });

            modelBuilder.Entity<EmployeeStatus>(entity =>
            {
                entity.ToTable("employee_status", "settings");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<GoodsRecieveNote>(entity =>
            {
                entity.ToTable("goods_recieve_note", "purchase");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CheckedDate).HasColumnName("checked_date");

                entity.Property(e => e.CheckedEmployeeId).HasColumnName("checked_employee_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .HasColumnName("description");

                entity.Property(e => e.PurchaseInvoiceId).HasColumnName("purchase_invoice_id");

                entity.Property(e => e.PurchaseOrderId).HasColumnName("purchase_order_id");

                entity.HasOne(d => d.CheckedEmployee)
                    .WithMany(p => p.GoodsRecieveNotes)
                    .HasForeignKey(d => d.CheckedEmployeeId)
                    .HasConstraintName("goods_recieve_note_fk_2");

                entity.HasOne(d => d.PurchaseInvoice)
                    .WithMany(p => p.GoodsRecieveNotes)
                    .HasForeignKey(d => d.PurchaseInvoiceId)
                    .HasConstraintName("goods_recieve_note_fk_3");

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.GoodsRecieveNotes)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .HasConstraintName("goods_recieve_note_fk_1");
            });

            modelBuilder.Entity<GoodsRecieveNoteItem>(entity =>
            {
                entity.ToTable("goods_recieve_note_items", "purchase");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedDateTime).HasColumnName("created_date_time");

                entity.Property(e => e.DamagedQty).HasColumnName("damaged_qty");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .HasColumnName("description");

                entity.Property(e => e.GoodsRecieveNoteId).HasColumnName("goods_recieve_note_id");

                entity.Property(e => e.OrderedQty).HasColumnName("ordered_qty");

                entity.Property(e => e.OtherDeductedQty).HasColumnName("other_deducted_qty");

                entity.Property(e => e.RecievedQty).HasColumnName("recieved_qty");

                entity.Property(e => e.RemainingQty).HasColumnName("remaining_qty");

                entity.Property(e => e.UnitPrice)
                    .HasPrecision(20, 2)
                    .HasColumnName("unit_price");

                entity.Property(e => e.UpdatedDateTime).HasColumnName("updated_date_time");

                entity.HasOne(d => d.GoodsRecieveNote)
                    .WithMany(p => p.GoodsRecieveNoteItems)
                    .HasForeignKey(d => d.GoodsRecieveNoteId)
                    .HasConstraintName("goods_recieve_note_items_fk_1");
            });

            modelBuilder.Entity<PurchaseInvoice>(entity =>
            {
                entity.ToTable("purchase_invoice", "purchase");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_date_time")
                    .HasDefaultValueSql("(now())::timestamp without time zone");

                entity.Property(e => e.PreparedEmployee).HasColumnName("prepared_employee");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_date_time");

                entity.HasOne(d => d.PreparedEmployeeNavigation)
                    .WithMany(p => p.PurchaseInvoices)
                    .HasForeignKey(d => d.PreparedEmployee)
                    .HasConstraintName("purchase_invoice_fk_3");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.PurchaseInvoices)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("purchase_invoice_fk_1");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PurchaseInvoices)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("purchase_invoice_fk_2");
            });

            modelBuilder.Entity<PurchaseInvoiceItem>(entity =>
            {
                entity.ToTable("purchase_invoice_item", "purchase");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(20)
                    .HasColumnName("item_name");

                entity.Property(e => e.ItemQuantity).HasColumnName("item_quantity");

                entity.Property(e => e.PurchaseInvoiceId).HasColumnName("purchase_invoice_id");

                entity.Property(e => e.UnitPrice)
                    .HasPrecision(28, 2)
                    .HasColumnName("unit_price");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.PurchaseInvoiceItems)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("purchase_invoice_item_fk_1");

                entity.HasOne(d => d.PurchaseInvoice)
                    .WithMany(p => p.PurchaseInvoiceItems)
                    .HasForeignKey(d => d.PurchaseInvoiceId)
                    .HasConstraintName("purchase_invoice_item_fk_2");
            });

            modelBuilder.Entity<PurchaseInvoiceStatus>(entity =>
            {
                entity.ToTable("purchase_invoice_status", "settings");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.ToTable("purchase_order", "purchase");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_date_time")
                    .HasDefaultValueSql("(now())::timestamp without time zone");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.DueDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("due_date");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("order_date");

                entity.Property(e => e.PreparedEmployee).HasColumnName("prepared_employee");

                entity.Property(e => e.ShipToAddress)
                    .HasMaxLength(100)
                    .HasColumnName("ship_to_address");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.Property(e => e.TermsCondition)
                    .HasMaxLength(500)
                    .HasColumnName("terms_condition");

                entity.Property(e => e.TotalPrice)
                    .HasPrecision(28, 2)
                    .HasColumnName("total_price");

                entity.Property(e => e.TotalQty).HasColumnName("total_qty");

                entity.Property(e => e.UpdatedDateTime)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_date_time");

                entity.HasOne(d => d.PreparedEmployeeNavigation)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.PreparedEmployee)
                    .HasConstraintName("purchase_order_fk_2");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("purchase_order_fk");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("purchase_order_fk_1");
            });

            modelBuilder.Entity<PurchaseOrderItem>(entity =>
            {
                entity.ToTable("purchase_order_item", "purchase");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(20)
                    .HasColumnName("product_name");

                entity.Property(e => e.PurchaseOrderId).HasColumnName("purchase_order_id");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.UnitPrice)
                    .HasPrecision(28, 2)
                    .HasColumnName("unit_price");

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.PurchaseOrderItems)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .HasConstraintName("purchase_order_item_fk");
            });

            modelBuilder.Entity<PurchaseOrderStatus>(entity =>
            {
                entity.ToTable("purchase_order_status", "settings");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SalesOrder>(entity =>
            {
                entity.ToTable("sales_order", "sales");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .HasColumnName("category_name");

                entity.Property(e => e.CusId).HasColumnName("cus_id");

                entity.Property(e => e.CusName)
                    .HasMaxLength(20)
                    .HasColumnName("cus_name");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(20)
                    .HasColumnName("item_name");

                entity.Property(e => e.OdrQty).HasColumnName("odr_qty");

                entity.Property(e => e.SellingPrice)
                    .HasPrecision(20, 2)
                    .HasColumnName("selling_price");

                entity.Property(e => e.Status)
                    .HasMaxLength(30)
                    .HasColumnName("status");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.StockId).HasColumnName("stock_id");

                entity.Property(e => e.TotalPrice)
                    .HasPrecision(20, 2)
                    .HasColumnName("total_price");

                entity.HasOne(d => d.Cus)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.CusId)
                    .HasConstraintName("sales_order_fk_1");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.SalesOrders)
                    .HasForeignKey(d => d.StockId)
                    .HasConstraintName("sales_order_fk_2");
            });

            modelBuilder.Entity<SalesOrderItem>(entity =>
            {
                entity.ToTable("sales_order_items", "sales");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(30)
                    .HasColumnName("product_name");

                entity.Property(e => e.Qty).HasColumnName("qty");

                entity.Property(e => e.SalesOrderId).HasColumnName("sales_order_id");

                entity.Property(e => e.UnitPrice)
                    .HasPrecision(20, 2)
                    .HasColumnName("unit_price");

                entity.HasOne(d => d.SalesOrder)
                    .WithMany(p => p.SalesOrderItems)
                    .HasForeignKey(d => d.SalesOrderId)
                    .HasConstraintName("sales_order_fk_1");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("stock");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .HasColumnName("category_name");

                entity.Property(e => e.EmpId).HasColumnName("emp_id");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(20)
                    .HasColumnName("item_name");

                entity.Property(e => e.ItemQty).HasColumnName("item_qty");

                entity.Property(e => e.SellingPrice)
                    .HasPrecision(20, 2)
                    .HasColumnName("selling_price");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.UnitPrice)
                    .HasPrecision(20, 2)
                    .HasColumnName("unit_price");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("stock_fk_1");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Stocks)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("stock_fk_2");
            });

            modelBuilder.Entity<StockStatus>(entity =>
            {
                entity.ToTable("stock_status");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.Description)
                    .HasMaxLength(30)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("supplier", "person");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("uuid_generate_v4()");

                entity.Property(e => e.SupplierContact)
                    .HasMaxLength(10)
                    .HasColumnName("supplier_contact");

                entity.Property(e => e.SupplierName)
                    .HasMaxLength(20)
                    .HasColumnName("supplier_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
