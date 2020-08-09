using Microsoft.EntityFrameworkCore;

namespace Gap.Insurance.EntityFramework
{
    public partial class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext()
        {
        }

        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientPolicy> ClientPolicy { get; set; }
        public virtual DbSet<Coverage> Coverage { get; set; }
        public virtual DbSet<Policy> Policy { get; set; }
        public virtual DbSet<PolicyCoverage> PolicyCoverage { get; set; }
        public virtual DbSet<PolicyStatus> PolicyStatus { get; set; }
        public virtual DbSet<Risk> Risk { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.Document)
                    .HasName("UK_Client_Document")
                    .IsUnique();

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.CellPhone)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Document)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ClientPolicy>(entity =>
            {
                entity.HasKey(e => e.ClientPolicyId);

                entity.Property(e => e.ClientPolicyId).HasColumnName("ClientPolicy");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientPolicy)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientPolicy_ClientId");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.ClientPolicy)
                    .HasForeignKey(d => d.PolicyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientPolicy_PolicyId");

                entity.HasOne(d => d.PolicyStatus)
                    .WithMany(p => p.ClientPolicy)
                    .HasForeignKey(d => d.PolicyStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientPolicy_PolicyStatusId");
            });

            modelBuilder.Entity<Coverage>(entity =>
            {
                entity.HasIndex(e => e.Description)
                    .HasName("UK_Coverage_Description")
                    .IsUnique();

                entity.Property(e => e.CoverageId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Policy>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UK_Policy_Name")
                    .IsUnique();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Risk)
                    .WithMany(p => p.Policy)
                    .HasForeignKey(d => d.RiskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Policy_Risk");
            });

            modelBuilder.Entity<PolicyCoverage>(entity =>
            {
                entity.HasIndex(e => new { e.PolicyId, e.CoverageId })
                    .HasName("UK_PolicyCoverage")
                    .IsUnique();

                entity.Property(e => e.Percentage).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Coverage)
                    .WithMany(p => p.PolicyCoverage)
                    .HasForeignKey(d => d.CoverageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PolicyCoverage_Coverage");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.PolicyCoverage)
                    .HasForeignKey(d => d.PolicyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_PolicyCoverage_Policy");
            });

            modelBuilder.Entity<PolicyStatus>(entity =>
            {
                entity.HasIndex(e => e.Description)
                    .HasName("UK_PolicyStatus_Description")
                    .IsUnique();

                entity.Property(e => e.PolicyStatusId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Risk>(entity =>
            {
                entity.HasIndex(e => e.Description)
                    .HasName("UK_Risk_Description")
                    .IsUnique();

                entity.Property(e => e.MaxCoverage).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RiskId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
