﻿using event_booking.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace event_booking.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Discount> Discounts { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<EventCategory> EventCategories { get; set; }

        public virtual DbSet<GroupDiscount> GroupDiscounts { get; set; }

        public virtual DbSet<Loyalty> Loyalties { get; set; }

        public virtual DbSet<Organizer> Organizers { get; set; }

        public virtual DbSet<OrganizerCategory> OrganizerCategories { get; set; }

        public virtual DbSet<Purchase> Purchases { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }

        public virtual DbSet<Seat> Seats { get; set; }

        public virtual DbSet<Section> Sections { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<TicketGroup> TicketGroups { get; set; }

        public virtual DbSet<TicketType> TicketTypes { get; set; }

        public virtual DbSet<Venue> Venues { get; set; }

        public virtual DbSet<Vip> Vips { get; set; }

        public DbSet<JunctionTicketVip> JunctionTicketVips { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
               .ToTable("AspNetUsers");

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.DiscountId).HasName("PK_TicketPricing");

                entity.ToTable("Discount", "evnt", tb => tb.HasComment("Ticket Pricing Information"));

                entity.Property(e => e.DiscountId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId).ValueGeneratedNever();

                entity.HasOne(d => d.EventCategory).WithMany(p => p.Events)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_EventCategories");

                entity.HasOne(d => d.Organizer).WithMany(p => p.Events)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Events_Organizers");
            });

            modelBuilder.Entity<EventCategory>(entity =>
            {
                entity.Property(e => e.EventCategoryId).ValueGeneratedNever();
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasMany(d => d.Events).WithMany(p => p.ApplicationUsers)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserEventFollow",
                        r => r.HasOne<Event>().WithMany()
                            .HasForeignKey("EventId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_User_Event_Follow_Events_1"),
                        l => l.HasOne<ApplicationUser>().WithMany()
                            .HasForeignKey("Id")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_User_Event_Follow_ApplicationUser"),
                        j =>
                        {
                            j.HasKey("Id", "EventId");
                            j.ToTable("User_Event_Follow", "evnt");
                            j.IndexerProperty<string>("Id").HasColumnName("Id");
                            j.IndexerProperty<int>("EventId").HasColumnName("EventID");
                        });

                entity.HasMany(d => d.Organizers).WithMany(p => p.ApplicationUsers)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserOrganizerFollow",
                        r => r.HasOne<Organizer>().WithMany()
                            .HasForeignKey("OrganizerId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_User_Organizer_Follow_Organizers_1"),
                        l => l.HasOne<ApplicationUser>().WithMany()
                            .HasForeignKey("Id")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_User_Organizer_Follow_ApplicationUser"),
                        j =>
                        {
                            j.HasKey("Id", "OrganizerId");
                            j.ToTable("User_Organizer_Follow", "evnt");
                            j.IndexerProperty<string>("Id").HasColumnName("Id");
                            j.IndexerProperty<int>("OrganizerId").HasColumnName("OrganizerID");
                        });

                entity.HasMany(d => d.Venues).WithMany(p => p.ApplicationUsers)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserVenueFollow",
                        r => r.HasOne<Venue>().WithMany()
                            .HasForeignKey("VenueId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_User_Venue_Follow_Venue_1"),
                        l => l.HasOne<ApplicationUser>().WithMany()
                            .HasForeignKey("Id")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_User_Venue_Follow_ApplicationUser"),
                        j =>
                        {
                            j.HasKey("Id", "VenueId");
                            j.ToTable("User_Venue_Follow", "evnt");
                            j.IndexerProperty<string>("Id").HasColumnName("Id");
                            j.IndexerProperty<int>("VenueId").HasColumnName("VenueID");
                        });
            });

            modelBuilder.Entity<GroupDiscount>(entity =>
            {
                entity.ToTable("GroupDiscounts", "evnt", tb => tb.HasComment("Discounts on groups"));

                entity.Property(e => e.GroupDiscountId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Loyalty>(entity =>
            {
                entity.HasOne(d => d.ApplicationUser).WithOne(p => p.Loyalty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Loyalty_ApplicationUser");
            });

            modelBuilder.Entity<Organizer>(entity =>
            {
                entity.HasKey(e => e.OrganizerId).HasName("PK_HostsOrganizers");

                entity.Property(e => e.OrganizerId).ValueGeneratedNever();

                entity.HasOne(d => d.OrganizerCategory).WithMany(p => p.Organizers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Organizers_OrganizerCategories");
            });

            modelBuilder.Entity<OrganizerCategory>(entity =>
            {
                entity.HasKey(e => e.OrganizerCategoryId).HasName("PK_HostOrganizerCategories");

                entity.Property(e => e.OrganizerCategoryId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Purchase>(entity =>
            {
                entity.Property(e => e.PurchaseId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.Property(e => e.SaleId).ValueGeneratedNever();

                entity.HasOne(d => d.ApplicationUser).WithMany(p => p.Sales).HasConstraintName("FK_Sales_Application");

                entity.HasOne(d => d.Purchase).WithMany(p => p.Sales).HasConstraintName("FK_Sales_Purchase_1");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.Property(e => e.SeatId).ValueGeneratedNever();
                entity.Property(e => e.VenueId).HasComment("Seats Information");

                entity.HasOne(d => d.Section).WithMany(p => p.Seats).HasConstraintName("FK_Seats_Section");

                entity.HasOne(d => d.Venue).WithMany(p => p.Seats).HasConstraintName("FK_Seats_Venue");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.Property(e => e.SectionId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Tickets", "evnt", tb => tb.HasComment("Event Tickets"));

                entity.Property(e => e.TicketId).ValueGeneratedNever();

                entity.HasOne(d => d.Discount).WithMany(p => p.Tickets).HasConstraintName("FK_Tickets_Discount_4");

                entity.HasOne(d => d.Event).WithMany(p => p.Tickets)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_Events");

                entity.HasOne(d => d.ApplicationUser).WithMany(p => p.Tickets).HasConstraintName("FK_Tickets_ApplicationUser_3");

                entity.HasOne(d => d.Purchase).WithMany(p => p.Tickets).HasConstraintName("FK_Tickets_Purchase_6");

                entity.HasOne(d => d.Seat).WithMany(p => p.Tickets)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_Seats_2");

                entity.HasOne(d => d.TicketType).WithMany(p => p.Tickets).HasConstraintName("FK_Tickets_TicketType_5");

                entity.HasOne(d => d.Venue).WithMany(p => p.Tickets)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tickets_Venue_1");


            });

            modelBuilder.Entity<TicketGroup>(entity =>
            {
                entity.Property(e => e.TicketGroupId).ValueGeneratedNever();

                entity.HasOne(d => d.GroupDiscount).WithMany(p => p.TicketGroups).HasConstraintName("FK_TicketGroup_GroupDiscounts_1");

                entity.HasOne(d => d.Purchase).WithMany(p => p.TicketGroups).HasConstraintName("FK_TicketGroup_Purchase");
            });

            modelBuilder.Entity<TicketType>(entity =>
            {
                entity.HasKey(e => e.TicketTypeId).HasName("PK_UserFollows");

                entity.Property(e => e.TicketTypeId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Venue>(entity =>
            {
                entity.Property(e => e.VenueId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Vip>(entity =>
            {
                entity.HasKey(e => e.VipId).HasName("PK_VIPAccess");

                entity.ToTable("VIP", "evnt", tb => tb.HasComment("VIP Area"));

                entity.Property(e => e.VipId).ValueGeneratedNever();

                entity.HasOne(d => d.Event).WithMany(p => p.Vips)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VIP_Events");
            });

            modelBuilder.Entity<JunctionTicketVip>(entity =>
            {
                entity.HasKey(jtv => new { jtv.TicketId, jtv.VipId });

                entity.HasOne(jtv => jtv.Ticket)
                    .WithMany(t => t.JunctionTicketVips)
                    .HasForeignKey(jtv => jtv.TicketId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_JunctionTicketVip_Tickets");

                entity.HasOne(jtv => jtv.Vip)
                    .WithMany(v => v.JunctionTicketVips)
                    .HasForeignKey(jtv => jtv.VipId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_JunctionTicketVip_Vips");
            });


            modelBuilder.Entity<Discount>()
        .Property(d => d.PriceMultiplier)
        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Loyalty>()
                .Property(l => l.PriceMultiplier)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Sale>()
                .Property(s => s.SalePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Section>()
                .Property(s => s.PriceMultiplier)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Ticket>()
                .Property(t => t.BasePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Ticket>()
                .Property(t => t.TicketPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<TicketType>()
                .Property(tt => tt.PriceMultiplier)
                .HasColumnType("decimal(18,2)");

        }
    }
}