using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CleanBackend.Models
{
    public partial class cleanContext : DbContext
    {
        public cleanContext()
        {
        }

        public cleanContext(DbContextOptions<cleanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DolgozoBeoszta> DolgozoBeosztas { get; set; }
        public virtual DbSet<ElvegzesDolgozoRovid> ElvegzesDolgozoRovids { get; set; }
        public virtual DbSet<ElvegzesRaktarRovid> ElvegzesRaktarRovids { get; set; }
        public virtual DbSet<EszkozHasznalat> EszkozHasznalats { get; set; }
        public virtual DbSet<Felhasznalo> Felhasznalos { get; set; }
        public virtual DbSet<Munka> Munkas { get; set; }
        public virtual DbSet<Raktar> Raktars { get; set; }
        public virtual DbSet<Registry> Registries { get; set; }
        public virtual DbSet<Szolgaltata> Szolgaltatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=localhost;database=clean;user=root;password=;sslmode=none;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DolgozoBeoszta>(entity =>
            {
                entity.HasKey(e => e.DbId)
                    .HasName("PRIMARY");

                entity.ToTable("dolgozo_beosztas");

                entity.HasIndex(e => new { e.DId, e.DbMId }, "d_id");

                entity.HasIndex(e => e.DbMId, "m_id");

                entity.Property(e => e.DbId)
                    .HasColumnType("int(11)")
                    .HasColumnName("db_id");

                entity.Property(e => e.DId)
                    .HasColumnType("int(11)")
                    .HasColumnName("d_id");

                entity.Property(e => e.DbMId)
                    .HasColumnType("int(11)")
                    .HasColumnName("db_m_id");

                entity.HasOne(d => d.DIdNavigation)
                    .WithMany(p => p.DolgozoBeoszta)
                    .HasForeignKey(d => d.DId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dolgozo_beosztas_ibfk_5");

                entity.HasOne(d => d.DbM)
                    .WithMany(p => p.DolgozoBeoszta)
                    .HasForeignKey(d => d.DbMId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("dolgozo_beosztas_ibfk_4");
            });

            modelBuilder.Entity<ElvegzesDolgozoRovid>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("elvegzes_dolgozo_rovid");

                entity.Property(e => e.DbId)
                    .HasColumnType("int(11)")
                    .HasColumnName("db_id");

                entity.Property(e => e.FelhasznaloNev)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("felhasznalo_nev");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.MunkaId)
                    .HasColumnType("int(11)")
                    .HasColumnName("munka_id");
            });

            modelBuilder.Entity<ElvegzesRaktarRovid>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("elvegzes_raktar_rovid");

                entity.Property(e => e.EhId)
                    .HasColumnType("int(11)")
                    .HasColumnName("eh_id");

                entity.Property(e => e.ElhasznaltDb)
                    .HasColumnType("int(9)")
                    .HasColumnName("elhasznalt_db");

                entity.Property(e => e.MunkaId)
                    .HasColumnType("int(11)")
                    .HasColumnName("munka_id");

                entity.Property(e => e.Nev)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nev");

                entity.Property(e => e.RId)
                    .HasColumnType("int(11)")
                    .HasColumnName("r_id");
            });

            modelBuilder.Entity<EszkozHasznalat>(entity =>
            {
                entity.HasKey(e => e.EhId)
                    .HasName("PRIMARY");

                entity.ToTable("eszkoz_hasznalat");

                entity.HasIndex(e => e.EhMId, "m_id");

                entity.HasIndex(e => e.EhRId, "r_id");

                entity.Property(e => e.EhId)
                    .HasColumnType("int(11)")
                    .HasColumnName("eh_id");

                entity.Property(e => e.EhMId)
                    .HasColumnType("int(11)")
                    .HasColumnName("eh_m_id");

                entity.Property(e => e.EhRId)
                    .HasColumnType("int(11)")
                    .HasColumnName("eh_r_id");

                entity.Property(e => e.ElhasznaltMennyiseg)
                    .HasColumnType("int(9)")
                    .HasColumnName("elhasznalt_mennyiseg");

                entity.HasOne(d => d.EhM)
                    .WithMany(p => p.EszkozHasznalats)
                    .HasForeignKey(d => d.EhMId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("eszkoz_hasznalat_ibfk_1");

                entity.HasOne(d => d.EhR)
                    .WithMany(p => p.EszkozHasznalats)
                    .HasForeignKey(d => d.EhRId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("eszkoz_hasznalat_ibfk_2");
            });

            modelBuilder.Entity<Felhasznalo>(entity =>
            {
                entity.ToTable("felhasznalo");

                entity.HasIndex(e => new { e.FelhasznaloNev, e.Email }, "felhasznalo_nev");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Aktiv)
                    .HasColumnType("int(1)")
                    .HasColumnName("aktiv");

                entity.Property(e => e.Cim)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("cim");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FelhasznaloNev)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("felhasznalo_nev");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("HASH");

                entity.Property(e => e.Iranyitoszam)
                    .HasColumnType("int(4)")
                    .HasColumnName("iranyitoszam");

                entity.Property(e => e.Rank)
                    .HasColumnType("int(1)")
                    .HasColumnName("rank");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("SALT");

                entity.Property(e => e.Telefonszam)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("telefonszam");

                entity.Property(e => e.Telepules)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("telepules");

                entity.Property(e => e.TeljesNev)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("teljes_nev");
            });

            modelBuilder.Entity<Munka>(entity =>
            {
                entity.ToTable("munka");

                entity.HasIndex(e => e.SzId, "felh_id");

                entity.Property(e => e.MunkaId)
                    .HasColumnType("int(11)")
                    .HasColumnName("munka_id");

                entity.Property(e => e.Allapot)
                    .HasColumnType("int(1)")
                    .HasColumnName("allapot")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ar)
                    .HasColumnType("int(9)")
                    .HasColumnName("ar")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Datum)
                    .IsRequired()
                    .HasColumnName("datum")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Idopont)
                    .HasColumnName("idopont")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.MunkaCim)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("munka_cim");

                entity.Property(e => e.MunkaEmail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("munka_email");

                entity.Property(e => e.MunkaIranyitoszam)
                    .HasColumnType("int(4)")
                    .HasColumnName("munka_iranyitoszam");

                entity.Property(e => e.MunkaLeiras)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasColumnName("munka_leiras");

                entity.Property(e => e.MunkaTelefonszam)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("munka_telefonszam");

                entity.Property(e => e.MunkaTelepules)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("munka_telepules");

                entity.Property(e => e.MunkaTeljesNev)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("munka_teljes_nev");

                entity.Property(e => e.SzId)
                    .HasColumnType("int(11)")
                    .HasColumnName("sz_id");

                entity.HasOne(d => d.Sz)
                    .WithMany(p => p.Munkas)
                    .HasForeignKey(d => d.SzId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("munka_ibfk_2");
            });

            modelBuilder.Entity<Raktar>(entity =>
            {
                entity.HasKey(e => e.RId)
                    .HasName("PRIMARY");

                entity.ToTable("raktar");

                entity.Property(e => e.RId)
                    .HasColumnType("int(11)")
                    .HasColumnName("r_id");

                entity.Property(e => e.Kepfajl)
                    .IsRequired()
                    .HasColumnType("mediumblob")
                    .HasColumnName("kepfajl");

                entity.Property(e => e.Megjelenes).HasColumnName("megjelenes");

                entity.Property(e => e.Mennyiseg)
                    .HasColumnType("int(9)")
                    .HasColumnName("mennyiseg");

                entity.Property(e => e.Nev)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nev");
            });

            modelBuilder.Entity<Registry>(entity =>
            {
                entity.ToTable("registry");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Cim)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("cim");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FelhasznaloNev)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("felhasznalo_nev");

                entity.Property(e => e.Hash)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("HASH");

                entity.Property(e => e.Iranyitoszam)
                    .HasColumnType("int(4)")
                    .HasColumnName("iranyitoszam");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("key");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("SALT");

                entity.Property(e => e.Telefonszam)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("telefonszam");

                entity.Property(e => e.Telepules)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("telepules");

                entity.Property(e => e.TeljesNev)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("teljes_nev");
            });

            modelBuilder.Entity<Szolgaltata>(entity =>
            {
                entity.ToTable("szolgaltatas");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Kepfajl)
                    .IsRequired()
                    .HasColumnType("mediumblob")
                    .HasColumnName("kepfajl");

                entity.Property(e => e.Leiras)
                    .IsRequired()
                    .HasColumnName("leiras");

                entity.Property(e => e.Nev)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("nev");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
