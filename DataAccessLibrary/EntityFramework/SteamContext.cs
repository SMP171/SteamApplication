namespace DataAccessLibrary.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SteamContext : DbContext
    {
        public SteamContext()
            : base("name=SteamContext")
        {
        }

        public virtual DbSet<developer> Developers { get; set; }
        public virtual DbSet<friend_messages> Friend_messages { get; set; }
        public virtual DbSet<friend> Friends { get; set; }
        public virtual DbSet<group_comments> Group_comments { get; set; }
        public virtual DbSet<group> Groups { get; set; }
        public virtual DbSet<groups_users> Groups_users { get; set; }
        public virtual DbSet<order_statuses> Order_statuses { get; set; }
        public virtual DbSet<order> Orders { get; set; }
        public virtual DbSet<product_comments> Product_comments { get; set; }
        public virtual DbSet<productComment_marks> ProductComment_marks { get; set; }
        public virtual DbSet<product> Products { get; set; }
        public virtual DbSet<sysdiagram> Sysdiagrams { get; set; }
        public virtual DbSet<thread_answers> Thread_answers { get; set; }
        public virtual DbSet<thread> Threads { get; set; }
        public virtual DbSet<threads_users> Threads_users { get; set; }
        public virtual DbSet<user_statuses> User_statuses { get; set; }
        public virtual DbSet<user> Users { get; set; }
        public virtual DbSet<users_products> Users_products { get; set; }
        public virtual DbSet<wallet> Wallets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<developer>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<developer>()
                .Property(e => e.website)
                .IsUnicode(false);

            modelBuilder.Entity<developer>()
                .HasMany(e => e.products)
                .WithRequired(e => e.developer)
                .HasForeignKey(e => e.devoloper_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<friend_messages>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<group_comments>()
                .Property(e => e.comment_text)
                .IsUnicode(false);

            modelBuilder.Entity<group>()
                .Property(e => e.group_name)
                .IsUnicode(false);

            modelBuilder.Entity<group>()
                .HasMany(e => e.group_comments)
                .WithRequired(e => e.group)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<group>()
                .HasMany(e => e.groups_users)
                .WithRequired(e => e.group)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<order_statuses>()
                .Property(e => e.OS_name)
                .IsUnicode(false);

            modelBuilder.Entity<order_statuses>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.order_statuses)
                .HasForeignKey(e => e.status_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product_comments>()
                .Property(e => e.text)
                .IsUnicode(false);

            modelBuilder.Entity<productComment_marks>()
                .Property(e => e.mark)
                .IsUnicode(false);

            modelBuilder.Entity<productComment_marks>()
                .HasMany(e => e.product_comments)
                .WithRequired(e => e.productComment_marks)
                .HasForeignKey(e => e.comment_mark)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<product>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.product_comments)
                .WithRequired(e => e.product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<product>()
                .HasMany(e => e.users_products)
                .WithOptional(e => e.product)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<thread_answers>()
                .Property(e => e.text)
                .IsUnicode(false);

            modelBuilder.Entity<thread>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<thread>()
                .Property(e => e.text)
                .IsUnicode(false);

            modelBuilder.Entity<thread>()
                .HasMany(e => e.thread_answers)
                .WithRequired(e => e.thread)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<thread>()
                .HasMany(e => e.threads_users)
                .WithRequired(e => e.thread)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user_statuses>()
                .Property(e => e.US_name)
                .IsUnicode(false);

            modelBuilder.Entity<user_statuses>()
                .HasMany(e => e.users)
                .WithRequired(e => e.user_statuses)
                .HasForeignKey(e => e.status_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .Property(e => e.nickname)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.friend_messages)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.friend_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.friend_messages1)
                .WithRequired(e => e.user1)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.friends)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.friend_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.friends1)
                .WithRequired(e => e.user1)
                .HasForeignKey(e => e.user_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.group_comments)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.groups)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.groups_users)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.orders)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.product_comments)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.thread_answers)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.threads)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.creator_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.threads_users)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.users_products)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<wallet>()
                .Property(e => e.balance)
                .HasPrecision(19, 4);

            modelBuilder.Entity<wallet>()
                .HasMany(e => e.users)
                .WithRequired(e => e.wallet)
                .WillCascadeOnDelete(false);
        }
    }
}
